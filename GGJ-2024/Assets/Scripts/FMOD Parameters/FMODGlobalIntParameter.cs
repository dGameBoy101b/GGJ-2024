using UnityEngine;
using FMODUnity;

namespace WinterwireGames.FMODParameters
{
	public class FMODGlobalIntParameter : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The name of the parameter to set")]
		public string ParameterName;

		public int Value
		{
			get
			{
				float value;
				RuntimeManager.StudioSystem.getParameterByName(this.ParameterName, out value);
				return (int) value;
			}
			set
			{
				RuntimeManager.StudioSystem.setParameterByName(this.ParameterName, (float) value);
			}
		}
	}
}
