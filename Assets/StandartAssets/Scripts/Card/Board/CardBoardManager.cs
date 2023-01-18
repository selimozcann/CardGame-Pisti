using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CardBoardManager : Singleton<CardBoardManager>
{
    [SerializeField] private List<CardMovementController> _cardMovementControllers;
    [SerializeField] private List<CardStatController> cardStatControllers;
    public List<CardStatController> CardStatControllers { get { return cardStatControllers;} }
    
    [SerializeField] private CardStatController oldBoardCard;

    private CardStatController OldBoardCard
    {
        get { return oldBoardCard;}
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
        SetToBoardCard(cardStatController);
    }
    private CardStatController SetToBoardCard(CardStatController cardStatController) {
        OldBoardCard = CurrentBoardCard;
        CurrentBoardCard = cardStatController;
        return cardStatController;
    } 
    public async void CheckToCardToThread()
    {
        if (OldBoardCard &&CurrentBoardCard)
        {
            var canCheckCardControls = oldBoardCard.CardValue == CurrentBoardCard.CardValue;
            var canCheckCardControlsSecond = _cardMovementControllers.Count > 1  && CurrentBoardCard.CardValue == "J";
            if (canCheckCardControls)
            {
                await Task.Delay(1200);
                CheckToCard();
            }
            else if (canCheckCardControlsSecond)
            {
                await Task.Delay(1200);
                CheckToCard();
            }
            await Task.Delay(2000);
            GameManager.I.ChangeToGameState(GameState.Playing);
        }
    }
    private void CheckToCard()
    {
        for (int i = _cardMovementControllers.Count - 1; i >= 0; i--)
        {
            _cardMovementControllers[i]._cardMoveState = CardMoveState.CardTakeMoveButtom;
        }
        OnCardDelete();
        CheckToCardToThread();
    }
    private void OnCardDelete()
    {
        CurrentBoardCard = OldBoardCard = null;
        cardStatControllers.Clear();
        _cardMovementControllers.Clear();
        GameManager.I.ChangeToGameState(GameState.Playing);
    }
}
