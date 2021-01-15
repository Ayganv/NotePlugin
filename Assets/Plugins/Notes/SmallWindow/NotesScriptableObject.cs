using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NotesScriptableObject : ScriptableObject
{
    [SerializeField] public string objectID;
    [SerializeField] public string title;
    [SerializeField] public string description;
    [SerializeField] public string tags;
    [SerializeField] public Color color;
    [SerializeField] public string deviceName;
    [SerializeField] public string timeCreated;
}