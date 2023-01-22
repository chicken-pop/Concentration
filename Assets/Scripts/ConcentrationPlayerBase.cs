using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �_�o����̃v���C���[�̊��N���X
/// </summary>
public class ConcentrationPlayerBase : MonoBehaviour
{
    public int Score;

    public Card currentChoiceCard;

    public Image currentChoiceCardImage;

    public bool IsMyTurn = false;

    public virtual void CardChoice(Card choiceCard,Image choiceCardImage)
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
            //�y�A����������̂ŏ���
            currentChoiceCardImage.gameObject.SetActive(false);
            choiceCardImage.gameObject.SetActive(false);
            currentChoiceCard = null;

            //�����̃^�[���͑��s
            IsMyTurn = true;
            //�X�R�A�̉��Z
            Score += 2;
           
        }
        else
        {
            //�����̃^�[���͏I��
            currentChoiceCard = null;
            IsMyTurn = false;
        }
  
    }   

}
