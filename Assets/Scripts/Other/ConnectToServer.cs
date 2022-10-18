using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public object SceneManagement { get; private set; }

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
       PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //What happens when you join the lobby
        //SceneManager.LoadScene("CharacterCreator");
        CreateRoom();
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;
        roomOptions.BroadcastPropsChangeToAll = true;
        PhotonNetwork.JoinOrCreateRoom("Game", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        SceneManager.LoadScene("CharacterCreator_Reworked");
        //PhotonNetwork.LoadLevel("CharacterCreator_Reworked");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Joining Room Failed");
    }
}
