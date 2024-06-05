using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverObject : MonoBehaviour, ICover, IInteractable
{
    public Vector3 Position { get { return transform.position; } }
    private ICoverable coveredObject = null;
    public ICoverable CoveredObject => coveredObject;

    public void OnInteraction(GameObject iteractObject)
    {
        ICoverable coverable = iteractObject.GetComponent<ICoverable>();

        if (coverable == null) return;

        Cover(coverable);
    }

    public bool CanCover()
    {
        return coveredObject != null;
    }

    public void Cover(ICoverable objectToCover)
    {
        objectToCover.Cover(this);
    }

    public void LeaveCover()
    {
        coveredObject = null;
    }
}
