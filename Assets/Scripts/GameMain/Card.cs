using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �g�����v�̊�{���
/// </summary>
public class Card
{
    //��
    public enum Suit
    {
        Invalid = -1,
        Club,
        dia,
        Heart,
        Spade,
        Max
    }

    /// <summary>
    /// �g�����v�̕�
    /// </summary>
    public Suit CardSuit = Suit.Invalid;

    /// <summary>
    /// �g�����v�̐���
    /// </summary>
    public int Number = 0;

    /// <summary>
    /// �J�[�h�̏�����
    /// </summary>
    /// <param name="suit">��</param>
    /// <param name="number">�g�����v�̐���</param>
    public Card(Suit suit,int number)
    {
        this.CardSuit = suit;
        this.Number = number;
    }
}