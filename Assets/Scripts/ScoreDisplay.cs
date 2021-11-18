using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{

    TextMeshProUGUI scoreText;
    GameSession gameSession;
    [SerializeField] int maxScoreLength = 7;


    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ConvertToScore(gameSession.GetScore());
    }

    private string ConvertToScore(int score)
    {
        string amountOfZeros = "";
        for(int zeros = maxScoreLength-score.ToString().Length; zeros > 0; zeros--)
        {
            amountOfZeros += "0";
        }
        return amountOfZeros + score.ToString();
    }
}
