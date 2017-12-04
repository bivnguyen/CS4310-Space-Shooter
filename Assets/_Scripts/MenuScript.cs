using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas helpMenu;
    public Canvas creditsMenu;
    public Canvas difficultyMenu;
    public Button startText;
    public Button exitText;
    public Button helpText;
    public Button creditsText;
    public Button easyText;
    public Button normalText;
    public Button hardText;

    // Use this for initialization
    void Start ()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        helpMenu = helpMenu.GetComponent<Canvas>();
        creditsMenu = creditsMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        helpText = helpText.GetComponent<Button>();
        creditsText = creditsText.GetComponent<Button>();
        easyText = easyText.GetComponent<Button>();
        normalText = normalText.GetComponent<Button>();
        hardText = hardText.GetComponent<Button>();
        quitMenu.enabled = false;
        helpMenu.enabled = false;
        creditsMenu.enabled = false;
        difficultyMenu.enabled = false;
    }
	
	public void ExitPress()
    {
        creditsMenu.enabled = false;
        creditsText.enabled = false;
        quitMenu.enabled = true;
        helpMenu.enabled = false;
        difficultyMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
        helpText.enabled = false;
    }

    public void HelpPress()
    {
        creditsMenu.enabled = false;
        creditsText.enabled = false;
        quitMenu.enabled = false;
        helpMenu.enabled = true;
        difficultyMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
        helpText.enabled = false;
    }

    public void CreditsPress()
    {
        creditsMenu.enabled = true;
        creditsText.enabled = false;
        quitMenu.enabled = false;
        helpMenu.enabled = false;
        difficultyMenu.enabled = false;
        startText.enabled = false;
        exitText.enabled = false;
        helpText.enabled = false;
    }

    public void NoPress()
    {
        creditsMenu.enabled = false;
        creditsText.enabled = true;
        quitMenu.enabled = false;
        helpMenu.enabled = false;
        difficultyMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
        helpText.enabled = true;
    }

    public void StartPress()
    {
        creditsMenu.enabled = false;
        creditsText.enabled = false;
        quitMenu.enabled = false;
        helpMenu.enabled = false;
        difficultyMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
        helpText.enabled = false;
    }

    public void EasyPress()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        StartLevel();
    }

    public void NormalPress()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        StartLevel();
    }

    public void HardPress()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        StartLevel();
    }
    public void StartLevel()
    {
        SceneManager.LoadSceneAsync("main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
