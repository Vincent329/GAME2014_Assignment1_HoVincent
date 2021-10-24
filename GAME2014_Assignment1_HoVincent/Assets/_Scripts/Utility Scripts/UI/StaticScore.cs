//-----------StaticScore.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is used to store a publicly available static score that's pervasive across different scenes
 * Anytime score is updated, we set that score over to this public static variable
 * Once game is over, the Final score handle in that scene will get the score saved in this class to display
 * 
 * Revision History:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the static score class
/// </summary>
public class StaticScore
{
    public static int SetStaticScore;
}