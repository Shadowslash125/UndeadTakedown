using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    [SerializeField] int score = 0;
    [SerializeField] int highScore;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0); // use "highScore" consistently
        scoreText.text = score + " Points";
        highScoreText.text = "High Score: " + highScore;
    }

    // Update is called once per frame
    public void AddPoint(int points)
    {
        score += points;
        scoreText.text = score + " Points";
        if (highScore < score)
        {
            highScore = score; // update the highScore variable
            PlayerPrefs.SetInt("highScore", score); // use "highScore" consistently
        }
        highScoreText.text = "High Score: " + highScore; // update the high score display
    }
}
