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
        // AI Check To Code.
        var currentAIMove = CardManager.I.carDataValues.Contains(CardBoardManager.I.CurrentBoardCard.CardValue);
        Debug.Log("CurrentAIMove =>>> " + currentAIMove);
    }
}
