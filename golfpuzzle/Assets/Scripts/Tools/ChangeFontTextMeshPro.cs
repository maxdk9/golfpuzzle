using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;




    [ExecuteInEditMode]
    public class ChangeFontTextMeshPro : MonoBehaviour
    {
        public TMP_FontAsset fontAsset;
        
        
        #if UNITY_EDITOR
        /// <summary>
        /// change fonts
        /// </summary>
        public void ChangeFonts()
        {
            StopAllCoroutines();
            StartCoroutine(ChangeFontsRoutine());
        }

        private IEnumerator ChangeFontsRoutine()
        {
            yield return new WaitForSeconds(.1f);
            TextMeshProUGUI[] labelArray = FindObjectsOfType<TextMeshProUGUI>();
            foreach (var VARIABLE in labelArray)
            {
                VARIABLE.font = fontAsset;
            }

            yield return null;
        }
        #endif
    }
