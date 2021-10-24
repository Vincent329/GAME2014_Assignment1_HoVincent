//-----------MultiplierDisplay.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is a utility class to display the score multiplier
 * 
 * Revision History:
 * 1) created script and set the text to update based on the value of the score multiplier

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The Multiplier Display class
/// </summary>
public class MultiplierDisplay : MonoBehaviour
{
    // get a reference to the Score class to get its multiplier variable
    [SerializeField] private Score scoreHandle;

    // get the text mesh pro component
    private TextMeshProUGUI textHandle;

    private void Start()
    {
        textHandle = GetComponent<TextMeshProUGUI>();
        scoreHandle = GameObject.FindObjectOfType<Score>();
    }

    /// <summary>
    /// Update the Multiplier display to reflect the current score multiplier
    /// </summary>
    public void UpdateHandle()
    {
        textHandle.text = scoreHandle.ScoreMultiplier.ToString("0.00"); // to string format for two decimal places
    }
}
