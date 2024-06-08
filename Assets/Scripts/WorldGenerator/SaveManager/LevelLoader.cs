using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;



public class LevelLoader : MonoBehaviour
{
    public string filePath;
    //public UnityEngine.Transform parentObject; // The parent object to which instantiated prefabs will be attached
    public string layerName = "Environment";
    List<GameObjectData> jsonObjects = new List<GameObjectData>();
    [SerializeField] protected List<GameObject> prefabsList;
    [SerializeField]public GameObject parentGameObject;
    //[SerializeField] private GameObject parentGameObjectEnvironmnet;

    public void Start()
    {
        if (GameManager.isSave)
        {
            ClearLayer();

            filePath = GameManager.saveFile;
            LoadLevel();
        }
    }

    void LoadLevel()
    {
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

        foreach (GameObjectData gameObject in gameObjects)
        {
            Debug.Log("current game object:" + gameObject.objectLayerName);
            foreach (GameObject prefab in prefabsList)
            {
                    // Compare prefab name with the current layer name
                    if (prefab.name == gameObject.objectLayerName)
                    {
                    //foreach (Vector2 vector2 in gameObject.positions)
                    //{
                        //select prefab from prefab list
                        GameObject instantiatedPrefab = Instantiate(prefab);

                        Debug.Log($"Prefab '{prefab.name}' matches the current layer name '{gameObject.objectLayerName}'.");

                        //set prefabs parent
                        instantiatedPrefab.transform.SetParent(parentGameObject.transform, false);
                        //instantiatedPrefab.transform.localPosition = vector2;

                        int x = Random.Range(50, -50);
                        int y = Random.Range(50, -50);
                        UnityEngine.Vector3 vector3 = new UnityEngine.Vector3(10, 10, 0);
                    instantiatedPrefab.transform.localPosition = vector3;

                    //instantiatedPrefab.transform.localPosition = UnityEngine.Vector3.zero;
                    instantiatedPrefab.transform.localRotation = UnityEngine.Quaternion.identity;
                        instantiatedPrefab.transform.localScale = UnityEngine.Vector3.one;
                    //}

                        //place children
                        //foreach (Vector2 vector2 in gameObject.positions)
                        //{
                        //    instantiatedPrefab.transform.localPosition = vector2;
                        //    instantiatedPrefab.transform.localRotation = Quaternion.identity;

                        //}
                        // Optionally, reset the local position, rotation, and scale

                    
                    }
            }
            
            // Set the parent of the instantiated prefab
            Debug.Log("Prefab instantiated and set as child of " + parentGameObject.name);

            
        }
    }



    [System.Serializable]
    private class Serialization<T>
    {
        public List<T> items;
        public Serialization(List<T> items)
        {
            this.items = items;
        }
    }

    public void ClearLayer()
    {
        int layer = LayerMask.NameToLayer(layerName);

        // Find all game objects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Loop through all objects and delete the ones on the specified layer
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                Destroy(obj); 
            }
        }

    }

}

