using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Object = UnityEngine.Object;

public class Notes : EditorWindow
{
    private const float padding = 10;
    private List<NoteComponent> sceneNotes;

    private string[] scriptableobject =
        AssetDatabase.FindAssets(string.Format("t:{0}", (object) typeof(NotesScriptableObject)), new string[1]);
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
            EditorGUILayout.Foldout(showProjectNotes, "Project Notes");

        if (showProjectNotes)
        {
            //TODO: Also do a for-loop for the projectNotes List and output what we need to get from it

            for (int index = 0; index < this.sceneNotes.Count; index++)
            {
                //Add here what we want to show for each Component Object
                GUI.backgroundColor = sceneNotes[index].color;
                GUILayout.Space(padding);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Note Name");
                GUILayout.FlexibleSpace();
                GUILayout.Label("Created by: Linus PC - 13/1 2021 17:53");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Edited by: Linus PC - 13/1 2021 17:53");
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                if (sceneNotes[index].Note != String.Empty) GUILayout.TextArea(sceneNotes[index].Note);
                else GUILayout.TextArea("#Empty Note");
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                GUILayout.Label("Tags: ");
                GUILayout.TextArea("Project," + " " + "TestNote2,");
                GUILayout.EndHorizontal();

                GUI.backgroundColor = originalColor;


                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Edit Note"))
                {
                    //TODO: Open the EditorWindow to Edit the note   
                }

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Delete"))
                {
                    //TODO: Make it delete the object & refresh the window
                }

                GUILayout.EndHorizontal();

                //TODO: Needs to show: NoteName, NoteText, Tags, Color, Created Date, Edited Date, DeviceInfo
            }
        }

        GUILayout.Space(padding);


        // Foldout Header Group for the SceneNotes

        showSceneNotes = EditorGUILayout.Foldout(showSceneNotes, "Scene Notes");

        if (showSceneNotes)
        {
            //Iterates through every sceneNote in the List and output what we need to get from it
            for (int index = 0; index < this.sceneNotes.Count; index++)
            {
                //Add here what we want to show for each Component Object

                GUI.backgroundColor = sceneNotes[index].color;
                GUILayout.Space(padding);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Note Name");
                GUILayout.FlexibleSpace();
                GUILayout.Label("Created by: ");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Edited by: Linus PC - 13/1 2021 17:53");
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                if (sceneNotes[index].Note != String.Empty) GUILayout.TextArea(sceneNotes[index].Note);
                else GUILayout.TextArea("#Empty Note");
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Tags: ");
                GUILayout.TextArea("Project," + " " + "TestNote2,");
                GUILayout.EndHorizontal();


                GUI.backgroundColor = originalColor;


                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Ping GameObject"))
                {
                    //TODO: Make it ping the related GameObject
                }

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Delete"))
                {
                    //TODO: Make it delete the object & refresh the window
                }

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
            ObjectScript.deviceName = Environment.UserName;
            ObjectScript.timeCreated = DateTime.UtcNow.ToString();
            AssetDatabase.CreateAsset((Object) ObjectScript,
                path: "Assets/Scripts/SavedObjects" + "/" + ObjectScript.objectID + ".asset");
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