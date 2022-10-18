using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public PlayerItem playerItem;
    public void CreateRoom()
    {
        playerItem.GetComponent<PlayerItem>().setUserName();
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnLeftRoom()
    {
        print("Left Room");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("lobby");
        //What happens when you join the lobby
        //SceneManager.LoadScene("CharacterCreator");
        CreateAnotherRoom();
    }

    public void CreateAnotherRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;
        roomOptions.BroadcastPropsChangeToAll = true;
        PhotonNetwork.JoinOrCreateRoom("Overworld", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        PhotonNetwork.LoadLevel("Overworld");
        //PhotonNetwork.LoadLevel("CharacterCreator_Reworked");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Joining Room Failed");
    }




}
