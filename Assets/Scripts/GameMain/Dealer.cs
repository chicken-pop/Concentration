using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private TextMeshProUGUI turnInformationText;

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

    public ConcentrationGameProgressionManager.GameModes GameModes;

    public void Deal()
    {
        Deck.GetDeck(true);

        Player.PlayerInitialize(CardAtlas.GetSprite($"Card_54"), TurnChange);
        CPU.PlayerInitialize(CardAtlas.GetSprite($"Card_54"), TurnChange);

        //�����_����bool�𔻒肵�AList���ɔ�������ɍ��v���邩true��false�ŕԂ�
        var clubCards = Deck.CardDeck.Where(card => card.CardSuit == Card.Suit.Club).ToList();
        var clubone = clubCards.FirstOrDefault();

        //�����_����bool�𔻒肵�AList���ɔ�������ɍ��v���邩true��false�ŕԂ�
        var clubCardsInHeartCard = Deck.CardDeck.Any(card => card.CardSuit == Card.Suit.Heart);

        StartCoroutine(FinishDealingCards());
    }

    private IEnumerator FinishDealingCards()
    {
        foreach (var card in Deck.CardDeck)
        {
            var cardImage = Instantiate(CardImage, cardBG);
            //Instantiate���ꂽcardImage����CardButtonExtension���擾����
            var cardButton = cardImage.gameObject.GetComponent<CardButtonExtension>();
            //cardButton�̈����Ŏg���\������摜��SpriteAtlas����擾����
            var cardSprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
            //cardButton�̈����Ŏg���B���悤�̉摜��SpriteAtlas����擾����
            var hideCarSprite = CardAtlas.GetSprite($"Card_54");

            cardButton.Initialize(cardSprite, hideCarSprite, () =>
            {
                switch (ActionTurn)
                {
                    case Turn.Player:
                        Player.CardChoice(card, cardImage);
                        break;
                    case Turn.CPU:
                        CPU.CardChoice(card, cardImage);
                        break;

                }
            });

        }


        yield return new WaitForEndOfFrame();

        cardBG.GetComponent<GridLayoutGroup>().enabled = false;

    }

    private void TurnChange()
    {
        switch (ActionTurn)
        {
            case Turn.Player:
                if (!Player.IsMyTurn)
                {
                    ActionTurn = Turn.CPU;
                    CPU.IsMyTurn = true;
                }
                break;
            case Turn.CPU:
                if (!CPU.IsMyTurn)
                {
                    ActionTurn = Turn.Player;
                    Player.IsMyTurn = true;
                }
                break;
        }

        StartCoroutine(TurnInformaiton(ActionTurn));
    }

    private IEnumerator CardChoiceVerification(Card card, Image cardImage)
    {
        switch (ActionTurn)
        {
            //Player�̃^�[����������
            case Turn.Player:
                Debug.Log(cardImage);
                Player.CardChoice(card, cardImage);
                yield return new WaitForSeconds(1f);
                if (!Player.IsMyTurn)
                {
                    //�I�����ꂽ�J�[�h�𗠌�����
                    cardImage.sprite = CardAtlas.GetSprite($"Card_54");
                    Player.currentChoiceCardImage.sprite = CardAtlas.GetSprite($"Card_54");
                    ActionTurn = Turn.CPU;
                    CPU.IsMyTurn = true;
                }
                break;
            //CPU�̃^�[����������
            case Turn.CPU:
                CPU.CardChoice(card, cardImage);
                yield return new WaitForSeconds(1f);
                if (!CPU.IsMyTurn)
                {
                    //�I�����ꂽ�J�[�h�𗠌�����
                    cardImage.sprite = CardAtlas.GetSprite($"Card_54");
                    CPU.currentChoiceCardImage.sprite = CardAtlas.GetSprite($"Card_54");
                    ActionTurn = Turn.Player;
                    Player.IsMyTurn = true;
                }
                break;
        }

     

    }



    private IEnumerator TurnInformaiton(Turn turn)
    {
        turnInformationText.gameObject.SetActive(true);
        switch (turn)
        {
            case Turn.Player:
                turnInformationText.text = $"Next turn is Player";
                break;

            case Turn.CPU:
                if (GameModes == 0)
                {
                    turnInformationText.text = $"Next turn is Player2";
                }
                else
                {
                    turnInformationText.text = $"Next turn is CPU";
                }
                break;
        }
        yield return new WaitForSeconds(0.9f);
        turnInformationText.gameObject.SetActive(false);

    }
}
