using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;

    public void RespawnClick()
    {
        deathScreen.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float randomX = Random.Range(-50, 50);
        float randomY = Random.Range(-50, 50);

        Vector2 newPosition = new Vector2(randomX, randomY);
        player.transform.position = newPosition;

        GameManager.isPaused = false;
    }

    public void QuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
