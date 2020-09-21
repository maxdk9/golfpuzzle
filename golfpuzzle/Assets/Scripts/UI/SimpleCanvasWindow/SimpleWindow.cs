using DG.Tweening;
using Highscores;
using UnityEngine;

namespace UI
{
    public class SimpleWindow : MonoBehaviour
    {
        
        public float moveDuration = .5f;

        public void Close()
        {
            this.transform.DOLocalMove(new Vector3(2000, 0, 0), moveDuration);
        }


        public virtual void Show()
        {
            
            this.transform.DOLocalMove(new Vector3(0, 0, 0), moveDuration);
        }
    }
}