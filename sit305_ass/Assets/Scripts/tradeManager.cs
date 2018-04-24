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

    public dataManager dm;

    public mainPanelManager mpm;

    // Use this for initialization
    void Start () {

        dm = new dataManager();
        gameData = returnedGameData.Split(',');

        //Need to get the port that you have travelled to



        //Need to take the tax off your money OR receive money from the tax if you own it
        portTax();

        //Need to pay the crew (which is influenced by market forces)
        payCrew();
        		
	}

    public void portTax()
    {
        System.Random random = new System.Random();
        int randomNumber;

        if (mapManager.lastPortChosen.doYouOwnPort.Equals("no"))
        {
            //Take money off your total if you don;t own the port
            gameData[0] = (Int32.Parse(gameData[0]) - mapManager.lastPortChosen.portTax).ToString();
        }
        else
        {
            //Add money to your total if you own the port
            int ran = randomNumber = random.Next(100, 5000);

            gameData[0] = (Int32.Parse(gameData[0]) + ran).ToString();
        }

        //Write it to the data file



        //Update the Main Panel
        mpm.updateMainGamePanel();

    }

    public void payCrew()
    {
        //The more crew you have then the more you pay, and it is determined by marketForces


        //Write it to the data file

        
        //Update the Main Panel
        mpm.updateMainGamePanel();


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
