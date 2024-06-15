using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerPrefabs;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Start()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatars"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint, Quaternion.identity);
    }


}
