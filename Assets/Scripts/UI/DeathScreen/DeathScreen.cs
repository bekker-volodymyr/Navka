using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    GameObject deathScreen;
    CinemachineVirtualCamera _camera;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        deathScreen = GameObject.FindAnyObjectByType<DeathScreen>().gameObject;
        _camera = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();
    }

    public void RespawnClick()
    {
        deathScreen.SetActive(false);
       
        float randomX = Random.Range(-50, 50);
        float randomY = Random.Range(-50, 50);

        Vector2 newPosition = new Vector2(randomX, randomY);
        player.transform.position = newPosition;
        player.Respawn();

        GameManager.isDead = false;

        _camera.Follow = player.transform;
        _camera.LookAt = player.transform;

        Time.timeScale = 1f;

    }

    public void QuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
