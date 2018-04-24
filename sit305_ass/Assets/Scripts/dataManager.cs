﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class dataManager {

    
    public void writeToText(string textToWrite)
    {

        string temporaryTextFileName = "data";
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", textToWrite);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;
        Debug.Log(textAsset);

    }

    //I need a function to be able to insert values into the gameData file

    public void writeToDataFile(string updatedGameFile)
    {
        string temporaryTextFileName = "data";

        //I want to add the data file headings to the updatedGameDataFile
        updatedGameFile = returnGameDataHeadings() + updatedGameFile;

        //Then add only the end of the file to the game data
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", updatedGameFile);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

    }





    //I need a function to be able to insert values into the marketForces file






    public string returnMarketForces()
    {
        string marketForces = "";

        //Read the port text file
        string temporaryTextFileName = "marketForces";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the market forces starts
        int startIndex = wholeTextFile.IndexOf("_") + 1;

        //Slice off any text before the file intro
        string partTextFile = wholeTextFile.Substring(startIndex);

        //Find where the market forcesfinish at the underscore
        int endIndex = partTextFile.IndexOf("_");

        //The end result is the part text file ending at the first underscore
        marketForces = partTextFile.Substring(0, endIndex);

        return marketForces;
    }

    public string returnAllPorts()
    {
        string allPorts = "";

        //Read the port text file
        string temporaryTextFileName = "ports";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the first port starts
        int startIndex = wholeTextFile.IndexOf("_")+1;

        allPorts = wholeTextFile.Substring(startIndex);

        return allPorts;
    }


    public string returnPortDetails(string portName)
    {
        string portDetails = "";

        //Read the port text file
        string temporaryTextFileName = "ports";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the port name starts
        int startIndex = wholeTextFile.IndexOf(portName);

        //Slice off any text before the port name
        string partTextFile = wholeTextFile.Substring(startIndex);

        //Find where the port details finish at the underscore
        int endIndex = partTextFile.IndexOf("_");

        //The end result is the part text file ending at the first underscore
        portDetails = partTextFile.Substring(0, endIndex);

        return portDetails;

    }


    public string returnGameDataHeadings()
    {
        string gameDataHeadings = "";

        //Read the data text file
        string temporaryTextFileName = "data";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the game data headings finish
        int endIndex = wholeTextFile.IndexOf("_") + 1;

        //Get the substring
        gameDataHeadings = wholeTextFile.Substring(0,endIndex);
        
        return gameDataHeadings;
    }

    public string returnGameData()
    {
        string gameData = "";

        //Read the data text file
        string temporaryTextFileName = "data";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the game data starts
        int startIndex = wholeTextFile.IndexOf("_") + 1;

        //Get the substring
        gameData = wholeTextFile.Substring(startIndex);

        return gameData;

    }


}