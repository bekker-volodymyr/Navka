using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour
{
    private BefriendableNPC animal;

    [Space]
    [SerializeField] private Button baseOrderButton;
    [SerializeField] private Button secondaryOrderButton;
    [SerializeField] private Button mainButton;

    public void InitPanel(BefriendableNPC animal)
    {
        this.animal = animal;

        mainButton.GetComponentInChildren<TextMeshProUGUI>().text = animal.DescriptionSO.Name;

        animal.NPCDeathEvent += RemovePanel;
    }

    private void OnDestroy()
    {
        animal.NPCDeathEvent -= RemovePanel;
    }

    public void RemovePanel()
    {
        Destroy(gameObject);
    }

    public void BaseOrderClick()
    {
        animal.SetDefaultState();
    }

    public void SecondaryOrderClick()
    {
        animal.SetSecondaryState();
    }
}
