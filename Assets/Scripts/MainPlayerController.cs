using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
	[Header("Player")]
	public GameObject player = null;

	void Start() { /* ... */ }

	void Update()
	{
	}

	public GameObject getPlayer() { return player; }
}
