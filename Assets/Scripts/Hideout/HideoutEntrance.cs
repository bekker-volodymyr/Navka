using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideoutEntrance : MonoBehaviour, IInteractable
{
    //[SerializeField] private HideoutStruct hideoutData;
    public string sceneName;

    private void EnterHideout() {
        //Debug.Log("attempt to enter hideout");
        SceneManager.LoadScene(sceneName);
    }

    public void OnInteraction(Player player)
    {
        EnterHideout(); // - crash here - says no reference for an object 
    }
}
