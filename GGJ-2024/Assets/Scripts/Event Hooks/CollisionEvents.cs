using UnityEngine;
using UnityEngine.Events;
using WinterwireGames.PhysicsHelpers;

public class CollisionEvents : MonoBehaviour
{
	[Tooltip("Only trigger events involving colliders on these layers will trigger events")]
	public LayerMask Mask;

	[Tooltip("Invoked when a trigger is entered")]
	public UnityEvent<Collision> OnEnter = new();

	[Tooltip("Invoked every frame while a trigger has an intersection")]
	public UnityEvent<Collision> OnStay = new();

	[Tooltip("Invoked when a trigger is exited")]
	public UnityEvent<Collision> OnExit = new();

	private void OnCollisionEnter(Collision collision)
	{
		if (this.Mask.HasLayer(collision.gameObject.layer))
			this.OnEnter.Invoke(collision);
	}

	private void OnCollisionStay(Collision collision)
	{
		if (this.Mask.HasLayer(collision.gameObject.layer))
			this.OnStay.Invoke(collision);
	}

	private void OnCollisionExit(Collision collision)
	{
		if (this.Mask.HasLayer(collision.gameObject.layer))
			this.OnExit.Invoke(collision);
	}
}
