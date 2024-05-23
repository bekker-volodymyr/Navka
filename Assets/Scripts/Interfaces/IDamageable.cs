using UnityEngine;

public interface IDamageable
{
    void GetDamage(float damage, GameObject attacker);
    void Death();
    float CurrentHealth { get; }
    float MaxHealth { get; }
}
