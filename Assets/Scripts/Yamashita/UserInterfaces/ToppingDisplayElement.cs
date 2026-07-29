using UnityEngine;

namespace UserInterfaces
{
    public class ToppingDisplayElement : MonoBehaviour
    {
        [SerializeField] private Transform appearencePivot;

        public void SetVisible(bool isVisible)
        {
            this.gameObject.SetActive(isVisible);
        }

        public void SetAppearence(GameObject appearenceInstance)
        {
            appearencePivot.SetParent(appearenceInstance.transform);
            appearenceInstance.transform.localPosition = Vector3.zero;
        }
    }
}
