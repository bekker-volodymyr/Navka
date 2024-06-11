using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletButton : MonoBehaviour
{
    [Space]
    [SerializeField] private Button button;

    private Player player;
    private ItemSO amulet;

    public void InitButton(ItemSO amulet, Player player)
    {
        this.amulet = amulet;
        this.player = player;
        button.image.sprite = amulet.Sprite;
        
    }

    public void OnClick()
    {
        player.TakeOffAmulet(amulet);
        Destroy(this.gameObject);
    }
} 
