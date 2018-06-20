using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using SimpleJSON;


namespace RuntimeTests
{
    public class RuntimeTestWindow : EditorWindow
    {
        private Queue<TypeAndMethod> ExistingTests = new Queue<TypeAndMethod>();
        private Dictionary<TypeAndMethod, bool> ToggleByTest;
        private TestProvider TestProvider;
        private Vector2 ScrollPos;
        private string TestAttributeValue = String.Empty;

        [MenuItem("Window/RuntimeTest")]
        public static void ShowWindwow()
        {
            GetWindow<RuntimeTestWindow>("Runtime test");
        }

        private void OnEnable()
        {
            SubscribeToPlaymodeStateChange();
            TestProvider = new TestProvider();
            FillTestQueue();
            FillToggleByTest();
        }

        private void FillTestQueue()
        {
            if (TestAttributeValue == String.Empty)
                ExistingTests = TestProvider.GetAllExistTestQueue();
            else
            {
                ExistingTests = TestProvider.GetTestWithAttributeValue(TestAttributeValue);
            }
        }

        private void SubscribeToPlaymodeStateChange()
        {
            EditorApplication.playModeStateChanged -= EditorApplicationOnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplicationOnPlayModeStateChanged;
        }

        private void FillToggleByTest()
        {
            ToggleByTest = new Dictionary<TypeAndMethod, bool>();
            foreach (TypeAndMethod typeAndMethod in ExistingTests)
            {
                ToggleByTest.Add(typeAndMethod, false);
            }
        }

        private void EditorApplicationOnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (HasTestsAndPlaymodeRunning(playModeStateChange))
            {
                GameObject testGameObject = new GameObject("testGameObject");
                testGameObject.AddComponent<RuntimeTestManager>();
            }
        }

        private bool HasTestsAndPlaymodeRunning(PlayModeStateChange playModeStateChange)
        {
            TestProvider provider = new TestProvider();
            return provider.AtleastOneTestAvailable() && playModeStateChange == PlayModeStateChange.EnteredPlayMode;
        }

        private void OnGUI()
        {
            StartButtonRender();
            HeaderLabelRender();
            TestTogglesRender();
            SelectButtonsRender();
            AttributeTextRender();
        }
        
        private void StartButtonRender()
        {
            if (GUILayout.Button("Start tests"))
            {
                TestProvider.SaveToFile(ToggleByTest);
                PlayStopRuntime();
            }
        }

        private void HeaderLabelRender()
        {
            GUILayout.Label("Tests", EditorStyles.boldLabel);
        }

        private void AttributeTextRender()
        {
            TestAttributeValue = EditorGUILayout.TextField(TestAttributeValue);
            if (GUILayout.Button("Update"))
            {
                FillTestQueue();
                FillToggleByTest();
            }
        }

        private void TestTogglesRender()
        {
            EditorGUILayout.BeginVertical();
            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos);
            foreach (TypeAndMethod typeAndMethod in ExistingTests)
            {
                ToggleByTest[typeAndMethod] = GUILayout.Toggle(ToggleByTest[typeAndMethod],
                    typeAndMethod.Type.Name + "  " + typeAndMethod.Method);
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void SelectButtonsRender()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SelectAll"))
            {
                SelectAllTest(true);
            }

            if (GUILayout.Button("UnselectAll"))
            {
                SelectAllTest(false);
            }
            GUILayout.EndHorizontal();
        }

        private void SelectAllTest(bool value)
        {
            List<TypeAndMethod> keys = ToggleByTest.Keys.ToList();
            ToggleByTest = new Dictionary<TypeAndMethod, bool>();
            foreach (TypeAndMethod toggleByTest in keys)
            {
                ToggleByTest.Add(toggleByTest, value);
            }
        }

        private void PlayStopRuntime()
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }
}