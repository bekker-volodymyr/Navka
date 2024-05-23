using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms;

public class DialogMenu : MonoBehaviour
{
    public GameObject dialog_menu;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InitDialog()
    {
        
        dialog_menu.SetActive(true);
        this.enabled = true;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
    public void CloseDialog()
    {
        counter = 0;
        index = 0;
        textComponent.text = string.Empty;
        dialog_menu.SetActive(false);
        this.enabled = false;
    }
    public void ConfirmDialog()
    {
        counter++;
        if (counter == lines.Length && index == lines.Length - 1)
        {
            counter = 0;
            index = 0;
            textComponent.text = string.Empty;
            dialog_menu.SetActive(false);
            this.enabled = false;
        }
    }
}   
