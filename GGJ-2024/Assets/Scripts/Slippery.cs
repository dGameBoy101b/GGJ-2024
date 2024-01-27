using UnityEngine;
using UnityEngine.Events;
using WinterwireGames.PhysicsHelpers;

public class Slippery : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The rigidbody to apply self forces to")]
	private Rigidbody _rigidbody;

	public Rigidbody Rigidbody
	{
		get
		{
			if (this._rigidbody == null)
				this._rigidbody = this.GetComponentInParent<Rigidbody>();
			return this._rigidbody;
		}
	}

	[Tooltip("The layers to slip against")]
	public LayerMask Mask;

	[Tooltip("The amount of force to add to this body")]
	[Min(0)]
	public float SelfForce = 1f;

	[Tooltip("The amount of force to add to the other body")]
	[Min(0)]
	public float OtherForce = 1f;

	[Tooltip("Invoked when something slips on this")]
	public UnityEvent OnSlip = new();

	private Vector3 CalculateSlipForce(Collision collision, float magnitude)
	{
		return collision.relativeVelocity.normalized * magnitude;
	}

	private void Slip(Collision collision)
	{
		if (!this.enabled)
			return;
		if (!this.Mask.HasLayer(collision.gameObject.layer))
			return;
		Slipable slipable = collision.gameObject.GetComponent<Slipable>();
		if (slipable == null)
			return;
		Vector3 self_force = this.CalculateSlipForce(collision, this.SelfForce);
		Vector3 other_force = this.CalculateSlipForce(collision, this.OtherForce);
		Debug.DrawRay(this.transform.position, self_force, Color.white);
		Debug.Log("Slip! " + self_force, this);
		this.Rigidbody.AddForce(self_force, ForceMode.Impulse);
		slipable.Slip(other_force);
		this.OnSlip.Invoke();
	}

	private void OnCollisionEnter(Collision collision)
	{
		this.Slip(collision);
	}

	private void OnEnable() {}
}
