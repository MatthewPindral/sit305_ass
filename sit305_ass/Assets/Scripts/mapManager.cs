using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class mapManager : MonoBehaviour {

    public Text portName;
    public Text doYouOwnPort;
    public Text portTax;
    public Text chancePirates;
    public Text silverValue;
    public Text potteryValue;
    public dataManager dm;

    public Button ButtonAlexis;
    public Button ButtonZeon;
    public Button ButtonNova;
    public Button ButtonSerilda;
    public Button ButtonClaudia;
    public Button TravelButton;

    string[] gameData;
    string[] port;
    string[] forces;

    List<portTempValueManager> portsListForPresentationOnScreen = new List<portTempValueManager>();

    //string lastPortChosen;
    public static portTempValueManager lastPortChosen;

    private void Start()
    {
        dm  = new dataManager();
        string returnedPorts = dm.returnAllPorts();
        string returnedMarketForces = dm.returnMarketForces();
        string returnedGameData = dm.returnGameData();

        port = returnedPorts.Split('_');
        forces = returnedMarketForces.Split(',');
        gameData = returnedGameData.Split(',');

        disableButton();

        populatePortDetails();

    }

    public void clickPortShowDetails(string portClicked)
    {

        TravelButton.interactable = true;

        foreach (var pm in portsListForPresentationOnScreen)
        {
            if (portClicked == pm.portName)
            {

                //Assign each array value into the relevant text field
                portName.text = pm.portName;
                doYouOwnPort.text = pm.doYouOwnPort;
                portTax.text = "$"+pm.portTax.ToString();
                chancePirates.text = pm.chancePirates.ToString()+"%";
                silverValue.text = "$"+pm.silverValue.ToString();
                potteryValue.text = "$"+ pm.potteryValue;

                //Keep a record of the last clickedPort
                lastPortChosen = new portTempValueManager(pm.portName, pm.doYouOwnPort, pm.portTax, pm.chancePirates, pm.silverValue, pm.potteryValue);

            }
        }

    }

    public void disableButton()
    {
        switch (gameData[5])
        {
            case "Alexis":
                ButtonAlexis.interactable = false;
                break;
            case "Zeon":
                ButtonZeon.interactable = false;
                break;
            case "Nova":
                ButtonNova.interactable = false;
                break;
            case "Serilda":
                ButtonSerilda.interactable = false;
                break;
            case "Claudia":
                ButtonClaudia.interactable = false;
                break;

            default:
                break;

        }
    }

    public void populatePortDetails()
    {
        System.Random random = new System.Random();
        int randomNumber;

        //Isolate a single port
        for (int i = 0; i < port.Length - 1; i++)
        {
            //Isolate a single value in each port
            string[] portValue = port[i].Split(',');

            //Then run over them to add dollar values rather than low, med, high.

            //TAX
            if (portValue[2].Equals("low"))
            {
                if (forces[0].Equals("low"))
                {

                    randomNumber = random.Next(100, 300);
                    portValue[2] = randomNumber.ToString();
                }

                if (forces[0].Equals("medium"))
                {

                    randomNumber = random.Next(400, 600);
                    portValue[2] = randomNumber.ToString();
                }

                if (forces[0].Equals("high"))
                {

                    randomNumber = random.Next(600, 800);
                    portValue[2] = randomNumber.ToString();
                }

            }

            if (portValue[2].Equals("medium"))
            {
                if (forces[0].Equals("low"))
                {

                    randomNumber = random.Next(400, 600);
                    portValue[2] = randomNumber.ToString();
                }

                if (forces[0].Equals("medium"))
                {

                    randomNumber = random.Next(600, 800);
                    portValue[2] = randomNumber.ToString();
                }

                if (forces[0].Equals("high"))
                {

                    randomNumber = random.Next(800, 1000);
                    portValue[2] = randomNumber.ToString();
                }

            }

            if (portValue[2].Equals("high"))
            {
                if (forces[0].Equals("low"))
                {

                    randomNumber = random.Next(600, 800);
                    portValue[2] = randomNumber.ToString();
                }

                if (forces[0].Equals("medium"))
                {

                    randomNumber = random.Next(800, 1000);
                    portValue[2] = randomNumber.ToString();
                }

                if (forces[0].Equals("high"))
                {

                    randomNumber = random.Next(1000, 1200);
                    portValue[2] = randomNumber.ToString();
                }

            }

            //PIRATES
            if (portValue[3].Equals("low"))
            {
                if (forces[1].Equals("low"))
                {

                    randomNumber = random.Next(0, 20);
                    portValue[3] = randomNumber.ToString();
                }

                if (forces[1].Equals("medium"))
                {

                    randomNumber = random.Next(30, 50);
                    portValue[3] = randomNumber.ToString();
                }

                if (forces[1].Equals("high"))
                {

                    randomNumber = random.Next(60, 80);
                    portValue[3] = randomNumber.ToString();
                }

            }

            if (portValue[3].Equals("medium"))
            {
                if (forces[1].Equals("low"))
                {

                    randomNumber = random.Next(30, 50);
                    portValue[3] = randomNumber.ToString();
                }

                if (forces[1].Equals("medium"))
                {

                    randomNumber = random.Next(40, 60);
                    portValue[3] = randomNumber.ToString();
                }

                if (forces[1].Equals("high"))
                {

                    randomNumber = random.Next(60, 80);
                    portValue[3] = randomNumber.ToString();
                }

            }

            if (portValue[3].Equals("high"))
            {
                if (forces[1].Equals("low"))
                {

                    randomNumber = random.Next(60, 80);
                    portValue[3] = randomNumber.ToString();
                }

                if (forces[1].Equals("medium"))
                {

                    randomNumber = random.Next(70, 90);
                    portValue[3] = randomNumber.ToString();
                }

                if (forces[1].Equals("high"))
                {

                    randomNumber = random.Next(80, 100);
                    portValue[3] = randomNumber.ToString();
                }

            }


            //SILVER VALUES
            if (portValue[4].Equals("low"))
            {
                if (forces[2].Equals("low"))
                {

                    randomNumber = random.Next(200, 300);
                    portValue[4] = randomNumber.ToString();
                }

                if (forces[2].Equals("medium"))
                {

                    randomNumber = random.Next(300, 400);
                    portValue[4] = randomNumber.ToString();
                }

                if (forces[2].Equals("high"))
                {

                    randomNumber = random.Next(400, 500);
                    portValue[4] = randomNumber.ToString();
                }

            }

            if (portValue[4].Equals("medium"))
            {
                if (forces[2].Equals("low"))
                {

                    randomNumber = random.Next(600, 800);
                    portValue[4] = randomNumber.ToString();
                }

                if (forces[2].Equals("medium"))
                {

                    randomNumber = random.Next(700, 900);
                    portValue[4] = randomNumber.ToString();
                }

                if (forces[2].Equals("high"))
                {

                    randomNumber = random.Next(800, 1000);
                    portValue[4] = randomNumber.ToString();
                }

            }

            if (portValue[4].Equals("high"))
            {
                if (forces[2].Equals("low"))
                {

                    randomNumber = random.Next(700, 900);
                    portValue[4] = randomNumber.ToString();
                }

                if (forces[2].Equals("medium"))
                {

                    randomNumber = random.Next(800, 900);
                    portValue[4] = randomNumber.ToString();
                }

                if (forces[2].Equals("high"))
                {

                    randomNumber = random.Next(900, 1000);
                    portValue[4] = randomNumber.ToString();
                }

            }

            //POTTERY VALUES
            if (portValue[5].Equals("low"))
            {
                if (forces[3].Equals("low"))
                {

                    randomNumber = random.Next(200, 400);
                    portValue[5] = randomNumber.ToString();
                }

                if (forces[3].Equals("medium"))
                {

                    randomNumber = random.Next(300, 500);
                    portValue[5] = randomNumber.ToString();
                }

                if (forces[3].Equals("high"))
                {

                    randomNumber = random.Next(600, 800);
                    portValue[5] = randomNumber.ToString();
                }

            }

            if (portValue[5].Equals("medium"))
            {
                if (forces[3].Equals("low"))
                {

                    randomNumber = random.Next(600, 800);
                    portValue[5] = randomNumber.ToString();
                }

                if (forces[3].Equals("medium"))
                {

                    randomNumber = random.Next(700, 900);
                    portValue[5] = randomNumber.ToString();
                }

                if (forces[3].Equals("high"))
                {

                    randomNumber = random.Next(800, 1000);
                    portValue[5] = randomNumber.ToString();
                }

            }

            if (portValue[5].Equals("high"))
            {
                if (forces[3].Equals("low"))
                {

                    randomNumber = random.Next(700, 900);
                    portValue[5] = randomNumber.ToString();
                }

                if (forces[3].Equals("medium"))
                {

                    randomNumber = random.Next(800, 900);
                    portValue[5] = randomNumber.ToString();
                }

                if (forces[3].Equals("high"))
                {

                    randomNumber = random.Next(900, 1000);
                    portValue[5] = randomNumber.ToString();
                }

            }

            portTempValueManager pm = new portTempValueManager(portValue[0], portValue[1], Int32.Parse(portValue[2]), Int32.Parse(portValue[3]), Int32.Parse(portValue[4]), Int32.Parse(portValue[5]));
            portsListForPresentationOnScreen.Add(pm);

        }


    }

    public void updateGameDataPort()
    {

        string updatedGameData = "";
        
        //Save the last port chosen into the game Data array
        gameData[5]=lastPortChosen.portName;
        
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



}
