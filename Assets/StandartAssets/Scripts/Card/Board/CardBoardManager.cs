using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardManager : Singleton<CardBoardManager>
{
    [SerializeField] private List<CardMovementController> _cardMovementControllers;
    [SerializeField] private List<CardStatController> cardStatControllers;
    public List<CardStatController> CardStatControllers { get { return cardStatControllers;} }
    
    [SerializeField] private CardStatController oldBoardCard;

    private CardStatController OldBoardCard
    {
        set
        {
            if (cardStatControllers.Count >= 2)
            {
                currentBoardCard = cardStatControllers[cardStatControllers.Count - 2];
            }
            oldBoardCard = value;
        }
    }

    [SerializeField] private CardStatController currentBoardCard;
    public CardStatController CurrentBoardCard
    {
        get
        {
            return currentBoardCard;
        }
        set
        {
            if (cardStatControllers.Count != 0)
            {
                currentBoardCard = cardStatControllers[cardStatControllers.Count - 1];
            }
            currentBoardCard = value;
        }
    }
    private void Start()
    {
        cardStatControllers = new List<CardStatController>();
    }
    public void OnCardAdd(CardStatController cardStatController,CardMovementController cardMovementController)
    {
        cardStatControllers.Add(cardStatController);
        _cardMovementControllers.Add(cardMovementController);
        CurrentBoardCard = cardStatController;
    }
    private void SetToBoardCard() => OldBoardCard = CurrentBoardCard;
    public IEnumerator CheckToCard()
    {
        SetToBoardCard();
        yield return new WaitForSeconds(1.2f);
        if (oldBoardCard.CardValue == CurrentBoardCard.CardValue)
        {
            for (int i = _cardMovementControllers.Count - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(.025f);
                _cardMovementControllers[i]._cardMoveState =
                    CardMoveState.CardTakeMoveButtom;
            }
        }
        OnCardDelete();
    }
    public void OnCardDelete()
    {
        cardStatControllers.Clear();
        _cardMovementControllers.Clear();
    }
}
