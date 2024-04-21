using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private BuildSystemManager buildManager;

    public void ChoseConstruction(int constructionIndex)
    {
        buildManager.StartBuilding(constructionIndex);
        transform.GetChild(0).gameObject.gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        GameObject ui = transform.GetChild(0).gameObject;
        ui.SetActive(!ui.activeSelf);
    }
}
