using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNoticedCheck : MonoBehaviour
{
    [SerializeField] private NPCBase npc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            NPCBase target = collision.GetComponentInParent<NPCBase>();

            if (target != null)
            {
                npc.AddNPCTarget(target);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            
        }
    }
}
