using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.UserInterfaces.Derived
{
    public class StandardSceneTransition : SceneTransition
    {
        [SerializeField] private Animator Anime;
        [SerializeField] private string EnterAnimationName;
        [SerializeField] private float EnterDuration;
        [SerializeField] private string ExitAnimationName;
        [SerializeField] private float ExitDuration;

        protected override IEnumerator OnEnterTransition()
        {
            DontDestroyOnLoad(this.gameObject);
            Anime.Play(EnterAnimationName);
            yield return new WaitForSeconds(EnterDuration);
            yield break;
        }

        protected override IEnumerator OnExitTransition()
        {
            Anime.Play(ExitAnimationName);
            yield return new WaitForSeconds(ExitDuration);
            Destroy(this.gameObject);
            yield break;
        }
    }
}
