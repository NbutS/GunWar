using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCoin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    private void Start()
    {
        textCoin.text = TotalScore.instance.Score.ToString();
    }
    private void Update()
    {
        SetText();
    }
    public void SetText()
    {
        textCoin.text = TotalScore.instance.Score.ToString();
    }
}
