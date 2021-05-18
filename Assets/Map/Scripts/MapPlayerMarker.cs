using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerMarker : MonoBehaviour
{
    public MainPlayerController player = null;
    public Map map = null;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = (MainPlayerController) GameObject.FindObjectOfType<MainPlayerController>();
        if (map == null) map = (Map) GameObject.FindObjectOfType<Map>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.localPosition = new Vector3(- player.transform.position.x/1000, - player.transform.position.z/1000, transform.localPosition.z);
        }
    }
}
