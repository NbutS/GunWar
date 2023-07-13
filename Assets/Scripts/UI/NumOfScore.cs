using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumOfScore : MonoBehaviour
{
    [SerializeField] private Text numScoreText;

    private void Update()
    {
        SetText();
    }
    public void SetText()
    {
        numScoreText.text = TotalScore.instance.Score.ToString();
    }
}
