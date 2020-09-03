using DG.Tweening;
using UnityEngine;

namespace Tutorial
{
    public class ScaledChosenComponent : MonoBehaviour
    {
        private Sequence _sequence;

        public static void RemoveAll()
        {
            
            ScaledChosenComponent [] stcomponents = Resources.FindObjectsOfTypeAll<ScaledChosenComponent>();
            foreach (ScaledChosenComponent sc in stcomponents)
            {
                GameObject.Destroy(sc);
            }
            
        }
        
        

        private void OnDestroy()
        {
            _sequence.SetLoops(1).onComplete= () => { transform.DOScale(new Vector3(1, 1, 0), 0); };
            _sequence.Kill();
            transform.DOScale(new Vector3(1, 1, 0), 0);
        }
        private void Start()
        {

        }

        private void Awake()
        {
            float animationTime = 1f;
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DOScale(new Vector3(1.4f, 1.4f, 0), animationTime * .5f));
            _sequence.Append(transform.DOScale(new Vector3(1.0f, 1.0f, 0), animationTime * .5f));
            _sequence.SetLoops(-1);
        }
    }
}