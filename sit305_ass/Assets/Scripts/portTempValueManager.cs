﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portTempValueManager {

    public string portName;
    public string doYouOwnPort;
    public int portTax;
    public int chancePirates;
    public int silverValue;
    public int potteryValue;

    public portTempValueManager(string nm, string own, int tax, int pirates,int silver, int pottery)
    {
        portName = nm;
        doYouOwnPort = own;
        portTax = tax;
        chancePirates = pirates;
        silverValue = silver;
        potteryValue = pottery;
    }

}
