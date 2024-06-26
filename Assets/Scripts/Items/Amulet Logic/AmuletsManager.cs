using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletsManager : MonoBehaviour
{
    [Space]
    [SerializeField] private AmuletButton amuletButtonPrefab;

    private int _amuletsCapacity;

    private List<GameObject> _amuletsParentGOs = new List<GameObject>();
    private List<AmuletButton> _amulets = new List<AmuletButton>();

    private void Start()
    {
        foreach(Transform child in transform)
        {
            _amuletsParentGOs.Add(child.gameObject);
        }

        _amuletsCapacity = _amuletsParentGOs.Count;

        _amuletsParentGOs.Reverse();
    }

    public void AddAmulet(ItemSO amulet, Player player)
    {
        if(_amulets.Count == _amuletsCapacity)
        {
            return;
        }

        if (_amulets.FindAll(button => button.Amulet == amulet).Count > 0)
        {
            return;
        }


        AmuletButton newAmuletButton = Instantiate(amuletButtonPrefab);
        newAmuletButton.InitButton(amulet, player, this);

        foreach(var parent in _amuletsParentGOs)
        {
            if(parent.transform.childCount == 0)
            {
                newAmuletButton.transform.SetParent(parent.transform, false);
                newAmuletButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -20f, 0);
            }
        }
    }

    public void RemoveAmulet(AmuletButton button)
    {
        _amulets.Remove(button);
        Destroy(button.gameObject);
    }
}
