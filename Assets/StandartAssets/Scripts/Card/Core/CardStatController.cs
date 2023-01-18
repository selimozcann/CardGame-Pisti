using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class CardStatController : CardBaseStatController
{
    public void CheckToCardDataValue()
    {
        if (cardState == CardState.AI)
        {
            CardManager.I.carDataValues.Add(RemoveIndexZero());
            CardManager.I.aiCards.Add(this);
            AddToAICardValue();
        }
    }
    void AddToAICardValue()
    {
        for (int i = 0; i < CardManager.I.aiCards.Count; i++)
        {
            CardManager.I.aiCards[i].CardValue = CardManager.I.carDataValues[i];
        }
    }
    public async void OnAIMove()
    {
        await Task.Delay(2000);
        CardManager.I.aiCards.Where(x => x.cardValue.ToString().Equals(cardValue))
        .ToList()
        .ForEach(x =>
        {
              x.cardMovementController._cardMoveState = CardMoveState.CardBoardMove;
            ChangeToCardStat(x);
        });
    }
}
