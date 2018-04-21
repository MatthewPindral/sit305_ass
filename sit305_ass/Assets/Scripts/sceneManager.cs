using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {

    public void buttonStartNew(string sceneName)
    {
        
        //Clear the data file for any previous game
        dataManager dm = new dataManager();
        dm.writeToText("");

        //Open trade scene
        SceneManager.LoadScene(sceneName);

    }

    public void buttonChangeScene(string sceneName)
    {

        //Open relevant scene
        SceneManager.LoadScene(sceneName);

    }

    public void buttonExitApplication()
    {

        //Open trade scene
        Application.Quit();

    }


}
