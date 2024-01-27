using UnityEngine;
using UnityEngine.Events;

public class Slipable : MonoBehaviour
{
	[Tooltip("Invoked when this slips on something.\nParameter is the slip force")]
	public UnityEvent<Vector3> OnSlip = new();

	internal void Slip(Vector3 force)
	{
		Debug.DrawRay(this.transform.position, force, Color.magenta);
		Debug.Log("Slip! " + force);
		this.OnSlip.Invoke(force);
	}
}
