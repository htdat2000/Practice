using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public static PanelController instance { get; private set; }

    [SerializeField] protected Sprite[] images;
    [SerializeField] protected GameObject cardPrefab;

    protected int countClick = 0;
    protected Card cardOne = null;
    protected Card cardTwo = null;

    protected bool isComparing = false;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    protected void Start()
    {
        SpawnCard(); 
    }
    protected void SpawnCard()
    {
        List<Sprite> tempList = new();
        foreach (var image in images)
        {
            tempList.Add(image);
            tempList.Add(image);
        }
        while(tempList.Count > 0)
        {
            int randIndex = Random.Range(0, tempList.Count);

            Card card = Instantiate(cardPrefab, this.gameObject.transform).GetComponent<Card>();
            card.gameImage = tempList[randIndex];
    
            tempList.RemoveAt(randIndex);
        }
    }
    public void OnPlayerClick(Card _card, bool isCardActived)
    {
        if (isComparing == true)
        {
            return;
        }
        if (isCardActived == true && cardTwo == null)
        {
            Reset();
            return;
        }
        switch (countClick)
        {
            case 0:
                countClick++;
                cardOne = _card;
                break;
            case 1:
                cardTwo = _card;
                isComparing = true;
                CompareCard();
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
    protected void CompareCard()
    {
        if (cardOne.gameImage == cardTwo.gameImage)
        {
            StartCoroutine(DeactiveCard());
        }
        else
        {
            StartCoroutine(TurnOffCard());
        }
        
    }
    IEnumerator DeactiveCard()
    {
        yield return new WaitForSeconds(0.2f);
        cardOne.gameObject.SetActive(false);
        cardTwo.gameObject.SetActive(false);
        Reset();
    }
    IEnumerator TurnOffCard()
    {
        yield return new WaitForSeconds(0.2f);
        if (cardOne != null)
        {
            cardOne.TriggerCard();
        }
        if (cardTwo != null)
        {    
            cardTwo.TriggerCard();
        }
        Reset();
    }
    protected void Reset()
    {
        cardOne = null;
        cardTwo = null;
        countClick = 0;
        isComparing = false;
    }
}
