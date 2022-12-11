
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static int lives = 3;
    public static int points = 0;
    public static bool playGame = true;

    public TMP_Text livesText;
    public TMP_Text scoreText;

    void Start() {
        lives = 3;
        points = 0;
        livesText.text = "Lives:" + lives;
        scoreText.text = "Score:" + points;
    }

    void Update() {
        livesText.text = "Lives:" + lives;
        scoreText.text = "Score:" + points;
    }
}