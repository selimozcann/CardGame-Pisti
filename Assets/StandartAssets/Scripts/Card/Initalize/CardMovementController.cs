using UnityEngine;

public enum CardMoveState { None = 0, CardDealMove,CardBoardMove,CardTakeMoveButtom, CardTakeMoveTop}
public class CardMovementController : MonoBehaviour
{
    public CardMoveState _cardMoveState;
    [SerializeField] private CardMovementSettings _cardMovementSettings;
    [SerializeField] private int cardIndex;
    private RectTransform _rectTransform;
    private void Start()
    {
        SetToRectTransform();   
    }
    private void SetToRectTransform()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        SetMovement();
    }
    private void SetMovement()
    {
        switch (_cardMoveState)
        {
            case CardMoveState.CardDealMove:
                CardDealMoving();
                break;
            case CardMoveState.CardBoardMove:
                CardBoardMoving(CardTargetManager.I.MainCardTarget.localPosition);
                break;
            case CardMoveState.CardTakeMoveButtom:
                CardBoardMoving(CardTargetManager.I.ButtomCard.localPosition);
                break;
            case CardMoveState.CardTakeMoveTop:
                CardBoardMoving(CardTargetManager.I.TopCard.localPosition);
                break;
            default:
                break;
        }
    }
    public void SetToCardIndex(int setCardIndexValue)
    {
        cardIndex = setCardIndexValue;
    } 
    public void OnCanMove(CardMoveState cardMoveState)
    {
        gameObject.SetActive(true);
        _cardMoveState = cardMoveState;
    }
    private void CardDealMoving()
    {
        _rectTransform.localPosition = Vector2.Lerp(_rectTransform.localPosition, CardTargetManager.I.InitalizeCardTarget[cardIndex].localPosition,_cardMovementSettings.LerpTime * Time.deltaTime);
        _rectTransform.localRotation = Quaternion.Lerp(_rectTransform.localRotation, CardTargetManager.I.InitalizeCardTarget[cardIndex].localRotation, .8f * Time.deltaTime);
    }
    private void CardBoardMoving(Vector3 targetPos)
    {
        _rectTransform.localPosition = Vector2.Lerp(_rectTransform.localPosition, targetPos, 2.5f * Time.deltaTime);
    }
}
