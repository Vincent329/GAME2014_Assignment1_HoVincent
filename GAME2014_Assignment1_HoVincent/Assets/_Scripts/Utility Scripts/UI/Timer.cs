//-----------Timer.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used to display the timer in the game
 * 
 * Revision History:
 * 1) created script 
 * 2) Tried to set separate values for both minutes and seconds
 * 3) retooled the display so that we have a singular float that increases via Time.time
 * 4) created two variables for minutes and seconds that change depending on the current value of the time elapsed variable

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The Timer class
/// </summary>
public class Timer : MonoBehaviour
{
    // one float for time elapsed
    [SerializeField] private float timeElapsed; 

    // Getter for elapsed time
    public float GetElapsedTime
    {
        get => timeElapsed;
    }

    private TextMeshProUGUI tmp; // the text mesh pro component of the Timer

    void Start()
    {
        // get the component for the TMProUGUI and set the time elapsed to 0 on start
        tmp = GetComponent<TextMeshProUGUI>();
        timeElapsed = 0;
    }

    /// <summary>
    /// using fixed timestep update for consistent timing
    /// </summary>
    void FixedUpdate()
    {
        CheckTime();
    }

    /// <summary>
    /// increase the time based on time.deltatime
    /// </summary>
    private void CheckTime()
    {
        // increase timeElapsed by time.deltaTime
        timeElapsed += Time.deltaTime;

        // two variables
        int minutes = Mathf.FloorToInt(timeElapsed / 60); // using Unity's floor to int functionality
        int seconds = Mathf.FloorToInt(timeElapsed % 60); //  https://docs.unity3d.com/ScriptReference/Mathf.FloorToInt.html
        tmp.text = ": " + minutes.ToString() + ":" + seconds.ToString("00"); // string format functionality found in microsoft documentation
        
    }
}
