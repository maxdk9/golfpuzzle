using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FollowTrail : MonoBehaviour
{
    
    private TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

         // Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         // transform.position = pos;

    }

    public void Move(Vector2 move)
    {
        StartCoroutine(MoveRoutine(move));
    }

    private IEnumerator MoveRoutine(Vector2 move)
    {


        float duration = .5f;
        
        
        Vector3 destination = transform.position + (new Vector3(move.x,move.y,0) * 2);
        this.transform.DOMove(destination, duration);
        yield return new WaitForSeconds(duration);
        
    }
}
