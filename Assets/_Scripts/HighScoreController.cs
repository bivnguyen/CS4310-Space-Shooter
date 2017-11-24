using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
	private List<Scores> highScores;
	private int place;  //What place on the leader board did the player score
	private int newScore; //New high score to be recorded

	public int listLength;  // Set number of scores saved, default to 10
	public InputField inputBox;
	public Button submitButton;
	public Text highScoreText;

	public void submitButtonController ()
	{
		name = inputBox.text;
		PlayerPrefs.SetInt ("Score"+place,newScore);
		PlayerPrefs.SetString ("Name"+place,name);
		PlayerPrefs.Save();
		submitButton.gameObject.SetActive (false);
		inputBox.gameObject.SetActive (false);

		LoadScores ();
		PrintScores ();
	}

	public void SaveScores ()
	{
		submitButton.gameObject.SetActive (true);
		inputBox.gameObject.SetActive (true);
	}

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

		//for testing purposes only

//		Scores temp1 = new Scores ();
//
//		temp1.score = 100;
//		temp1.name = "JTL";
//
//		highScores.Add (temp1);

	}

	public void PrintScores ()
	{
		string scoreList = "";

		for (int i = 0; i < highScores.Count; i++) {
			scoreList += i+1 + ".\t" + highScores[i].name + "\t\t" + highScores[i].score +  "\n";
		}
		highScoreText.text = scoreList;
	}

	public bool isHighScore (int playerScore)
	{
		bool isHighScore = false;

		if (highScores.Count = 0) {
			isHighScore = true;
			place = 1;
		}
		for (int i = 0; i < highScores.Count; i++) {
			if (playerScore >= highScores [i].score) {
				isHighScore = true;
				place = i+1;
				newScore = playerScore;
			}
		}

		return isHighScore;
	}

}



public class Scores
{
	public int score;
	public string name;
}