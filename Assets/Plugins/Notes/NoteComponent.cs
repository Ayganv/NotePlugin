using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    public string title;
    
    [SerializeField] public string deviceName;
    
    [SerializeField] public string CreatedDate;

    [SerializeField] public string LastEditedBy;
    
    [SerializeField] public string LastEditDate;
    
    [TextArea(10, 10)]
    public string note;
    public string tags;
    public Color color;

    
    public bool show;

    private NoteComponent()
    {
        note = string.Empty;
        color = Color.white;
        CreatedDate = DateTime.Now.ToString();
        deviceName = Environment.UserName;
    }
    
    public void Delete()
    {
        DestroyImmediate (this);
    }
}
