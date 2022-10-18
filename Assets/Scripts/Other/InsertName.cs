using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class InsertName : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI display_player_name;

    PhotonView view;

    public void Awake()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            view.Owner.NickName = NameCollector.scene1.player_name;
            display_player_name.text = view.Owner.NickName;
        }
        else
        {
            Debug.Log(view.Owner.NickName);
            display_player_name.text = view.Owner.NickName;
        }
        //Debug.Log("im here");
        //this.transform.SetParent(GameObject.Find("SpawnPlayer").transform);
        //updateName();
    }

    public void FixedUpdate()
    {
        SetNames();
    }

    public void SetNames()
    {
        if (view.IsMine)
        {
            view.Owner.NickName = NameCollector.scene1.player_name;
            display_player_name.text = view.Owner.NickName;
        }
        else
        {
            //Debug.Log(view.Owner.NickName);
            display_player_name.text = view.Owner.NickName;
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        //for (int i = 0; i <= 1; i++)
        //{
        //    Debug.Log("Room Entered");
        //    SetNames();
        //}
    }

    //public void updateName()
    //{
    //    for(int i = 0; i < GameObject.Find("SpawnPlayer").transform.childCount; i++)
    //    {
    //        GameObject.Find("SpawnPlayer").transform.GetChild(i).gameObject.GetComponent<InsertName>().SetNames();
    //    }
    //}
}
