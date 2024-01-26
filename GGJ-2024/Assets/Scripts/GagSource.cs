using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GagSource : MonoBehaviour
{
	[Tooltip("The type of gag this is")]
	public string GagType;

	[Tooltip("How many points this is worth the first time it's performed")]
	[Min(0)]
	public float BasePoints = 0;

	public readonly struct Gag
	{
		public readonly GagSource Source;
		public readonly float BasePoints;
		public readonly string Type;

		public Gag(GagSource source, float base_points, string type)
		{
			this.Source = source;
			this.BasePoints = base_points;
			this.Type = type;
		}
	}

	public void SendGag()
	{
		Gag gag = new(this, this.BasePoints, this.GagType);
		LaughOMeter.Instance.AddGag(gag);
	}
}
