using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class SendReceiveMessageExample : SimulationManager
{

 GAMAMessage message = null;

    protected override void ManageOtherMessages(string content)
 {
     message = GAMAMessage.CreateFromJSON(content);
 }



}


[System.Serializable]
public class GAMAMessage
{

    public bool subside;
    public int cycle;

    public static GAMAMessage CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMAMessage>(jsonString);
    }

}
