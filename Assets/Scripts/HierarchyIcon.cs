using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[InitializeOnLoad]
class HierarchyIcon {
    static Texture2D texture;
    static List<int> markedObjects;

    static HierarchyIcon() {
        texture = AssetDatabase.LoadAssetAtPath("Assets/Images/note.png", typeof(Texture2D)) as Texture2D;
        EditorApplication.update += UpdateCB;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
    }

    static void UpdateCB() {
        GameObject[] go = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        markedObjects = new List<int>();
        foreach (GameObject g in go) {
            // Mark all note components
            if (g.GetComponent<NoteComponent>() != null)
                markedObjects.Add(g.GetInstanceID());
        }
    }

    static void HierarchyItemCB(int instanceID, Rect selectionRect) {
        // place the icon to the right of the list:
        Rect r = new Rect(selectionRect);
        r.x = r.width - 20;
        r.width = 18;
        if (markedObjects != null) {
            if (markedObjects.Contains(instanceID)) {
                // Draw the texture
                GUI.Label(r, texture);
            }
        }
    }
}