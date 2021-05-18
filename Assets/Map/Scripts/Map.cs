using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : VisibilitySwitchObject
{
    protected GameObject playerCamera = null;


    void Start()
    {
        playerCamera = (GameObject)GameObject.FindGameObjectsWithTag("MainCamera")[0];

        transform.SetParent(playerCamera.transform);

        setVisible(false); // Map is not visible by default, inherited class variable from VisibilitySwitchObject
    }

    public bool isAvailable()
    {
        return isVisible();
    }
}
