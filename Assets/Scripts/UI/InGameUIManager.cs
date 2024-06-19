using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [Space]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseBtn;

    [Space]
    [SerializeField] private GameObject deathScreen;

    private void Start()
    {
        GameManager.DeathEvent += DeathScreenEnable;
    }

    private void OnDestroy()
    {
        GameManager.DeathEvent -= DeathScreenEnable;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        pauseMenu.SetActive(!GameManager.isPaused);
        GameManager.isPaused = !GameManager.isPaused;

        pauseBtn.SetActive(!GameManager.isPaused);

        Time.timeScale = GameManager.isPaused ? 0f : 1f;
    }

    private void DeathScreenEnable()
    {
        deathScreen.SetActive(true);
    }
}
