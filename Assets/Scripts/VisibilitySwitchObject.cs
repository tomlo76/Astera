using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilitySwitchObject : MonoBehaviour
{
    protected void show()
    {
        transform.gameObject.SetActive(true);
    }

    protected void hide()
    {
        transform.gameObject.SetActive(false);
    }

    protected void setVisible(bool visible)
    {
        transform.gameObject.SetActive(visible);
    }
}
