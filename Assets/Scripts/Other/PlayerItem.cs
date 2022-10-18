using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerItem : MonoBehaviourPunCallbacks
{


    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    public TextMeshProUGUI userName;

    //body parts
    public GameObject playerHair;
    public GameObject playerBody;
    public GameObject playerTorso;
    public GameObject playerLeg;

    //sprites array
    public Sprite[] hairs;
    public Sprite[] bodies;
    public Sprite[] torsos;
    public Sprite[] legs;

    Player player;

    private void Start()
    {
    }

    public void setUserName()
    {
        PhotonNetwork.NickName = userName.text;
    }
    public void SetPlayerInfo(Player _player)
    {
        player = _player;
        UpdatePlayerItem(player);
    }

    public void HairLeftArrow()
    {
        if ((int)playerProperties["playerHair"] == 0)
        {
            playerProperties["playerHair"] = hairs.Length - 1;
        }
        else
        {
            playerProperties["playerHair"] = (int)playerProperties["playerHair"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void HairRightArrow()
    {
        if ((int)playerProperties["playerHair"] == hairs.Length - 1)
        {
            playerProperties["playerHair"] = 0;
        }
        else
        {
            playerProperties["playerHair"] = (int)playerProperties["playerHair"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void BodyLeftArrow()
    {
        if ((int)playerProperties["playerBody"] == 0)
        {
            playerProperties["playerBody"] = hairs.Length - 1;
        }
        else
        {
            playerProperties["playerBody"] = (int)playerProperties["playerBody"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void BodyRightArrow()
    {
        if ((int)playerProperties["playerBody"] == hairs.Length - 1)
        {
            playerProperties["playerBody"] = 0;
        }
        else
        {
            playerProperties["playerBody"] = (int)playerProperties["playerBody"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void TorsoLeftArrow()
    {
        if ((int)playerProperties["playerTorso"] == 0)
        {
            playerProperties["playerTorso"] = hairs.Length - 1;
        }
        else
        {
            playerProperties["playerTorso"] = (int)playerProperties["playerTorso"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void TorsoRightArrow()
    {
        if ((int)playerProperties["playerTorso"] == hairs.Length - 1)
        {
            playerProperties["playerTorso"] = 0;
        }
        else
        {
            playerProperties["playerTorso"] = (int)playerProperties["playerTorso"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void LegsLeftArrow()
    {
        if ((int)playerProperties["playerLegs"] == 0)
        {
            playerProperties["playerLegs"] = hairs.Length - 1;
        }
        else
        {
            playerProperties["playerLegs"] = (int)playerProperties["playerLegs"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void LegsRightArrow()
    {
        if ((int)playerProperties["playerLegs"] == hairs.Length - 1)
        {
            playerProperties["playerLegs"] = 0;
        }
        else
        {
            playerProperties["playerLegs"] = (int)playerProperties["playerLegs"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    public void UpdatePlayerItem(Player player)
    {
        if(player.CustomProperties.ContainsKey("playerHair") && player.CustomProperties.ContainsKey("playerBody") 
            && player.CustomProperties.ContainsKey("playerTorso") && player.CustomProperties.ContainsKey("playerLegs"))
        {
            playerHair.GetComponent<SpriteRenderer>().sprite = hairs[(int)player.CustomProperties["playerHair"]];
            playerBody.GetComponent<SpriteRenderer>().sprite = bodies[(int)player.CustomProperties["playerBody"]];
            playerTorso.GetComponent<SpriteRenderer>().sprite = torsos[(int)player.CustomProperties["playerTorso"]];
            playerLeg.GetComponent<SpriteRenderer>().sprite = legs[(int)player.CustomProperties["playerLegs"]];

            playerProperties["playerHair"] = (int)player.CustomProperties["playerHair"];
            playerProperties["playerBody"] = (int)player.CustomProperties["playerBody"];
            playerProperties["playerTorso"] = (int)player.CustomProperties["playerTorso"];
            playerProperties["playerLegs"] = (int)player.CustomProperties["playerLegs"];
        }
        else
        {
            playerProperties["playerHair"] = 0;
            playerProperties["playerBody"] = 0;
            playerProperties["playerTorso"] = 0;
            playerProperties["playerLegs"] = 0;

            playerHair.GetComponent<SpriteRenderer>().sprite = hairs[0];
            playerBody.GetComponent<SpriteRenderer>().sprite = bodies[0];
            playerTorso.GetComponent<SpriteRenderer>().sprite = torsos[0];
            playerLeg.GetComponent<SpriteRenderer>().sprite = legs[0];
        }
    }

}
