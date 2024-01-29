using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable 
{
    bool IsPlayerNoticed { get; set; }
    bool IsWithinAttackDistance { get; set; }
    void SetPlayerNoticedStatus(bool isPlayerNoticed);
    void SetAttackDistanceBool(bool isWithinAttackDistance);
}
