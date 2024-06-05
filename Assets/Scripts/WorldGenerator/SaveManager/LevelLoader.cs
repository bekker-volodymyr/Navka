using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;



public class LevelLoader : MonoBehaviour
{
    public string filePath;
    public UnityEngine.Transform parentObject; // The parent object to which instantiated prefabs will be attached
    public string layerName = "Environment";
    List<GameObjectData> jsonObjects = new List<GameObjectData>();


    [SerializeField] private GameObject bushPrefab;
    [SerializeField] private GameObject hideoutEntrancePrefab;
    [SerializeField] private GameObject rockCoverPrefab;
    [SerializeField] private GameObject rockSmallPrefab;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject villagePrefab;

    
    public void Start()
    {
        if (GameManager.isSave)
        {
            int layer = LayerMask.NameToLayer(layerName);

            // Find all game objects in the scene
            GameObject[] allObjects = FindObjectsOfType<GameObject>();

            // Loop through all objects and delete the ones on the specified layer
            foreach (GameObject obj in allObjects)
            {
                if (obj.layer == layer)
                {
                    //Destroy(obj);   //!!!!!!!!!!
                }
            }

            //LevelLoader levelLoader = new LevelLoader();
            filePath = GameManager.saveFile;
            LoadLevel();
        }
    }

    void LoadLevel()
    {
        Debug.Log("starting to load level");
        // Load JSON data from file
        string json = File.ReadAllText(filePath);
        if (json == null)
        {
            Debug.LogError("Failed to load JSON data from file: " + filePath);
            return;
        }

        // Parse JSON data using JsonUtility
        Serialization<GameObjectData> data = JsonUtility.FromJson<Serialization<GameObjectData>>(json);

        // Access the list of GameObjectData objects
        List<GameObjectData> gameObjects = data.items;


        foreach (GameObjectData item in gameObjects)
        {
            switch (item.objectLayerName)
            {
                case "Village(Clone)":
                    foreach (Vector2 position in item.positions)
                    {
                        GameObject obj = Instantiate(villagePrefab, position, Quaternion.identity);
                        obj.layer = LayerMask.NameToLayer(item.objectLayerName);
                    }
                    break;
            }
             
        }
    }





    //foreach (GameObjectData gameObjectData in gameObjects)
    //{
    //    // Find the parent object by its name (layer name)
    //    GameObject parentObject = GameObject.Find(gameObjectData.objectLayerName);

    //    // If the parent object is not found, log an error and continue to the next GameObjectData
    //    if (parentObject == null)
    //    {
    //        Debug.LogError("Parent object not found for layer: " + gameObjectData.objectLayerName);
    //        continue;
    //    }

    //    // Iterate through positions and instantiate GameObjects
    //    foreach (Vector2 position in gameObjectData.positions)
    //    {
    //        GameObject newObject = new GameObject(); // Instantiate your prefab or create a new GameObject as needed
    //        newObject.transform.position = position;
    //        newObject.transform.parent = parentObject.transform; // Set the parent of the new object
    //        // Add more setup for the instantiated GameObject if needed
    //    }
    //}

    [System.Serializable]
    private class Serialization<T>
    {
        public List<T> items;
        public Serialization(List<T> items)
        {
            this.items = items;
        }
    }
  

}

