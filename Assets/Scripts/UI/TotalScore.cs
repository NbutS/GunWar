using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScore : MonoBehaviour
{
    public static TotalScore instance;
    private int score = 0;
    private int currentID = 1;
    private bool isReplay = false;
    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public int CurrentID
    {
        get { return currentID; }
        set { currentID = value; }
    }
    public bool IsReplay
    {
        set { isReplay = value; }
        get { return isReplay; }
    }
}
