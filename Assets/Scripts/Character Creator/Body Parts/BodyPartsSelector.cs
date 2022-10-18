// Code written by tutmo (youtube.com/tutmo)
// For help, check out the tutorial - https://youtu.be/PNWK5o9l54w

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BodyPartsSelector : MonoBehaviourPunCallbacks
{
    // ~~ 1. Handles Body Part Selection Updates

    // Full Character Body
    [SerializeField] private SO_CharacterBody characterBody;
    // Body Part Selections
    [SerializeField] private BodyPartSelection[] bodyPartSelections;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    Player player;


    private void Start()
    {
        // Get All Current Body Parts
        for (int i = 0; i < bodyPartSelections.Length; i++)
        {
            GetCurrentBodyParts(i);
        }
    }

    public void SetPlayerInfo(Player _player)
    {
        print(_player);
        player = _player;
        //UpdatePlayerItem(player);
    }

    public void NextBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex < bodyPartSelections[partIndex].bodyPartOptions.Length - 1)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex++;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = 0;
            }

            UpdateCurrentPart(partIndex);
        }
    }

    public void PreviousBody(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex > 0)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex--;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = bodyPartSelections[partIndex].bodyPartOptions.Length - 1;
            }

            UpdateCurrentPart(partIndex);
        }    
    }

    private bool ValidateIndexValue(int partIndex)
    {
        if (partIndex > bodyPartSelections.Length || partIndex < 0)
        {
            Debug.Log("Index value does not match any body parts!");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GetCurrentBodyParts(int partIndex)
    {
        // Get Current Body Part Name
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartName;
        // Get Current Body Part Animation ID
        bodyPartSelections[partIndex].bodyPartCurrentIndex = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartAnimationID;
    }

    private void UpdateCurrentPart(int partIndex)
    {
        switch (partIndex)
        {
            case 0:
                playerProperties["playerBody"] = bodyPartSelections[partIndex].bodyPartCurrentIndex;
                break;
            case 1:
                playerProperties["playerHair"] = bodyPartSelections[partIndex].bodyPartCurrentIndex;
                break;
            case 2:
                playerProperties["playerTorso"] = bodyPartSelections[partIndex].bodyPartCurrentIndex;
                break;
            case 3:
                playerProperties["playerPants"] = bodyPartSelections[partIndex].bodyPartCurrentIndex;
                break;
        }
        // Update Selection Name Text
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex].bodyPartName;
        // Update Character Body Part
        //characterBody.characterBodyParts[partIndex].bodyPart = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex];
        PhotonNetwork.SetPlayerCustomProperties(playerProperties); 
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        print(targetPlayer);
        if(player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }



    void UpdatePlayerItem(Player player)
    {
        if(player.CustomProperties.ContainsKey("playerBody") && player.CustomProperties.ContainsKey("playerHair") 
            && player.CustomProperties.ContainsKey("playerTorso") && player.CustomProperties.ContainsKey("playerPants"))
        {
            characterBody.characterBodyParts[0].bodyPart = bodyPartSelections[0].bodyPartOptions[(int)player.CustomProperties["playerBody"]];
            characterBody.characterBodyParts[1].bodyPart = bodyPartSelections[1].bodyPartOptions[(int)player.CustomProperties["playerHair"]];
            characterBody.characterBodyParts[2].bodyPart = bodyPartSelections[2].bodyPartOptions[(int)player.CustomProperties["playerTorso"]];
            characterBody.characterBodyParts[3].bodyPart = bodyPartSelections[3].bodyPartOptions[(int)player.CustomProperties["playerPants"]];
        }
        else
        {
            playerProperties["playerBody"] = 0;
            playerProperties["playerHair"] = 0;
            playerProperties["playerTorso"] = 0;
            playerProperties["playerPants"] = 0;
        }
    }
}

[System.Serializable]
public class BodyPartSelection
{
    public string bodyPartName;
    public SO_BodyPart[] bodyPartOptions;
    public Text bodyPartNameTextComponent;
    [HideInInspector] public int bodyPartCurrentIndex;
}
