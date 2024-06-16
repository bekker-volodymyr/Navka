using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletsManager : MonoBehaviour
{
    [Space]
    [SerializeField] private AmuletButton amuletButtonPrefab;

    public void AddAmulet(ItemSO amulet, Player player)
    {
        AmuletButton newAmuletButton = Instantiate(amuletButtonPrefab);
        newAmuletButton.InitButton(amulet, player);
        newAmuletButton.transform.SetParent(transform, false);
    }
}
