using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;

public class SmallWindow : EditorWindow
{
    private NotesScriptableObject _objectData;

    private void OnGUI()
    {
        this._objectData.description = EditorGUILayout.TextArea(this._objectData.description);
    }
    
    
}