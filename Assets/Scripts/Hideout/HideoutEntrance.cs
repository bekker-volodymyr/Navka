using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideoutEntrance : MonoBehaviour, IInteractable
{
    private string sceneName = "HideoutScene";

    private void EnterHideout(GameObject interactObject) 
    {
        Player player = interactObject.GetComponent<Player>();
        if (player != null)
        {
            GameManager.health = player.CurrentHealth;
            GameManager.hunger = player.CurrentHunger;
            GameManager.mana = player.CurrentMana;
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnInteraction(GameObject interactObject)
    {
        EnterHideout(interactObject);
    }
}
