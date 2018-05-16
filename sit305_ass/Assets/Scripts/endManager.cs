using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endManager : MonoBehaviour {

    public dataManager dm;
	public Button endButton;

	// Use this for initialization
	void Start () {

		dm = new dataManager();
		
	}
	


	public void clickEndButton(){

		    //If you dont have money then it takes you back to the main scene and resets the game save
            dm.writeDataResetToFile();
            dm.writePortResetToFile();
            dm.writeMarketForcesResetToFile();

            SceneManager.LoadScene("sceneMain", LoadSceneMode.Single);


	}


}
