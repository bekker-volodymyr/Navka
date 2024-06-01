using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : NPCBase, IDialog
{
    [Space]
    [SerializeField]
    public GameObject dialog_menu;
    public DialogMenu dialog_script;
    [Space]
    [SerializeField]
    private List<CharacterLine> lines;
    public List<CharacterLine> Lines { get { return lines; } }
    private bool in_range = false;

   // void Update()
   // {
        //if (in_range && Input.GetKey("t"))
        //{
       //     dialog_menu.SetActive(true);
       //     dialog_script.enabled = true;
       // }
   // }
    public override void OnInteraction(Player player)
    {
        //dialog_menu.SetActive(true);
        //dialog_script.enabled = true;
        StateMachine.ChangeState(DialogState);
        dialog_script.InitDialog(this, this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            in_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            in_range = false;
        }
    }
}
