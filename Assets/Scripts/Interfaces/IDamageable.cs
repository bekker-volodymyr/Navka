public interface IDamageable
{
    void GetDamage(float damage);
    void Death();
    float CurrentHealth { get; }
    float MaxHealth { get; }
}
