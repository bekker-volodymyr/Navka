using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class QuestManager : MonoBehaviour
{
    public List<QuestsSO> questsList;
    public List<QuestsSO> completedQuestsList;
    [SerializeField] private GameObject questLog;
    [SerializeField] private GameObject panelPrefab;

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
        GameObject newPanel;
        newPanel = Instantiate(panelPrefab);
        newPanel.GetComponent<QuestLogPanel>().InitPanel(questLog, quest);
    }

    public void QuestComplete(QuestsSO quest)
    {
        completedQuestsList.Add(quest);
        //questsList.Remove(quest);
    }
}
