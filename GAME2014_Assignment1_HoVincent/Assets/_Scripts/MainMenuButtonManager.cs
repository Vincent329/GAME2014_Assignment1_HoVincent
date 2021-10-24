//------------MainMenuButtonManager.cs------------
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
 * 5) Commenting and documenting function and variable descriptions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtonManager : MonoBehaviour
{
    // Main Menu elements
    public GameObject PlayButton;
    public GameObject InstructionButton;
    public GameObject BackButton;
    public GameObject Avatar;
    public TextMeshProUGUI Title;

    // Instruction Screen elements
    public GameObject InstructionsPanels;
    public GameObject DarkenPanel;

    private AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // makes sure that only in the main menu affects these buttons
        // if the current scene is the main menu, initialize game objects
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

    // Loads the main menu scene
    public void loadMenuScene()
    {
        audioSource.PlayOneShot(audioSource.clip);
        SceneManager.LoadScene((int)Menus.MAINMENU);
    }

    // loads the game screen
    public void loadGameScene()
    {
        audioSource.PlayOneShot(audioSource.clip);
        SceneManager.LoadScene((int)Menus.GAME);
    }

    // loads the game over scene
    public void loadGameOverScene()
    {
        SceneManager.LoadScene((int)Menus.GAMEOVER);
    }

    // Turns on the instructions screen by disabling the Main Menu elements
    // and toggling on the instruction screen elements
    public void EnableInstruction()
    {
        audioSource.PlayOneShot(audioSource.clip);

        PlayButton.SetActive(false);
        InstructionButton.SetActive(false);
        BackButton.SetActive(true);
        Avatar.SetActive(false);
        Title.enabled = false;

        InstructionsPanels.SetActive(true);
        DarkenPanel.SetActive(true);

    }

  // Turns on the main menu screen by enabling the Main Menu elements
  // and toggling off the instruction screen elements
    public void DisableInstruction()
    {
        audioSource.PlayOneShot(audioSource.clip);

        PlayButton.SetActive(true);
        InstructionButton.SetActive(true);
        BackButton.SetActive(false);
        Avatar.SetActive(true);
        Title.enabled = true;

        InstructionsPanels.SetActive(false);
        DarkenPanel.SetActive(false);

    }

    // exiting the application
    public void QuitGame()
    {
        audioSource.PlayOneShot(audioSource.clip);

        Application.Quit();
    }    

}

// Enumerator for monitoring the different scene indices for level loading
enum Menus
{
    MAINMENU = 0,
    GAME = 1,
    GAMEOVER = 2
}