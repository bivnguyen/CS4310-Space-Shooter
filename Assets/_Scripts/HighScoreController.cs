using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
	private List<Scores> highScores;  //High score list
	private int place;  //What place on the leader board did the player score
	private int newScore; //New high score to be recorded
	private string newName; //New player name to be recorded

	public int listLength;  // Set number of scores saved, default to 10
	public InputField inputBox; //Player name input gui box
	public Button submitButton;	//Name submit button
	public Text highScoreText;	//Text box to display high scores


	//Button controller function to be attached to submit button canvas member
	public void submitButtonController ()
	{
		newName = inputBox.text; // Set new name to contents of input box
		submitButton.gameObject.SetActive (false); //Remove input box and submit button
		inputBox.gameObject.SetActive (false);
		SaveScores ();
		LoadScores ();
		PrintScores ();
	}

	//Display input GUI elements
	public void DisplayScoreInput ()
	{
		submitButton.gameObject.SetActive (true);
		inputBox.gameObject.SetActive (true);
	}

	//Insert new high score into place and save score list
	public void SaveScores ()
	{
		Scores temp = new Scores ();
		temp.score = newScore;
		temp.name = newName;

		highScores.Insert ((place-1), temp);

		for (int i = 0; i < highScores.Count; i++) {
			Debug.Log ("Save temp name = " + temp.name + " save temp score = " + temp.score);
			PlayerPrefs.SetInt ("Score"+(i+1),highScores[i].score);
			PlayerPrefs.SetString ("Name"+(i+1),highScores[i].name);
		}
		PlayerPrefs.Save();


	}

	//Load saved scores into list
	public void LoadScores ()
	{	
		highScores = new List<Scores> ();
		int i = 1;

		while (i <= listLength && PlayerPrefs.HasKey ("Score"+i)) {
			Scores temp = new Scores ();
			temp.score = PlayerPrefs.GetInt ("Score"+i);
			temp.name = PlayerPrefs.GetString ("Name"+i);
			highScores.Add (temp);
			i++;
		}

	}

	//Display high scores into text box GUI element
	public void PrintScores ()
	{
		string scoreList = "High Scores\n\n";

		for (int i = 0; i < highScores.Count; i++) {
			scoreList += i+1 + ".\t" + highScores[i].name + "\t\t" + highScores[i].score +  "\n";
		}
		highScoreText.text = scoreList;
	}

	//test if player's score is a high score and save the place on the list if so
	public bool isHighScore (int playerScore)
	{
		bool isHighScore = false;

		for (int i = 0; i < highScores.Count && !isHighScore; i++) {
			if (playerScore > highScores [i].score) {
				isHighScore = true;
				place = i+1;
				newScore = playerScore;
			}
		}

		if ((highScores.Count < listLength) && isHighScore == false && playerScore > 0) {
			place = highScores.Count + 1;
			newScore = playerScore;
			isHighScore = true;
		}

		return isHighScore;
	}

}


//Score element of list
public class Scores
{
	public int score;
	public string name;
}