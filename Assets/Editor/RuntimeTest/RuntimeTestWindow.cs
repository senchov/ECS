using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using SimpleJSON;
using Object = UnityEngine.Object;


namespace RuntimeTests
{
    public class RuntimeTestWindow : EditorWindow
    {
        private Queue<TypeAndMethod> ExistingTests = new Queue<TypeAndMethod>();
        private Dictionary<TypeAndMethod, bool> ToggleByTest;
        private TestProvider TestProvider;
        private Vector2 ScrollPos;
        private string TestAttributeValue = String.Empty;
        private Object Folder;

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
        }

        private void FillTestQueue()
        {
            if (Folder != null)
            {
                ExistingTests = TestProvider.GetTests(Folder);
                FillToggleByTest();
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
                ToggleByTest.Add(typeAndMethod, true);
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
            PathForTestRender();
            HeaderLabelRender();
            TestTogglesRender();
            StartButtonRender();
            SelectButtonsRender();
        }

        private void PathForTestRender()
        {
            GUILayout.Label("drag and drop folder with tests", EditorStyles.helpBox);
            Folder = EditorGUILayout.ObjectField(Folder, typeof(Object));
            if (GUILayout.Button("update"))
            {
                FillTestQueue();
            }
        }

        private void HeaderLabelRender()
        {
            GUILayout.Label("Tests", EditorStyles.boldLabel);
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

        private void StartButtonRender()
        {
            if (GUILayout.Button("Start tests"))
            {
                TestProvider.SaveToFile(ToggleByTest);
                PlayStopRuntime();
            }
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