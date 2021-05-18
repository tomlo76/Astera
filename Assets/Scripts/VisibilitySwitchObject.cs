using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilitySwitchObject : MonoBehaviour
{
    /* This class allow an object to be visible or invisible, an invisible object will not 
     * be rendered, but also, all children and scripts attached to it will be deactivated 
     * (that means that the update function will not be called).
     */

    protected bool visible = true;

    protected void show()
    {
        visible = true;

        transform.gameObject.SetActive(true);
    }

    protected void hide()
    {
        visible = false;

        transform.gameObject.SetActive(false);
    }

    protected void setVisible(bool v)
    {
        visible = v;

        transform.gameObject.SetActive(visible);
    }

    public void switchVisibility()
    {
        visible = !visible;

        transform.gameObject.SetActive(visible);
    }
}
