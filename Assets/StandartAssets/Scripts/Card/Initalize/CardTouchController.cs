using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardTouchController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,ITouch
{
    [SerializeField] private RectTransform cardParentObject;
    [SerializeField] private CardMovementController _cardMovementController;
    [SerializeField] private CardStatController cardStatController;
    private RectTransform _rectTransform;
    private bool canTakeCard = true;
    private void Start()
    {
         cardParentObject = GetComponentInParent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Touched()
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
    IEnumerator TouchedCoroutine()
    {
        var canCardMove = GameManager.I.GameState == GameState.Playing && canTakeCard && cardStatController.cardState == CardState.PlayerCard;
        if (canCardMove)
        {
            canTakeCard = false;
            CardBoardManager.I.OnCardAdd(cardStatController,_cardMovementController);
            StartCoroutine(CardBoardManager.I.CheckToCardCoroutine());
            Touched();
            GameManager.I.ChangeToGameState(GameState.None);
            cardStatController.OnAIMove();
            yield return new WaitForSeconds(2f);
            GameManager.I.ChangeToGameState(GameState.Playing);
        }
    }
    public void OnPointerDown()
    {
        StartCoroutine(TouchedCoroutine());
    }
    public void OnPointerUp()
    {
        
    }
}
