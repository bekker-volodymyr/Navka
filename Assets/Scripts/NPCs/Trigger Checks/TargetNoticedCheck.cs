using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNoticedCheck : MonoBehaviour
{
    [SerializeField] private NPCBase npc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IDamageable"))
        {
            npc.AddNPCTarget(collision.gameObject.GetComponent<NPCBase>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IDamageable"))
        {
            //_npc.RemoveTarget(collision.gameObject.GetComponent<Npc>());
        }
    }
}
