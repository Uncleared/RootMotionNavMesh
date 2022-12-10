using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DebugEntry
{
    public Component component;
    public string variableName;
}
public class VariableDebugger : MonoBehaviour
{
    public DebugEntry debugEntry;

    public void DisplayVariables()
    {
        Type type = debugEntry.component.GetType();
        //BindingFlags.Instance
        FieldInfo pInfo = type.GetField(debugEntry.variableName);
        string value = (string)pInfo.GetValue(debugEntry.component).ToString();
        print(value);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(VariableDebugger))]
    public class VariableDebuggerEditor : UnityEditor.Editor
    {
        //private SerializedProperty debugEntry;

        void OnEnable()
        {
            //debugEntry = serializedObject.FindProperty("debugEntry");
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            VariableDebugger uiElement = (VariableDebugger)target;
            if (GUILayout.Button("Debug variables"))
            {
                uiElement.DisplayVariables();
            }

        }
    }
#endif
}
