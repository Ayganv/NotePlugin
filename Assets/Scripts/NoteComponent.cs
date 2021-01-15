using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    public string title;
    
    [ReadOnly]
    [SerializeField] public string deviceName;
    
    [ReadOnly]
    [SerializeField] public string CreatedDate;

    [ReadOnly]
    [SerializeField] public string LastEditedBy;
    
    [ReadOnly]
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
