using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

	public InputField initials;

	public string submit()
	{	
		return initials.text;
	}
}
