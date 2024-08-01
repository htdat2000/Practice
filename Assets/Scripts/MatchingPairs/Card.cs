using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] protected Sprite defaultImage;
    public Sprite gameImage; 

    protected bool cardState = false;

    public void TriggerCard()
    {
        Debug.Log("Click");
        
        if(cardState == false)
        {
            image.sprite = gameImage;
            PanelController.instance.OnPlayerClick(this, false);
        }
        else
        {
            image.sprite = defaultImage;
            PanelController.instance.OnPlayerClick(this, true);
        }
        cardState = !cardState;
    }   
}
