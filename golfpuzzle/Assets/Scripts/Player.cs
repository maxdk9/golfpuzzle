using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool Move(Vector2 direction)
    {

        if (Mathf.Abs(direction.x) < .5f)
        {
            direction.x = 0;
        }

        if (Mathf.Abs(direction.y) < .5f)
        {
            direction.y = 0;
        }
        
        
        direction.Normalize();
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }



    bool Blocked(Vector2 position, Vector2 direction)
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
                Box box = VARIABLE.gameObject.GetComponent<Box>();
                if (box )
                {
                    if (box.Move(direction))
                    {
                        return false;    
                    }
                    else
                    {
                        return true;
                    }
                }
                
            }
            
        }
        
        return false;
    }
}
