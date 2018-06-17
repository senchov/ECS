using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RuntimeTests
{
    public class RuntimeTestWindow : EditorWindow
    {
        bool toggle = false;
        private Queue<TypeAndMethod> TestQueue = new Queue<TypeAndMethod>();

        
        [MenuItem("Window/RuntimeTest")]
        public static void ShowWindwow()
        {
            GetWindow<RuntimeTestWindow>("Runtime test");
        }
       
        private void OnGUI()
        {
            GUILayout.Label("Tests", EditorStyles.boldLabel);
            toggle = GUILayout.Toggle(toggle, "toggle");
            if (GUILayout.Button("Start tests"))
            {
                FillTestQueue();
                StartTest();
            }
        }

        private void StartTest()
        {
           // PlayStopRuntime();
            RuntimeTestDataHolder.TestQueue = TestQueue;
            GameObject testGameObject = new GameObject("tesGameObject");
            RuntimeTestManager testManager = testGameObject.AddComponent<RuntimeTestManager>();
        }

        private void EditorSceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
        {
            Debug.LogError("loaded");
            
            
        }

        private void OnOnTestOver()
        {
            PlayStopRuntime();
        }

        private void PlayStopRuntime()
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        private void FillTestQueue()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    if (method.GetCustomAttributes().OfType<RuntimeTestAtribute>().Any())
                    {
                        TestQueue.Enqueue(new TypeAndMethod(type, method.Name));
                    }
                }
            }
        }
    }
}