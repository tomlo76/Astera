using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    protected GameObject playerCamera = null;


    void Start()
    {
        playerCamera = (GameObject)GameObject.FindGameObjectsWithTag("MainCamera")[0];

        transform.SetParent(playerCamera.transform);
    }
}
