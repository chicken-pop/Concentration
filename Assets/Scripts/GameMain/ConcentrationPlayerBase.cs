using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// �_�o����̃v���C���[�̊��N���X
/// </summary>
public class ConcentrationPlayerBase : MonoBehaviour
{
    public int Score;

    public Card currentChoiceCard;

    public Image currentChoiceCardImage;

    public bool IsMyTurn = false;

    //�v���C���[�����Ԃ�����
    public Sprite hideCardSprite;

    //�v���C���[���J�[�h��I�������ۂ̏���
    public UnityAction CardChoiceCallback;

    public virtual void PlayerInitialize(Sprite hideCardSprite, UnityAction cardChoiceCallback)
    {
        Score = 0;
        this.hideCardSprite = hideCardSprite;
        this.CardChoiceCallback += cardChoiceCallback;
    }

    public virtual void CardChoice(Card choiceCard, Image choiceCardImage)
    {
        //1�����I�������J�[�h�̏���
        if (currentChoiceCard == null)
        {
            currentChoiceCard = choiceCard;
            currentChoiceCardImage = choiceCardImage;
            IsMyTurn = true;
            return;
        }

        //�����J�[�h��I��ł���ꍇ�͋A��
        if (choiceCard == currentChoiceCard)
        {
            return;
        }


        if (currentChoiceCard.Number == choiceCard.Number)
        {
            StartCoroutine(PairChoice(choiceCardImage));

        }
        else
        {
            //�����������̏���
            StartCoroutine(MissChoice(choiceCardImage));
        }

    }

    IEnumerator PairChoice(Image choiceCardImage)
    {
        yield return new WaitForSeconds(1f);
        //�y�A����������̂ŏ���
        currentChoiceCardImage.gameObject.SetActive(false);
        choiceCardImage.gameObject.SetActive(false);
        currentChoiceCard = null;

        //�����̃^�[���͑��s
        IsMyTurn = true;
        //�X�R�A�̉��Z
        Score += 2;
    }

    IEnumerator MissChoice(Image choiceCardImage)
    {
        yield return new WaitForSeconds(1f);

        //�������I�񂾃J�[�h�𗠑���
        choiceCardImage.sprite = hideCardSprite;
        currentChoiceCardImage.sprite = hideCardSprite;

        //�����̃^�[���͏I��
        currentChoiceCard = null;
        IsMyTurn = false;
        //�J�[�h�I�����I������ۂ̃R�[���o�b�N
        CardChoiceCallback?.Invoke();

    }

}
