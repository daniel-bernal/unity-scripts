using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

// creates an intial project folder structure
public class CreateFolders : EditorWindow {

    private static string projectName = "PROJECT_NAME";
    [MenuItem("Assets/--Create Project Folders--")]
    private static void SetUpFolders() 
    {
        CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>();
        window.position = new Rect(Screen.width/2, Screen.height/2, 400, 150);
        window.ShowPopup();
    }

    private static void CreateAllFolders()
    {
        // update this list with the top level folders
        List<string> folders = new List<string>
        {
            "Animations",
            "Audio",
            "Editor",
            "Materials",
            "Meshes",
            "Prefabs",
            "Scripts",
            "Scenes",
            "Shaders",
            "Textures",
            "UI"
        };

        foreach (string folder in folders)
        {
            if (!Directory.Exists("Assets/" + folder))
            {
                //Debug.Log("Directory" + folder + " does not exist, creating it.\n");
                var newFolder = Directory.CreateDirectory("Assets/" + projectName + "/" + folder);
                // returns a DirectoryInfo object
                var newFile = File.Create("Assets/" + projectName + "/" + folder + "/.keeps"); 
                // returns a FileInfo object
            }
        }

        // update this list for the subfolders under /UI/
        List<string> uiFolders = new List<string>
        {
            "Assets",
            "Fonts",
            "Icons"
        };

        foreach (string subfolder in uiFolders)
        {
            if (!Directory.Exists("Assets/" + projectName + "/UI/" + subfolder))
            {
                //Debug.Log("Directory UI/" + subfolder + " does not exist, creating it.\n");
                var newFolder = Directory.CreateDirectory("Assets/" + projectName + "/UI/" + subfolder);
                // returns a DirectoryInfo object
                var newFile = File.Create("Assets/" + projectName + "/UI/" + subfolder + "/.keeps"); 
                // returns a FileInfo object
            }
        }

        // this makes sure the Project View refreshes and shows the new folders
        AssetDatabase.Refresh();
    }

    void OnGUI() 
    {
        EditorGUILayout.LabelField("Insert the Project name used as the root folder:");
        projectName = Directory.GetParent(Directory.GetCurrentDirectory() + "/").Name;        
        projectName = EditorGUILayout.TextField("Project Name:", projectName);
        this.Repaint();
        GUILayout.Space(50);
        if (GUILayout.Button("Create Folders"))
        {
            CreateAllFolders();
            this.Close();
        }
        if (GUILayout.Button("Exit"))
        {
            this.Close();
        }
        
    }
}
