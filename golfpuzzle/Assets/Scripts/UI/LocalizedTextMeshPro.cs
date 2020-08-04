using System;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextMeshPro : MonoBehaviour
    {
        public string LocalizationKey;
        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }
        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }
        private void Localize()
        {
            GetComponent<TextMeshProUGUI>().text = LocalizationManager.Localize(LocalizationKey);
        }

        public void SetLocalizationKey(String k)
        {
            LocalizationKey = k;
            Localize();
        }
        
    }
}