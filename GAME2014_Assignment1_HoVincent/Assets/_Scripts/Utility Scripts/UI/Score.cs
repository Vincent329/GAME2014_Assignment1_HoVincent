//-----------Score.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used to display the score in the game
 * 
 * Revision History:
 * 1) created script 
 * 2) Get the score value

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// the score class
/// </summary>
public class Score : MonoBehaviour
{

    [SerializeField] private int scoreValue;  // the score value
    [SerializeField] private float scoreMultiplier; // the score multiplier

    private bool activeGame;

    private float scoreAccrue;  // this variable works like the spawn timer, added by scoremultiplier * Time.deltaTime
    private TextMeshProUGUI scoreText; // the text mesh pro component

    // getter and setter for the score value to be altered in different classes
    public int ScoreValue
    {
        get => scoreValue;
        set
        {
            scoreValue = value;
        }
    }

    /// <summary>
    /// getter and setter for the score multiplier to be altered in different classes
    /// </summary>
    public float ScoreMultiplier
    {
        get => scoreMultiplier;
        set
        {
            scoreMultiplier = value;
        }
    }

    private void Awake()
    {
        int ScoreCount = FindObjectsOfType<Score>().Length;
        if (ScoreCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreMultiplier = 1.0f;
        scoreAccrue = 0;
        activeGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        // while the game is active
        if (activeGame)
        {
            // score timer is increased via Time.deltaTime * score multiplier
            scoreAccrue += Time.deltaTime * scoreMultiplier;

            // once the store accrue reaches past this threshold, we increment the score and reset the score accrue timer
            if (scoreAccrue >= 1.0f)
            {
                scoreValue++;
                scoreAccrue = 0;
                UpdateScore();
            }
        }
    }

    /// <summary>
    /// Update the UI based on the current score value
    /// </summary>
    public void UpdateScore()
    {
        scoreText.text = ": " + scoreValue;
        StaticScore.SetStaticScore = scoreValue;
    }

    /// <summary>
    /// resets the score
    /// </summary>
    public void ResetScore()
    {
        scoreValue = 0;
        UpdateScore();
    }

    
}
