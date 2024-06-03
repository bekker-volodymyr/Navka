using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public string fileName
    {
        get
        {
            return fileName;
        }
        set
        {
            fileName = value;
        }
    }

    public void LoadLevel()
    {
        string filePath = GetFilePath();
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            foreach (GameObjectData data in levelData.gameObjects)
            {
                GameObject obj = new GameObject(data.name);
                obj.transform.position = data.position;
                obj.transform.eulerAngles = new Vector3(0, 0, data.rotation);

                if (!string.IsNullOrEmpty(data.spriteName))
                {
                    SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Resources.Load<Sprite>(data.spriteName);
                }
            }

            Debug.Log("Level loaded from " + filePath);
        }
        else
        {
            Debug.LogError("Save file not found at " + filePath);
        }
    }

    private string GetFilePath()
    {
        return System.IO.Path.Combine(Application.persistentDataPath, fileName);
    }
}
