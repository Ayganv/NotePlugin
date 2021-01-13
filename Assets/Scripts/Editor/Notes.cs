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


    [MenuItem("Window/Note")]
    static void Init()
    {
        Notes window = (Notes) EditorWindow.GetWindow(typeof(Notes));
        window.Show();
    }

    private void OnEnable()
    {
        Refresh();
    }

    void OnGUI()
    {
        //GUILayout.BeginVertical();
        GUILayout.Label("Vert", EditorStyles.boldLabel);


        //GUILayout.BeginVertical();
        GUILayout.Label("Search-field-placeholder", EditorStyles.boldLabel);
        //GUILayout.EndVertical();

        //GUILayout.BeginVertical();

        scroll = GUILayout.BeginScrollView(scroll);
        //GUILayout.BeginVertical();

        // Foldout Header Group for the ProjectNotes
        //GUILayout.BeginVertical();
        //showProjectNotes = EditorGUI.Foldout(new Rect(3, 3, position.width - 6, 15), showProjectNotes, projectNotesGroup);

        showProjectNotes =
            EditorGUI.BeginFoldoutHeaderGroup(new Rect(0, 3, 200, 20), showProjectNotes, projectNotesGroup);


        GUILayout.Space(padding);


        if (showProjectNotes)
        {
            //GUILayout.BeginVertical();
            //TODO: Also do a for-loop for the projectNotes List and output what we need to get from it

            for (int index = 0; index < this.sceneNotes.Count; index++)
            {
                //Add here what we want to show for each Component Object
                GUILayout.Space(padding);
                GUILayout.BeginVertical();
                GUILayout.Space(padding);
                GUILayout.Label("NoteInScene", EditorStyles.boldLabel);
                GUILayout.Label(sceneNotes[index].Note);
                GUILayout.Space(padding);
                GUILayout.EndVertical();

                //TODO: Needs to show: NoteName, NoteText, Tags, Color, Created Date, Edited Date, DeviceInfo
            }


            //GUILayout.EndVertical();
            //offsetRect = ;
        }

        EditorGUI.EndFoldoutHeaderGroup();
        //GUILayout.EndVertical();

        //GUILayout.EndVertical();

        //GUILayout.BeginVertical();
        GUILayout.Space(padding);


        // Foldout Header Group for the SceneNotes
        //GUILayout.BeginVertical();
        showSceneNotes = EditorGUI.BeginFoldoutHeaderGroup(new Rect(0, 23, 200, 20), showSceneNotes, sceneNotesGroup);

        //showSceneNotes = EditorGUI.Foldout(new Rect(3, 23, position.width - 6, 15), showSceneNotes, sceneNotesGroup);

        if (showSceneNotes)
        {
            //GUILayout.BeginVertical();
            //Iterates through every sceneNote in the List and output what we need to get from it
            for (int index = 0; index < this.sceneNotes.Count; index++)
            {
                //Add here what we want to show for each Component Object
                GUILayout.Space(padding);
                GUILayout.BeginVertical();
                GUILayout.Space(padding);
                GUILayout.Label("NoteInScene", EditorStyles.boldLabel);
                GUILayout.Label(sceneNotes[index].Note);
                GUILayout.Space(padding);
                GUILayout.EndVertical();

                //TODO: Needs to show: NoteName, NoteText, Tags, Color, Created Date, Edited Date, DeviceInfo
            }

            //GUILayout.EndVertical();
        }

        EditorGUI.EndFoldoutHeaderGroup();
        //GUILayout.EndVertical();

        GUILayout.Space(padding);

        //GUILayout.EndVertical();

        GUILayout.EndScrollView();


        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create"))
        {
            CreateInstance<SmallWindow>();
            NotesScriptableObject ObjectScript =
                ScriptableObject.CreateInstance(typeof(NotesScriptableObject)) as NotesScriptableObject;
            ObjectScript.objectID = Path.GetRandomFileName();
            AssetDatabase.CreateAsset((Object) ObjectScript, "Assets/Scripts/SavedObjects" + ObjectScript.objectID + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Refresh List"))
        {
            Refresh();
        }


        GUILayout.EndHorizontal();
        //GUILayout.EndVertical();
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