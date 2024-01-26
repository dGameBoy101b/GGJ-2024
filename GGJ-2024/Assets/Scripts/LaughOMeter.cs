using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class LaughOMeter : MonoBehaviour
{
	#region Points
	[Header("Points")]
	[Tooltip("The maximum number of points on the laugh o meter")]
	public float PointsMinimum = -100f;

	[Tooltip("The maximum number of points on the laugh o meter")]
	public float PointsMaximum = 100f;

	[Tooltip("The number of points initially on the laugh o meter")]
	public float InitialPoints = 0f;

	public float CurrentPoints { get; private set; }

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

	private void UpdateSlider()
	{
		this.Slider.value = this.CurrentPoints;
	}

	private void InitialisePoints()
	{
		this.CurrentPoints = this.InitialPoints;
	}

	private void InitialiseSlider()
	{
		this.Slider.maxValue = this.PointsMaximum;
		this.Slider.minValue = this.PointsMinimum;
		this.Slider.value = this.CurrentPoints;
	}
	#endregion

	#region Singleton
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
	#endregion

	#region Gag History
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
	#endregion

	#region Recency Penalties
	[Header("Receny Penalties")]
	[Tooltip("The penalty accumulated for each instance of a gag performance")]
	[Min(0)]
	public float RecencyPenaltyIncrement = 10f;

	[Tooltip("The rate at which recency penalties decay in points per second")]
	[Min(0)]
	public float RecencyPenaltyDecayRate = 1f;

	[Tooltip("The maximum recency penalty applied to gags")]
	[Min(0)]
	public float RecencyPenaltyMaximum = 100f;

	private readonly Dictionary<string, float> _recencyPenalties = new();

	public IReadOnlyDictionary<string, float> RecencyPenalties => this._recencyPenalties;

	private float GetRecencyPenalty(GagSource.Gag gag)
	{
		return this.RecencyPenalties.ContainsKey(gag.Type) ? this.RecencyPenalties[gag.Type] : 0;
	}

	private void UpdateRecencyPenalties(float delta_time)
	{
		foreach (var key in this.RecencyPenalties.Keys)
			this._recencyPenalties[key] = Mathf.Max(0, this._recencyPenalties[key] - this.RecencyPenaltyDecayRate * delta_time);
	}

	private void IncrementRecencyPenalty(GagSource.Gag gag)
	{
		if (gag.IsFailed)
			return;
		if (!this.RecencyPenalties.ContainsKey(gag.Type))
			this._recencyPenalties.Add(gag.Type, 0);
		this._recencyPenalties[gag.Type] = Mathf.Min(this.RecencyPenaltyMaximum, this._recencyPenalties[gag.Type] + this.RecencyPenaltyIncrement);
	}
	#endregion

	#region Events
	[Header("Events")]
	[Tooltip("Invoked when a gag is performed with the number of points scored from it.\nFailed gags will have negative points")]
	public UnityEvent<float> OnGagAdded = new();

	[Tooltip("Invoked when the maximum point threshold is crossed")]
	public UnityEvent OnHitMaximum = new();

	[Tooltip("Invoked when the minimum point threshold is crossed")]
	public UnityEvent OnHitMinimum = new();
	#endregion

	private float CalculateGagScore(GagSource.Gag gag)
	{
		float recency_penalty = this.GetRecencyPenalty(gag);
		return gag.IsFailed 
			? -gag.BasePoints
			: Mathf.Max(0, gag.BasePoints - recency_penalty);
	}

	public void AddGag(GagSource.Gag gag)
	{
		float points = this.CalculateGagScore(gag);
		float total_points = this.CurrentPoints + points;
		this.IncrementRecencyPenalty(gag);
		bool hit_max = total_points >= this.PointsMaximum;
		bool hit_min = total_points <= this.PointsMinimum;
		this.CurrentPoints = Mathf.Clamp(total_points, this.PointsMinimum, this.PointsMaximum);
		this._gagHistory.Add(new(gag, points));
		this.OnGagAdded.Invoke(points);
		if (hit_max)
			this.OnHitMaximum.Invoke();
		if (hit_min)
			this.OnHitMinimum.Invoke();
	}

	private void Update()
	{
		this.UpdateRecencyPenalties(Time.deltaTime);
		this.UpdateSlider();
	}

	private void Awake()
	{
		this.SetSingletonInstance();
		this.InitialisePoints();
		this.InitialiseSlider();
	}

	private void OnDestroy()
	{
		this.ResetSingletonInstance();
	}
}
