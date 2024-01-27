using UnityEngine;
using UnityEngine.Events;

namespace WinterwireGames.SceneManagement
{
	public class Pauser : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Invoked when game is paused")]
		private UnityEvent _onPause = new UnityEvent();

		public UnityEvent OnPause
		{
			get => this._onPause;
		}
		
		[SerializeField]
		[Tooltip("Invoked when game is resumed")]
		private UnityEvent _onResume = new UnityEvent();

		public UnityEvent OnResume
		{
			get => this._onResume;
		}
		
		public bool IsPaused { get; private set; } = false;

		public float PreviousTimeScale { get; private set; } = 1f;
		
		public void Toggle()
		{
			if (this.IsPaused)
			{
				this.Resume();
			}
			else
			{
				this.Pause();
			}
		}

		public void Pause()
		{
			if (IsPaused) 
				return;
			
			this.PreviousTimeScale = Time.timeScale;
			Time.timeScale = 0f;
			this.IsPaused = true;
			this.OnPause.Invoke();
		}

		public void Resume()
		{
			if (!IsPaused) 
				return;

			Time.timeScale = this.PreviousTimeScale;
			this.IsPaused = false;
			this.OnResume.Invoke();
		}

		private void OnDestroy()
		{
			this.Resume();
		}
	}
}
