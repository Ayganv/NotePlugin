using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    public string title;
    
    [ReadOnly]
    [SerializeField] public string deviceName;
    
    [ReadOnly]
    [SerializeField] public string CreatedDate;

    [TextArea(10, 10)]
    public string Note;
    public string tags;
    public Color color;

    
    
    private NoteComponent()
    {
        CreatedDate = DateTime.UtcNow.ToString();
        deviceName = Environment.UserName;
    }
}
