using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainManager : MonoBehaviour {

    public Button startNewButton;
    public Button continueButton;
    public Button manageGameDataButton;
    public Button exitGameButton;

    dataManager dm;


	// Use this for initialization
	void Start () {

        dm = new dataManager();

    }
	

    public void startNewGame()
    {

        dm.writeDataResetToFile();
        dm.writePortResetToFile();

        SceneManager.LoadScene("sceneMap", LoadSceneMode.Single);

    }

    public void continueGame()
    {
        SceneManager.LoadScene("sceneMap", LoadSceneMode.Single);
    }

    public void manageGameData()
    {
        SceneManager.LoadScene("sceneTrade", LoadSceneMode.Single);
    }

    public void exitGame()
    {
        //Exit application
        Application.Quit();

    }





}
