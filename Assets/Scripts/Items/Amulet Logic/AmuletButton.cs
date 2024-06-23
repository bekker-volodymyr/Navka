using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletButton : MonoBehaviour
{
    [Space]
    [SerializeField] private Button _button;

    private Player _player;
    private ItemSO _amulet;
    public ItemSO Amulet => _amulet;
    private AmuletsManager _manager;

    public void InitButton(ItemSO amulet, Player player, AmuletsManager manager)
    {
        _amulet = amulet;
        _player = player;
        _button.image.sprite = amulet.Sprite;
        _manager = manager;
    }

    public void OnClick()
    {
        _player.TakeOffAmulet(_amulet);
        _manager.RemoveAmulet(this);
    }
} 
