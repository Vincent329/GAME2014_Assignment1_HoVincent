using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private int scoreValue;
    [SerializeField] private float scoreMultiplier;

    private bool activeGame;

    private float scoreAccrue;
    private TextMeshProUGUI text;

    public int ScoreValue
    {
        get => scoreValue;
        set
        {
            scoreValue = value;
        }
    }

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
        text = GetComponent<TextMeshProUGUI>();
        scoreMultiplier = 1.0f;
        scoreAccrue = 0;
        activeGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeGame)
        {
            scoreAccrue += Time.deltaTime * scoreMultiplier;
            if (scoreAccrue >= 1.0f)
            {
                scoreValue++;
                scoreAccrue = 0;
                UpdateScore();
            }
        }
    }

    public void UpdateScore()
    {
        text.text = ": " + scoreValue;
        StaticScore.SetStaticScore = scoreValue;
    }

    public void ResetScore()
    {
        scoreValue = 0;
        UpdateScore();
    }

    
}
