using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimpleJSON;
using UnityEngine;
using Const = RuntimeTests.RuntimeTestConstants;

namespace RuntimeTests
{
    public class TestProvider 
    {
        public void SaveToFile(Dictionary<TypeAndMethod,bool> toggleByTestDict)
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
    
        public Queue<TypeAndMethod> GetAllExistTestQueue()
        {
            Queue<TypeAndMethod> testQueue = new Queue<TypeAndMethod>();
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    if (method.GetCustomAttributes().OfType<RuntimeTestAtribute>().Any())
                    {
                        testQueue.Enqueue(new TypeAndMethod(type, method.Name));
                    }
                }
            }

            return testQueue;
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
                    testQueue.Enqueue(new TypeAndMethod(type,method));
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