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
                //ゲームのステートがChoice以外だったら帰る
                if (concentrationGameProgressionManager.GetGameStates != ConcentrationGameProgressionManager.GameStates.Choice)
                {
                    return;
                }
                switch (ActionTurn)
                {
                    //Playerのターンだったら
                    case Turn.Player:
                        Debug.Log(cardImage);
                        Player.CardChoice(card, cardImage);
                        if (!Player.IsMyTurn)
                        {
                            //選択されたカードを裏向ける
                            cardImage.sprite = CardAtlas.GetSprite($"Card_54");
                            Player.currentChoiceCardImage.sprite = CardAtlas.GetSprite($"Card_54");
                            ActionTurn = Turn.CPU;
                            CPU.IsMyTurn = true;
                Debug.Log("ｓｓｓｓｓｓ");
                            
                            return;
                        }
                        break;
                   　//CPUのターンだったら
                    case Turn.CPU:
                        CPU.CardChoice(card, cardImage);
                        if (!CPU.IsMyTurn)
                        {
                            //選択されたカードを裏向ける
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
