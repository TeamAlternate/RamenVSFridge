using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util.UserInterfaces
{
    public class SceneTransition : MonoBehaviour
    {
        private static SceneTransition Current;
        private static string NextSceneName;

        public Coroutine EnterTransition(string NextSceneName)
        {
            Current = this;
            SceneTransition.NextSceneName = NextSceneName;
            return StartCoroutine(InternalRoutine());
            IEnumerator InternalRoutine()
            {
                AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(SceneTransition.NextSceneName);
                LoadOperation.allowSceneActivation = false;
                yield return OnEnterTransition();
                LoadOperation.allowSceneActivation = true;
                yield return new WaitUntil(() => LoadOperation.isDone);
            }
        }

        public static Coroutine ExitTransition()
        {
            if(Current == null)
            {
                return null;
            }
            return Current.StartCoroutine(Current.OnExitTransition());
        }

        protected virtual IEnumerator OnEnterTransition()
        {
            return null;
        }

        protected virtual IEnumerator OnExitTransition()
        {
            return null;
        }
    }
}