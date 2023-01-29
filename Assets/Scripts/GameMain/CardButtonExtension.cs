using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// カードを押したときの挙動
/// </summary>
public class CardButtonExtension : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler

{
    private Image cardImage;
    private Sprite cardSprite;
    private Sprite hideCardSprite;

    public Image GetCardImage
    {
        get { return cardImage; }
    }

    public UnityAction OnClickCallBack;
    public UnityAction OnPointerDownCallBack;
    public UnityAction OnPointerUpCallBack;

    public void Initialize(Sprite cardSprite, Sprite hideSprite, UnityAction onClickAction)
    {
        this.cardImage = this.GetComponent<Image>();
        this.cardSprite = cardSprite;
        this.hideCardSprite = hideSprite;
        //最初はカードを隠しておく
        this.cardImage.sprite = hideCardSprite;

        OnClickCallBack += onClickAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickCallBack?.Invoke();
        //カードの表示を表にする
        this.cardImage.sprite = this.cardSprite;
    }
    //タップアップ
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpCallBack?.Invoke();
    }

    //タップダウン
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCallBack?.Invoke();
    }

    //カーソルがこのボタンに触れたとき
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.cardImage.color = new Color(0.9f, 0.9f, 0.9f);
    }

    //カーソルがこのボタンを離れたとき
    public void OnPointerExit(PointerEventData eventData)
    {
        this.cardImage.color = Color.white;
    }


}
