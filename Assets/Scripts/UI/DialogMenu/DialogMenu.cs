using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class DialogMenu : MonoBehaviour
{
    [SerializeField]
    private Button answerButtonPrefab;
    [SerializeField]
    private GameObject answersParent;
    [SerializeField]
    private GameObject answersParentPrefab;
    [SerializeField]
    private CherecterLine firstLine;
    private CherecterLine currentLine;
    private NPCBase NPC;
    private IDialog npcDialog;
    public GameObject dialogMenu;
    public TextMeshProUGUI textComponent; 
    public float textSpeed;

    private int index;
    private int counter = 0;

    public void InitDialog(NPCBase NPC, IDialog npcDialog) 
    {
        this.NPC = NPC;
        this.npcDialog = npcDialog;
        dialogMenu.SetActive(true);
        this.enabled = true;
        textComponent.text = string.Empty;
        InitAnswerParent();
        StartDialogue();
    }

    public void StartDialogue()
    {
        currentLine = npcDialog.Lines[0];
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in currentLine.line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        foreach (var answer in currentLine.answers)
        {
            CreatePlayerAnswerButton(answer);
        }

    }
    public void NextLine(CherecterLine nextLine)
    {  
        Destroy(answersParent);
        InitAnswerParent();
        textComponent.text = string.Empty;
        currentLine = nextLine;
        StartCoroutine(TypeLine());
    }
    public void CloseDialog()
    {
        counter = 0;
        index = 0;
        textComponent.text = string.Empty;
        dialogMenu.SetActive(false);
        this.enabled = false;
        Destroy(answersParent);
        NPC.StateMachine.ChangeState(NPC.IdleState);
    }

    private void CreatePlayerAnswerButton(PlayerAnswers playerAnswer)
    {
        Button newButton;
        newButton = Instantiate(answerButtonPrefab);
        newButton.GetComponent<PlayerAnswerButton>().InitButton(playerAnswer, answersParent, this);
    }
    void InitAnswerParent()
    {
        answersParent = Instantiate(answersParentPrefab);
        answersParent.transform.SetParent(dialogMenu.transform, false);
        answersParent.GetComponentInChildren<Button>().onClick.AddListener(CloseDialog);
    }
}   
