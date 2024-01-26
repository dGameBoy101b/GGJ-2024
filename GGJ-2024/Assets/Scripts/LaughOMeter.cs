using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LaughOMeter : MonoBehaviour
{
	private Slider _slider = null;
	public Slider Slider
	{
		get
		{
			if (this._slider == null)
				this._slider = this.GetComponent<Slider>();
			return this._slider;
		}
	}

	public static LaughOMeter Instance { get; private set; } = null;

	private void SetSingletonInstance()
	{
		if (LaughOMeter.Instance == null)
			LaughOMeter.Instance = this;
		else if (LaughOMeter.Instance != this)
			Destroy(this);
	}

	private void ResetSingletonInstance()
	{
		if (LaughOMeter.Instance == this)
			LaughOMeter.Instance = null;
	}

	public readonly struct GagEvent
	{
		public readonly GagSource.Gag Gag;
		public readonly float Time;
		public readonly float Points;

		public GagEvent(GagSource.Gag gag, float points)
		{
			this.Gag = gag;
			this.Points = points;
			this.Time = UnityEngine.Time.time;
		}
	}

	private readonly List<GagEvent> _gagHistory = new();

	public IReadOnlyCollection<GagEvent> GagHistory => this._gagHistory;

	private float ScoreGag(GagSource.Gag gag) //stub
	{
		return gag.BasePoints;
	}

	public void AddGag(GagSource.Gag gag)
	{
		float points = this.ScoreGag(gag);
		this._gagHistory.Add(new(gag, points));
	}

	private void Awake()
	{
		this.SetSingletonInstance();
	}

	private void OnDestroy()
	{
		this.ResetSingletonInstance();
	}
}
