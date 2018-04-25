using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class tradeManager : MonoBehaviour {

    public Button buyOrSellPort;
    public Button hireOrFireCrew;
    public Button buyOrSellSilver;
    public Button buyOrSellPottery;

    public Button buyButton;
    public Button sellButton;

    string returnedGameData;
    string[] gameData;
    string[] scripts;
    string[] marketForces;

    public dataManager dm;

    public mainPanelManager mpm;

    public GameObject alertPanel;
    public Text alertText;

    public Button alertButton;

    string alertString;




    // Use this for initialization
    void Start () {

        alertPanel.SetActive(false);

        dm = new dataManager();
        mpm = new mainPanelManager();

        string returnedGameData = dm.returnGameData();
        gameData = returnedGameData.Split(',');

        string returnedScripts = dm.returnScripts();
        scripts = returnedScripts.Split(',');

        string returnedMarketForces = dm.returnMarketForces();
        marketForces = returnedMarketForces.Split(',');

        //Need to take the tax off your money OR receive money from the tax if you own it
        portTax();

        //Need to pay the crew (which is influenced by market forces)
        payCrew();
        		
	}

    public void portTax()
    {
        System.Random random = new System.Random();
        int randomNumber;

        //If you don't own the port
        if (mapManager.lastPortChosen.doYouOwnPort.Equals("no"))
        {

            //Need to do some checking if I do not have any money


            //Take money off your total if you don't own the port
            gameData[0] = (Int32.Parse(gameData[0]) - mapManager.lastPortChosen.portTax).ToString();

            //compile the alert string
            alertString = scripts[3] + mapManager.lastPortChosen.portTax;

        } else
        {
            //Add money to your total if you own the port
            int ran = randomNumber = random.Next(100, 5000);

            gameData[0] = (Int32.Parse(gameData[0]) + ran).ToString();

            //compile the alert string
            alertString = scripts[4] + (Int32.Parse(gameData[0]) + ran);
        }

        //Write it to the data file
        updateGameDataItems();

        //Update the Main Panel
        mpm.updateMainGamePanel();

    }

    public void showAlert()
    {
        alertPanel.SetActive(true);

        alertText.text = alertString;

    }


    public void updateGameDataItems()
    {

        string updatedGameData = "";

        //Compile the array back to a single string
        foreach (string gd in gameData)
        {
            updatedGameData = updatedGameData + gd + ",";
        }

        //Chop off the last comma
        int index = updatedGameData.LastIndexOf(',');
        updatedGameData = updatedGameData.Substring(0, index);

        //Then update the data file
        dm.writeToDataFile(updatedGameData);

    }


    public void payCrew()
    {

        System.Random random = new System.Random();

        int wages;

        //The more crew you have then the more you pay, and it is determined by marketForces
        //Get market forces
        if (marketForces[4].Equals("low"))
        {
            wages = Int32.Parse(gameData[2]) * random.Next(100, 300);
        } else if (marketForces[4].Equals("medium"))
        {
            wages = Int32.Parse(gameData[2]) * random.Next(400, 600);
        }
        else{
            wages = Int32.Parse(gameData[2]) * random.Next(400, 600);
        }

        //Compile the alert list for the last time
        alertString = alertString + scripts[5] + wages;


        //Write it to the data file
        updateGameDataItems();

        //Update the Main Panel
        mpm.updateMainGamePanel();

        //Show an alert with all of the money you made or lost
        showAlert();
        

    }

    public void closeAlert()
    {
        alertPanel.SetActive(false);
    }


    public void buySellPortButton()
    {

        //If you click the buy or sell button


        //Write it to the data file


        //Update the Main Panel
        mpm.updateMainGamePanel();

    }

    public void hireFireCrewButton()
    {
        //If you click the buy or sell button


        //Write it to the data file


        //Update the Main Panel
        mpm.updateMainGamePanel();

    }

    public void buySellSilverButton()
    {

        //If you click the buy or sell button


        //Write it to the data file


        //Update the Main Panel
        mpm.updateMainGamePanel();

    }

    public void buySellPotteryButton()
    {

        //If you click the buy or sell button

        

        //Write it to the data file


        //Update the Main Panel
        mpm.updateMainGamePanel();

    }

    


}
