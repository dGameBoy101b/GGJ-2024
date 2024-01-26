using UnityEngine;
using FMODUnity;
using System;

namespace WinterwireGames.FMODParameters
{
	public class FMODGlobalStringParameter : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The name of the parameter to set")]
		public string ParameterName;

		public string[] AcceptedValues;

		public string Value
		{
			get
			{
				float value;
				RuntimeManager.StudioSystem.getParameterByName(this.ParameterName, out value);
				return AcceptedValues[(int) value];
			}
			set
			{
				int index = Array.IndexOf(AcceptedValues, value);
				RuntimeManager.StudioSystem.setParameterByName(this.ParameterName, index < 0 ? 0 : index);
			}
		}
	}
}
