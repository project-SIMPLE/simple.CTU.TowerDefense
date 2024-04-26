using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private BuildSystemManager buildManager;
    [SerializeField] private GameObject content;
    [SerializeField] private TextMeshProUGUI currentBuildInfo;
    
    void Start()
    {
        content.SetActive(false);
    }

    public void ChoseConstruction(int constructionIndex)
    {
        buildManager.StartBuilding(constructionIndex);
        if (buildManager.IsBuilding) ToggleMenu();
    }

    public void ToggleMenu()
    {
        content.SetActive(!content.activeSelf);
    }

    public void OnHover(int constructionIndex)
    {
        currentBuildInfo.text = buildManager.GetConstructionInfo(constructionIndex);
    }

    public void ExitHover()
    {
        currentBuildInfo.text = "";
    }
}
