public interface IDamageable
{
    void GetDamage(float damage);
    void Death();
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
}
