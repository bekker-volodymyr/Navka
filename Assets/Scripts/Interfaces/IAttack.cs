using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    float Damage { get; set; }
    float cooldown { get;set; }
    float delayBeforeDamage { get; set; }
    void Attack(IDamageable target);
    void ApplyDamage(IDamageable target);
}
