using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MovementSystem;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RuntimeTests
{
    public class RuntimeTestManager : MonoBehaviour
    {
        private Queue<TypeAndMethod> TestQueue = new Queue<TypeAndMethod>();
        private TestProvider TestProvider = new TestProvider();

        private void Start()
        {
            TestQueue = TestProvider.GetTestQueue();
            StartTests();
        }

        [ContextMenu("StartTests")]
        public void StartTests()
        {
            StartCoroutine(TestEnumerator());
        }

        private IEnumerator TestEnumerator()
        {
            if (TestQueue.Count > 0)
            {
                TypeAndMethod currentTest = TestQueue.Dequeue();
                MonoBehaviour currentComponent = (MonoBehaviour) GetCurrentComponent(currentTest);
                yield return currentComponent.StartCoroutine(currentTest.Method);
            }
            else
            {
                EditorApplication.ExecuteMenuItem("Edit/Play");
                Destroy(gameObject);
                yield break;
            }

            yield return TestEnumerator();
        }

        private Component GetCurrentComponent(TypeAndMethod currentTest)
        {
            return gameObject.GetComponent(currentTest.Type) ?? gameObject.AddComponent(currentTest.Type);
        }
    }

    public class TypeAndMethod
    {
        public Type Type;
        public string Method;

        public TypeAndMethod(Type type, string method)
        {
            Type = type;
            Method = method;
        }
    }
}