using UnityEngine;
using UnityEngine.SceneManagement;

public class HideoutExit : MonoBehaviour, IInteractable
{
    private void ExitHideout()
    {
        SceneManager.LoadScene(GameManager.worldScene);
    }

    public void OnInteraction(GameObject interactObject)
    {
        ExitHideout();
    }
}
