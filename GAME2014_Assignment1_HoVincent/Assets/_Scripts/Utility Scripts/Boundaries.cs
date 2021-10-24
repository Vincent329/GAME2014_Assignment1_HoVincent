//-----------Boundaries.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: October 23, 2021
 * 
 * Description: This script is a public serializable struct that other  classes can use to set their own boundaries based on an absolute Y value and absolute X value
 * 
 * Revision History:
 * 1) created script and public float values for the absolute Y and X values
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable Boundaries class that returns an absolute Y and absolute X value
/// What this
/// </summary>
[System.Serializable]
public struct Boundaries
{
    public float absY; // absolute value of vertical bounds, used to quantify both the top and bottom bounds
    public float absX; // absolute value of horizontal bounds, used to quantify both the left and right bounds
}
