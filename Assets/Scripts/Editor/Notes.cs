using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class Notes : EditorWindow
{
    private const float padding = 10;
    private List<NoteComponent> sceneNotes;
    private Vector2 scroll;

    private Rect offsetRect;

    private bool showProjectNotes = true;
    private bool showSceneNotes = true;
    private string projectNotesGroup = "Project Notes";
    private string sceneNotesGroup = "Scene Notes";
    
    private Color originalColor;


    [MenuItem("Window/Note")]
    static void Init()
    {
        Notes window = (Notes) EditorWindow.GetWindow(typeof(Notes));
        window.Show();
    }

    private void OnEnable()
    {
        originalColor = GUI.backgroundColor;
        Refresh();
    }

    void OnGUI()
    {
      
        scroll = GUILayout.BeginScrollView(scroll);
        GUILayout.BeginVertical();
        
        GUILayout.Space(10f);
        GUILayout.Label("Search-field-placeholder", EditorStyles.boldLabel);
        
        
        showProjectNotes =
            EditorGUILayout.Foldout(showProjectNotes , "Project Notes");

        if (showProjectNotes)
        {
            
            //TODO: Also do a for-loop for the projectNotes List and output what we need to get from it

            for (int index = 0; index < this.sceneNotes.Count; index++)
            {
                //Add here what we want to show for each Component Object
                GUILayout.Space(padding);
                GUILayout.BeginHorizontal();
                GUILayout.Space(padding);
                GUILayout.Label("NoteInScene", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.Label(sceneNotes[index].Note);
                GUILayout.Space(padding);
                GUILayout.EndHorizontal();

                //TODO: Needs to show: NoteName, NoteText, Tags, Color, Created Date, Edited Date, DeviceInfo
            }
        }

        GUILayout.Space(padding);


        // Foldout Header Group for the SceneNotes
        
        showSceneNotes = EditorGUILayout.Foldout(showSceneNotes , "Scene Notes");

        if (showSceneNotes)
        {
            
            //Iterates through every sceneNote in the List and output what we need to get from it
            for (int index = 0; index < this.sceneNotes.Count; index++)
            {
                //Add here what we want to show for each Component Object
                GUILayout.Space(padding);
                GUILayout.BeginHorizontal();
                GUILayout.Space(padding);
                GUILayout.Label("NoteInScene", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.Label(sceneNotes[index].Note);
                GUILayout.Space(padding);
                GUILayout.EndHorizontal();

                //TODO: Needs to show: NoteName, NoteText, Tags, Color, Created Date, Edited Date, DeviceInfo
            }
        }

        
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        GUILayout.Space(padding);

        

        


        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Create"))
        {
            NotesScriptableObject ObjectScript =
                ScriptableObject.CreateInstance(typeof(NotesScriptableObject)) as NotesScriptableObject;
            ObjectScript.objectID = Path.GetRandomFileName();
            AssetDatabase.CreateAsset((Object) ObjectScript, path: "Assets/Scripts/SavedObjects" + "/" + ObjectScript.objectID + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Refresh List"))
        {
            Refresh();
        }


        GUILayout.EndHorizontal();
        
        GUILayout.EndScrollView();
        
    }

    public void Refresh()
    {
        this.sceneNotes = new List<NoteComponent>();
        // Also needs to have a foreach that looks through Project notes?


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