using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPanel : VisibilitySwitchObject
{
    [Header("Properties")]
    public float maxInteractionDistanceSTATIC = 0.25f;
    public static float maxInteractionDistance = 0.25f;

    protected GameObject playerCamera;
    protected TeleportationPoint tpPoint;

    protected bool available;

    void Start()
    {
        // C'est stupidement [insérer ici un terme français vulgaire de trois lettres commençant par un c et désignant l'organe sexuel externe de la 
        // femelle humaine], ça va finir avec des bugs, si la variable maxInteractionDistanceSTATIC est différente entre deux objets dans l'inspecteur
        // la valeur prise sera au petit bonheur la chance, MAIS les variables statiques ne fonctionnent pas dans l'inspecteur, et je veux que tous les
        // panneaux aient la même valeur, donc je le fais quand même (en vrai il faudrait mettre un booléen useMainblablabla qui si il est coché fait 
        // passer la valeur de la variable (en version non statique) à une valeur renseignée dans un fichier de configuration existant une fois dans 
        // le projet (voir les singletons)
        maxInteractionDistance = maxInteractionDistanceSTATIC;

        playerCamera = (GameObject) GameObject.FindGameObjectsWithTag("MainCamera")[0];
        tpPoint = transform.parent.Find("Summon_circle").gameObject.GetComponent<TeleportationPoint>();

        if (tpPoint != null) if(!tpPoint.setActivationPanel(this)) tpPoint = null;

        if (tpPoint != null && !tpPoint.isActive()) available = true;
        else available = false;

        setVisible(available);
    }

    void Update()
    {
        // Orientate the panel to the player eyes (~3h to do that)
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.transform.position);
    }

    public bool isAvailable()
    {
        return available;
    }

    public TeleportationPoint getTpPoint()
    {
        return tpPoint;
    }

    public bool activateTp()
    {
        if (tpPoint == null || !available || tpPoint.isActive()) return false;

        tpPoint.activate();
        available = false;
        setVisible(available);

        return true;
    }

    public static float getMaxInteractionDistance()
    {
        return maxInteractionDistance;
    }
}
