using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageWindowCommon : MonoBehaviour, IPointerDownHandler
{

    public static MessageWindowCommon CreateWindow(Transform parentTransform,Vector3 position)
    {

        MessageWindowCommon.RemoveAll();

        GameObject prefab=Resources.Load("Prefabs/ui/MessageWindowCommon") as GameObject;
        GameObject MessageWindowGO = Instantiate(prefab,parentTransform);
        
        MessageWindowGO.transform.position = position;
        return MessageWindowGO.GetComponent<MessageWindowCommon>();
    }
    
    
    public TextMeshProUGUI textLabel;
    private float duration = 3f;
    

    public void Show(string s)
    {
        
        
        textLabel.text = s;
       // this.StartCoroutine(RemoveMessageRoutine());
    }

    private static void RemoveAll()
    {
        MessageWindowCommon[] array = FindObjectsOfType<MessageWindowCommon>();
        foreach (var VARIABLE in array)
        {
            GameObject.DestroyImmediate(VARIABLE.gameObject);
        }
    }

    private IEnumerator RemoveMessageRoutine()
    {
        
        yield return new WaitForSeconds(duration);
        GameObject.Destroy(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Destroy(this.gameObject);
    }
}
