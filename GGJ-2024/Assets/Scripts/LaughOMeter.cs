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

	private void Awake()
	{
		this.SetSingletonInstance();
	}

	private void OnDestroy()
	{
		this.ResetSingletonInstance();
	}
}
