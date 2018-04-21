using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class dataManager : MonoBehaviour {


    public void writeToText(string textToWrite)
    {

        string temporaryTextFileName = "data";
        File.WriteAllText(Application.dataPath + "/Resources/" + temporaryTextFileName + ".txt", textToWrite);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;
        Debug.Log(textAsset);

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

        //Find where the port details finishes
        int endIndex = partTextFile.IndexOf("_");

        //The end result
        portDetails = partTextFile.Substring(startIndex, endIndex);

        return portDetails;

    }


}
