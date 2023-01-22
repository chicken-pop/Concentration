using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcentrationGameProgressionManager : MonoBehaviour
{
    [SerializeField]
    private Dealer Dealer;

    public enum GameStates
    {
        Invalide = -1,
        Start,
        Deal,
        Choice,
        GameEnd
    }

    private GameStates gameState = GameStates.Invalide;

    public GameStates GetGameStates
    {
        get { return gameState; }
    }

    public enum GameModes
    {
        CPUCardPlayerChoice,
        CPUCardIsComputerChoice
    }

    private float choiceTime = 1.0f;

    private bool IsCPUChoice;

    public GameModes GameMode = GameModes.CPUCardIsComputerChoice;

    private void Update()
    {
        switch (gameState)
        {
            case GameStates.Invalide:
                gameState = GameStates.Start;
                break;
            case GameStates.Start:
                //スタートの演出などしたい場合ここで行う
                gameState = GameStates.Deal;
                break;
            case GameStates.Deal:
                Dealer.Deal();
                gameState = GameStates.Choice;
                break;
            case GameStates.Choice:
                if (GameMode == GameModes.CPUCardIsComputerChoice)
                {
                    if (Dealer.GetCPUConcentrationPlayer.IsMyTurn)
                    {
                        choiceTime -= Time.deltaTime;
                        if (choiceTime < 0)
                        {
                            var randChoice = Random.Range(0, Dealer.GetCardBGRoot.GetComponentsInChildren<Button>().Length+1);

                            var randCount = 0;

                            IsCPUChoice = false;

                            foreach(var card in Dealer.GetCardBGRoot.GetComponentsInChildren<Button>())
                            {
                                randCount++;
                                if (card.image != Dealer.GetCPUConcentrationPlayer.currentChoiceCardImage)
                                {
                                    if (card.gameObject.activeSelf && !IsCPUChoice && randCount == randChoice)
                                    {
                                        card.onClick.Invoke();
                                        IsCPUChoice = true;
                                    }
                                }
                            }
                            choiceTime = 1f;
                        }
                    }
                }
                //カードを切り取る
                if (Dealer.GetPlayerCardCount + Dealer.GetCPUCardCount == 52)
                {
                    gameState = GameStates.GameEnd;
                }
                break;
            case GameStates.GameEnd:
                if (Dealer.GetPlayerCardCount > Dealer.GetCPUCardCount)
                {
                    Debug.Log("Playerの勝ち");
                }
                else
                {
                    Debug.Log("CPUの勝ち");
                }
                break;
        }

    }
}
