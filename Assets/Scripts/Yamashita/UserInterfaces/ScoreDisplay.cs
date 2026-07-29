using UnityEngine;

namespace UserInterfaces
{

    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Text text;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private string format;

        private void Update()
        {
            UpdateDisplay(scoreManager.GetScore());
        }

        public void UpdateDisplay(int newScore)
        {
            text.text = newScore.ToString(format);
        }
    }

}