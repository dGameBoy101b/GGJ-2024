using UnityEngine;
using UnityEngine.Events;

public class EnableEvents : MonoBehaviour
{
	[Tooltip("Invoked when this is enabled")]
	public UnityEvent OnActivate = new();

	[Tooltip("Invoked when this is disabled")]
	public UnityEvent OnDeactivate = new();

	private void OnEnable()
	{
		this.OnActivate.Invoke();
	}

	private void OnDisable()
	{
		this.OnDeactivate.Invoke();
	}
}
