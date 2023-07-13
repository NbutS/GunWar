using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NamePlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;

    private void Start()
    {
        textName.text = "Boy";
    }
    public void SetTextName(string name)
    {
        textName.text = name;
    }
}
