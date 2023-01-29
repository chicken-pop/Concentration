using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Deck
{
    /// <summary>
    /// トランプのカードデック
    /// </summary>
    public List<Card> CardDeck = new List<Card>();

    /// <summary>
    /// 昇順のDeckをゲットする
    /// </summary>
    public List<Card> GetDeck(bool isShuffle = false)
    {
        //一度作られたデッキがある場合はCardDeckを返す
        if(CardDeck.FirstOrDefault() != null)
        {
            return CardDeck;
        }

        //デッキがない場合はここでデッキを作成する
        for(int i = 0; i < CardHelper.CardMax; i++)
        {
            CardDeck.Add(new Card(CardHelper.CardSuitJudge(i), CardHelper.CardNumJudge(i)));
        }

        //シャッフルする場合はCardDeckをGuidを使って並べる
        if (isShuffle)
        {
            return CardDeck = CardDeck.OrderBy(card => Guid.NewGuid()).ToList();
        }
        return CardDeck;
    }
}
