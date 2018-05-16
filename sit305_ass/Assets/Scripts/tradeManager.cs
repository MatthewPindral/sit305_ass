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

    void getMarketForces(){

        //Clear it out
        //Array.Clear(gameData, 0, gameData.Length);
        //then refresh it
        string returnedMarketForces = dm.returnMarketForces();
        marketForces = returnedMarketForces.Split(',');

    }


    void getPortData(){

        //Clear it out
        Array.Clear(ports, 0, ports.Length);
        //then refresh it
        string returnedPorts = dm.returnAllPorts();
        ports = returnedPorts.Split(',');

    }





    public void portTax()
    {
        System.Random random = new System.Random();
        int randomNumber;

        //If you don't own the port
        if (travelManager.currentPort.doYouOwnPort.Equals("no"))
        {

            //Need to do some checking if I do not have any money


            //Take money off your total if you don't own the port
            gameData[0] = (Int32.Parse(gameData[0]) - travelManager.currentPort.portTax).ToString();

            //compile the alert string
            alertString = scripts[3] + travelManager.currentPort.portTax;

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
            dm.writePortResetToFile();
            dm.writeMarketForcesResetToFile();

            SceneManager.LoadScene("sceneMain", LoadSceneMode.Single);
        }
    }

    public void buySellPortButton()
    {
        buttonClicked = "port";
        inputValue.text = "";
        textOutput.text = "";

        //State in the description whether you own the port
        if (travelManager.currentPort.doYouOwnPort.Equals("yes"))
        {
            textHeaderDescription.text = scripts[7];
        }
        else
        {
            //If you do not own the port
            textHeaderDescription.text = scripts[8];
        }

        //State the value of the port
        textValueDescription.text = scripts[9] +": "+travelManager.currentPort.portOwnValue;

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

    public void buySellSilverButton()
    {

        buttonClicked = "silver";
        inputValue.text = "";
        textOutput.text = "";

        //State how much silver you own
        textHeaderDescription.text = gameData[3]+" "+scripts[14];

        //State how much silver is valued at
        textValueDescription.text = scripts[9] +": "+travelManager.currentPort.silverValue;

    }

   public void buySellPotteryButton()
    {

        buttonClicked = "pottery";
        inputValue.text = "";
        textOutput.text = "";

        //State how much pottery you own
        textHeaderDescription.text = gameData[4]+" "+scripts[15];

        //State how much pottery is valued at
        textValueDescription.text = scripts[9] +": "+travelManager.currentPort.potteryValue;

    }

 
    public void clickedBuyButton(){

        //Firstly make sure they have entered a value
        if(inputValue.text != ""){

            //If they click buy when on the port screen
            if (buttonClicked.Equals("port")){

                //Get the cost of the product(s)
                int cost = Int32.Parse(inputValue.text) * travelManager.currentPort.portOwnValue;

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
                    travelManager.currentPort.doYouOwnPort = "yes";

                    //Update text output to Transaction confirmed
                    textOutput.text = scripts[11];

                }

                //update the text header as to whether you own the port
                //State in the description whether you own the port
                if (travelManager.currentPort.doYouOwnPort.Equals("yes"))
                {
                    textHeaderDescription.text = scripts[7];
                }
                else
                {
                    //If you do not own the port
                    textHeaderDescription.text = scripts[8];
                }

                //Update realestate market
                if(gameData[1].Equals("2")){
                    marketForces[5]="low";
                }if(gameData[1].Equals("3")){
                    marketForces[5]="medium";
                }if(gameData[1].Equals("4")){
                    marketForces[5]="high";
                }
                //Write Market forces to file
                writeMarketForcesToFile();

                //If they now own all ports then switch to the end screen

                if (gameData[1].Equals("5")){

                    SceneManager.LoadScene("sceneEnd", LoadSceneMode.Single);

                }


            }



            if (buttonClicked.Equals("crew")){
            
            //Get the number of crew that want to be hired
            int crewHired = Int32.Parse(inputValue.text);

            //Add it to the total crew hired
            changeCrewHired(crewHired);

            //Update text output to Transaction confirmed
            textOutput.text = scripts[11];

            //update the text header regarding how many crew you own
            textHeaderDescription.text = gameData[2]+" "+scripts[13];

            //Update wage market
            if(gameData[2].Equals("4")){
                marketForces[4]="low";
            }if(gameData[2].Equals("5")){
                marketForces[4]="medium";
            }if(gameData[2].Equals("6")){
                marketForces[4]="high";
            }
            //Write Market forces to file
            writeMarketForcesToFile();
            
            }


            if (buttonClicked.Equals("silver")){

            //Get the cost of the product(s)
            int silverCost = Int32.Parse(inputValue.text) * travelManager.currentPort.silverValue;

                //Check whether they have enough money
                if(Int32.Parse(gameData[0])-silverCost<0){

                    //You do not have enough money
                    textOutput.text = scripts[10];

                } else{
                    
                    //They do have enough money

                    //Data.MoneyOwned goes down
                    changeMoney(Int32.Parse(gameData[0])-silverCost);

                    //Data.SilverItems goes up by the amount
                    changeSilverItems(Int32.Parse(inputValue.text));

                    //Update text output to Transaction confirmed
                    textOutput.text = scripts[11];

                    //State how much silver you own
                    textHeaderDescription.text = gameData[3]+" "+scripts[14];

                }

                //Update silver market
                if(gameData[3].Equals("4")){
                    marketForces[2]="low";
                }if(gameData[3].Equals("5")){
                    marketForces[2]="medium";
                }if(gameData[3].Equals("6")){
                    marketForces[2]="high";
                }
                //Write Market forces to file
                writeMarketForcesToFile();

            
            }

            if (buttonClicked.Equals("pottery")){

            //Get the cost of the product(s)
            int potteryCost = Int32.Parse(inputValue.text) * travelManager.currentPort.potteryValue;

                //Check whether they have enough money
                if(Int32.Parse(gameData[0])-potteryCost<0){

                    //You do not have enough money
                    textOutput.text = scripts[10];

                } else{
                    
                    //They do have enough money

                    //Data.MoneyOwned goes down
                    changeMoney(Int32.Parse(gameData[0])-potteryCost);

                    //Data.PotteryItems goes up by the amount
                    changePotteryItems(Int32.Parse(inputValue.text));

                    //Update text output to Transaction confirmed
                    textOutput.text = scripts[11];

                    //State how much pottery you own
                    textHeaderDescription.text = gameData[4]+" "+scripts[15];

                }

                //Update pottery market
                if(gameData[4].Equals("4")){
                    marketForces[3]="low";
                }if(gameData[4].Equals("5")){
                    marketForces[3]="medium";
                }if(gameData[4].Equals("6")){
                    marketForces[3]="high";
                }
                //Write Market forces to file
                writeMarketForcesToFile();

            
            }

            //Update the Main Panel in all cases
            //mpm.updateMainGamePanel();
            updateMainGamePanel();


        }


    }


    public void clickedSellButton(){

        //Firstly make sure they have entered a value
        if(inputValue.text != ""){

            //If they click buy when on the port screen
            if (buttonClicked.Equals("port")){

                //Check if they own the port
                if(!travelManager.currentPort.doYouOwnPort.Equals("yes")){

                    //You dont own the port
                    textOutput.text = scripts[8];

                } else{

                    //They own the port

                    //Data.MoneyOwned goes up
                    changeMoney(Int32.Parse(gameData[0])+travelManager.currentPort.portOwnValue);

                    //Data.Portsowned goes down by one
                    changePortsOwned(-1);

                    //port.DoYouOwnPort changes to yes
                    changeDoYouOwnPort("no");

                    //mapManager.lastPortChosen.doYouOwnPort goes to Yes
                    travelManager.currentPort.doYouOwnPort = "no";

                    //Update text output to Transaction confirmed
                    textOutput.text = scripts[11];

                }

                //update the text header as to whether you own the port
                //State in the description whether you own the port
                if (travelManager.currentPort.doYouOwnPort.Equals("no"))
                {
                    textHeaderDescription.text = scripts[8];
                }
                else
                {
                    //If you do not own the port
                    textHeaderDescription.text = scripts[7];
                }

            }

            if (buttonClicked.Equals("crew")){
            
                //Get the number of crew that want to fire
                int crewHired = Int32.Parse(inputValue.text);

                    //Check whether they own enough to sell that amount
                    if(Int32.Parse(gameData[2])<Int32.Parse(inputValue.text)){

                        //You do not own enough
                        textOutput.text = scripts[16];

                    } else{
                        //Remove it from the total crew hired
                        changeCrewHired(-crewHired);

                        //Update text output to Transaction confirmed
                        textOutput.text = scripts[11];

                        //update the text header regarding how many crew you own
                        textHeaderDescription.text = gameData[2]+" "+scripts[13];

                    }

            }

            if (buttonClicked.Equals("silver")){

            //Get the profit of the product(s)
            int silverProfit = Int32.Parse(inputValue.text) * travelManager.currentPort.silverValue;

                //Check whether they own enough to sell that amount
                if(Int32.Parse(gameData[3])<Int32.Parse(inputValue.text)){

                    //You do not own enough
                    textOutput.text = scripts[16];

                } else{
                    
                    //They do have enough money

                    //Data.MoneyOwned goes down
                    changeMoney(Int32.Parse(gameData[0])+silverProfit);

                    //Data.SilverItems goes down by the amount
                    changeSilverItems(-Int32.Parse(inputValue.text));

                    //Update text output to Transaction confirmed
                    textOutput.text = scripts[11];

                    //State how much silver you own
                    textHeaderDescription.text = gameData[3]+" "+scripts[14];

                }

            
            }

            if (buttonClicked.Equals("pottery")){

            //Get the profit of the product(s)
            int potteryProfit = Int32.Parse(inputValue.text) * travelManager.currentPort.potteryValue;

                //Check whether they own enough to seel that amount
                if(Int32.Parse(gameData[4])<Int32.Parse(inputValue.text)){

                    //You do not own enough
                    textOutput.text = scripts[16];

                } else{
                    
                    //They do have enough money

                    //Data.MoneyOwned goes down
                    changeMoney(Int32.Parse(gameData[0])+potteryProfit);

                    //Data.PotteryItems goes down by the amount
                    changePotteryItems(-Int32.Parse(inputValue.text));

                    //Update text output to Transaction confirmed
                    textOutput.text = scripts[11];

                    //State how much pottery you own
                    textHeaderDescription.text = gameData[4]+" "+scripts[15];

                }

            
            }

            //Update the Main Panel in all cases
            //mpm.updateMainGamePanel();
            updateMainGamePanel();


        }


    }



    void changeDoYouOwnPort(string own){

        for (int i = 0; i < ports.Length - 1; i++){

            if(ports[i].StartsWith(travelManager.currentPort.portName)){

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

    void changeSilverItems(int items){

        gameData[3] = (Int32.Parse(gameData[3]) + items).ToString();

        writeDataToFile();

    }

    void changePotteryItems(int items){

        gameData[4] = (Int32.Parse(gameData[4]) + items).ToString();

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

    void writeMarketForcesToFile(){
        string updatedMarketForces = "";

        //Compile the array back to a single string
        foreach (string gd in marketForces)
        {
            updatedMarketForces = updatedMarketForces + gd+",";
        }

        //Chop off the last comma
        int index = updatedMarketForces.LastIndexOf(',');
        updatedMarketForces = updatedMarketForces.Substring(0, index);

        //Then update the data file
        dm.writeToMarketForcesFile(updatedMarketForces);

        //refresh the game data array
        getMarketForces();


    }

}
