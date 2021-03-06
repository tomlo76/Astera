using UnityEngine;

// Doc : https://developer.oculus.com/documentation/unity/unity-ovrinput/

public class HandController : MonoBehaviour // MonoBehaviour est la classe de base de tous les objets C# d'Unity
{

	// Store the hand type to know which button should be pressed
	public enum HandType : int { LeftHand, RightHand };		// Définition de l'énumération main droite, main gauche
	[Header("Hand Properties")]								// Ajoute la catégorie « Hand Properties » à l'inspecteur d'Unity
	public HandType handType;								// Paramètres modifiable dans l'inspecteur, le nom est généré en transformant le nom de la variable (camelCase -> Camel Case), il doit être en public


	// Store the player controller to forward it to the object
	[Header("Player Controller")]
	public MainPlayerController playerController;
	public Map map;



	// Store all gameobjects containing an Anchor
	// N.B. This list is static as it is the same list for all hands controller
	// thus there is no need to duplicate it for each instance
	static protected ObjectAnchor[] anchors_in_the_scene;

	static protected ActivationPanel[] tpActivationPanels;

	static protected MapTeleportMarker[] tpMarkers;


	// Store the previous state of the buttons
	protected bool button_A_previous_state;
	protected bool button_B_previous_state;
	protected bool button_thumbstick_previous_state;
	protected bool button_start_previous_state;
	protected bool hand_previous_state; // 0 -> opened ; 1 -> closed


	// Called at the beginning
	void Start()
	{
		// Prevent multiple fetch
		if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();

		if (tpActivationPanels == null) tpActivationPanels = GameObject.FindObjectsOfType<ActivationPanel>();

		if (map == null) map = GameObject.FindObjectOfType<Map>();
		if (playerController == null) playerController = (MainPlayerController)GameObject.FindObjectOfType<MainPlayerController>();

		// Initialization of the states
		button_A_previous_state = is_A_pressed();
		button_B_previous_state = is_B_pressed();
		button_thumbstick_previous_state = is_thumbstick_pressed();
		button_start_previous_state = is_start_pressed();
		hand_previous_state = is_hand_closed();
	}

	// Automatically called at each frame
	void Update()
	{
		OVRInput.Update();

		if (tpMarkers == null) tpMarkers = GameObject.FindObjectsOfType<MapTeleportMarker>(true);
		if (tpMarkers == null || tpMarkers.Length == 0) tpMarkers = null;

		handle_controller_behavior();
	}
	
	
	void LateUpdate()
	{
		// Update buttons state for the next frame
		button_A_previous_state = is_A_pressed();
		button_B_previous_state = is_B_pressed();
		button_thumbstick_previous_state = is_thumbstick_pressed();
		button_start_previous_state = is_start_pressed();
		hand_previous_state = is_hand_closed();
	}


	// Return true if button A is pressed
	protected bool is_A_pressed()
	{
		return ((handType == HandType.RightHand) && OVRInput.Get(OVRInput.Button.One)) || ((handType == HandType.LeftHand) && OVRInput.Get(OVRInput.Button.Three));
	}

	// Return true if button B is pressed
	protected bool is_B_pressed()
	{
		return ((handType == HandType.RightHand) && OVRInput.Get(OVRInput.Button.Two)) || ((handType == HandType.LeftHand) && OVRInput.Get(OVRInput.Button.Four));
	}

	protected bool is_thumbstick_pressed()
    {
		return ((handType == HandType.RightHand) && OVRInput.Get(OVRInput.Button.SecondaryThumbstick)) || ((handType == HandType.LeftHand) && OVRInput.Get(OVRInput.Button.PrimaryThumbstick));
	}

	protected bool is_start_pressed()
    {
		return OVRInput.Get(OVRInput.RawButton.Start);
    }

	// Return true if the current state is different from the previous one
	protected bool has_A_changed()
    {
		return (button_A_previous_state != is_A_pressed());
	}

	// Return true if the current state is different from the previous one
	protected bool has_B_changed()
	{
		return (button_B_previous_state != is_B_pressed());
	}

	protected bool has_thumbstick_changed()
	{
		return (button_thumbstick_previous_state != is_thumbstick_pressed());
	}

	protected bool has_start_changed()
    {
		return (button_start_previous_state != is_start_pressed());
    }



