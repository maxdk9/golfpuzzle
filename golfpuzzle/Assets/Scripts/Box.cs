using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isOnCross;

    public bool Move(Vector2 direction)
    {
        
        
        if (BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            TestForOnCross();
            return true;
        }
    }

    private void TestForOnCross()
    {
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");
        foreach (GameObject cross in crosses)
        {
            if (cross.transform.position.x == this.transform.position.x &&
                transform.position.y == cross.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color=Color.red;
                isOnCross = true;
                return;
            }
                
        }
    }

    private bool BoxBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos=new Vector2(position.x,position.y)+direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var VARIABLE in walls)
        {
            if (VARIABLE.transform.position.x == newPos.x && VARIABLE.transform.position.y == newPos.y)
            {
                return true;
            }
        }


        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var VARIABLE in boxes)
        {
            if (VARIABLE.transform.position.x == newPos.x && VARIABLE.transform.position.y == newPos.y)
            {
                        return true;
            }
            
        }
        
        return false;
    }
}
