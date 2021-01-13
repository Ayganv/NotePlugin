using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Notes : EditorWindow
{
    
    private const float padding = 10;
    private float hsBar;
    private List<NoteComponent> sceneNotes;
    private Vector2 scroll;
    
    
    
    [MenuItem("Window/Note")]
    static void Init()
    {
       
        Notes window = (Notes)EditorWindow.GetWindow(typeof(Notes));
        window.Show();
    }

    private void OnEnable()
    {
        Refresh();
    }

    void OnGUI()
    {
        
        GUILayout.Label("Notes", EditorStyles.boldLabel);

        GUILayout.BeginVertical();
        //hsBar = GUILayout.VerticalScrollbar(hsBar, 0f,0f,10f);

        scroll = GUILayout.BeginScrollView(scroll);
        
        //Iterate through every sceneNote in the List and output what we need to get from it
        for (int index = 0; index < this.sceneNotes.Count; index++)
        {
            //Add here what we want to show for each Component Object
            
            GUILayout.BeginVertical();
            GUILayout.Space(padding);
            GUILayout.Label("NoteInScene", EditorStyles.boldLabel);
            GUILayout.Label(sceneNotes[index].Note);
            GUILayout.Space(padding);
            GUILayout.EndVertical();
        }
        
        GUILayout.EndScrollView();
        
        GUILayout.EndVertical();
      
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            
        }
        
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("Refresh List"))
        {
            
            Refresh();
        }
        GUILayout.EndHorizontal();
    }

    public void Refresh()
    {
        this.sceneNotes = new List<NoteComponent>();
        foreach (GameObject gameObject in (GameObject[]) Object.FindObjectsOfType<GameObject>())
        {
            NoteComponent[] components = (NoteComponent[]) gameObject.GetComponents<NoteComponent>();
            if (components != null)
            {
                foreach (NoteComponent noteComponent in components)
                {
                    this.sceneNotes.Add(noteComponent);
                }
            }
        }
        this.Repaint();
    }
}