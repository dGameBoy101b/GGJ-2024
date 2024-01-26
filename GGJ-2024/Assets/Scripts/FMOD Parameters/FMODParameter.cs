using UnityEngine;
using FMODUnity;

namespace WinterwireGames.FMODParamaters
{
	public class FMODParameter : MonoBehaviour
	{
		public StudioEventEmitter Emitter;

		public string ParameterName;

		public void SetValue(float value)
		{
			this.Emitter.SetParameter(this.ParameterName, value);
		}

		private void Awake()
		{
			this.Emitter = this.GetComponent<StudioEventEmitter>();
		}
	}
}
