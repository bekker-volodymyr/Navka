using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideoutExit : MonoBehaviour, IInteractable
{
    private void EnterHideout()
    {
        SceneManager.LoadScene(GameManager.worldScene);
    }

    public void OnInteraction(GameObject interactObject)
    {
        EnterHideout();
    }
}
