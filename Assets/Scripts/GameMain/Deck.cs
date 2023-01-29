using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Deck
{
    /// <summary>
    /// �g�����v�̃J�[�h�f�b�N
    /// </summary>
    public List<Card> CardDeck = new List<Card>();

    /// <summary>
    /// ������Deck���Q�b�g����
    /// </summary>
    public List<Card> GetDeck(bool isShuffle = false)
    {
        //��x���ꂽ�f�b�L������ꍇ��CardDeck��Ԃ�
        if(CardDeck.FirstOrDefault() != null)
        {
            return CardDeck;
        }

        //�f�b�L���Ȃ��ꍇ�͂����Ńf�b�L���쐬����
        for(int i = 0; i < CardHelper.CardMax; i++)
        {
            CardDeck.Add(new Card(CardHelper.CardSuitJudge(i), CardHelper.CardNumJudge(i)));
        }

        //�V���b�t������ꍇ��CardDeck��Guid���g���ĕ��ׂ�
        if (isShuffle)
        {
            return CardDeck = CardDeck.OrderBy(card => Guid.NewGuid()).ToList();
        }
        return CardDeck;
    }
}
