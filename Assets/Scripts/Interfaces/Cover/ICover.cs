using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICover
{
    Vector3 Position { get; }
    ICoverable CoveredObject { get; }
    bool CanCover();
    void Cover(ICoverable objectToCover);
    void LeaveCover();
}
