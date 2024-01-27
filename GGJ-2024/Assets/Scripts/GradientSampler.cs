using UnityEngine;
using UnityEngine.Events;

public class GradientSampler : MonoBehaviour
{
	[Tooltip("The gradient to sample from")]
	public Gradient Gradient = new();

	[Tooltip("The value at the minimum end of the gradient")]
	public float Minimum = 0;

	[Tooltip("The value at the maximum end of the gradient")]
	public float Maximum = 1;

	[Tooltip("Invoked with the evaluated colour every time this samples its gradient")]
	public UnityEvent<Color> OnSample = new();

	public void Sample(float value)
	{
		value = Mathf.Clamp(value, this.Minimum, this.Maximum);
		float t = (value - this.Minimum) / (this.Maximum - this.Minimum);
		Color colour = this.Gradient.Evaluate(t);
		this.OnSample.Invoke(colour);
	}
}
