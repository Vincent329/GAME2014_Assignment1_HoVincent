//------------Scene Manager.cs------------
/* Name: Vincent Ho
 * Student Number: 101334300
 * 
 * Date Last Modified: September 29, 2021
 * 
 * Description: This script is used as a manager class for level loading
 * An enumerator designates the available screens that the player can select, 
 * and the designated method will load the appropriate level based on the enumerator
 * 
 * Revision History:
 * 1) created script
 * 2) Filled script with functions that load specific levels based on enumeration 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    enum Menus
    {
        MAINMENU = 0,
        INSTRUCTIONS = 1,
        GAME = 2,
        GAMEOVER = 3
    }
}
