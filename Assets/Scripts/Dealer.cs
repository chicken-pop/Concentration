using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Dealer : MonoBehaviour
{
    private Deck Deck = new Deck();

    [SerializeField]
    private SpriteAtlas CardAtlas;

    //トランプを表示するImage
    public Image CardImage;

    //トランプを産むルート
    [SerializeField]
    private RectTransform cardBG;

    private void Start()
    {
        Deck.GetDeck();
        /*
         Debug.Log($"スート: {Deck.CardDeck.FirstOrDefault().CardSuit}" +
            $"数字:{Deck.CardDeck.FirstOrDefault().Number}");
        */

        //ラムダ式でboolを判定し、List内に判定条件に合致するかtrueかfalseで返す
        var clubCards = Deck.CardDeck.Where(card => card.CardSuit == Card.Suit.Club).ToList();
        var clubone = clubCards.FirstOrDefault();

        //ラムダ式でboolを判定し、List内に判定条件に合致するかtrueかfalseで返す
        var clubCardsInHeartCard = Deck.CardDeck.Any(card => card.CardSuit == Card.Suit.Heart);

        foreach (var card in Deck.CardDeck)
        {
            var cardImage = Instantiate(CardImage, cardBG);
            //カードの文字列をフックに表示する
            cardImage.sprite = CardAtlas.GetSprite($"Card_54");

            var button = cardImage.gameObject.AddComponent<Button>();

            button.onClick.AddListener(() =>
            {
                cardImage.sprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
            });

        }
     
    }
}
