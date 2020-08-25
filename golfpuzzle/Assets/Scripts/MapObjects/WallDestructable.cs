using System;
using System.Collections;
using System.Collections.Generic;
using MapObjects;
using UnityEngine;

public class WallDestructable : iWall
{
    


    private void Start()
    {
    
    }

    public void Explode()
    {
        StartCoroutine(ExplodeRoutine());
    }

    private IEnumerator ExplodeRoutine()
    {
        GameObject explosion = Instantiate(GameManager.Instance.mPrefabDatabase.BlueExplosionPrefab,
            this.transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
        yield return null;

    }
}
