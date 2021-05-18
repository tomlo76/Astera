using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Properties")]
    public MainPlayerController player = null;

    protected GameObject playerCamera = null;


    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            foreach (Transform child in player.transform)
            {
                if (child.tag == "MainCamera") playerCamera = child.gameObject;
            }
        }

        playerCamera = (GameObject)GameObject.FindGameObjectsWithTag("MainCamera")[0];

        transform.SetParent(playerCamera.transform);
    }

    void LateUpdate()
    {
        //transform.position = playerCamera.transform.position + playerCamera.transform.forward * 0.2f + Vector3.up * 0.6f;
        //transform.rotation = new Quaternion(0.0f, playerCamera.transform.rotation.y, 0.0f, playerCamera.transform.rotation.w);
    }
}
