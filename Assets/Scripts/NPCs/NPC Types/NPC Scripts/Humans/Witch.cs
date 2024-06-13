using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : NPCBase, IDialog
{
    [Space]
    [SerializeField] private List<CharacterLine> lines;
    public List<CharacterLine> Lines { get { return lines; } }
    public NPCBase npc { get { return this; } }
    [Space]
    [SerializeField] private List<ItemSO> questItemsList;
    private QuestsSO quest;
    private bool isQuestGiven = false;
    private bool isQuestComplete = false;
    private CharacterLine firstLine;
    public CharacterLine FirstLine { get { return firstLine; } }

    override protected void Start()
    {
        base.Start();
        firstLine = lines[0];
        GameManager.QuestStartEvent += QuestTake;
    }
    private void OnDestroy()
    {
        GameManager.QuestStartEvent -= QuestTake;
    }

    private void QuestTake(QuestsSO quest)
    {
        this.quest = quest;
        isQuestGiven = true;
    }

    public override void OnInteraction(GameObject interactObject)
    {
        Player player = interactObject.GetComponent<Player>();

        if (player == null) return;

        if(isQuestGiven)
        {
            WitchQuestComplete(player);
        }
        else
        {
            StateMachine.ChangeState(DialogState);
            GameManager.DialogStartEvent?.Invoke(this);
        }       
    }

    public void WitchQuestComplete(Player player)
    {
        ItemSO selectedItem = player.SelectedItem;

        if (selectedItem != null)
        {
            if (questItemsList.Contains(selectedItem))
            {
                questItemsList.Remove(selectedItem);
                player.FeedItem();

                if (questItemsList.Count == 0)
                {
                    GameManager.QuestStopEvent?.Invoke(this.quest);
                    isQuestGiven = false;
                    firstLine = lines[1];
                }
            }
        }
    }
}
