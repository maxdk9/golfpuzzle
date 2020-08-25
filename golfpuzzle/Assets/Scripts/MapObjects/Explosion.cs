using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityScript.Steps;

namespace MapObjects
{
    public class Explosion: MonoBehaviour
    {
        
        public static UnityEvent ExplosionEvent=new UnityEvent();
        private float duration;
        private void Start()
        {
            
            duration = this.GetComponent<ParticleSystem>().duration;
            this.StartCoroutine(ExplodeRoutine());
        }

        private IEnumerator ExplodeRoutine()
        {
            ExplosionEvent.Invoke();
            yield return new WaitForSeconds(duration);
                GameObject.Destroy(gameObject);
        }
    }
}