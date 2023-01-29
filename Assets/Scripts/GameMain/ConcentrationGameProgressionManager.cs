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

    private float choiceTime = 1.5f;

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
                //�X�^�[�g�̉��o�Ȃǂ������ꍇ�����ōs��
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
                            var randChoice = Random.Range(0, Dealer.GetCardBGRoot.GetComponentsInChildren<CardButtonExtension>().Length+1);

                            var randCount = 0;

                            IsCPUChoice = false;

                            foreach(var card in Dealer.GetCardBGRoot.GetComponentsInChildren<CardButtonExtension>())
                            {
                                randCount++;
                                if (card.GetCardImage != Dealer.GetCPUConcentrationPlayer.currentChoiceCardImage)
                                {
                                    if (card.gameObject.activeSelf && !IsCPUChoice && randCount == randChoice)
                                    {
                                        card.OnPointerClick(null);
                                        IsCPUChoice = true;
                                    }
                                }
                            }
                            choiceTime = 1.5f;
                        }
                    }
                }
                //�J�[�h��؂���
                if (Dealer.GetPlayerCardCount + Dealer.GetCPUCardCount == 52)
                {
                    gameState = GameStates.GameEnd;
                }
                break;
            case GameStates.GameEnd:
                if (Dealer.GetPlayerCardCount > Dealer.GetCPUCardCount)
                {
                    Debug.Log("Player�̏���");
                }
                else
                {
                    Debug.Log("CPU�̏���");
                }
                break;
        }

    }
}
