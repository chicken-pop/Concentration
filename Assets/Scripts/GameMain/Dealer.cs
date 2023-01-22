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

    public RectTransform GetCardBGRoot
    {
        get { return cardBG; }
    }

    [SerializeField]
    private ConcentrationGameProgressionManager concentrationGameProgressionManager;

    [SerializeField]
    private ConcentrationPlayerBase Player;

    [SerializeField]
    private ConcentrationPlayerBase CPU;

    public ConcentrationPlayerBase GetCPUConcentrationPlayer
    {
        get { return CPU; }
    }

    public int GetPlayerCardCount
    {
        get { return Player.Score; }
    }

    public int GetCPUCardCount
    {
        get { return CPU.Score; }
    }

    public void Deal()
    {
        Deck.GetDeck();

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
                //�Q�[���̃X�e�[�g��Choice�ȊO��������A��
                if (concentrationGameProgressionManager.GetGameStates != ConcentrationGameProgressionManager.GameStates.Choice)
                {
                    return;
                }
                switch (ActionTurn)
                {
                    //Player�̃^�[����������
                    case Turn.Player:
                        Debug.Log(cardImage);
                        Player.CardChoice(card, cardImage);
                        if (!Player.IsMyTurn)
                        {
                            //�I�����ꂽ�J�[�h�𗠌�����
                            cardImage.sprite = CardAtlas.GetSprite($"Card_54");
                            Player.currentChoiceCardImage.sprite = CardAtlas.GetSprite($"Card_54");
                            ActionTurn = Turn.CPU;
                            CPU.IsMyTurn = true;
                Debug.Log("������������");
                            
                            return;
                        }
                        break;
                   �@//CPU�̃^�[����������
                    case Turn.CPU:
                        CPU.CardChoice(card, cardImage);
                        if (!CPU.IsMyTurn)
                        {
                            //�I�����ꂽ�J�[�h�𗠌�����
                            cardImage.sprite = CardAtlas.GetSprite($"Card_54");
                            CPU.currentChoiceCardImage.sprite = CardAtlas.GetSprite($"Card_54");
                            ActionTurn = Turn.Player;
                            Player.IsMyTurn = true;
                            return;
                        }
                        break;
                }
                cardImage.sprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");

            });

        }

    }
}
