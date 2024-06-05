using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public string fileName;

    public void LoadLevel()
    {
        string json = System.IO.File.ReadAllText(fileName);

        List<GameObjectData> gameObjects = JsonUtility.FromJson<List<GameObjectData>>(json);

        foreach (GameObjectData gameObjectData in gameObjects)
        {
            // Find the parent object by its name (layer name)
            GameObject parentObject = GameObject.Find(gameObjectData.objectLayerName);

            // If the parent object is not found, log an error and continue to the next GameObjectData
            if (parentObject == null)
            {
                Debug.LogError("Parent object not found for layer: " + gameObjectData.objectLayerName);
                continue;
            }

            // Iterate through positions and instantiate GameObjects
            foreach (Vector2 position in gameObjectData.positions)
            {
                GameObject newObject = new GameObject(); // Instantiate your prefab or create a new GameObject as needed
                newObject.transform.position = position;
                newObject.transform.parent = parentObject.transform; // Set the parent of the new object
                // Add more setup for the instantiated GameObject if needed
            }
        }
    }

    private string GetFilePath()
    {
        return System.IO.Path.Combine(Application.dataPath + "/Saves", fileName);
    }
}
