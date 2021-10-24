//-----------Final Score Display.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used to display the final score in the Game Over Screen
 * 
 * Revision History:
 * 1) created script
 * 2) upon start, get the static score from the static score class, then display it on screen
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The Final Score Display class
/// </summary>
public class FinalScoreDisplay : MonoBehaviour
{
    // get a reference to the TextMeshPro component
    private TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        // set the text to the score last logged into Static Score
        tmp.text = ": " + StaticScore.SetStaticScore.ToString();
    }

   
}
