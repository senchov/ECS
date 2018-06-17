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

        private void Start()
        {
            StartTests();
        }

        [ContextMenu("StartTests")]
        public void StartTests()
        {
            Debug.Log(TestQueue.Count);
            StartCoroutine(TestEnumerator());
        }

        private IEnumerator TestEnumerator()
        {
            yield return new WaitForSeconds(2.0f);

            if (TestQueue.Count > 0)
            {
                TypeAndMethod currentTest = TestQueue.Dequeue();
                MonoBehaviour currentComponent = (MonoBehaviour) GetCurrentComponent(currentTest);
                yield return currentComponent.StartCoroutine(currentTest.Method);
            }
            else
            {
                EditorApplication.ExecuteMenuItem("Edit/Play");
                yield break;
            }

            yield return TestEnumerator();
        }

        private Component GetCurrentComponent(TypeAndMethod currentTest)
        {
            return gameObject.GetComponent(currentTest.Type) ?? gameObject.AddComponent(currentTest.Type);
        }

        public void SetQueueToTest(Queue<TypeAndMethod> testQueue)
        {
            Queue<TypeAndMethod> TestQueue = testQueue;
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
            Debug.LogError(type.Name + " " + method);
        }
    }
}