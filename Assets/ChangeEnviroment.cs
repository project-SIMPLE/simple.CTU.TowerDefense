using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnviroment : MonoBehaviour
{
    public GameObject Env_Sea;
    public GameObject Env_UnderSea;
    public GameObject Env_SeaBed;
    public GameObject Env_Filter;
    public GameObject Env_Trees;
    public GameObject Env_Rocks;
    public Material Skybox_Sea;
    public Material Skybox_UnderSea;
    public Material Skybox_SeaBed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToEnv_Sea()
    {
        Env_Sea.SetActive(true);
        Env_Filter.SetActive(false);
        Env_UnderSea.SetActive(false);
        Env_SeaBed.SetActive(false);
        Env_Trees.SetActive(true);
        Env_Rocks.SetActive(true);
        RenderSettings.skybox = Skybox_Sea;
        DynamicGI.UpdateEnvironment();
    }
    public void ChangeToEnv_UnderSea()
    {
        RenderSettings.skybox = Skybox_UnderSea;
        DynamicGI.UpdateEnvironment();
        Env_Sea.SetActive(false);
        Env_Filter.SetActive(true);
        Env_UnderSea.SetActive(true);
        Env_SeaBed.SetActive(false);
        Env_Trees.SetActive(false);
        Env_Rocks.SetActive(false);
        
    }
    public void ChangeToEnv_SeaBed()
    {
        RenderSettings.skybox = Skybox_SeaBed;
        DynamicGI.UpdateEnvironment();
        Env_Sea.SetActive(false);
        Env_Filter.SetActive(false);
        Env_UnderSea.SetActive(false);
        Env_SeaBed.SetActive(true);
        Env_Trees.SetActive(false);
        Env_Rocks.SetActive(false);
    }

}
