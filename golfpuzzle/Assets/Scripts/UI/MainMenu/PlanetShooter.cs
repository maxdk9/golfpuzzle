using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlanetShooter : MonoBehaviour
{

    public GameObject PlanetPrefab;
    private bool right;
    public float duration = 1f;
    public float timeout = 3f;

    public List<Sprite> planetSprites;
    
    private Vector3 leftPosition=new Vector3(5,0,0);
    private Vector3 rightPosition=new Vector3(-5,0,0);
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootPlanerRoutine());
    }

    private IEnumerator ShootPlanerRoutine()
    {
        yield return new WaitForSeconds(1f);

        int planetSpriteIndex = Random.Range(0, planetSprites.Count);
        Sprite plSprite = planetSprites[planetSpriteIndex];
        
        Vector3 planetPosition=new Vector3();
        Vector3 planetDestination=new Vector3();
        if (right)
        {
            planetPosition = new Vector3(rightPosition.x,rightPosition.y,rightPosition.z);
            planetDestination = new Vector3(leftPosition.x,leftPosition.y,leftPosition.z);
        }
        else
        {
            planetDestination = new Vector3(rightPosition.x,rightPosition.y,rightPosition.z);
            planetPosition = new Vector3(leftPosition.x,leftPosition.y,leftPosition.z);
        }

        float y = Random.Range(4, -4);
        planetPosition.y = y;
        float y1 = Random.Range(4, -4);

        planetDestination.y = y1;
        
        GameObject planetGO = Instantiate(PlanetPrefab, planetPosition, Quaternion.identity);
        planetGO.GetComponent<SpriteRenderer>().sprite = plSprite;
        planetGO.transform.DOMove(planetDestination, duration).onComplete = () =>
        {
            Destroy(planetGO);
        };

        yield return new WaitForSeconds(duration+timeout);
        right = !right;
        StartCoroutine(ShootPlanerRoutine());



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
