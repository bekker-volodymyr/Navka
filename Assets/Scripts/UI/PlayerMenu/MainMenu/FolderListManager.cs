using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FolderListManager : MonoBehaviour
{
    private string folderPath = Application.dataPath + "/Saves"; // Path to your folder
    public GameObject listItemPrefab; // Prefab for the list item
    public Transform content; // Reference to the Content object of the ScrollView
    
    void Start()
    {
        PopulateList();
        Debug.Log("start");
    }

    void PopulateList()
    {
        //
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                //Debug.Log("file:" + file);
                CreateListItem(Path.GetFileName(file));
            }
        }
        else
        {
            Debug.Log(folderPath.ToString());
            Debug.LogError("Directory doesnt exist");
        }
    }

    public void AddNewItem()
    {
        
    }

    public void DeleteItem(GameObject item)
    {
        string itemName = item.GetComponentInChildren<Text>().text;
        string filePath = Path.Combine(folderPath, itemName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Destroy(item);
        }
    }

    void CreateListItem(string itemName)
    {
        Debug.Log("create item");
        GameObject newItem = Instantiate(listItemPrefab, content);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = itemName;
        newItem.transform.SetParent(content);
        //newItem.GetComponent<Button>().onClick.AddListener(() => DeleteItem(newItem));
    }
}
