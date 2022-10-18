using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManagerScript : MonoBehaviourPunCallbacks
{
    public PlayerItem playerItem;
    public Transform playerItemParent;

    
    // Start is called before the first frame update
    void Start()
    {
        
        if(PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("null");
            return;
        }
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                playerItem.SetPlayerInfo(player.Value);
                //PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
                //BodySelector.GetComponent<BodyPartsSelector>().SetPlayerInfo(player.Value);
            }
        }
    }

}
