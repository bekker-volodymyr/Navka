using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class LevelSaver : MonoBehaviour
{
    private string fileName;
    [SerializeField] private WorldGenerator WorldGeneratorInstance;
    public string folderPath = Application.dataPath + "/Saves";
    public int layerName = 6; //Environment

    private GameObject parentObject;
    public GameObjectData testobj = new GameObjectData();

    public void SaveLevel()
    {

        List<GameObject> objectsOnLayer = FindGameObjectsOnLayer(layerName); // get all object layers on layer "environment"

        List<GameObjectData> gameObjects = new List<GameObjectData>();


        foreach (GameObject obj in objectsOnLayer)
        {
            //Debug.Log("Object on layer: " + obj.name);


            GameObjectData gameObject = new GameObjectData();
            gameObject.positions = new List<Vector2>();

            parentObject = obj; // layer
            gameObject.objectLayerName = obj.name;


            //get children (clones) from object layer
            List<GameObject> allChildren = new List<GameObject>();
            GetAllChildren(parentObject, allChildren);


            foreach (GameObject child in allChildren)
            {
                gameObject.positions.Add(child.transform.position);
                //Debug.Log("Child: " + child.name); // child position

            }
            gameObjects.Add(gameObject);

        }

        //BiomsEnum[,] biomsEnums  = WorldGeneratorInstance.biomsGrid;


        string json = JsonUtility.ToJson(new Serialization<GameObjectData>(gameObjects), true);
        //json = JsonUtility.ToJson(biomsEnums, true);
        System.IO.File.WriteAllText(GetFilePath(), json);
        Debug.Log("Level saved to " + GetFilePath());
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

    private string GetFilePath()
    {
        fileName = GenerateUniqueFileName("Save");
        //Debug.Log("generated file name:" + fileName);
        return System.IO.Path.Combine(Application.dataPath, "Saves" ,fileName);
    }

    private string GenerateUniqueFileName(string baseName)
    {
        string fileName = baseName + ".json";
        string filePath = folderPath + "/" + fileName;
        int count = 1;

        while (File.Exists(filePath))
        {
            fileName = $"{baseName} ({count}).json";
            filePath = Path.Combine(folderPath, fileName);
            count++;
        }

        return fileName;
    }

    private List<GameObject> FindGameObjectsOnLayer(int layer)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> objectsOnLayer = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                objectsOnLayer.Add(obj);
            }
        }

        return objectsOnLayer;
    }

    private void GetAllChildren(GameObject parent, List<GameObject> allChildren)
    {
        foreach (Transform child in parent.transform)
        {
            allChildren.Add(child.gameObject);
        }
    }
}
