using UnityEngine;
using UnityEngine.Events;
using WinterwireGames.PhysicsHelpers;

public class CollisionGagSource : GagSource
{
	[Tooltip("Colliding with objects on these layers will perform a successful gag")]
	public LayerMask SuccessMask;

	[Tooltip("Colliding with objects on these layer will perform a failed gag")]
	public LayerMask FailMask;

	[Tooltip("The minimum number of seconds between gag performances")]
	[Min(0f)]
	public float Cooldown = 0f;

	public float CooldownEndTime { get; private set; } = 0f;

	[Tooltip("Invoked when this performs a successful gag")]
	public UnityEvent OnSuccess = new();

	[Tooltip("Invoked when this performs a failed gag")]
	public UnityEvent OnFail = new();

	private void CheckCollision(Collision collision)
	{
		if (this.CooldownEndTime > Time.time)
			return;
		bool is_success = this.SuccessMask.HasLayer(collision.gameObject.layer);
		bool is_fail = this.FailMask.HasLayer(collision.gameObject.layer);
		Debug.Log("success:"+is_success+", fail:"+is_fail, this);
		if (is_success || is_fail)
			this.CooldownEndTime = Time.time + this.Cooldown;
		if (is_success)
		{
			this.SendGag(false);
			this.OnSuccess.Invoke();
		}
		if (is_fail)
		{
			this.SendGag(true);
			this.OnFail.Invoke();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Gag collision detected", this);
		this.CheckCollision(collision);
	}
}
