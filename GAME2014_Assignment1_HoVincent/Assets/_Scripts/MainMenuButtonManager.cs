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
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtonManager : MonoBehaviour
{
    public Button PlayButton;
    public Button InstructionButton;
    public Button BackButton;
    public GameObject Avatar;
    public TextMeshProUGUI Title;
   
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.enabled = true;
        InstructionButton.enabled = true;
        BackButton.enabled = false;

        PlayButton.GetComponent<Image>().enabled = true;
        InstructionButton.GetComponent<Image>().enabled = true;
        BackButton.GetComponent<Image>().enabled = false;
        Avatar.SetActive(true);
        Title.enabled = true;
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene((int)Menus.GAME);
    }

    public void EnableInstruction()
    {
        PlayButton.enabled = false;
        InstructionButton.enabled = false;
        BackButton.enabled = true;

        PlayButton.GetComponent<Image>().enabled = false;
        InstructionButton.GetComponent<Image>().enabled = false;
        BackButton.GetComponent<Image>().enabled = true;
        Avatar.SetActive(false);
        Title.enabled = false;
    }

    public void DisableInstruction()
    {
        PlayButton.enabled = true;
        InstructionButton.enabled = true;
        BackButton.enabled = false;

        PlayButton.GetComponent<Image>().enabled = true;
        InstructionButton.GetComponent<Image>().enabled = true;
        BackButton.GetComponent<Image>().enabled = false;
        Avatar.SetActive(true);
        Title.enabled = true;
    }



}
enum Menus
{
    MAINMENU = 0,
    GAME = 1,
    GAMEOVER = 2
}