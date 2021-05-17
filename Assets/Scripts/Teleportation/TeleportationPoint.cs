using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationPoint : MonoBehaviour
{
    [Header("Default properties")]
    public bool active = false;

    [Header("Materials")]
    public Material activeMaterial;
    public Material unactiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        setMaterial();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activate(bool a = true)
    {
        active = a;

        setMaterial();
    }

    protected void setMaterial()
    {
        if (active)
        {
            this.GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = unactiveMaterial;
        }
    }
}
