using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class UnityMethods
    {
        /// <summary>
        /// Reverts the <see cref="Object.DontDestroyOnLoad"/> method.
        /// </summary>
        /// <param name="go">The GameObject to allow to be destroyed again on load</param>
        /// <param name="normalScene">The scene that it originally belonged to.</param>
        /// <remarks>Use <see cref="SceneManager.GetActiveScene"/> to get the currently active scene.</remarks>
        public static void DoDestroyOnLoad(GameObject go, Scene normalScene)
        {
            
            SceneManager.MoveGameObjectToScene(go, normalScene);
        }
    }
}
