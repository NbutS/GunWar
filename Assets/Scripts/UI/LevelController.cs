using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    
    //Screen-------------------------------
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject soundScreen;
    [SerializeField] private GameObject figureScreen;
    [SerializeField] private GameObject awardScreen;
    //Button---------------------------------------
    [SerializeField] private GameObject bgButton;
    [SerializeField] private GameObject soundButton;
        
    //GameObject-----------------------------------
    [SerializeField] private GameObject chooseFigure;
    [SerializeField] private GameObject figure;
    [SerializeField] private Text numScoreText;
    [SerializeField] private Text gameOverScoreText;
    [SerializeField] private Text bestScoreText;
    //Animator
    private Animator animator;
    private Animator bgAnimator;
    private Animator effectAnimator;
    private Animator figAnimator;
    private void Start()
    {
        gameOver.SetActive(false);
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        gameOverScoreText.text = Player.instance.Score.ToString();
        if ( Player.instance.Score > TotalScore.instance.Score )
        {
            TotalScore.instance.Score = Player.instance.Score;
        }
    }
    public void RePlay()
    {
        Debug.Log("RePlay the Game");
        int currentID = Player.instance.CurrentID;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TotalScore.instance.CurrentID = currentID;
        TotalScore.instance.IsReplay = true;
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
    public void SoundScreen()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
        homeScreen.SetActive(false);
        soundScreen.SetActive(true);
        if ( FindObjectOfType<AudioManager>().GetVolumnOfSound("HomeBG") == 0f)
        {
            bgAnimator = bgButton.GetComponent<Animator>();
            bgAnimator.SetBool("BGSound", false);
        }
        if ( FindObjectOfType<AudioManager>().GetVolumnOfSound("ClickButton") == 0f)
        {
            effectAnimator = soundButton.GetComponent<Animator>();
            effectAnimator.SetBool("ESound", false);
        }
    }

    public void PauseScreen()
    {
        pauseScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }

    public void TurnOnOffSound()
    {
        bgAnimator = bgButton.GetComponent<Animator>();
        bgAnimator.SetBool("BGSound", !bgAnimator.GetBool("BGSound"));
        FindObjectOfType<AudioManager>().Play("ClickButton");
        if (bgAnimator.GetBool("BGSound") == false)
        {
            FindObjectOfType<AudioManager>().TurnOffBGSound("HomeBG");
            FindObjectOfType<AudioManager>().TurnOffBGSound("GameplayBG");
        }
        else if (bgAnimator.GetBool("BGSound") == true )
        {
            FindObjectOfType<AudioManager>().TurnOnBGSound("HomeBG", 1f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("GameplayBG", 1f);
            FindObjectOfType<AudioManager>().Play("HomeBG");
        }
        
    }
    public void TurnOnOffSoundEffect()
    {
        effectAnimator = soundButton.GetComponent<Animator>();
        effectAnimator.SetBool("ESound", !effectAnimator.GetBool("ESound"));
        FindObjectOfType<AudioManager>().Play("ClickButton");

        if (effectAnimator.GetBool("ESound") == false)
        {
            FindObjectOfType<AudioManager>().TurnOffBGSound("ClickButton");
            FindObjectOfType<AudioManager>().TurnOffBGSound("ChooseFirgure");
            FindObjectOfType<AudioManager>().TurnOffBGSound("ManFire");
            FindObjectOfType<AudioManager>().TurnOffBGSound("RobotFire");
            FindObjectOfType<AudioManager>().TurnOffBGSound("HitPlayer");
            FindObjectOfType<AudioManager>().TurnOffBGSound("HitGround");
            FindObjectOfType<AudioManager>().TurnOffBGSound("HitEnermy");
            FindObjectOfType<AudioManager>().TurnOffBGSound("PrepareFire");
        }
        else if (effectAnimator.GetBool("ESound") == true)
        {
            FindObjectOfType<AudioManager>().TurnOnBGSound("ClickButton",0.8f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("ChooseFirgure",0.8f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("ManFire",0.8f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("RobotFire",0.8f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("HitPlayer",0.8f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("HitGround", 0.6f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("HitEnermy",0.6f);
            FindObjectOfType<AudioManager>().TurnOnBGSound("PrepareFire",1f);
        }
    }
    public void HomeScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChooseManButton()
    {
        FindObjectOfType<AudioManager>().Play("ChooseFirgure");
        animator = chooseFigure.GetComponent<Animator>();
        figAnimator = figure.GetComponent<Animator>();
        if (animator.GetBool("choosenRobot") == true || animator.GetBool("isFinalRobot") == true)
        {
            animator.SetBool("isFinalMan", true);
            animator.SetBool("isFinalRobot", false);
            animator.SetBool("choosenMan", false);
            figAnimator.SetInteger("choosen", 1);
        }
        else
        {
            animator.SetBool("choosenMan", true);
            figAnimator.SetInteger("choosen", 1);
        }
        Player.instance.SetForManPlayer();
        Player.instance.CurrentID = 1;
        TakeBackFigure("RobotBody", "RobotGun");
    }
    public void ChooseRobotButton()
    {
        FindObjectOfType<AudioManager>().Play("ChooseFirgure");
        animator = chooseFigure.GetComponent<Animator>();
        figAnimator = figure.GetComponent<Animator>();
        if (animator.GetBool("choosenMan") == true || animator.GetBool("isFinalMan") == true)
        {
            animator.SetBool("isFinalRobot", true);
            animator.SetBool("isFinalMan", false);
            animator.SetBool("choosenMan", false);
            figAnimator.SetInteger("choosen", 2);
        }
        else
        {
            animator.SetBool("choosenRobot", true);
            figAnimator.SetInteger("choosen", 2);
        }
        Player.instance.CurrentID = 2;
        Player.instance.SetForRobotPlayer();
        TakeBackFigure("manBody", "manGun");
    }

    public void PlayGame()
    {
        homeScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ClickButton");
        FindObjectOfType<AudioManager>().Stop("HomeBG");
        FindObjectOfType<AudioManager>().Play("GameplayBG");
    }
    
    public void FigureScreen()
    {
        homeScreen.SetActive(false);
        figureScreen.SetActive(true);
        animator = chooseFigure.GetComponent<Animator>();
        figAnimator = figure.GetComponent<Animator>();
        if ( Player.instance.CurrentID == 1 )
        {
            animator.SetBool("choosenMan", true);
            figAnimator.SetInteger("choosen", 1);
        }
        else if( Player.instance.CurrentID == 2 )
        {
            animator.SetBool("choosenRobot", true);
            figAnimator.SetInteger("choosen", 2);
        }
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }

    public void BackHomeFromFirgureScreen()
    {
        figureScreen.SetActive(false);
        homeScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
    public void BackHomeScrFromSoundScr()
    {
        soundScreen.SetActive(false);
        homeScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
   
    public void TakeBackFigure(string f1, string f2)
    {
        ObjectPooler.instance.TakeBackObject(f1);
        ObjectPooler.instance.TakeBackObject(f2);
    }
    public void BackHome()
    {
        int currentID = Player.instance.CurrentID;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TotalScore.instance.CurrentID = currentID;
        FindObjectOfType<AudioManager>().Play("ClickButton");
        FindObjectOfType<AudioManager>().Play("HomeBG");
        FindObjectOfType<AudioManager>().Stop("GameplayBG");
    }
    private void Update()
    {
        SetTextScore();
    }
    public void SetTextScore()
    {
        numScoreText.text = Player.instance.Score.ToString();
    }
    public void AwardScreen()
    {
        homeScreen.SetActive(false);
        awardScreen.SetActive(true);
        bestScoreText.text = TotalScore.instance.Score.ToString();
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
    public void BackHomeFromAwardScreen()
    {
        awardScreen.SetActive(false);
        homeScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }
    

}

