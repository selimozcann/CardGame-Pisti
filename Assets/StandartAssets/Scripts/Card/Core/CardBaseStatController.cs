using System;
using UnityEngine;
using UnityEngine.UI;

public enum CardState { PlayerCard,AI }
public abstract class CardBaseStatController : MonoBehaviour
{
    public CardState cardState;
    
    [SerializeField] private Sprite closedCardSprite;
    [SerializeField] private Image currentImage;
    
    [SerializeField] private string cardValue;
    public string CardValue { get { return  cardValue; } }

    private void Start()
    {
        SetCardValue();
    }
    private void SetCardValue()
    {
        gameObject.name = RemoveIndexZero();
        cardValue = gameObject.name;
    }
    private string RemoveIndexZero()
    {
        gameObject.name = cardValue.Contains("J") ? gameObject.name.Remove(0, 1) 
            : gameObject.name.Remove(0, 2);
        
        return gameObject.name;
    } 
    public void OnClosedCardImage()
    {
        currentImage.sprite =  closedCardSprite;
    }
    public void OnChangeCardStat(CardState canCardState)
    {
        cardState = canCardState;
    }
}
