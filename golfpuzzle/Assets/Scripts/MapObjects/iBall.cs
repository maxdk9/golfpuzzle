using UnityEngine;
using UnityEngine.Events;

namespace MapObjects
{
    public class iBall:MonoBehaviour
    {
        public bool moved;

        public bool isSunked;
        public UnityEvent ballStopped=new UnityEvent();

        public virtual void Move(Vector2 direction)
        {
            
        }

        public virtual void  DestroySunk()
        {
            
        }
    }
}