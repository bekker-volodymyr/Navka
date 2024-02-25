using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    Rigidbody2D objectRB { get; set; }
    void Move(Vector2 velocity);
    void CheckFacing(Vector2 velocity);
}
