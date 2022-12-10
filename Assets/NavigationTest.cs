using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;

public class NavigationTest : MonoBehaviour
{
    public Transform dest;
    public RootMotionAgent iNavAgent;
    // Start is called before the first frame update
    void Start()
    {
        iNavAgent = GetComponent<RootMotionAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestNavigation()
    {
        iNavAgent.Move(dest.position);
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(NavigationTest))]
public class NavigationTestEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        NavigationTest uiElement = (NavigationTest) target;
        if (GUILayout.Button("Test Navigation"))
        {
            uiElement.TestNavigation();
        }

    }
}
#endif