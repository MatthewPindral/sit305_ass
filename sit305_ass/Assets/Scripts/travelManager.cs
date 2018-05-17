using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class travelManager : MonoBehaviour {

    public GameObject pirateShip;
    public dataManager dm;
    public GameObject alertPanel;
    public Text alertText;
    public Button okButton;

    string[] gameData;
    string returnedGameData;

    int crewHired;
    int silverOwned;
    int potteryOwned;
    int howManySilverPiratesTook;
    int howManyPotteryPiratesTook;


    public Text money;
    public Text portsOwned;
    public Text crewHiredText;
    public Text silverItemsOwned;
    public Text potteryItemsOwned;

    public static portTempValueManager currentPort;

    // Use this for initialization
    void Start () {

        alertPanel.SetActive(false);

        dm = new dataManager();

        getGameData();

        //copy last port chosen over to current port
        cloneLastPortToCurrentPort();

        //if parates come
        if (doesPirateCome())
        {

            StartCoroutine(movePirateShip());

            StartCoroutine(waitAndPiratesCome());

        }
        else
        {
            //if pirates do not come

            //wait and then close scene
            StartCoroutine(waitAndClose());
        }


    }

    IEnumerator movePirateShip(){

        Vector3 targetPosition = new Vector3 (2, 0, 2);  
        Vector3 currentPosition = pirateShip.transform.position;  
        while (Vector3.Distance (currentPosition, targetPosition) > 0.1f) { 
            pirateShip.transform.position = Vector3.Lerp (pirateShip.transform.position, targetPosition, 0.6f * Time.deltaTime);  
     
            yield return null;
        }

    }


    IEnumerator waitAndPiratesCome()
    {
        yield return new WaitForSeconds(5.0f);

            pirateTakesItems();

            updateGameDataItems();

            updateMainGamePanel();

            //Show an alert of what just happened
            showAlert();

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

    void getGameData(){

        //Clear it out
        //Array.Clear(gameData, 0, gameData.Length);
        //then refresh it
        string returnedGameData = dm.returnGameData();
        gameData = returnedGameData.Split(',');

    }

    public void closeAlert()
    {
        alertPanel.SetActive(false);

        //wait and then close scene
        StartCoroutine(waitAndClose());

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
        crewHiredText.text = gameData[2];
        silverItemsOwned.text = gameData[3];
        potteryItemsOwned.text = gameData[4];
    }


    IEnumerator waitAndClose()
    {
        yield return new WaitForSeconds(10.0f);

        mapManager.doITakeMoney = true;

        SceneManager.LoadScene("sceneTrade", LoadSceneMode.Single);

    }
    
    public void pirateTakesItems()
    {
        
        string returnedPortDetails = dm.returnPortDetails(mapManager.lastPortChosen.portName);

        string[] port = returnedPortDetails.Split(',');

        System.Random random = new System.Random();
        int pirateCrew = 0;

        //calculate how many pirate crew there are
        switch (port[3])
        {
            case "low":
                pirateCrew = random.Next(1, 4);
                break;
            case "medium":
                pirateCrew = random.Next(3, 6);
                break;
            case "high":
                pirateCrew = random.Next(5, 10);
                break;
        }

        //get the game data and get the crew you own within it

        string returnedGameData = dm.returnGameData();

        //Split it by commas
        gameData = returnedGameData.Split(',');

        crewHired = Int32.Parse(gameData[2]);
        silverOwned = Int32.Parse(gameData[3]);
        potteryOwned = Int32.Parse(gameData[4]);

        int itemsPirateWillTake = pirateCrew - crewHired;

        int potteryItemstoTake = 0;

        //If the pirates have more crew
        if (itemsPirateWillTake > 0)
        {

            //if you have more silver than what they will take
            if (silverOwned - itemsPirateWillTake >= 0)
            {
                //Adjust silver
                silverOwned = silverOwned - itemsPirateWillTake;

                //Take a record of how may silver items were taken
                howManySilverPiratesTook = itemsPirateWillTake;

                //Set the pottery pieces to zero
                howManyPotteryPiratesTook = 0;

            } else
            {

                //determine how many more items to take and turn to positive
                potteryItemstoTake = (silverOwned - itemsPirateWillTake) * -1;

                //if you have more pottery than what they will take
                if (potteryOwned - potteryItemstoTake >= 0)
                {
                    //Adjust pottery
                    potteryOwned = potteryOwned - potteryItemstoTake;

                    //Take a record of how may pottery items were taken
                    howManyPotteryPiratesTook = potteryItemstoTake;
                }
                else
                {

                    //Take a record of how may pottery items were taken
                    howManyPotteryPiratesTook = potteryOwned;

                    //adjust pottery
                    potteryOwned = 0;

                }


                //Take a record of how may silver items were taken
                howManySilverPiratesTook = silverOwned;

                //if pirate took all your silver then set it to zero
                silverOwned = 0;
                
            }


            

        }

    }

    public void updateGameDataItems()
    {

        string updatedGameData = "";

        //Save the updated items into the game Data array
        gameData[3] = silverOwned.ToString();
        gameData[4] = potteryOwned.ToString();

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

    public void showAlert()
    {
        alertPanel.SetActive(true);

        string returnedPirateAlertScripts = dm.returnScripts();

        string[] scripts = returnedPirateAlertScripts.Split(',');

        alertText.text = scripts[0] + howManySilverPiratesTook.ToString() + scripts[1].ToString() + howManyPotteryPiratesTook.ToString() + scripts[2].ToString();

    }

    bool doesPirateCome()
    {
        System.Random random = new System.Random();
        int randomNumber;

        bool doesPirateCome = false;

        randomNumber = random.Next(1, 3);
 
        int chancePirates = mapManager.lastPortChosen.chancePirates;

        int pirateCalculation = randomNumber * chancePirates;

        if (pirateCalculation > 70)
        {
            doesPirateCome = true;
        }
        
        return doesPirateCome;

    }


}
