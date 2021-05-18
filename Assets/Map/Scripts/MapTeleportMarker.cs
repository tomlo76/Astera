using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTeleportMarker : MonoBehaviour
{
    [Header("Materials")]
    public Material activeMaterial;
    public Material unactiveMaterial;


    protected TeleportationPoint tpPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tpPoint != null && tpPoint.isActive())
        {
            this.GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = unactiveMaterial;
        }
    }

    public void setTpPoint(TeleportationPoint p)
    {
        tpPoint = p;
        
        if (tpPoint != null)
        {
            transform.localPosition = new Vector3(-tpPoint.transform.position.x / 1000, -tpPoint.transform.position.z / 1000, transform.localPosition.z);
        }
    }
}
