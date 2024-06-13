using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public List<QuestsSO> questsList;
    public List<QuestsSO> completedQuestsList;
    [SerializeField] private GameObject questLog;
    [SerializeField] private TextMeshProUGUI textPrefab;

    void Start()
    {
        questsList = new List<QuestsSO>();
        completedQuestsList = new List<QuestsSO>();
        GameManager.QuestStartEvent += QuestTake;
        GameManager.QuestStopEvent += QuestComplete;
    }
    private void OnDestroy()
    {
        GameManager.QuestStartEvent -= QuestTake;
        GameManager.QuestStopEvent += QuestComplete;
    }

    public void QuestTake(QuestsSO quest)
    {
        questsList.Add(quest);
        TextMeshProUGUI newText = Instantiate(textPrefab);
        newText.text = $"Quest giver: {quest.QuestGiver}\nDescription: {quest.Description}";
        newText.transform.SetParent(questLog.transform, false);
    }

    public void QuestComplete(QuestsSO quest)
    {
        completedQuestsList.Add(quest);
        //questsList.Remove(quest);
        TextMeshProUGUI newText = Instantiate(textPrefab);
        newText.text = $"Quest giver: {quest.QuestGiver}\nDescription: {quest.Description}\nCompleted!";
        newText.transform.SetParent(questLog.transform, false);
    }
}
