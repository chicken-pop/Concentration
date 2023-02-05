using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConcentrationCPU : ConcentrationPlayerBase
{

    public ConcentrationGameProgressionManager.GameModes GameModes;

    public TextMeshProUGUI ScoreText;

    /// <summary>
    /// Player‚Ì‘I‘ð
    /// </summary>
    /// <param name="choiceCard"></param>
    /// <param name="choiceCardImage"></param>
    public override void CardChoice(Card choiceCard, Image choiceCardImage)
    {
        base.CardChoice(choiceCard, choiceCardImage);
        if (GameModes == ConcentrationGameProgressionManager.GameModes.CPUCardIsComputerChoice)
        {

            ScoreText.text = $"CPU Score:{Score}";
            return;
        }
        ScoreText.text = $"Player2 Score:{Score}";
    }
}

