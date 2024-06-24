using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<GameObject> playerPrefabs;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Start()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Hashtable customProperties = PhotonNetwork.LocalPlayer.CustomProperties;



        if (customProperties["playerAvatar"] == null || (int)customProperties["playerAvatar"] >= 3)
        {
            Debug.Log("Player avatar not set, defaulting to: player0");

            GameObject playerToSpawn = playerPrefabs[0];
            PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint, Quaternion.identity);
        }
        else
        {
            Debug.Log("placing recieved prefab");
            GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
            PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint, Quaternion.identity);
        }
    }
}
