using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObjectPosition : MonoBehaviour
{

    public string ObjectId;
    public GameObject ObjectManager;

    // Update is called once per frame
    void Update()
    {
        if(transform.hasChanged)
        {
            string x = transform.position.x.ToString();
            string y = transform.position.y.ToString();
            //Debug.Log(ObjectId + ' ' + x + ' ' + y);
            ObjectManager.GetComponent<DatabaseService>().UpdateCoordsDatabase(ObjectId, x, y);
            transform.hasChanged = false;
        }
    }
}