	// This method checks that the hand is closed depending on the hand side
	protected bool is_hand_closed()
	{
		// Case of a left hand
		if (handType == HandType.LeftHand) return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5;
		//OVRInput.Get(OVRInput.Button.Three)                           // Check that the A button is pressed
		//&& OVRInput.Get(OVRInput.Button.Four)                         // Check that the B button is pressed
		//&& OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5     // Check that the middle finger is pressing
		//&& OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5;   // Check that the index finger is pressing


		// Case of a right hand
		else return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5;
		//OVRInput.Get(OVRInput.Button.One)                             // Check that the A button is pressed
		//	&& OVRInput.Get(OVRInput.Button.Two)                          // Check that the B button is pressed
		//	&& OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5   // Check that the middle finger is pressing
		//	&& OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5; // Check that the index finger is pressing
	}

	protected bool has_hand_changed()
    {
		return (hand_previous_state != is_hand_closed());
    }




	protected ObjectAnchor object_grasped = null;

	protected void handle_controller_behavior()
    {
		if (is_B_pressed() && has_B_changed())
		{

		}

		if (is_A_pressed() && has_A_changed())
		{
			// Do not do multiple actions, there is priority
			if (panelInteraction()) return;
			if (mapInteraction()) return;
		}

		if (is_thumbstick_pressed() && has_thumbstick_changed())
		{
			if (map != null)
            {
				map.switchVisibility();
			}
        }


		/*if (has_hand_changed())
		{
			if (is_hand_closed())
			{
				// Determine which object available is the closest from the left hand
				int best_object_id = -1;
				float best_object_distance = float.MaxValue;
				float oject_distance;

				// Iterate over objects to determine if we can interact with it
				for (int i = 0; i < anchors_in_the_scene.Length; i++)
				{

					// Skip object not available
					if (!anchors_in_the_scene[i].is_available()) continue;

					// Compute the distance to the object
					oject_distance = Vector3.Distance(this.transform.position, anchors_in_the_scene[i].transform.position);

					// Keep in memory the closest object
					// N.B. We can extend this selection using priorities
					if (oject_distance < best_object_distance && oject_distance <= anchors_in_the_scene[i].get_grasping_radius())
					{
						best_object_id = i;
						best_object_distance = oject_distance;
					}
				}

				// If the best object is in range grab it
				if (best_object_id != -1)
				{

					// Store in memory the object grasped
					object_grasped = anchors_in_the_scene[best_object_id];

					// Grab this object
					object_grasped.attach_to(this);
				}
			}
			else if (object_grasped != null)
			{
				// Release the object
				object_grasped.detach_from(this);

				object_grasped.set_visibility(true);

				object_grasped = null;
			}
		}

		if (object_grasped != null)
		{
			if (has_B_changed())
			{
				object_grasped.switch_visibility();
			}
		}*/
	}

	protected bool panelInteraction()
    {
		int nearest_id = getNearestPanelId(ActivationPanel.getMaxInteractionDistance());

		if (nearest_id < 0) return false;

		tpActivationPanels[nearest_id].activateTp();

		return true;
	}

	protected int getNearestPanelId(float distance_threshold)
    {
		int nearestId = -1;

		float distance;
		float nearestDistance = float.MaxValue;

		for (int i = 0 ; i < tpActivationPanels.Length ; i++)
		{
			if (!tpActivationPanels[i].isAvailable()) continue;

			distance = Vector3.Distance(this.transform.position, tpActivationPanels[i].transform.position);

			if (distance < distance_threshold && distance < nearestDistance)
			{
				nearestId = i;
				nearestDistance = distance;
			}
		}

		return nearestId;
	}

	protected bool mapInteraction()
    {
		if (!map.isAvailable() || tpMarkers == null) return false;



		int nearestId = -1;

		float distance;
		float nearestDistance = float.MaxValue;

		for (int i = 0 ; i < tpMarkers.Length ; i++)
        {
			if (!tpMarkers[i].getTpPoint().isActive()) continue; // Continue if the current tp point is not activated

			distance = Vector3.Distance(transform.position, tpMarkers[i].transform.position);

			if (distance < 0.05 && distance < nearestDistance)
            {
				nearestId = i;
				nearestDistance = distance;
            }
        }

		if (nearestId < 0) return false;

		return tpMarkers[nearestId].getTpPoint().teleport(playerController);
    }
}
