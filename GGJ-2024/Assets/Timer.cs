using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Timer : MonoBehaviour
{
	[Tooltip("The format string used to convert the time span to a string")]
	public string TimeFormat = "mm\\:ss";

	[Tooltip("Invoked with a time string every update")]
	public UnityEvent<string> OnTimeChanged = new();

	public TimeSpan TimeElapsed { get; private set; } = new();

	private void UpdateTime(float delta_time)
	{
		this.TimeElapsed += TimeSpan.FromSeconds(delta_time);
		this.OnTimeChanged.Invoke(this.TimeElapsed.ToString(this.TimeFormat));
	}

	public void ResetTime()
	{
		this.TimeElapsed = new();
		this.OnTimeChanged.Invoke(this.TimeElapsed.ToString(this.TimeFormat));
	}

	private void Update()
	{
		this.UpdateTime(Time.deltaTime);
	}
}
