 using UnityEditor;
 using UnityEngine;
 using System.Collections.Generic;
 
 [InitializeOnLoad]
 class HierarchyIcon
 {
     static Texture2D texture;
     static List<int> markedObjects;
     
     static HierarchyIcon ()
     {
         // Init
         texture = AssetDatabase.LoadAssetAtPath ("Assets/Images/note.png", typeof(Texture2D)) as Texture2D;
         EditorApplication.update += UpdateCB;
         EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
     }
     
     static void UpdateCB ()
     {
         // Check here
         GameObject[] go = Object.FindObjectsOfType (typeof(GameObject)) as GameObject[];
         
         markedObjects = new List<int> ();
         foreach (GameObject g in go) 
         {
             // Example: mark all lights
             if (g.GetComponent<NoteComponent> () != null)
                 markedObjects.Add (g.GetInstanceID ());
         }
         
     }
 
     static void HierarchyItemCB (int instanceID, Rect selectionRect)
     {
         // place the icoon to the right of the list:
         Rect r = new Rect (selectionRect); 
         r.x = r.width - 20;
         r.width = 18;
         
         if (markedObjects.Contains (instanceID)) 
         {
             // Draw the texture if it's a light (e.g.)
             GUI.Label (r, texture); 
         }
     }
     
 }