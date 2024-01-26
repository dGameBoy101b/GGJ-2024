using UnityEngine;
using FMODUnity;

namespace WinterwireGames.FMODParameters
{
	public class FMODGlobalParameter : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The name of the parameter to set")]
		public string ParameterName;

		public float Value
		{
			get
			{
				float value;
				RuntimeManager.StudioSystem.getParameterByName(this.ParameterName, out value);
				return value;
			}
			set
			{
				RuntimeManager.StudioSystem.setParameterByName(this.ParameterName, value);
			}
		}
	}
}
