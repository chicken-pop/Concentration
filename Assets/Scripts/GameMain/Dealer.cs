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

    //トランプを表示するImage
    public Image CardImage;

    public enum Turn
    {
        Player,
        CPU
    }

    public Turn ActionTurn = Turn.Player;

    //1つ前に選択したカード
    private Card currentCard;
    //1つ前に選択したカードイメージ
    private Image currentCardImage;

    //トランプを産むルート
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

        //ラムダ式でboolを判定し、List内に判定条件に合致するかtrueかfalseで返す
        var clubCards = Deck.CardDeck.Where(card => card.CardSuit == Card.Suit.Club).ToList();
        var clubone = clubCards.FirstOrDefault();

        //ラムダ式でboolを判定し、List内に判定条件に合致するかtrueかfalseで返す
        var clubCardsInHeartCard = Deck.CardDeck.Any(card => card.CardSuit == Card.Suit.Heart);

        StartCoroutine(FinishDealingCards());
    }

    private IEnumerator FinishDealingCards()
    {
        foreach (var card in Deck.CardDeck)
        {
            var cardImage = Instantiate(CardImage, cardBG);
            //InstantiateされたcardImageからCardButtonExtensionを取得する
            var cardButton = cardImage.gameObject.GetComponent<CardButtonExtension>();
            //cardButtonの引数で使う表示する画像をSpriteAtlasから取得する
            var cardSprite = CardAtlas.GetSprite($"Card_{((int)card.CardSuit * 13) + card.Number - 1}");
            //cardButtonの引数で使う隠すようの画像をSpriteAtlasから取得する
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
            //Playerのターンだったら
            case Turn.Player:
                Debug.Log(cardImage);
                Player.CardChoice(card, cardImage);
                yield return new WaitForSeconds(1f);
                if (!Player.IsMyTurn)
                {
                    //選択されたカードを裏向ける
                    cardImage.sprite = CardAtlas.GetSprite($"Card_54");
                    Player.currentChoiceCardImage.sprite = CardAtlas.GetSprite($"Card_54");
                    ActionTurn = Turn.CPU;
                    CPU.IsMyTurn = true;
                }
                break;
            //CPUのターンだったら
            case Turn.CPU:
                CPU.CardChoice(card, cardImage);
                yield return new WaitForSeconds(1f);
                if (!CPU.IsMyTurn)
                {
                    //選択されたカードを裏向ける
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
