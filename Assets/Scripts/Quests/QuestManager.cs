using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Space]
    [SerializeField] private List<ItemSO> questItemsList;
    [SerializeField] private DialogMenu dialogMenu;
    [Space]
    [SerializeField] private int firstDialog;
    [SerializeField] private int lastDialog;


    public void QuestTake()
    {
        dialogMenu.currentDialogLine = firstDialog;
    }

    public void QuestComplete(IDialog npc, GameObject interactObject)
    {
        Player player = interactObject.GetComponent<Player>();

        if (player == null)
        {
            return;
        }

        ItemSO selectedItem = player.SelectedItem;

        if (selectedItem != null)
        {
            if (questItemsList.Contains(selectedItem))
                {
                    questItemsList.Remove(selectedItem);

                    if (questItemsList.Count == 0)
                    {
                        dialogMenu.CloseDialog();
                        dialogMenu.currentDialogLine = lastDialog;
                        dialogMenu.InitDialog(npc);
                    }
                }
        }
    }
}
