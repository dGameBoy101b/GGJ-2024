using UnityEngine;
using UnityEngine.Events;

public class Throwable : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The rigidbody this applies forces to")]
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

	[Tooltip("Invoked when this is thrown.\nFirst parameter is the throw force")]
	public UnityEvent<Vector3> OnThrow = new();

	internal void Throw(Vector3 force)
	{
		this.Rigidbody.AddForce(force, ForceMode.Impulse);
		this.OnThrow.Invoke(force);
	}
}
