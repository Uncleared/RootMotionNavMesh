using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using YZ.RootMotionAgent;

public class NavigationTest : MonoBehaviour
{
    Vector3 lastDestPos;
    public Transform dest;
    public RootMotionAgent iNavAgent;
    // Start is called before the first frame update
    void Start()
    {
        lastDestPos = dest.position;
        iNavAgent = GetComponent<RootMotionAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastDestPos, dest.position) > Mathf.Epsilon)
        {
            TestNavigation();
        }

        lastDestPos = dest.position;
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