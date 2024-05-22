using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Npc _npc;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _npc = GetComponentInParent<Npc>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            //_npc.SetAttackDistanceBool(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            //_npc.SetAttackDistanceBool(false);
        }
    }
}
