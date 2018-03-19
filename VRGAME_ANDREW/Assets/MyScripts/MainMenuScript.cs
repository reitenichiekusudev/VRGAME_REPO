using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	[SerializeField] Canvas mainCanvas;
	[SerializeField] Canvas instructionsCanvas;
	[SerializeField] Button startButton;
	[SerializeField] Button instructionsButton;
	[SerializeField] Button instructionsReturnButton;
	[SerializeField] Button quitButton;
	private bool isMainScreen = true;
	// Use this for initialization
	void Start () {
		mainCanvas.gameObject.SetActive (true);
		instructionsCanvas.gameObject.SetActive (true);
		startButton.onClick.AddListener (GameStart);
		instructionsButton.onClick.AddListener (ScreenToggle);
		instructionsReturnButton.onClick.AddListener (ScreenToggle);
		quitButton.onClick.AddListener (GameEnd);
		instructionsCanvas.gameObject.SetActive (false);
	}

	void GameStart() {
		
		// Insert game scene index here
		SceneManager.LoadScene(1);

	}

	void ScreenToggle() {
		if (isMainScreen) {
			isMainScreen = false;
			mainCanvas.gameObject.SetActive (false);
			instructionsCanvas.gameObject.SetActive (true);

		} else {
			
			isMainScreen = true;
			mainCanvas.gameObject.SetActive (true);
			instructionsCanvas.gameObject.SetActive (false);
		}
	}

	void GameEnd() {
		Application.Quit();
	}
}
