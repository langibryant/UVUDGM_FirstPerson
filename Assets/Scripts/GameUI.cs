using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public Image healthBarFill;

    [Header("Pause Menu")]
    public GameObject pauseMenu;

    [Header("End Game Screen")]
    public GameObject endGameScreen;
    public TextMeshProUGUI endGameHeaderText;
    public TextMeshProUGUI endGameScoreText;


    public static GameUI instance;

    void Awake(){
        instance = this;
    }

    public void UpdateHealthBar(int curHP, int maxHP){
        healthBarFill.fillAmount = (float)curHP / (float)maxHP;
    }

    public void UpdateScoreText(int score){
        scoreText.text = "Score: " + score;
    }

    public void UpdateAmmoText(int currAmmo, int maxAmmo){
        ammoText.text = "Ammo: " + currAmmo + " / " + maxAmmo;
    }

    public void TogglePauseMenu(bool paused){
        pauseMenu.SetActive(paused);
    }

    public void SetEndGame(bool won, int score){
        endGameScore.SetActive(true);
        endGameHeaderText.text = won == true ? "You win" : "You Lose";
        endGameHeaderText.color = won == true? Color.green : Color.red;
        endGameScoreText.text = "<b>Score</b>: " + score;
    }

    public void OnResumeButton(){
        GameManager.instance.TogglePauseGame();
    }

    public void SetEndGameScreen(bool won, int score){
        endGameScreen.SetActive(true);
        endGameHeaderText.text = won == true ? "You Win" : "You lose";
        endGameHeaderText.color = won == true ? Color.green : Color.red;
        endGameScoreText.text = "<b>Score</b>: " + score;
    }

    public void OnRestartButton(){
        SceneManager.LoadScene("Game");
    }

    public void OnMenuButton(){
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
