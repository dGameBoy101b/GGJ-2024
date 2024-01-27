using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterwireGames.SceneManagement
{
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The path to the scene that will be loaded")]
		public string ScenePath;

		[SerializeField]
		[Tooltip("The load mode to use")]
		public LoadSceneMode Mode;

		[SerializeField]
		[Tooltip("Whether the completion of the scene load should be blocked until it is released")]
		public bool ShouldBlockActivation = false;

		public static readonly Queue<AsyncOperation> LoadingOperations = new();

		/** The current scene loading operation */
		public static AsyncOperation LoadingOperation => LoadingOperations.Peek();

		public static bool IsReady 
		{ 
			get => SceneLoader.LoadingOperation != null
				&& (SceneLoader.LoadingOperation.allowSceneActivation
					? SceneLoader.LoadingOperation.isDone
					: SceneLoader.LoadingOperation.progress >= .9f);
		}

		public void LoadScene()
		{
			this.LoadScene(this.ScenePath, this.Mode, !this.ShouldBlockActivation);
		}

		public void LoadScene(string scene_path, LoadSceneMode mode, bool allow_activation)
		{
			var operation = SceneManager.LoadSceneAsync(scene_path, mode);
			operation.allowSceneActivation = allow_activation;
			operation.completed += delegate (AsyncOperation op) { DynamicGI.UpdateEnvironment(); };
			SceneLoader.LoadingOperations.Enqueue(operation);
		}
	}
}
