using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : VisibilitySwitchObject
{
    public GameObject teleportMarkerPrefab;


    protected GameObject playerCamera = null;

    protected MapTeleportMarker[] mapTpPoints;


    void Start()
    {
        playerCamera = (GameObject)GameObject.FindGameObjectsWithTag("MainCamera")[0];

        transform.SetParent(playerCamera.transform);



        TeleportationPoint[] tpPoints = GameObject.FindObjectsOfType<TeleportationPoint>();

        foreach (TeleportationPoint point in tpPoints)
        {
            Object obj = Instantiate(teleportMarkerPrefab, transform.GetChild(0));
        }

        mapTpPoints = GameObject.FindObjectsOfType<MapTeleportMarker>();

        int i = 0;
        foreach (TeleportationPoint point in tpPoints)
        {
            mapTpPoints[i].setTpPoint(point);

            i++;
        }




        setVisible(false); // Map is not visible by default, inherited class variable from VisibilitySwitchObject
    }

    public bool isAvailable()
    {
        return isVisible();
    }
}
