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

    string[] ports;

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

    public InputField inputValue;

    public string buttonClicked;

    public static portTempValueManager currentPort;


    // Use this for initialization
    void Start () {

        alertPanel.SetActive(false);

        dm = new dataManager();

        getGameData();
        
        //mpm = new mainPanelManager();

        string returnedScripts = dm.returnScripts();
        scripts = returnedScripts.Split(',');

        string returnedMarketForces = dm.returnMarketForces();
        marketForces = returnedMarketForces.Split(',');

        string returnedPorts = dm.returnAllPorts();
        ports = returnedPorts.Split('_');

        //copy last port chosen over to current port
        cloneLastPortToCurrentPort();

        if (mapManager.doITakeMoney)
        {

            //Need to take the tax off your money OR receive money from the tax if you own it
            portTax();

            //Need to pay the crew (which is influenced by market forces)
            payCrew();

        }
        		
	}

    void getGameData(){

        //Clear it out
        //Array.Clear(gameData, 0, gameData.Length);
        //then refresh it
        string returnedGameData = dm.returnGameData();
        gameData = returnedGameData.Split(',');

    }


    void getPortData(){

        //Clear it out
        Array.Clear(ports, 0, ports.Length);
        //then refresh it
        string returnedPorts = dm.returnAllPorts();
        ports = returnedPorts.Split(',');

    }


    void cloneLastPortToCurrentPort(){

        //update the class
        currentPort = new portTempValueManager(
            mapManager.lastPortChosen.portName,
            mapManager.lastPortChosen.doYouOwnPort,
            mapManager.lastPortChosen.portTax,
            mapManager.lastPortChosen.chancePirates,
            mapManager.lastPortChosen.silverValue,
            mapManager.lastPortChosen.potteryValue,
            mapManager.lastPortChosen.portOwnValue);

        changeCurrentPort();

    }


    public void portTax()
    {
        System.Random random = new System.Random();
        int randomNumber;

        //If you don't own the port
        if (currentPort.doYouOwnPort.Equals("no"))
        {

            //Need to do some checking if I do not have any money


            //Take money off your total if you don't own the port
            gameData[0] = (Int32.Parse(gameData[0]) - currentPort.portTax).ToString();

            //compile the alert string
            alertString = scripts[3] + currentPort.portTax;

        } else
        {
            //Add money to your total if you own the port
            int ran = randomNumber = random.Next(100, 5000);

            gameData[0] = (Int32.Parse(gameData[0]) + ran).ToString();

            //compile the alert string
            alertString = " " + scripts[4] + ran;
        }

        //Write it to the data file
        writeDataToFile();

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
        writeDataToFile();

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
        inputValue.text = "";
        textOutput.text = "";

        //State in the description whether you own the port
        if (currentPort.doYouOwnPort.Equals("yes"))
        {
            textHeaderDescription.text = scripts[7];
        }
        else
        {
            //If you do not own the port
            textHeaderDescription.text = scripts[8];
        }

        //State the value of the port
        textValueDescription.text = scripts[9] +": "+currentPort.portOwnValue;

    }


    public void hireFireCrewButton()
    {

        buttonClicked = "crew";
        inputValue.text = "";
        textOutput.text = "";

        //State how many crew you own
        textHeaderDescription.text = gameData[2]+" "+scripts[13];

        //State crew are paid market rate
        textValueDescription.text = scripts[12];

    }




    public void clickedBuyButton(){

        //Firstly make sure they have entered a value
        if(inputValue.text != ""){

            //If they click buy when on the port screen
            if (buttonClicked.Equals("port")){

                //Get the cost of the product(s)
                int cost = Int32.Parse(inputValue.text) * currentPort.portOwnValue;

                //Check whether they have enough money
                if(Int32.Parse(gameData[0])-cost<0){

                    //You do not have enough money
                    textOutput.text = scripts[10];

                } else{

                    //They have enough money

                    //Data.MoneyOwned goes down
                    changeMoney(Int32.Parse(gameData[0])-cost);

                    //Data.Portsowned goes up by one
                    changePortsOwned(1);

                    //port.DoYouOwnPort changes to yes
                    changeDoYouOwnPort("yes");

                    //mapManager.lastPortChosen.doYouOwnPort goes to Yes
                    currentPort.doYouOwnPort = "yes";

                    //Update text output to Transaction confirmed
                    textOutput.text = scripts[11];

                }

                //update the text header as to whether you own the port
                //State in the description whether you own the port
                if (currentPort.doYouOwnPort.Equals("yes"))
                {
                    textHeaderDescription.text = scripts[7];
                }
                else
                {
                    //If you do not own the port
                    textHeaderDescription.text = scripts[8];
                }

            }

            if (buttonClicked.Equals("crew")){
            
            //Get the number of crew that want to be hired
            int crewHired = Int32.Parse(inputValue.text);

            //Add it to the total crew hired
            changeCrewHired(crewHired);

            //Update text output to Transaction confirmed
            textOutput.text = scripts[11];

            
            }


        

            


            //Update the Main Panel in all cases
            //mpm.updateMainGamePanel();
            updateMainGamePanel();


        }


    }


    void changeDoYouOwnPort(string own){

    for (int i = 0; i < ports.Length - 1; i++){

        if(ports[i].StartsWith(currentPort.portName)){

            //split it out
            string[] port = ports[i].Split(',');

            //upate the array value with the own string
            port[1]=own;

            //reconstitute then add it back to ports

            string updatedPort = "";

            //Compile the port array back to a single string
            foreach (string po in port)
            {
                updatedPort = updatedPort + po+",";
            }

            int index = updatedPort.LastIndexOf(',');
            updatedPort = updatedPort.Substring(0, index);

            //updatedPort = updatedPort+"_";
            Debug.Log("I am here2"+updatedPort);

            //Add it back to the ports array
            ports[i] = updatedPort;

            
            string updatedPorts = "";

            //Compile the array back to a single string
            foreach (string pos in ports)
            {
                updatedPorts = updatedPorts + pos+"_";
            }

            //Chop off the last underscore
            int index2 = updatedPorts.LastIndexOf('_');
            updatedPorts = updatedPorts.Substring(0, index2);
            
            Debug.Log("I am here3"+updatedPorts);
            
            //Then update the data file
            dm.writeToPortFile(updatedPorts);

            //refresh the game data array
            getPortData();

        }



    }



    }


    void changeMoney(int money){

        //Save the last port chosen into the game Data array
        gameData[0]=money.ToString();
        
        writeDataToFile();

    }


    void changeCrewHired(int crew){

        //Save the last port chosen into the game Data array
        gameData[2]=(Int32.Parse(gameData[2]) + crew).ToString();
        
        writeDataToFile();

    }

    void changePortsOwned(int owned){

        gameData[1] = (Int32.Parse(gameData[1]) + owned).ToString();

        writeDataToFile();

    }


    void changeCurrentPort(){

        //update the data file with the current port
        gameData[5] = currentPort.portName;

        //Write it to the data file
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

        //refresh the game data array
        getGameData();

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
