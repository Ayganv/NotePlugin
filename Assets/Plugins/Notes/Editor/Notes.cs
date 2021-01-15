using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Object = UnityEngine.Object;

public class Notes : EditorWindow
{
    private const float padding = 10;
    private List<Note> sceneNotes;

    // private string[] scriptableobject =
    //     AssetDatabase.FindAssets(string.Format("t:{0}", (object) typeof(NotesScriptableObject)), new string[1]);
    private Vector2 scroll;

    private Rect offsetRect;

    private bool showProjectNotes = true;
    private bool showSceneNotes = true;
    private string projectNotesGroup = "Project Notes";
    private string sceneNotesGroup = "Scene Notes";

    private Color originalColor;
    private string searchString;

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
        searchString = "";
    }

    void OnGUI()
    {
        scroll = GUILayout.BeginScrollView(scroll);
        GUILayout.BeginVertical();

        GUILayout.Space(10f);

        GUILayout.Label("Search", EditorStyles.boldLabel);
        searchString = EditorGUILayout.TextField(searchString);

        if (searchString == string.Empty || searchString == "" || searchString == " ")
        {
            foreach (Note noteComponent in sceneNotes)
            {
                noteComponent.show = true;
            }
        }
        else
        {
            foreach (Note noteComponent in sceneNotes)
            {
                //noteComponent.show = noteComponent.note.ToLower().Contains(searchString.ToLower());
                if (noteComponent.note.ToLower().Contains(searchString.ToLower()))
                {
                    noteComponent.show = true;
                }
                else if (noteComponent.title.ToLower().Contains(searchString.ToLower()))
                {
                    noteComponent.show = true;
                }
                else if (noteComponent.tags.ToLower().Contains(searchString.ToLower()))
                {
                    noteComponent.show = true;
                }
                else
                {
                    noteComponent.show = false;
                }
            }
        }

        showProjectNotes =
            EditorGUILayout.Foldout(showProjectNotes, "Project Notes");
        if (showProjectNotes)
            GUILayout.Label("WIP", EditorStyles.boldLabel);
        // if (showProjectNotes)
        // {
        //     //TODO: Also do a for-loop for the projectNotes List and output what we need to get from it
        //
        //     for (int index = 0; index < this.sceneNotes.Count; index++)
        //     {
        //         //Add here what we want to show for each Component Object
        //         GUI.backgroundColor = sceneNotes[index].color;
        //         GUILayout.Space(padding);
        //
        //         GUILayout.BeginHorizontal();
        //         GUILayout.Label("Note Name");
        //         GUILayout.FlexibleSpace();
        //         GUILayout.Label("Created by: Linus PC - 13/1 2021 17:53");
        //         GUILayout.EndHorizontal();
        //
        //         GUILayout.BeginHorizontal();
        //         GUILayout.FlexibleSpace();
        //         GUILayout.Label("Edited by: Linus PC - 13/1 2021 17:53");
        //         GUILayout.EndHorizontal();
        //
        //
        //         GUILayout.BeginHorizontal();
        //         if (sceneNotes[index].note != String.Empty) GUILayout.TextArea(sceneNotes[index].note);
        //         else GUILayout.TextArea("#Empty Note");
        //         GUILayout.EndHorizontal();
        //
        //
        //         GUILayout.BeginHorizontal();
        //         GUILayout.Label("Tags: ");
        //         GUILayout.TextArea("Project," + " " + "TestNote2,");
        //         GUILayout.EndHorizontal();
        //
        //         GUI.backgroundColor = originalColor;
        //
        //
        //         GUILayout.BeginHorizontal();
        //
        //         if (GUILayout.Button("Edit Note"))
        //         {
        //             //TODO: Open the EditorWindow to Edit the note   
        //         }
        //
        //         GUILayout.FlexibleSpace();
        //
        //         if (GUILayout.Button("Delete"))
        //         {
        //             //TODO: Make it delete the object & refresh the window
        //         }
        //
        //         GUILayout.EndHorizontal();
        //
        //         //TODO: Needs to show: NoteName, NoteText, Tags, Color, Created Date, Edited Date, DeviceInfo
        //     }
        // }

        GUILayout.Space(padding);


        // Foldout Header Group for the SceneNotes


        showSceneNotes = EditorGUILayout.Foldout(showSceneNotes, "Scene Notes");

        if (showSceneNotes)
        {
            //Iterates through every sceneNote in the List and output what we need to get from it
            for (int index = 0; index < this.sceneNotes.Count; index++)
            {
                //Add here what we want to show for each Component Object

                if (sceneNotes[index].show)
                {
                    GUI.backgroundColor = sceneNotes[index].color;
                    GUILayout.Space(padding);

                    GUILayout.BeginHorizontal();
                    if (sceneNotes[index].title != String.Empty || sceneNotes[index].title != "")
                        GUILayout.Label(sceneNotes[index].title, EditorStyles.boldLabel);
                    else GUILayout.Label("#Empty Title", EditorStyles.boldLabel);
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("Created by:" + " " + sceneNotes[index].deviceName + " - " +
                                    sceneNotes[index].CreatedDate);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("Edited by:" + " " + sceneNotes[index].LastEditedBy + " - " +
                                    sceneNotes[index].LastEditDate);
                    GUILayout.EndHorizontal();


                    GUILayout.BeginHorizontal();
                    if (sceneNotes[index].note != String.Empty) GUILayout.TextArea(sceneNotes[index].note);
                    else GUILayout.TextArea("#Empty Note");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Tags: ");
                    GUILayout.TextArea(sceneNotes[index].tags);
                    GUILayout.EndHorizontal();


                    GUI.backgroundColor = originalColor;


                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button("Ping GameObject"))
                    {
                        EditorGUIUtility.PingObject(sceneNotes[index]);
                    }

                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Delete")
                        && EditorUtility.DisplayDialog("Delete Note", "Are you sure you want to delete your note?",
                            "Yes", "No"))
                    {
                        sceneNotes[index].Delete();
                        Refresh();
                    }

                    GUILayout.EndHorizontal();
                }

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
        this.sceneNotes = new List<Note>();
        // Also needs to have a foreach that looks through Project notes?


        foreach (GameObject gameObject in (GameObject[]) Object.FindObjectsOfType<GameObject>())
        {
            Note[] components = (Note[]) gameObject.GetComponents<Note>();
            if (components != null)
            {
                foreach (Note noteComponent in components)
                {
                    this.sceneNotes.Add(noteComponent);
                }
            }
        }

        this.Repaint();
    }
}