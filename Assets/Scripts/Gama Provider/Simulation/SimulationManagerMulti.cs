using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit; 
using UnityEngine.InputSystem;
using TMPro;



public class SimulationManagerMulti : SimulationManager
{
    protected int score = 0 ;
    protected int ranking = 1;
    protected int numTokens = 0;
    private TextMeshProUGUI infoPlayerHUD;


    protected override void ManageOtherInformation()
    {
        


    }

    protected void updateHUD()
    {
     
    }

    protected override void TriggerMainButton()
    {
 
    }

    protected override void OtherUpdate()
    {

    }

    protected override void ManageOtherMessages(string content)
    {

    }

}