using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
	public int index;
	public int score;
	public string name;

	public void SaveScore ()
	{
		index = 1;
		score = 200;
		name = "ABC";
		PlayerPrefs.SetInt ("Place",index);
		PlayerPrefs.SetInt ("Score",score);
		PlayerPrefs.SetString ("Name",name);
		PlayerPrefs.Save();
	}

	public void LoadScore ()
	{
		index = PlayerPrefs.GetInt ("Place");
		score = PlayerPrefs.GetInt ("Score");
		name = PlayerPrefs.GetString ("Name");
	}

}