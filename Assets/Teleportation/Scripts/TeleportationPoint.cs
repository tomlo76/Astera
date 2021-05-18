using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationPoint : MonoBehaviour
{
    [Header("Properties")]
    public bool rotateOnActive = false;
    public float speed = 25;

    [Header("Default values")]
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
        // Model from blender, so right hand convention is used, Vector3.forward = (0, 0, 1) is the up vector
        // Doc : https://docs.unity3d.com/ScriptReference/Vector3.html
        if (rotateOnActive && active) transform.Rotate(Vector3.forward * speed * Time.deltaTime, Space.Self);
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
