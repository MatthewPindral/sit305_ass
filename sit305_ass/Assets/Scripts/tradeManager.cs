using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class tradeManager : MonoBehaviour {

    public Button buttonBuyOrSellPort;
    public Button ButtonHireOrFireCrew;
    public Button ButtonBuyOrSellSilver;
    public Button buttonBuyOrSellPottery;

    public Text textHeaderDescription;

    public Text textValueDescription;

    public Text textOutput;

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

    public Text money;
    public Text portsOwned;
    public Text crewHired;
    public Text silverItemsOwned;
    public Text potteryItemsOwned;

    public InputField inputBuy;
    public InputField inputSell;

    public string buttonClicked;


    // Use this for initialization
    void Start () {

        alertPanel.SetActive(false);

        dm = new dataManager();
        //mpm = new mainPanelManager();

        string returnedGameData = dm.returnGameData();
        gameData = returnedGameData.Split(',');

        string returnedScripts = dm.returnScripts();
        scripts = returnedScripts.Split(',');

        string returnedMarketForces = dm.returnMarketForces();
        marketForces = returnedMarketForces.Split(',');


        if (mapManager.doITakeMoney)
        {

            //Need to take the tax off your money OR receive money from the tax if you own it
            portTax();

            //Need to pay the crew (which is influenced by market forces)
            payCrew();

        }
        		
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
            alertString = " " + scripts[4] + (Int32.Parse(gameData[0]) + ran);
        }

        //Write it to the data file
        updateGameDataItems();

        //Update the Main Panel
        updateMainGamePanel();

    }

    public void payCrew()
    {

        System.Random random = new System.Random();

        int wages;

        //The more crew you have then the more you pay, and it is determined by marketForces
        //Get market forces
        if (marketForces[4].Equals("low"))
        {
            wages = Int32.Parse(gameData[2]) * random.Next(50, 100);
        }
        else if (marketForces[4].Equals("medium"))
        {
            wages = Int32.Parse(gameData[2]) * random.Next(100, 300);
        }
        else
        {
            wages = Int32.Parse(gameData[2]) * random.Next(400, 600);
        }

        //take those wages off your money
        gameData[0] = (Int32.Parse(gameData[0]) - wages).ToString();


        //If they still have money on the bank
        if (Int32.Parse(gameData[0]) > 0)
        {
            //Compile the alert list for the last time
            alertString = alertString + " " + scripts[5] + wages;

        }
        else
        {
            //If they havegone into negative
            alertString = scripts[6];

        }

        //Write it to the data file
        updateGameDataItems();

        //Update the Main Panel
        updateMainGamePanel();

        //Show an alert with all of the money you made or lost
        showAlert();

    }

    public void updateMainGamePanel()
    {
        dm = new dataManager();
        //Read the game data file
        returnedGameData = dm.returnGameData();

        //Split it by commas
        gameData = returnedGameData.Split(',');

        //Update the main game panel text boxes
        money.text = gameData[0];
        portsOwned.text = gameData[1];
        crewHired.text = gameData[2];
        silverItemsOwned.text = gameData[3];
        potteryItemsOwned.text = gameData[4];
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

    public void closeAlert()
    {
        //If they still have money
        if (Int32.Parse(gameData[0]) > 0)
        {
            alertPanel.SetActive(false);
        }else
        {
            //If you dont have money then it takes you back to the main scene and resets the game save
            dm.writeDataResetToFile();

            SceneManager.LoadScene("sceneMain", LoadSceneMode.Single);
        }
    }

    public void buySellPortButton()
    {

        buttonClicked = "port";

        string[] allPorts;
        string returnedAllPorts = dm.returnAllPorts();
        allPorts = returnedAllPorts.Split(',');

        string[] scripts;
        string returnedScripts = dm.returnScripts();
        scripts = returnedScripts.Split(',');


        //State in the description whether you own the port
        if (mapManager.lastPortChosen.doYouOwnPort.Equals("yes"))
        {
            textHeaderDescription.text = scripts[7];
        }
        else
        {
            //If you do not own the port
            textHeaderDescription.text = scripts[8];
        }

        //State the value of the port
        textValueDescription.text = scripts[9] +": "+mapManager.lastPortChosen.portOwnValue;

    }


    public void clickedBuyButton(){

        //If they click buy when on teh port screen
        if (buttonClicked.Equals("port")){

            //Check whether they have enough money
            if(Int32.Parse(gameData[0])-mapManager.lastPortChosen.portOwnValue<0){

                //You do not have enough money
                textOutput.text = scripts[10];

            } else{

                //They do have enough money

                //Data.MoneyOwned goes down
                changeMoney(Int32.Parse(gameData[0])-mapManager.lastPortChosen.portOwnValue);

                //Data.Portsowned goes up by one
                changePortsOwned(1);

                //port.DoYouOwnPort changes to yes
                //HERE


                //mapManager.lastPortChosen.doYouOwnPort goes to Yes


                //Update the Main Panel
                //mpm.updateMainGamePanel();
                updateMainGamePanel();


            }

        }



    }

    void changeMoney(int money){

        //Save the last port chosen into the game Data array
        gameData[0]=money.ToString();
        
        writeDataToFile();

    }

    void changePortsOwned(int owned){

        gameData[1] = gameData[1] + owned;

        writeDataToFile();

    }


    void writeDataToFile(){

        string updatedGameData = "";

        //Compile the array back to a single string
        foreach (string gd in gameData)
        {
            updatedGameData = updatedGameData + gd+",";
        }

        //Chop off the last comma
        int index = updatedGameData.LastIndexOf(',');
        updatedGameData = updatedGameData.Substring(0, index);

        //Then update the data file
        dm.writeToDataFile(updatedGameData);

    }










    public void hireFireCrewButton()
    {
        //If you click the buy or sell button


        //Write it to the data file


        //Update the Main Panel
        //mpm.updateMainGamePanel();
        updateMainGamePanel();

    }

    public void buySellSilverButton()
    {

        //If you click the buy or sell button


        //Write it to the data file


        //Update the Main Panel
        //mpm.updateMainGamePanel();
        updateMainGamePanel();

    }

    public void buySellPotteryButton()
    {

        //If you click the buy or sell button

        

        //Write it to the data file


        //Update the Main Panel
        //mpm.updateMainGamePanel();
        updateMainGamePanel();

    }

    


}
