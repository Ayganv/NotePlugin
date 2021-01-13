using UnityEngine;
using UnityEditor;

public class Notes : EditorWindow
{
    
    [MenuItem("Window/Note")]
    static void Init()
    {
       
        Notes window = (Notes)EditorWindow.GetWindow(typeof(Notes));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Notes", EditorStyles.boldLabel);
    
        
        if (GUILayout.Button("Create"))
        {
            
        }
    }
}