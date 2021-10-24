//-----------ExcitementBar.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 21, 2021
 * 
 * Description: This script is used as a handler for the excitement mechanic,
 * The longer the player lives, this bar will gradually fill, for every time the bar reaches maximum value, the score multiplier increases, but so does enemy sspawn rate
 * 
 * Revision History:
 * 1) created script, initialized variables and create references to game objects with necessary components
 * 2) Create a function to update the bar's value, as well as reset it
 * 3) Create a condition where if the bar's topped out, reset the bar, and increase respective multipliers for the score and enemy spawner
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// the excitement bar class
/// </summary>
public class ExcitementBar : MonoBehaviour
{
    [Range(0.1f, 10.0f)]
    [SerializeField] private float sliderGainRate;                  // the rate at which the slider increases
    [SerializeField] private Slider exciteSlider;                   // get a component to the slider
    [SerializeField] private EnemySpawner enemySpawner;             // get a reference to the enemy spawner
    [SerializeField] private MultiplierDisplay multDisplayHandle;   // get a reference to display the score multiplier
    [SerializeField] private Score scoreObject;                     // get a reference to update the score's UI

    void Start()
    {
        // get the necessary components
        exciteSlider = GetComponent<Slider>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        scoreObject = GameObject.FindObjectOfType<Score>();
        multDisplayHandle = GameObject.FindObjectOfType<MultiplierDisplay>();

        // initialize slider values
        exciteSlider.value = 0;
        exciteSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // while the slider hasn't topped out yet
        if (exciteSlider.value < exciteSlider.maxValue)
        {
            MonitorBar();
        } 
        // otherwise increase respective multipliers and reset the bar's display
        else
        {
            // now not only will score multiplier be increase, but spawn rate of enemies will increase too
            scoreObject.ScoreMultiplier += 0.25f;
            enemySpawner.SpawnRate += 0.5f;
            ResetBar();
            multDisplayHandle.UpdateHandle();
        }
    }

    /// <summary>
    /// called during update to increase the slider's value
    /// </summary>
    void MonitorBar()
    {
        exciteSlider.value += Time.deltaTime * sliderGainRate;
    }
    
    /// <summary>
    /// resets the bar to 0
    /// </summary>
    void ResetBar()
    {
        exciteSlider.value = 0;
    }
}
