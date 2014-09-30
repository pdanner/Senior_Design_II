using UnityEngine;
using System.Collections;

public class SettingsScript : MonoBehaviour {

	private GUISkin skin;
	
	void Start(){
		skin = Resources.Load ("GUISkin") as GUISkin;
	}
	
	void OnGUI()
	{
		const int buttonWidth = 84;
		const int buttonHeight = 60;
		
		GUI.skin = skin;
		// Draw a button to start the game
		if (
			GUI.Button (
			// 1/3 from left in X, 2/3 of the height in Y
			new Rect (Screen.width / 3 - (buttonWidth / 2),
		          (2 * Screen.height / 3) - (buttonHeight / 2),
		          buttonWidth, buttonHeight),
			"Menu")
			) {
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel ("Menu");
		}
	}
}
