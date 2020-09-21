using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    
    
    
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ShowChronometer : MonoBehaviour
    {

        
        private int resultValue=0;
        private TextMeshProUGUI lbl;


        private void Awake()
        {
            lbl = this.gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void StartChronometer(int res)
        {
            lbl.text = "0"; 
            this.gameObject.SetActive(true);
            this.StopAllCoroutines();
            resultValue = res;
           this.StartCoroutine(ChronometerCoroutine());

        }

        private IEnumerator ChronometerCoroutine()
        {
            
            yield return  new WaitForSeconds(.001f);
            for (int i = 0; i <= resultValue;i+=10)
            {
                yield return  new WaitForSeconds(.001f);
                
                lbl.SetText(i.ToString());        
            }
        }


        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}