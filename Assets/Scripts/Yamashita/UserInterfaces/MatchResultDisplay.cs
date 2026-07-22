using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaces
{
    public class MatchResultDisplay : MonoBehaviour
    {
        [SerializeField] private Image Image;
        [SerializeField] private Sprite[] resultTypeSprite;

        public void UpdateDisplay(MatchResult newMatchResult)
        {
            Image.sprite = resultTypeSprite[(int)newMatchResult.resultType];
        }
    }
}