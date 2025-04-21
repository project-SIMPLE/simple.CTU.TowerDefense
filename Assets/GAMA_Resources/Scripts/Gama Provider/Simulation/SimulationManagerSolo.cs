using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit; 
using UnityEngine.InputSystem;



public class SimulationManagerSolo : SimulationManager
{
    protected bool isNight = false;

    

    protected override void TriggerMainButton()
    {
        isNight = !isNight;
        Light[] lights = FindObjectsOfType(typeof(Light)) as Light[];
        foreach (Light light in lights)
        {
            light.intensity = isNight ? 0 : 1.0f;
        }
    }

    protected override void AdditionalInitAfterGeomLoading()
    {
        if (parameters.hotspots != null && parameters.hotspots.Count > 0)
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("selectable");

            foreach (GameObject gameObj in blocks)
            {
                if (parameters.hotspots.Contains(gameObj.name))
                {
                    SelectedObjects.Add(gameObj);
                    SimulationManagerSolo.ChangeColor(gameObj, Color.red);
                }
            }
        }
    }



  
    protected override void OtherUpdate()
    { 
        // if (message != null)
        // {
        //     // Debug.Log("received from GAMA: subside " + message.subside);
        //     if(message.subside){
        //             SubsidenceManager    subsidenceManager = FindObjectOfType<SubsidenceManager>();

        //         subsidenceManager.Flooded();
        //     }
        //     message = null;
        // }


    }

    // GAMAMessage message = null;
    protected override void ManageOtherMessages(string content)
    {
                // message = GAMAMessage.CreateFromJSON(content);

    }
    protected override void HoverEnterInteraction(HoverEnterEventArgs ev)
    {

        GameObject obj = ev.interactableObject.transform.gameObject;
        if (obj.tag.Equals("selectable") || obj.tag.Equals("car") || obj.tag.Equals("moto"))
            SimulationManagerSolo.ChangeColor(obj, Color.blue);
    }

    protected override void HoverExitInteraction(HoverExitEventArgs ev)
    {
        GameObject obj = ev.interactableObject.transform.gameObject;
        if (obj.tag.Equals("selectable"))
        {
            bool isSelected = SelectedObjects.Contains(obj);

            SimulationManagerSolo.ChangeColor(obj, isSelected ? Color.red : Color.gray);
        }
        else if (obj.tag.Equals("car") || obj.tag.Equals("moto"))
        {
            SimulationManagerSolo.ChangeColor(obj, Color.white);
        }


    }

   

}