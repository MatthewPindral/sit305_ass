using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainPanelManager : MonoBehaviour{

    public dataManager dm;
    string returnedGameData;
    string[] gameData;
    public Text money;
    public Text portsOwned;
    public Text crewHired;
    public Text silverItemsOwned;
    public Text potteryItemsOwned;


    private void Start()
    {
        dm = new dataManager();
        updateMainGamePanel();
    }


    public void updateMainGamePanel()
    {
        //Read the game data file
        returnedGameData = dm.returnGameData();

        Debug.Log("I am here: "+ returnedGameData);

        //Split it by commas
        gameData = returnedGameData.Split(',');

        //Update the main game panel text boxes
        money.text = gameData[0];
        portsOwned.text = gameData[1];
        crewHired.text = gameData[2];
        silverItemsOwned.text = gameData[3];
        potteryItemsOwned.text = gameData[4];
    }




}
