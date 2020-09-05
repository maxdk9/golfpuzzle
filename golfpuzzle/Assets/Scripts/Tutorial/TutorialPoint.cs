using System;
using System.Collections;
using Assets.SimpleLocalization;
using DG.Tweening;
using Preferences;
using UnityEngine;

namespace Tutorial
{
    public class TutorialPoint : MonoBehaviour
    {
        private TutorialPointData data;

        private float duration = .2f;
        


        public void SetData(TutorialPointData d)
        {
            data = d;
        }

        public void Activate()
        {
            
            TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
            tutorialManager.ActivateEventByName(data.TutorialEventName,data.MessageKey);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.tag.CompareTo("Ball") == 0)
            {
                if (!GamePreferences.ShowTutorial())
                {
                    return;
                }
                this.StartCoroutine(ActivateTutorialPointRoutine());
            }
        }

        private IEnumerator ActivateTutorialPointRoutine()
        {
            
            
            yield return new WaitForSeconds(duration);
            Activate();
            GameObject.Destroy(this.gameObject);
        }
    }
}