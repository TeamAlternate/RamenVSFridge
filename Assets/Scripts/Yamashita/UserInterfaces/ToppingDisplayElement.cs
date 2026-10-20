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
            appearenceInstance.transform.SetParent(appearencePivot);
            appearenceInstance.transform.localPosition = Vector3.zero;
            appearenceInstance.transform.localScale = Vector3.one;
            foreach(MonoBehaviour userScript in appearenceInstance.GetComponentsInChildren<MonoBehaviour>())
            {
                Destroy(userScript);
            }
            if(appearenceInstance.TryGetComponent<Rigidbody>( out var r))
            {
                Destroy(r);
            }
        }
    }
}
