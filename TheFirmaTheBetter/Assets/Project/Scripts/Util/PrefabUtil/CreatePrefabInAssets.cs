using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Util.PrefabUtil
{
    public static class CreatePrefabInAssets
    {
        private static string _prefabFolder = "Assets/Project/Prefabs";
        private static string _prefabExtension = ".prefab";
        [MenuItem("Astrofire/Create Prefab")]
        static void CreatePrefab()
        {//create blank prefab file
            GameObject obj = new GameObject("new Prefab");
            CreatePrefabFile(obj, false);
        }

        [MenuItem("Astrofire/Create Prefab(s) From Selection(s)")]
        static void CreateMultiplePrefabsFromSelection()
        {//create INDIVIDUAL prefabs from selected objects, making them unique assets
            GameObject[] objectArray = Selection.gameObjects;

            foreach (GameObject gameObject in objectArray)
            {
                //create prefabs for each object
                CreatePrefabFile(gameObject, true);
            }
        }

        [MenuItem("Astrofire/Create Single Prefab From Selections")]
        static void CreatePrefabFromSelection()
        {//create SINGLE prefab from selected objects, settings those as children
            GameObject[] objectArray = Selection.gameObjects;

            GameObject parentObj = new GameObject(objectArray[0].name);

            foreach (GameObject gameObject in objectArray)
            {
                //add selected objects as children
                gameObject.transform.parent = parentObj.transform;
            }
            CreatePrefabFile(parentObj, true);
        }

        [MenuItem("Astrofire/Create Single Prefab From Selections")]
        static void MakeSelectionIntoPrefabsInstances()
        {//create SINGLE prefab from selected objects, then set objects as instances of that prefab
            //GameObject[] objectArray = Selection.gameObjects;
            //
            //GameObject parentObj = new GameObject(objectArray[0].name);
            //
            //foreach (GameObject gameObject in objectArray)
            //{
            //    //add selected objects as children
            //    gameObject.transform.parent = parentObj.transform;
            //}
            //CreatePrefabFile(parentObj, true);
            //implement
        }

        private static void CreatePrefabFile(GameObject obj, bool makeIntoInstance)
        {
            // Create folder Prefabs and set the path as within the Prefabs folder,
            if (!Directory.Exists(_prefabFolder))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }
            string localPath = _prefabFolder + "/" + obj.name + _prefabExtension;

            // Make sure the file name is unique, in case an existing Prefab has the same name.
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            // Create the new Prefab and log whether Prefab was saved successfully.
            bool prefabSuccess;
            GameObject prefabObject;
            if (makeIntoInstance == true)
            {
                prefabObject = PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.UserAction, out prefabSuccess);
            }
            else
            {
                prefabObject = PrefabUtility.SaveAsPrefabAsset(obj, localPath, out prefabSuccess);
                GameObject.DestroyImmediate(obj);
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

        [MenuItem("Astrofire/Create Prefab(s) From Selection(s)", true)]
        static bool ValidateCreateMultiplePrefabsFromSelection()
        {
            return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject);
        }

        [MenuItem("Astrofire/Create Single Prefab From Selections", true)]
        static bool ValidateCreatePrefabFromSelection()
        {
            return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject) && Selection.gameObjects.Length > 1;
        }
    }
}