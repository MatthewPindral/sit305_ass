using System.Collections;
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


        public void writeToPortFile(string updatedPortFile)
    {
        string temporaryTextFileName = "ports";

        //I want to add the data file headings to the updatedGameDataFile
        updatedPortFile = returnPortHeadings() + updatedPortFile;

        Debug.Log("updatedPortFile"+updatedPortFile);

        //Then add only the end of the file to the game data
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", updatedPortFile);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

    }

    public void writeToMarketForcesFile(string updatedMarketForces)
    {
        string temporaryTextFileName = "marketForces";

        //I want to add the data file headings to the updatedGameDataFile
        updatedMarketForces = returnMarketForcesHeadings() + updatedMarketForces;

        //Then add only the end of the file to the game data
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", updatedMarketForces);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

    }


    public string returnPortHeadings()
    {
        string portHeadings = "";

        //Read the data text file
        string temporaryTextFileName = "ports";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the game data headings finish
        int endIndex = wholeTextFile.IndexOf("_") + 1;

        //Get the substring
        portHeadings = wholeTextFile.Substring(0,endIndex);

        Debug.Log("portHeadings"+portHeadings);

        return portHeadings;
    }


    public string returnScripts()
    {
        string scripts = "";

        //Read the port text file
        string temporaryTextFileName = "scripts";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the first port starts
        int startIndex = wholeTextFile.IndexOf("_") + 1;

        scripts = wholeTextFile.Substring(startIndex);

        return scripts;
    }



    public string returnMarketForces()
    {
        string marketForces = "";

        //Read the port text file
        string temporaryTextFileName = "marketForces";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the first port starts
        int startIndex = wholeTextFile.IndexOf("_") + 1;

        marketForces = wholeTextFile.Substring(startIndex);

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

    public string returnMarketForcesHeadings()
    {
        string marketForcesHeadings = "";

        //Read the data text file
        string temporaryTextFileName = "marketForces";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        string wholeTextFile = textAsset.text;

        //Find where the game data headings finish
        int endIndex = wholeTextFile.IndexOf("_") + 1;

        //Get the substring
        marketForcesHeadings = wholeTextFile.Substring(0,endIndex);
        
        return marketForcesHeadings;
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


    public string returnGameDataReset()
    {
        string gameData = "";

        //Read the data text file
        string temporaryTextFileName = "dataReset";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        gameData = textAsset.text;

        Debug.Log("here: "+ gameData);

        return gameData;

    }

    public string returnPortDataReset()
    {
        string portData = "";

        //Read the data text file
        string temporaryTextFileName = "portReset";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        portData = textAsset.text;

        Debug.Log("portReset: "+ portData);

        return portData;

    }

    public string returnMarketForcesReset()
    {
        string marketReset = "";

        //Read the data text file
        string temporaryTextFileName = "marketForcesReset";
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

        //Get the whole text file
        marketReset = textAsset.text;

        Debug.Log("marketReset: "+ marketReset);

        return marketReset;

    }

    public void writeDataResetToFile()
    {
        string temporaryTextFileName = "data";

        //Then add only the end of the file to the game data
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", returnGameDataReset());
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

    }

    public void writePortResetToFile()
    {
        string temporaryTextFileName = "ports";

        //Then add only the end of the file to the game data
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", returnPortDataReset());
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

    }

    public void writeMarketForcesResetToFile()
    {
        string temporaryTextFileName = "marketForces";

        //Then add only the end of the file to the game data
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", returnMarketForcesReset());
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;

    }


}
