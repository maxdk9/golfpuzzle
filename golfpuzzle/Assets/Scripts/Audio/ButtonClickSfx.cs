
using Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


    [RequireComponent(typeof(Button))]
    public class ButtonClickSfx: MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(AudioEvents.commonButtonClickEvent.Invoke);
        }
    }
