using UnityEngine;
using UnityEngine.Events;

public class Thrower : MonoBehaviour
{
	[Tooltip("The amount of force used to throw objects")]
	public float ThrowMagnitude = 1f;

	public Vector3 ThrowForce => this.transform.forward * this.ThrowMagnitude;

	[Tooltip("Invoked when this throws something")]
	public UnityEvent<Throwable> OnThrow = new();

	public void Throw(Throwable throwable)
	{
		Vector3 force = this.ThrowForce;
		throwable.Throw(force);
		this.OnThrow.Invoke(throwable);
	}

	private void DrawThrowForceGizmo()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawRay(this.transform.position, this.ThrowForce);
	}

	private void OnDrawGizmosSelected()
	{
		this.DrawThrowForceGizmo();
	}
}
