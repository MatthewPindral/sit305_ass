using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dataManagement : MonoBehaviour {

public InputField gameData;

public InputField portData;

public Button updateGameData;
public Button updatePortData;

public dataManager dm;


	// Use this for initialization
	void Start () {

		dm = new dataManager();

		gameData.text = dm.returnWholeGameData();
		portData.text = dm.returnWholePorts();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//get the text from the input field and write it to the game data file

	public void clickGameDataUpdateButton(){

		dm.writeWholeDataFile(gameData.text);
	}

	//get the text from the input field and write it to the port data file
	public void clickPortUpdateButton(){

		dm.writeWholePortFile(portData.text);

		
	}

	//If the data is screwed up then you can reset it
	public void resetAllData(){

		    dm.writeDataResetToFile();
            dm.writePortResetToFile();
            dm.writeMarketForcesResetToFile();

			//update the input fields
			gameData.text = dm.returnWholeGameData();
			portData.text = dm.returnWholePorts();

	}

	//Go back to main screen
	public void returnToMainScreen(){

		SceneManager.LoadScene("sceneMain", LoadSceneMode.Single);
	}



}
