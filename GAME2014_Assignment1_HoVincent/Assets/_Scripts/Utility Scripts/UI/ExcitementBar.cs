using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExcitementBar : MonoBehaviour
{
    [Range(0.1f, 10.0f)]
    [SerializeField] private float sliderGainRate;
    // Start is called before the first frame update
    [SerializeField] private Slider exciteSlider;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private MultiplierDisplay multDisplayHandle;

    private Score scoreObject;

    void Start()
    {
        exciteSlider = GetComponent<Slider>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        scoreObject = GameObject.FindObjectOfType<Score>();
        multDisplayHandle = GameObject.FindObjectOfType<MultiplierDisplay>();
        exciteSlider.value = 0;
        exciteSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (exciteSlider.value < exciteSlider.maxValue)
        {
            MonitorBar();
        } else
        {
            // now not only will score multiplier be increase, but spawn rate of enemies will increase too
            scoreObject.ScoreMultiplier += 0.25f;
            enemySpawner.SpawnRate += 0.5f;
            ResetBar();
            multDisplayHandle.UpdateHandle();
        }
    }

    void MonitorBar()
    {
        exciteSlider.value += Time.deltaTime * sliderGainRate;
    }
    
    void ResetBar()
    {
        exciteSlider.value = 0;
    }
}
