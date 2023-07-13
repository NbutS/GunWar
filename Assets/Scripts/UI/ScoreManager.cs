using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        SetText();
    }
    public void SetText()
    {
        scoreText.text = Player.instance.Score.ToString();
    }
}
