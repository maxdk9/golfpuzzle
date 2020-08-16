using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLevelPanel : MonoBehaviour
{


    [SerializeField]
    public Sprite grayStar;
    [SerializeField]
    public Sprite goldStar;
    
    public Transform WindowImage;
    public GameObject StarCounter;
    public List<Image> StarImages; 
    public float moveDuration = .3f;
    public Button NextLevelButton;

    private Vector3 startPosition=new Vector3(0,50);
    // Start is called before the first frame update
    void Start()
    {
        
        this.transform.position=new Vector3(0,-1000,0);
        
        NextLevelButton.onClick.AddListener(Hide);
        NextLevelButton.onClick.AddListener(GameManager.Instance.NextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(int winLevel)
    {
        this.gameObject.SetActive(true);
        
        
        SetStarImages(winLevel);    
        
        StartCoroutine(ShowCoroutine());
    }

    private void SetStarImages(int winLevel)
    {
        for (int i = 0; i < StarImages.Count; i++)
        {
            if (i < winLevel)
            {
                StarImages[i].sprite = goldStar;
            }
            else
            {
                StarImages[i].sprite = grayStar;
            }
        }
    }

    private IEnumerator ShowCoroutine()
    {
        
        yield return new WaitForSeconds(.1f);
        
        transform.DOLocalMove(startPosition,moveDuration);
        //WindowImage.DOLocalMove(new Vector3(0,0),moveDuration);
        yield return new WaitForSeconds(moveDuration);
        
    }

    public void Hide()
    {
        this.StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        transform.DOLocalMove(new Vector3(0,-1000),moveDuration);
        yield return new WaitForSeconds(moveDuration);
        this.gameObject.SetActive(false);
    }
}
