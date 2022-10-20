using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Util.PrefabUtil
{
    public static class CreatePrefabInAssets
    {
        private const string PREFAB_FOLDER = "Assets/Project/Prefabs";
        private const string PREFAB_EXTENSION = ".prefab";
        private const string MENU_ITEM_PATH = "Prefabs/";
        private const int HIERARCHY_PRIORITY = 0;

        #region Menu Items
        [MenuItem(MENU_ITEM_PATH + "Create Prefab", false, HIERARCHY_PRIORITY)]
        [MenuItem("Assets/Create/Create Blank Prefab", false, HIERARCHY_PRIORITY)]
        static void CreatePrefab()
        {//create blank prefab file
            GameObject obj = new GameObject("new Prefab");
            CreatePrefabFile(obj, PREFAB_FOLDER, false);
            GameObject.DestroyImmediate(obj);
        }

        [MenuItem(MENU_ITEM_PATH + "Create Prefab(s) From Selection(s)", false, HIERARCHY_PRIORITY)]
        [MenuItem("GameObject/Prefabs/Create Prefab(s) From Selection(s)", false, HIERARCHY_PRIORITY)]
        static void CreateMultiplePrefabsFromSelection()
        {//create INDIVIDUAL prefabs from selected objects, making them unique assets
            GameObject[] objectArray = Selection.gameObjects;

            foreach (GameObject gameObject in objectArray)
            {
                //create prefabs for each object
                CreatePrefabFile(gameObject, PREFAB_FOLDER, true);
            }
        }

        [MenuItem(MENU_ITEM_PATH + "Create Single Prefab From Selections", false, HIERARCHY_PRIORITY)]
        [MenuItem("GameObject/Prefabs/Create Single Prefab From Selections", false, HIERARCHY_PRIORITY)]
        static void CreatePrefabFromSelection()
        {//create SINGLE prefab from selected objects, settings those as children
            GameObject[] objectArray = Selection.gameObjects;

            GameObject parentObj = new GameObject(objectArray[0].name);

            foreach (GameObject gameObject in objectArray)
            {
                //add selected objects as children
                gameObject.transform.parent = parentObj.transform;
            }
            CreatePrefabFile(parentObj, PREFAB_FOLDER, true);
        }

        //Figure out a way to either combine selected gameobjects into one object then make them all instances of the prefab
        //Make a prefab of the first selected object, then make all others an instance of that prefab (adjusted for scale and position n stuff)
        //check if selected gameobjects are the same enough to make them into a single prefab
        //[MenuItem(MENU_ITEM_PATH + "Create Single Prefab And Convert All To Instance", false, HIERARCHY_PRIORITY)]
        //[MenuItem("GameObject/Prefabs/Create Single Prefab And Convert All To Instance", false, HIERARCHY_PRIORITY)]
        //static void MakeSelectionIntoPrefabsInstances()
        //{//create SINGLE prefab from selected objects, then set objects as instances of that prefab
        //    //GameObject[] objectArray = Selection.gameObjects;
        //    //
        //    //GameObject parentObj = new GameObject(objectArray[0].name);
        //    //
        //    //foreach (GameObject gameObject in objectArray)
        //    //{
        //    //    //add selected objects as children
        //    //    gameObject.transform.parent = parentObj.transform;
        //    //}
        //    //CreatePrefabFile(parentObj, true);
        //    //implement
        //}
        #endregion

        #region Validations
        [MenuItem(MENU_ITEM_PATH + "Create Prefab(s) From Selection(s)", true, HIERARCHY_PRIORITY)]
        [MenuItem("GameObject/Prefabs/Create Prefab(s) From Selection(s)", true, HIERARCHY_PRIORITY)]
        static bool ValidateCreateMultiplePrefabsFromSelection()
        {
            return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject);
        }

        [MenuItem(MENU_ITEM_PATH + "Create Single Prefab From Selections", true, HIERARCHY_PRIORITY)]
        [MenuItem("GameObject/Prefabs/Create Single Prefab From Selections", true, HIERARCHY_PRIORITY)]
        static bool ValidateCreatePrefabFromSelection()
        {
            return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject) && Selection.gameObjects.Length > 1;
        }

        [MenuItem(MENU_ITEM_PATH + "Create Single Prefab And Convert All To Instance", true, HIERARCHY_PRIORITY)]
        [MenuItem("GameObject/Prefabs/Create Single Prefab And Convert All To Instance", true, HIERARCHY_PRIORITY)]
        static bool ValidateCreateSelectionIntoPrefabInstance()
        {
            return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject) && Selection.gameObjects.Length > 1;
        }
        #endregion
     
        /// <summary>
        /// creates ".prefab" file in the prefab folder specified by given folder path
        /// </summary>
        /// <param name="obj">object to create prefab of</param>
        /// <param name="prefabFolder">folder to put prefab in. this folder will be created in <see cref="PREFAB_FOLDER"/> if it doesn't exist yet</param>
        /// <param name="makeIntoInstance">whether to make the given object into an instance of the created prefab.</param>
        private static void CreatePrefabFile(GameObject obj, string prefabFolder, bool makeIntoInstance)
        {
            // Create folder Prefabs and set the path as within the Prefabs folder,
            if (!Directory.Exists(prefabFolder))
            {
                AssetDatabase.CreateFolder("Assets", prefabFolder.Replace("Assets/", ""));
            }
            string localPath = prefabFolder + "/" + obj.name + PREFAB_EXTENSION;

            // Make sure the file name is unique, in case an existing Prefab has the same name.
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            //focus on project window 
            EditorUtility.FocusProjectWindow();
            Debug.Log(Selection.activeObject);

            //Create the new Prefab and log whether Prefab was saved successfully.
            bool prefabSuccess;
            GameObject prefabObject;
            if (makeIntoInstance == true)
            {
                prefabObject = PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.UserAction, out prefabSuccess);
            }
            else
            {
                prefabObject = PrefabUtility.SaveAsPrefabAsset(obj, localPath, out prefabSuccess);
                prefabSuccess = true;
            }
            if (prefabSuccess == false)
            {
                Debug.Log("Prefab failed to save" + prefabSuccess);
            }
            else
            {
                EditorGUIUtility.PingObject(prefabObject);
            }
        }


        //Figure out a way to get the current project window folder without it being focused
        //private static string GetCurrentSubFolderPath()
        //{
        //    string filePath;
        //    if (Selection.assetGUIDs.Length == 0)
        //    {
        //        filePath = string.Empty;
        //    }
        //    else
        //    {
        //        filePath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
        //    }
        //    if (!filePath.StartsWith(PREFAB_FOLDER))
        //    {
        //        filePath = PREFAB_FOLDER;
        //    }
        //    return filePath;
        //}
    }
}