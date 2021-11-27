using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int scoreToWin;

    public int curScore;

    public bool gamePaused;

    public static GameManager instance;

    void Awake(){
        instance = this;
    }

    void Start()
    {
        Time.timescale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")){
            TogglePauseGame();
        }   
    }

    public void TogglePauseGame(){
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused == true? 0.0f : 1.0f;

        GameUI.instance.TogglePuseMenu(gamePaused);
        curScore.lockState = gamePaused == true ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddScore(int score){
        curScore += score;
        gameUI.instance.UpdateScoreText(curScore);

        if(curScore >= scoreToWin){
            WinGame();
        }
    }

    private void WinGame(){
        GameUI.instance.SetEndGameScreen(true, curScore);
    }

    public void LoseGame(){
        GameUI.instance.SetEndGameScreen(false, curScore);
        Time.timescale = 0.0f;
        gamePauseed = true;
    }
}
