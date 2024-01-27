using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
	private static readonly Stack<float> _history = new();
	public static IReadOnlyCollection<float> History => TimeScaler._history;

	public void ScaleTime(float scale)
	{
		if (scale == Time.timeScale) 
			return;
		TimeScaler._history.Push(Time.timeScale);
		Time.timeScale = scale;
	}

	public void Revert()
	{
		if (TimeScaler.History.Count < 1)
			return;
		Time.timeScale = TimeScaler._history.Pop();
	}
}
