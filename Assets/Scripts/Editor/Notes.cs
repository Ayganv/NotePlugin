using UnityEngine;
using UnityEditor;

public class Note : EditorWindow
{
    
    [MenuItem("Window/Note")]
    static void Init()
    {
       
        Note window = (Note)EditorWindow.GetWindow(typeof(Note));
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