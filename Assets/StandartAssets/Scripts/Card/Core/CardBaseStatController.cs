using UnityEngine;
using UnityEngine.UI;

public enum CardState { PlayerCard,AI ,BoardCardState}
public abstract class CardBaseStatController : MonoBehaviour
{
    public CardState cardState;
    
    [SerializeField] private Sprite closedCardSprite;
    [SerializeField] private Image currentImage;
    
    public string cardValue;
    public string CardValue { get { return  cardValue; } }

    private void Start()
    {
        if (cardState != CardState.AI)
        {
            SetCardValue();
        }
    }
    void SetCardValue()
    {
        gameObject.name = RemoveIndexZero(); 
        cardValue = gameObject.name;
    }
    protected string RemoveIndexZero()
    {
        if (gameObject.name.Length >= 1)
        {
            gameObject.name = cardValue.Contains("J") ? gameObject.name.Remove(0, 1) 
                : gameObject.name.Remove(0, 2);
        
            return gameObject.name;
        }

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
