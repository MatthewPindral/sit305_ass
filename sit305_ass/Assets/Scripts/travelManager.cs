﻿using System;
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

    int crewHired;
    int silverOwned;
    int potteryOwned;
    int howManySilverPiratesTook;
    int howManyPotteryPiratesTook;

    // Use this for initialization
    void Start () {

        alertPanel.SetActive(false);

        dm = new dataManager();

        //if parates come
        if (doesPirateCome())
        {
            pirateTakesItems();

            updateGameDataItems();

            //Show an alert of what just happened
            showAlert();

        }
        else
        {
            //if pirates do not come

            //wait and then close scene
            StartCoroutine(waitAndClose());
        }


    }

    public void closeAlert()
    {
        alertPanel.SetActive(false);

        //wait and then close scene
        StartCoroutine(waitAndClose());

    }


    IEnumerator waitAndClose()
    {
        yield return new WaitForSeconds(10.0f);

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

                Debug.Log("More silver than items"+silverOwned+","+potteryOwned + "," +howManySilverPiratesTook + "," + itemsPirateWillTake + "," + potteryItemstoTake);

            } else
            {

                Debug.Log("Less silver than items" + silverOwned + "," + potteryOwned + "," + howManySilverPiratesTook + "," + itemsPirateWillTake + "," + potteryItemstoTake);

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

        string returnedPirateAlertScripts = dm.returnPirateAlert();

        string[] scripts = returnedPirateAlertScripts.Split(',');

        alertText.text = scripts[0] + howManySilverPiratesTook.ToString() + scripts[1].ToString() + howManyPotteryPiratesTook.ToString() + scripts[2].ToString();


    }





    // Update is called once per frame
    void Update () {
		
	}

    bool doesPirateCome()
    {
        System.Random random = new System.Random();
        int randomNumber;

        bool doesPirateCome = false;

        randomNumber = random.Next(1, 3);
 
        int chancePirates = mapManager.lastPortChosen.chancePirates;

        int pirateCalculation = randomNumber * chancePirates;

        Debug.Log(pirateCalculation);

        if (pirateCalculation > 70)
        {
            doesPirateCome = true;
        }
        
        return doesPirateCome;

    }


}
