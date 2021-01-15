using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Note))]
public class NoteComponentEditor : Editor
{
   public override void OnInspectorGUI()
   {
      
      // base.OnInspectorGUI();
      var noteComponent = this.target as Note;
      
      EditorGUI.BeginChangeCheck();

      noteComponent.title = EditorGUILayout.TextField("Title", noteComponent.title);
      
      noteComponent.tags = EditorGUILayout.TextField("Tags", noteComponent.tags);
      
      EditorGUILayout.LabelField("Note");
      noteComponent.note = EditorGUILayout.TextArea(noteComponent.note, GUILayout.MinHeight(50));

      noteComponent.color = EditorGUILayout.ColorField("Color", noteComponent.color);
      
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

      noteComponent.show = EditorGUILayout.Toggle(nameof(noteComponent.show), noteComponent.show);
   }
}
