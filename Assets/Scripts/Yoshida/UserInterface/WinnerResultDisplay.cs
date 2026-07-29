using UnityEngine;

public enum CharaType
{
    None,
    Ramen,
    Freezer
};

public class WinnerResultDisplay : MonoBehaviour
{
    [SerializeField] private GameObject ramenGameObject;
    [SerializeField] private GameObject freezerGameObject;

    CharaType winner = CharaType.None;

    /// <summary>
    /// デバッグ用
    /// </summary>
    private void Awake()
    {
        Initialize(CharaType.Freezer);
    }

    public void Initialize(CharaType win)
    {
        winner = win;

        PlayWinnerAnimation();
    }

    private void PlayWinnerAnimation()
    {
        switch (winner)
        {
            case CharaType.None:
                break;
            case CharaType.Ramen:
                Instantiate(ramenGameObject, this.transform);
                break;
            case CharaType.Freezer:
                Instantiate(freezerGameObject, this.transform);
                break;
        }
    }
}
