using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public GameObject SceneCamera;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));
        GameObject GO = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //Instantiate(playerPrefab, randomPosition, Quaternion.identity);
        SceneCamera.SetActive(false);
        //if (SceneCamera != null)
        //{
        //    SceneCamera.SetActive(false);
        //}

    }



}
