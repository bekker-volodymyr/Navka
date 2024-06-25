using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogMenu : MonoBehaviour
{
    [Space]
    [SerializeField] private Button answerButtonPrefab;
    [SerializeField] private GameObject answersParentPrefab;
    private GameObject answersParent;

    private CharacterLine currentLine;
    private IDialog npc;

    [Space]
    [SerializeField] private GameObject dialogMenu;
    [SerializeField] private GameObject dialogMenuChild;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Image speakerImage;
    [SerializeField] private float textSpeed;

    void Start()
    {
        GameManager.DialogStartEvent += InitDialog;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.DialogStartEvent -= InitDialog;
    }

    public void InitDialog(IDialog npc) 
    {
        this.npc = npc;
        
        speakerImage.sprite = npc.npc.DescriptionSO.Picture;
        
        dialogMenu.SetActive(true);

        this.enabled = true;
        textComponent.text = string.Empty;

        RefreshAnswersParent();

        StartDialogue();
    }

    private void StartDialogue()
    {
        currentLine = npc.FirstLine;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLine.Line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        foreach (var answer in currentLine.Answers)
        {
            CreatePlayerAnswerButton(answer);
        }

    }

    public void NextLine(CharacterLine nextLine)
    {
        RefreshAnswersParent();

        textComponent.text = string.Empty;
        currentLine = nextLine;

        StartCoroutine(TypeLine());
    }

    public void CloseDialog()
    {
        textComponent.text = string.Empty;
        dialogMenu.SetActive(false);
        this.enabled = false;
        RefreshAnswersParent();

        GameManager.DialogStopEvent?.Invoke();

        npc.npc.StateMachine.ChangeState(npc.npc.IdleState);
    }

    private void CreatePlayerAnswerButton(PlayerAnswers playerAnswer)
    {
        Button newButton;
        newButton = Instantiate(answerButtonPrefab);
        newButton.GetComponent<PlayerAnswerButton>().InitButton(playerAnswer, answersParent, this);
    }
    
    private void RefreshAnswersParent()
    {
        if (answersParent != null) Destroy(answersParent);

        answersParent = Instantiate(answersParentPrefab);
        answersParent.transform.SetParent(dialogMenuChild.transform, false);
        answersParent.GetComponentInChildren<Button>().onClick.AddListener(CloseDialog);
    }
}   
