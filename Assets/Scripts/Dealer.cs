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

    //�g�����v��\������Image
    public Image CardImage;

    //�g�����v���Y�ރ��[�g
    [SerializeField]
    private RectTransform cardBG;

    private void Start()
    {
        Deck.GetDeck();
        /*
         Debug.Log($"�X�[�g: {Deck.CardDeck.FirstOrDefault().CardSuit}" +
            $"����:{Deck.CardDeck.FirstOrDefault().Number}");
        */

        //�����_����bool�𔻒肵�AList���ɔ�������ɍ��v���邩true��false�ŕԂ�
        var clubCards = Deck.CardDeck.Where(card => card.CardSuit == Card.Suit.Club).ToList();
        var clubone = clubCards.FirstOrDefault();

        //�����_����bool�𔻒肵�AList���ɔ�������ɍ��v���邩true��false�ŕԂ�
        var clubCardsInHeartCard = Deck.CardDeck.Any(card => card.CardSuit == Card.Suit.Heart);

        foreach (var card in Deck.CardDeck)
        {
            var cardImage = Instantiate(CardImage, cardBG);
            //�J�[�h�̕�������t�b�N�ɕ\������
            cardImage.sprite = CardAtlas.GetSprite($"Card_54");

            var button = cardImage.gameObject.AddComponent<Button>();

            button.onClick.AddListener(() =>
            {
                cardImage.sprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
            });

        }
     
    }
}
