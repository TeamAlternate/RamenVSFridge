using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaces
{
    public class MatchResultDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject[] resultTypePrefabs;

        public void UpdateDisplay(MatchResult newMatchResult)
        {
            GameObject go = Instantiate(resultTypePrefabs[(int)newMatchResult.resultType]);
            go.transform.SetParent(this.transform, false);
        }
    }
}