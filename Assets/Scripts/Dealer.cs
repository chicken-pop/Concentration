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

    public enum Turn
    {
        Player,
        CPU
    }

    public Turn ActionTurn = Turn.Player;

    //1�O�ɑI�������J�[�h
    private Card currentCard;
    //1�O�ɑI�������J�[�h�C���[�W
    private Image currentCardImage;

    //�g�����v���Y�ރ��[�g
    [SerializeField]
    private RectTransform cardBG;

    [SerializeField]
    private ConcentrationPlayerBase Player;

    [SerializeField]
    private ConcentrationPlayerBase CPU;

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
                   switch (ActionTurn)
                {
                    //Player�̃^�[����������
                    case Turn.Player:
                        Player.CardChoice(card,cardImage);
                        break;
                   �@//CPU�̃^�[����������
                    case Turn.CPU:
                        CPU.CardChoice(card,cardImage);
                        break;
                }

                cardImage.sprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
                
            });

        }
     
    }
}
