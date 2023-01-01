using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum CardDealState { None,Dealing }
public class CardManager : Singleton<CardManager>
{
    public CardDealState cardDealState;
    [SerializeField] private CardMovementController image;
    [SerializeField] private List<Sprite> cardImage;
    [SerializeField] private Transform cardPosition;
    [SerializeField] private Transform cardParent;
    
    [SerializeField] private List<CardMovementController> currentCardMovementControllers;
    public List<CardMovementController> CurrentCardMovementControllers { get => currentCardMovementControllers; }
    
    public Action OnDealCard;
    private int cardAmonut = 12;
    private void Start()
    { 
        SetCardImage();
        OnChangeCardState(CardDealState.Dealing);
        StartCoroutine(DealingToCard());
    }
    private void SetCardImage()
    {
        currentCardMovementControllers = new List<CardMovementController>();
        for (int i = 0; i < cardImage.Count; i++)
        {
            CardMovementController initCardMovement =  Instantiate(image,cardPosition.position, image.transform.rotation);
            currentCardMovementControllers.Add(initCardMovement);
            initCardMovement.transform.SetParent(cardParent);
            initCardMovement.GetComponent<Image>().sprite = cardImage[i];
            initCardMovement.name = cardImage[i].name;
            initCardMovement.gameObject.SetActive(false);
        }
    }
    private IEnumerator DealingToCard()
    {
        for (int i = 0; i < cardAmonut; i++)
        {
            yield return new WaitForSeconds(.2f);
            SetCardDealToMoving(i);
        }
        OnChangeCardState(CardDealState.None);
        StopCoroutine(DealingToCard());
    }

    private void SetCardDealToMoving(int i)
    {
        CardMovementController cardMovementController = currentCardMovementControllers[0]; 
        CardStatController cardPlayerController = cardMovementController.GetComponent<CardStatController>();
        if (i == 0 || i == 1 || i == 2  || i == 3)
        {
            CardBoardManager.I.CurrentBoardCard = cardPlayerController;
            CardBoardManager.I.OnCardAdd(cardPlayerController,cardMovementController);
            if (i != 3)
            {
                cardPlayerController.OnClosedCardImage();
            }
        }
        if (i == 8 || i == 9 || i == 10 || i == 11)
        {
            cardPlayerController.OnChangeCardStat(CardState.AI);
        }
        cardMovementController.SetToCardIndex(i);
        cardMovementController.OnCanMove(CardMoveState.CardDealMove);
        currentCardMovementControllers.RemoveAt(0);
    }
    private void OnChangeCardState(CardDealState canCardDealState)
    {
        cardDealState = canCardDealState;
    }
}
