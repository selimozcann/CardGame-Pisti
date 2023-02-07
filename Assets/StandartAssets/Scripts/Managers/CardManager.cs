using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private CardMovementController image;
    [SerializeField] private List<Sprite> cardImage;
    [SerializeField] private Transform cardPosition;
    [SerializeField] private Transform cardParent;
    
    [SerializeField] private List<CardMovementController> currentCardMovementControllers;
    public List<CardMovementController> CurrentCardMovementControllers { get => currentCardMovementControllers; }
    
    public Action OnDealCard;
    private int cardAmonut = 12;

    public List<string> carDataValues;
    public List<CardStatController> aiCards;
    private void Start()
    { 
        SetCardImage();
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
        
        GameManager.I.ChangeToGameState(GameState.Playing);
    }
    private IEnumerator DealingToCard()
    {
        for (int i = 0; i < cardAmonut; i++)
        {
            yield return new WaitForSeconds(.2f);
            SetCardDealToMoving(i);
        }
        StopCoroutine(DealingToCard());
    }

    private void SetCardDealToMoving(int i)
    {
        CardMovementController cardMovementController = currentCardMovementControllers[0]; 
        CardStatController cardStatController = cardMovementController.GetComponent<CardStatController>();
        if (i == 0 || i == 1 || i == 2  || i == 3)
        {
            cardStatController.OnChangeCardStat(CardState.BoardCardState);
            CardBoardManager.I.CurrentBoardCard = cardStatController;
            CardBoardManager.I.OnCardAdd(cardStatController,cardMovementController);
            if (i != 3)
            {
                cardStatController.OnClosedCardImage();
            }
        }
        if (i == 8 || i == 9 || i == 10 || i == 11)
        {
            cardStatController.OnChangeCardStat(CardState.AI);
            cardStatController.CheckToCardDataValue();
        }
        cardMovementController.SetToCardIndex(i);
        cardMovementController.OnCanMove(CardMoveState.CardDealMove);
        currentCardMovementControllers.RemoveAt(0);
    }
}
