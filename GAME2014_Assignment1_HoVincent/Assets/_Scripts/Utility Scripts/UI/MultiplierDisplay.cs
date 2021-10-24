//-----------MultiplierDisplay.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is a utility class to display the score multiplier
 * 
 * Revision History:
 * 1) created script

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplierDisplay : MonoBehaviour
{
    [SerializeField] private Score scoreHandle;
    private TextMeshProUGUI textHandle;

    private void Start()
    {
        textHandle = GetComponent<TextMeshProUGUI>();
        scoreHandle = GameObject.FindObjectOfType<Score>();
    }

    public void UpdateHandle()
    {
        textHandle.text = scoreHandle.ScoreMultiplier.ToString("0.00");
    }
}
