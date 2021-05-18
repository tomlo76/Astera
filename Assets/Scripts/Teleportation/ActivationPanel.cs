using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPanel : MonoBehaviour
{
    protected GameObject playerCamera;

    void Start()
    {
        playerCamera = (GameObject) GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    void LateUpdate()
    {
        // Orientate the panel to the player eyes (~3h to do that)
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.transform.position);
    }
}
