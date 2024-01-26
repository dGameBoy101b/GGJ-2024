using UnityEngine;
using FMODUnity;
using System;

namespace WinterwireGames.FMODParamaters
{
	public class FMODParameter : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The emitter whose parameters this should be linked to.\nDefaults to the first emitter on this object")]
		private StudioEventEmitter _emitter;

		public StudioEventEmitter Emitter
		{
			get
			{
				if (this._emitter == null)
					this._emitter = this.GetComponent<StudioEventEmitter>();
				return this._emitter;
			}
			set => this._emitter = value;
		}

		public string ParameterName;

		public float Value
		{
			get
			{
				foreach (var param in this.Emitter.Params)
				{
					if (param.Name == this.ParameterName) 
						return param.Value;
				}
				throw new NullReferenceException("No fmod parameter found with name: "+this.ParameterName);
			}
			set
			{
				this.Emitter.SetParameter(this.ParameterName, value);
			}
		}
	}
}
