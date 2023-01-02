using UnityEngine;
using UnityEngine.EventSystems;
public class CardTouchController : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private RectTransform cardParentObject;
    [SerializeField] private CardMovementController _cardMovementController;
    [SerializeField] private CardStatController cardStatController;
    private bool canBoardMove;
    private RectTransform _rectTransform;
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
        if (cardStatController.cardState == CardState.PlayerCard)
        {
            CardBoardManager.I.OnCardAdd(cardStatController,_cardMovementController);
            StartCoroutine(CardBoardManager.I.CheckToCardCoroutine());
            Touched();
        }
    }
}
