using UnityEngine;

public class CardStatController : CardBaseStatController
{
    public void CheckToCardDataValue()
    {
        if (cardState == CardState.AI)
        {
            CardManager.I.carDataValues.Add(RemoveIndexZero()); 
        }
    }
    public void OnAIMove()
    {
        var currentAIMove = CardManager.I.carDataValues.Contains(CardBoardManager.I.CurrentBoardCard.CardValue);
    }
}
