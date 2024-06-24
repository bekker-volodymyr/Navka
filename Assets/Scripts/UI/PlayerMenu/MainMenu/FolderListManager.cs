using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FolderListManager : MonoBehaviour
{
    private string folderPath = Application.dataPath + "/Saves"; // Path to your folder
    public GameObject listItemPrefab; // Prefab for the list item
    public Transform content; // Reference to the Content object of the ScrollView

    private List<ListItem> items = new List<ListItem>(); // List of items
    private int selectedIndex = -1; // Index of the selected item
    public Button selectButton; // Button to show item details
    public Button deleteButton; // Button to delete item


    void Start()
    {
        PopulateList();
        
        selectButton.onClick.AddListener(OnSelectButtonClicked);
        deleteButton.onClick.AddListener(OnDeleteButtonClicked);
    }

    void PopulateList()
    {
        int i = 0;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");

            foreach (string file in files)
            {
                //Debug.Log("file:" + file);
                CreateListItem(Path.GetFileName(file), i);
                i++;
            }
        }
        else
        {
            Debug.Log(folderPath.ToString());
            Debug.LogError("Directory doesnt exist");
        }
        
    }

    void OnItemSelected(int index)
    {
        selectedIndex = index;
        Debug.Log("Item " + index + " selected");

    }

    void CreateListItem(string itemName, int index)
    {
        //Debug.Log("create item");
        GameObject newItem = Instantiate(listItemPrefab, content);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = itemName;
        newItem.transform.SetParent(content);

        ListItem listItem = new ListItem();
        listItem.filePath = folderPath + "/" + itemName;
        listItem.itemName = itemName;
        
        listItem.index = index;
        items.Add(listItem);

        newItem.GetComponent<Button>().onClick.AddListener(() => OnItemSelected(index));

    }

    void OnSelectButtonClicked()
    {
        if (selectedIndex != -1)
        {
            //Load new owrld 
            LoadSelecedItem(selectedIndex);
        }
    }

    void OnDeleteButtonClicked()
    {
        if (selectedIndex != -1)
        {
            DeleteSelectedItem(selectedIndex);
        }
    }

    void LoadSelecedItem(int index)
    {
        GameManager.isSave = true;
        GameManager.saveFile = items[index].filePath;
        SceneManager.LoadScene("Campaign");
    }


    void DeleteSelectedItem(int index)
    {
 
        Debug.Log("current selected index :" + index);

        string filePath = items[index].filePath;

        // Remove the item from the ScrollView
        Destroy(content.GetChild(index).gameObject);
        items.RemoveAt(index);

        // Delete the file associated with the item
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File deleted: " + filePath);
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }

        //update list
        PopulateList();

        // Hide the details panel if the deleted item was selected
        selectedIndex = -1;
    }


    [System.Serializable]
    public class ListItem
    {
        public int index;
        public string itemName;
        public string filePath; // Path to the associated file
    }
}
