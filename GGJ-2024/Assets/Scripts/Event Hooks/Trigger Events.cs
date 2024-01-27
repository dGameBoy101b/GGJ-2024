using UnityEngine;
using UnityEngine.Events;
using WinterwireGames.PhysicsHelpers;

public class TriggerEvents : MonoBehaviour
{
	[Tooltip("Only trigger events involving colliders on these layers will trigger events")]
	public LayerMask Mask;

	[Tooltip("Invoked when a trigger is entered")]
	public UnityEvent<Collider> OnEnter = new();

	[Tooltip("Invoked every frame while a trigger has an intersection")]
	public UnityEvent<Collider> OnStay = new();

	[Tooltip("Invoked when a trigger is exited")]
	public UnityEvent<Collider> OnExit = new();

	private void OnTriggerEnter(Collider other)
	{
		if (this.Mask.HasLayer(other.gameObject.layer))
			this.OnEnter.Invoke(other);
	}

	private void OnTriggerStay(Collider other)
	{
		if (this.Mask.HasLayer(other.gameObject.layer))
			this.OnStay.Invoke(other);
	}

	private void OnTriggerExit(Collider other)
	{
		if (this.Mask.HasLayer(other.gameObject.layer))
			this.OnExit.Invoke(other);
	}
}
