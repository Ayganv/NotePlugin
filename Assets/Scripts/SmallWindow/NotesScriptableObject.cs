using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NotesScriptableObject : ScriptableObject
{
    [SerializeField] public string objectID;
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public string tags;
    [SerializeField] public Color color;
    [SerializeField] private string deviceName;
    [SerializeField] private Time TimeStamp;
}