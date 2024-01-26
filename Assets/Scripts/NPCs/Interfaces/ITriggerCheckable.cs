using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable 
{
    bool IsPlayerNoticed { get; set; }
    bool IsWithinStrikingDistance { get; set; }
    void SetPlayerNoticedStatus(bool isPlayerNoticed);
    void SetStrikingDistanceBool(bool isWithinStrikingDistance);
}
