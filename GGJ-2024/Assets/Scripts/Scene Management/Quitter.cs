using UnityEngine;

namespace WinterwireGames.SceneManagement
{
	public class Quitter : MonoBehaviour
	{
		public void Quit()
		{
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}
	}
}
