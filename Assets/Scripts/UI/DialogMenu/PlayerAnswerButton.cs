using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAnswerButton : MonoBehaviour
{
    [SerializeField]
    private PlayerAnswers playerAnswer;
    [SerializeField]
    private Button answerButton;
    private DialogMenu menu;

    public void InitButton(PlayerAnswers playerAnswer, GameObject parent, DialogMenu menu)
    {
        this.menu = menu;
        this.playerAnswer = playerAnswer;
        answerButton.transform.SetParent(parent.transform, false);
        answerButton.onClick.AddListener(OnClick);
        answerButton.GetComponentInChildren<TextMeshProUGUI>().text = playerAnswer.player_response;
    }
    public void OnClick()
    {
        menu.NextLine(playerAnswer.next_line);
    }
}
