using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] protected int column;  //must be >= 2
    [SerializeField] protected int row;     //must be >= 2

    [SerializeField] protected GameObject card;
    [SerializeField] protected GameObject parentPanel;
    [SerializeField] protected RectTransform cardRect;

    [SerializeField] protected Sprite[] images;

    protected void Start()
    {
        Spawn(column, row);
    }
    protected void Spawn(int _column, int _row)
    {
        if (column < 2 || row < 2)
        {
            return;
        }

        float[] xPos = GetAxisPos(_column);
        float[] yPos = GetAxisPos(_row);

        foreach (var item in xPos)
        {
            for (int i = 0; i < yPos.Length; i++)
            {
                Vector3 pos = new(item, yPos[i], 0);
                GameObject newCard = Instantiate(card, parentPanel.transform);
                newCard.transform.localPosition = pos;
            }
        }
        SetRandomImage();
    }
    protected float[] GetAxisPos(int quantity)
    {
        float[] pos = new float[quantity];
        float cellSize = cardRect.rect.width;

        //case quantity is odd number
        if (quantity % 2 != 0)
        {
            Debug.Log("odd");
            for (int i = 0; i < quantity; i++)
            {
                pos[i] = (Mathf.FloorToInt(quantity / 2) - 1 * i) * cellSize * 2;
            }
        }
        //case quantity is even number
        else
        {
            Debug.Log("even");
            for (int i = 0; i < quantity; i++)
            {
                pos[i] = (quantity / 2 - 0.5f - 1 * i) * cellSize * 2;
            }
        }
        return pos;
    }
    protected void SetRandomImage()
    {
        if ((images.Length * 2) != (row * column))
        {
            Debug.LogError("Card's quantity is not match with the number of images");
            return;
        }

        List<Sprite> tempList = new();
        foreach (var image in images)
        {
            tempList.Add(image);
            tempList.Add(image);
        }

        Card[] cards = parentPanel.GetComponentsInChildren<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            int randIndex = Random.Range(0, tempList.Count);
            cards[i].gameImage = tempList[randIndex];
            tempList.RemoveAt(randIndex);
        }
    }
}
