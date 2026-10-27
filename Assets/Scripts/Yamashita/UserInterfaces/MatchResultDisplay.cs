using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UserInterfaces
{
    public class MatchResultDisplay : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material[] resultTypeMaterials;

        [SerializeField] private UnityEvent onWinnerRamen;

        public void UpdateDisplay(MatchResult newMatchResult)
        {
            meshRenderer.material = resultTypeMaterials[(int)newMatchResult.resultType];

            if( newMatchResult.resultType == MatchResult.ResultTypes.RamenWin )
            {
                onWinnerRamen.Invoke();
            }
        }
    }
}