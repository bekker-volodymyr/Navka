using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : NPCBase
{
    public GameObject dialog_menu;
    public DialogMenu dialog_script;
    private bool in_range = false;

   // void Update()
   // {
        //if (in_range && Input.GetKey("t"))
        //{
       //     dialog_menu.SetActive(true);
       //     dialog_script.enabled = true;
       // }
   // }
    public override void OnInteraction()
    {
        //dialog_menu.SetActive(true);
        //dialog_script.enabled = true;
        dialog_script.InitDialog();
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
