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

        [MenuItem("Window/RuntimeTest")]
        public static void ShowWindwow()
        {
            GetWindow<RuntimeTestWindow>("Runtime test");
        }

        private void OnEnable()
        {
            TestProvider = new TestProvider();
            SubscribeToPlaymodeStateChange();
            FillTestQueue();
        }

        private void SubscribeToPlaymodeStateChange()
        {
            EditorApplication.playModeStateChanged -= EditorApplicationOnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplicationOnPlayModeStateChanged;
        }

        private void FillTestQueue()
        {
            ExistingTests = TestProvider.GetAllExistTestQueue();
            ToggleByTest = new Dictionary<TypeAndMethod, bool>();
            foreach (TypeAndMethod typeAndMethod in ExistingTests)
            {
                ToggleByTest.Add(typeAndMethod, false);
            }
        }

        private void EditorApplicationOnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            TestProvider provider = new TestProvider();
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
            GUILayout.Label("Tests", EditorStyles.boldLabel);

            foreach (TypeAndMethod typeAndMethod in ExistingTests)
            {
                ToggleByTest[typeAndMethod] = GUILayout.Toggle(ToggleByTest[typeAndMethod],
                    typeAndMethod.Type.Name + "  " + typeAndMethod.Method);
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SelectAll"))
            {
                SelectAllTest(true);
            }

            if (GUILayout.Button("UnSelectAll"))
            {
                SelectAllTest(false);
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("Start tests"))
            {
                StartTest();
            }
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

        private void NewMethod(bool value, KeyValuePair<TypeAndMethod, bool> toggleByTest)
        {
            ToggleByTest[toggleByTest.Key] = value;
        }

        private void StartTest()
        {
            TestProvider.SaveToFile(ToggleByTest);
            PlayStopRuntime();
        }

        private void PlayStopRuntime()
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }
}