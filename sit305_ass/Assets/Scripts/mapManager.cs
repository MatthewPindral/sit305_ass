using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class mapManager : MonoBehaviour {

    string lastPortChosen;

    public Text portName;
    public Text doYouOwnPort;
    public Text portTax;
    public Text chancePirates;
    public Text silverValue;
    public Text potteryValue;

    public void clickPortShowDetails(string portClicked)
    {
        //Save the last port details so that if we trael there then we can update the data text file
        lastPortChosen = portClicked;

        dataManager dm = new dataManager();
        string returnedPort = dm.returnPortDetails(portClicked);

        //I now need to slice up the returned port so that I can assign it to the text fields.

        //portName = returnedPort HERE



        Debug.Log("again" + returnedPort);
    }


}
