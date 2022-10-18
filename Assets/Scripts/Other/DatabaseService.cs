using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEditor;
using System;

public class DatabaseService : MonoBehaviour
{
    private string path;

    public GameObject[] myObjects;

    private RootObject jsonObject;

    [Serializable]
    public class ObjectList
    {
        public int ID;
        public float X;
        public float Y;
    }

    [Serializable]
    public class RootObject
    {
        public ObjectList[] objects;
    }

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/CoordinatesData.json";
        StartCoroutine(GetData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://experiment.cravefx.com/akmal/Craving4Xmas/getATable-xmas.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            jsonObject = JsonUtility.FromJson<RootObject>("{\"objects\":" + www.downloadHandler.text + "}");

            PlaceObjects();

        }
    }

    public void UpdateCoordsDatabase(string itemId, string xCoord, string yCoord)
    {
        StartCoroutine(PostData(itemId, xCoord, yCoord));
    }

    IEnumerator PostData(string itemId, string xCoord, string yCoord)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemId);
        form.AddField("xCoord", xCoord);
        form.AddField("yCoord", yCoord);

        using (UnityWebRequest www = UnityWebRequest.Post("https://experiment.cravefx.com/akmal/Craving4Xmas/updateTable-xmas.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public void PlaceObjects()
    {
        for (int i = 0; i < myObjects.Length; i++)
        {

            myObjects[i].gameObject.transform.position = new Vector2(jsonObject.objects[i].X, jsonObject.objects[i].Y);

        }
    }
}
