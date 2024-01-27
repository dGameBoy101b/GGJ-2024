using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrolRoute : MonoBehaviour
{
	[Tooltip("Whether this should loop through its waypoints or reverse")]
	public bool ShouldLoop = false;

	[Serializable]
	public struct Waypoint
	{
		[Tooltip("The transform to go to")]
		public Transform Transform;

		[Tooltip("How long to wait here before moving onto the next waypoint")]
		public float WaitSeconds;
	}

	[Tooltip("The waypoints this will patrol through")]
	public List<Waypoint> Waypoints = new();

	[Tooltip("Invoked when its time to navigate to the next waypoint")]
	public UnityEvent<Vector3> OnDestinationChanged = new();

	private int WaypointIndex = -1;
	public Waypoint CurrentWaypoint => this.Waypoints[this.WaypointIndex];

	public bool IsReversed { get; private set; } = false;

	public void Arrived()
	{
		this.Invoke("AdvanceWaypoint", this.CurrentWaypoint.WaitSeconds);
	}

	private int NextIndex()
	{
		if (this.Waypoints.Count == 0)
			return -1;
		if (this.Waypoints.Count == 1)
			return 0;
		int index = this.WaypointIndex;
		if (this.IsReversed)
			index--;
		else
			index++;
		if (index >= 0 && index < this.Waypoints.Count)
			return index;
		if (this.ShouldLoop)
			if (index < 0)
				return this.Waypoints.Count - 1;
			else
				return 0;
		this.IsReversed = !this.IsReversed;
		if (index < 0)
			return 1;
		else
			return this.Waypoints.Count - 2;
	}

	private int FindClosestWaypoint()
	{
		if (this.Waypoints.Count < 1)
			return -1;
		int closest_index = 0;
		float closest_sqr_dist = (this.transform.position - this.Waypoints[0].Transform.position).sqrMagnitude;
		for (int index = 1; index < this.Waypoints.Count; ++index)
		{
			float sqr_dist = (this.transform.position - this.Waypoints[index].Transform.position).sqrMagnitude;
			if (sqr_dist < closest_sqr_dist)
			{
				closest_index = index;
				closest_sqr_dist = sqr_dist;
			}
		}
		return closest_index;
	}

	public void SetDestinationIndex(int index)
	{
		if (index < 0)
		{
			Debug.LogError("Cannot set destination index to a negative number", this);
			return;
		}
		if (index >= this.Waypoints.Count)
		{
			Debug.LogError("Cannot set destination index to number equal to or greater than number of waypoints", this);
			return;
		}
		this.WaypointIndex = index;
		this.OnDestinationChanged.Invoke(this.CurrentWaypoint.Transform.position);
	}

	private void AdvanceWaypoint()
	{
		this.SetDestinationIndex(this.NextIndex());
	}

	private void OnEnable()
	{
		this.SetDestinationIndex(this.FindClosestWaypoint());
	}

	private void OnDisable()
	{
		this.OnDestinationChanged.Invoke(this.transform.position);
	}

	private void DrawPathGizmo()
	{
		Gizmos.color = Color.white;
		for (int i = 1; i < Waypoints.Count; ++i)
			Gizmos.DrawLine(this.Waypoints[i - 1].Transform.position, this.Waypoints[i].Transform.position);
		if (this.ShouldLoop && this.Waypoints.Count > 1)
			Gizmos.DrawLine(this.Waypoints[0].Transform.position, this.Waypoints[this.Waypoints.Count - 1].Transform.position);
	}

	private void DrawWaypointGizmos()
	{
		Gizmos.color = Color.blue;
		foreach (Waypoint waypoint in this.Waypoints)
			Gizmos.DrawSphere(waypoint.Transform.position, 1);
	}

	private void OnDrawGizmosSelected()
	{
		this.DrawPathGizmo();
		this.DrawWaypointGizmos();
	}
}
