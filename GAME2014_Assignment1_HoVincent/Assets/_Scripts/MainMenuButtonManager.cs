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
 * 2) Filled script with functions that load specific levels based on enumeration (Sept. 29)
 * 3) Created functions to travel to different functions
 * 4) Implemented a condition where if the Game Manager GameObject exists in the Main Menu Scene, 
 * initialize the basic states of the attached variables
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtonManager : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject InstructionButton;
    public GameObject BackButton;
    public GameObject Avatar;
    public GameObject InstructionsPanels;
    public GameObject DarkenPanel;
    public TextMeshProUGUI Title;

    // Start is called before the first frame update
    void Start()
    {
        // makes sure that only in the main menu affects these buttons
        if (SceneManager.GetActiveScene().buildIndex == (int)Menus.MAINMENU)
        {
            PlayButton.SetActive(true);
            InstructionButton.SetActive(true);
            BackButton.SetActive(false);
            InstructionsPanels.SetActive(false);
            DarkenPanel.SetActive(false);

            Avatar.SetActive(true);
            Title.enabled = true;
        }
    }
    public void loadMenuScene()
    {
        SceneManager.LoadScene((int)Menus.MAINMENU);
    }
    public void loadGameScene()
    {
        SceneManager.LoadScene((int)Menus.GAME);
    }

    public void loadGameOverScene()
    {
        SceneManager.LoadScene((int)Menus.GAMEOVER);
    }

    public void EnableInstruction()
    {
        PlayButton.SetActive(false);
        InstructionButton.SetActive(false);
        BackButton.SetActive(true);
        InstructionsPanels.SetActive(true);
        DarkenPanel.SetActive(true);


        Avatar.SetActive(false);
        Title.enabled = false;
    }

    // Functions for the Return button on the instructions screen
    public void DisableInstruction()
    {
        PlayButton.SetActive(true);
        InstructionButton.SetActive(true);
        BackButton.SetActive(false);
        InstructionsPanels.SetActive(false);
        DarkenPanel.SetActive(false);

        Avatar.SetActive(true);
        Title.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }    

}
enum Menus
{
    MAINMENU = 0,
    GAME = 1,
    GAMEOVER = 2
}