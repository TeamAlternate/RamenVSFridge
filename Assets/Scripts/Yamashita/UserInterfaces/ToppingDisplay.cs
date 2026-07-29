using UnityEngine;

namespace UserInterfaces
{
    public class ToppingDisplay : MonoBehaviour
    {
        private static ToppingDisplay instance;
        [SerializeField] private ToppingDisplayElement elementPrefab;
        [SerializeField] private int maxInstanceCount;

        [SerializeField] private Transform alignmentPivot;
        [SerializeField] private Vector3 offsetPerTopping;

        private ToppingDisplayElement[] elements;

        private int currentToppingCount;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
                elements = new ToppingDisplayElement[maxInstanceCount];
            for (int i = 0; i < maxInstanceCount; i++)
            {
                var element = Instantiate(elementPrefab);
                element.transform.SetParent(alignmentPivot, false);
                element.SetVisible(false);
                elements[i] = element;
            }
        }

        public void OnAddTopping(GameObject toppingVisual)
        {
            elements[currentToppingCount].SetAppearence(toppingVisual);
            elements[currentToppingCount].transform.localPosition = alignmentPivot.localPosition + offsetPerTopping * currentToppingCount;
            elements[currentToppingCount].SetVisible(true);
            currentToppingCount++;
        }

        public static void AddTopping(GameObject toppingVisual)
        {
            instance.OnAddTopping(toppingVisual);
        }
    }

}