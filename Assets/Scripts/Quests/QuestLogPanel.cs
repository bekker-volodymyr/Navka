using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogPanel : MonoBehaviour
{
    void Start()
    {
        GameManager.QuestStopEvent += IfQuestCompleted;
    }
    private void OnDestroy()
    {
        GameManager.QuestStopEvent += IfQuestCompleted;
    }

    public void InitPanel(GameObject parent, QuestsSO quest)
    {
        this.transform.SetParent(parent.transform, false);
        this.GetComponentInChildren<TextMeshProUGUI>().text = $"Quest giver: {quest.QuestGiver}\nDescription: {quest.Description}";
    }

    public void IfQuestCompleted(QuestsSO quest)
    {
        this.GetComponentInChildren<TextMeshProUGUI>().text = $"Quest giver: {quest.QuestGiver}\nDescription: {quest.Description}\nCompleted!";
    }
}
