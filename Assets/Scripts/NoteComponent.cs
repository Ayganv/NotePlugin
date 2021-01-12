using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    [TextArea(10, 10)]
    public string Note;
    public string tags;
    public Color color;
}
