using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoteComponent))]
public class NoteComponentEditor : Editor
{
   public override void OnInspectorGUI()
   {
      
      // base.OnInspectorGUI();
      var noteComponent = this.target as NoteComponent;
      
      EditorGUI.BeginChangeCheck();

      noteComponent.title = EditorGUILayout.TextField(nameof(noteComponent.title), noteComponent.title);
      
      noteComponent.tags = EditorGUILayout.TextField(nameof(noteComponent.tags), noteComponent.tags);
      
      noteComponent.Note = EditorGUILayout.TextField(nameof(noteComponent.Note), noteComponent.Note, GUILayout.Height(100)  );

      noteComponent.color = EditorGUILayout.ColorField(nameof(noteComponent.color), noteComponent.color);
      
      if (EditorGUI.EndChangeCheck())
      {
         noteComponent.LastEditedBy = Environment.UserName;
         noteComponent.LastEditDate = DateTime.Now.ToString();
         EditorUtility.SetDirty(noteComponent);
      }
      
      EditorGUI.BeginDisabledGroup(true);
      EditorGUILayout.TextField("Created by: ", noteComponent.deviceName);
      EditorGUILayout.TextField("Created on: ", noteComponent.CreatedDate);
      
      EditorGUILayout.TextField("Last Edited By: ", noteComponent.LastEditedBy);

      EditorGUILayout.TextField("Last Edited Date: ", noteComponent.LastEditDate);
      EditorGUI.EndDisabledGroup();
   }
}
