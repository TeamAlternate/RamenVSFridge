using UnityEngine;

namespace UserInterfaces
{
    public class ToppingDisplay : MonoBehaviour
    {
        [SerializeField] private ToppingDisplayElement elementPrefab;
        [SerializeField] private int maxInstanceCount;

        [SerializeField] private Transform alignmentPivot;
        [SerializeField] private Vector3 offsetPerTopping;

        private ToppingDisplayElement[] elements;

        private int currentToppingCount;

        private void Awake()
        {
            elements = new ToppingDisplayElement[maxInstanceCount];
            for (int i = 0; i < maxInstanceCount; i++)
            {
                var element = Instantiate(elementPrefab);
                element.SetVisible(false);
                elements[i] = element;
            }
        }

        //public void AddTopping()
        //{
        //    elements[currentToppingCount].SetAppearence();
        //    elements[currentToppingCount].SetVisible(true);
        //    currentToppingCount++;
        //}
    }

}