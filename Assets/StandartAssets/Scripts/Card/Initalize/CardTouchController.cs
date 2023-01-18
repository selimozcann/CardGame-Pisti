using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardTouchController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,ITouch
{
   
    [SerializeField] private CardMovementController _cardMovementController;
    [SerializeField] private CardStatController cardStatController;
    [HideInInspector] public RectTransform _rectTransform;
    public RectTransform cardParentObject;
    private bool canTakeCard = true;
    private void Start()
    {
         cardParentObject = GetComponentInParent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
    }
    public void Touched()
    {
        _rectTransform.SetSiblingIndex(cardParentObject.transform.childCount - 1); 
        _cardMovementController._cardMoveState =  CardMoveState.CardBoardMove;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUp();
    }
    void OnTouchedToCard()
    {
        var canCardMove = GameManager.I.GameState == GameState.Playing && canTakeCard && cardStatController.cardState == CardState.PlayerCard;
        if (canCardMove)
        {
            canTakeCard = false;
            CardBoardManager.I.OnCardAdd(cardStatController,_cardMovementController);
            CardBoardManager.I.CheckToCardToThread();
            Touched();
            GameManager.I.ChangeToGameState(GameState.None);
            cardStatController.OnAIMove();
        }
    }
    public void OnPointerDown()
    {
        OnTouchedToCard();
    }
    public void OnPointerUp()
    {
        Debug.Log("OnPointerUp Event is called");
    }
}
