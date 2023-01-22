using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConcentrationCPU : ConcentrationPlayerBase
{
    public TextMeshProUGUI ScoreText;

    /// <summary>
    /// Player�̑I��
    /// </summary>
    /// <param name="choiceCard"></param>
    /// <param name="choiceCardImage"></param>
    public override void CardChoice(Card choiceCard, Image choiceCardImage)
    {
        base.CardChoice(choiceCard, choiceCardImage);
        ScoreText.text = $"CPUScore:{Score}";
    }
}