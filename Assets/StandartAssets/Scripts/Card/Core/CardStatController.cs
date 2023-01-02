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
}
