using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] spawnObject;
    private string path;

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
        //path = Application.persistentDataPath + "/CoordinatesData.json";
        //LoadJson();
    }

    public void LoadJson()
    {
        path = Application.persistentDataPath + "/CoordinatesData.json";
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        Debug.Log(json);


        RootObject objList = JsonUtility.FromJson<RootObject>(json.ToString());

        Debug.Log(objList);


    }
}
