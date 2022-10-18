using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameCollector : MonoBehaviour
{

    public static NameCollector scene1;
    public TMP_InputField inputField;

    public string player_name;

    // Start is called before the first frame update
    public void Awake()
    {
        if (scene1 == null)
        {
            scene1 = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerName()
    {
        player_name = inputField.text;


        if (!string.IsNullOrWhiteSpace(player_name))
        {
            player_name = inputField.text;
        }
        else
        {
            player_name = "elf";
        }
    }
}
