using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class LevelSaver : MonoBehaviour
{
    private string fileName;
    [SerializeField] private WorldGenerator WorldGeneratorInstance;
    public string folderPath = Application.dataPath + "/Saves";

    public void SaveLevel()
    {
        LevelData levelData = new LevelData();
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.activeInHierarchy)
            {
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                GameObjectData data = new GameObjectData
                {
                    name = obj.name,
                    position = obj.transform.position,
                    rotation = obj.transform.eulerAngles.z,
                    spriteName = spriteRenderer != null ? spriteRenderer.sprite.name : null
                };
                levelData.gameObjects.Add(data);
            }
        }

        //BiomsEnum[,] biomsEnums  = WorldGeneratorInstance.biomsGrid;
        

        string json = JsonUtility.ToJson(levelData, true);
        System.IO.File.WriteAllText(GetFilePath(), json);
        Debug.Log("Level saved to " + GetFilePath());
    }

    private string GetFilePath()
    {
        fileName = GenerateUniqueFileName("Save");
        Debug.Log("generated file name:" + fileName);
        return System.IO.Path.Combine(Application.dataPath, "Saves" ,fileName);
    }

    string GenerateUniqueFileName(string baseName)
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
}
