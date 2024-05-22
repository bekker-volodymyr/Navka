using UnityEngine;

public interface IMoveable
{
    Rigidbody2D ObjectRB { get; }
    void Move(Vector2 velocity);
    void CheckFacing(Vector2 velocity);
}
