using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimpleJSON;
using UnityEditor;
using UnityEngine;
using Const = RuntimeTests.RuntimeTestConstants;
using Object = UnityEngine.Object;

namespace RuntimeTests
{
    public class TestProvider
    {
        public void SaveToFile(Dictionary<TypeAndMethod, bool> toggleByTestDict)
        {
            JSONClass testJson = new JSONClass();
            testJson[Const.TypesAndMethods] = new JSONArray();
            foreach (KeyValuePair<TypeAndMethod, bool> toggleByTest in toggleByTestDict)
            {
                bool isPlayTest = toggleByTest.Value;
                if (isPlayTest)
                {
                    JSONClass node = new JSONClass();
                    string typeName = toggleByTest.Key.Type.Name;
                    node[Const.Type] = typeName;
                    node[Const.Method] = toggleByTest.Key.Method;
                    testJson[Const.TypesAndMethods].Add(node);
                }
            }

            string path = GetFilePath();
            testJson.SaveToFile(path);
        }

        public Queue<TypeAndMethod> GetTests(Object folder)
        {
            Queue<TypeAndMethod> testQueue = new Queue<TypeAndMethod>();
            string folderPath = AssetDatabase.GetAssetPath(folder);

            foreach (string script in GetScriptsNames(folderPath))
            {
                Type type = Type.GetType(script);
                if (type != null)
                {
                    FillQueue(type, testQueue);
                }
            }

            return testQueue;
        }

        private IEnumerable<string> GetScriptsNames(string folderPath)
        {
            return Directory.GetFiles(folderPath).Where(s => s.Contains(".cs") && !s.Contains(".meta"))
                .Select(s => s.Replace(".cs", String.Empty).Replace(folderPath + "\\", String.Empty));
        }

        private void FillQueue(Type type, Queue<TypeAndMethod> testQueue)
        {
            foreach (MethodInfo method in type.GetMethods())
            {
                foreach (var attribute in method.GetCustomAttributes(false))
                {
                    if (attribute is RuntimeTestAtribute)
                        testQueue.Enqueue(new TypeAndMethod(type, method.Name));
                }
            }
        }
       
        public Queue<TypeAndMethod> GetTestQueue()
        {
            Queue<TypeAndMethod> testQueue = new Queue<TypeAndMethod>();
            string path = GetFilePath();
            if (AtleastOneTestAvailable())
            {
                JSONClass jsonWithTests = JSONNode.LoadFromFile(path).AsObject;
                JSONArray testArray = jsonWithTests[Const.TypesAndMethods].AsArray;
                foreach (JSONNode test in testArray)
                {
                    Type type = Type.GetType(test[Const.Type].Value);
                    string method = test[Const.Method].Value;
                    testQueue.Enqueue(new TypeAndMethod(type, method));
                }

                File.Delete(path);
            }

            return testQueue;
        }

        public bool AtleastOneTestAvailable()
        {
            string path = GetFilePath();
            return File.Exists(path);
        }

        private string GetFilePath()
        {
            return Application.streamingAssetsPath + "/typesAndMethodsForTest.txt";
        }
    }
}