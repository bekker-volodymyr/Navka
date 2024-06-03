using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDropper : MonoBehaviour, IInteractable
{
    [Space]
    [SerializeField] protected ItemSO item;

    [Space]
    [SerializeField] private Item itemPrefab;

    private Enums.InteractionType interactType = Enums.InteractionType.EnvironmentObject;
    public Enums.InteractionType InteractionType => interactType;

    virtual public void OnInteraction(Player player)
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        if (item == null) return;

        Item newItem = Instantiate(itemPrefab);
        newItem.InitItem(item, 1);
        newItem.gameObject.transform.position = transform.position + GetRandomPointInBottomSemiCircle();
    }

    public Vector3 GetRandomPointInBottomSemiCircle()
    {
        // Generate a random angle between 180 and 360 degrees
        float angle = Random.Range(180f, 360f);

        // Convert the angle to radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calculate the x and y coordinates using trigonometric functions
        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        // Return the point as a Vector2
        return new Vector3(x, y, transform.position.z);
    }
}
