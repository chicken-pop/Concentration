using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �J�[�h���������Ƃ��̋���
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
        //�ŏ��̓J�[�h���B���Ă���
        this.cardImage.sprite = hideCardSprite;

        OnClickCallBack += onClickAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickCallBack?.Invoke();
        //�J�[�h�̕\����\�ɂ���
        this.cardImage.sprite = this.cardSprite;
    }
    //�^�b�v�A�b�v
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpCallBack?.Invoke();
    }

    //�^�b�v�_�E��
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCallBack?.Invoke();
    }

    //�J�[�\�������̃{�^���ɐG�ꂽ�Ƃ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.cardImage.color = new Color(0.9f, 0.9f, 0.9f);
    }

    //�J�[�\�������̃{�^���𗣂ꂽ�Ƃ�
    public void OnPointerExit(PointerEventData eventData)
    {
        this.cardImage.color = Color.white;
    }


}
