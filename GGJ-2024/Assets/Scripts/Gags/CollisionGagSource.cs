using UnityEngine;
using UnityEngine.Events;
using WinterwireGames.PhysicsHelpers;

public class CollisionGagSource : GagSource
{
	[Tooltip("Colliding with objects on these layers will perform a successful gag")]
	public LayerMask SuccessMask;

	[Tooltip("Colliding with objects on these layer will perform a failed gag")]
	public LayerMask FailMask;

	[Tooltip("Invoked when this performs a successful gag")]
	public UnityEvent OnSuccess = new();

	[Tooltip("Invoked when this performs a failed gag")]
	public UnityEvent OnFail = new();

	private void OnCollisionEnter(Collision collision)
	{
		if (this.SuccessMask.HasLayer(collision.gameObject.layer))
		{
			this.SendGag(false);
			this.OnSuccess.Invoke();
		}
		if (this.FailMask.HasLayer(collision.gameObject.layer))
		{
			this.SendGag(true);
			this.OnFail.Invoke();
		}
	}
}
