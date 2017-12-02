using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas helpMenu;
    public Canvas creditsMenu;
    public Button startText;
    public Button exitText;
    public Button helpText;
    public Button creditsText;
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
        quitMenu.enabled = false;
        helpMenu.enabled = false;
        creditsMenu.enabled = false;
    }
	
	public void ExitPress()
    {
        creditsMenu.enabled = false;
        creditsText.enabled = false;
        quitMenu.enabled = true;
        helpMenu.enabled = false;
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
        startText.enabled = true;
        exitText.enabled = true;
        helpText.enabled = true;
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
