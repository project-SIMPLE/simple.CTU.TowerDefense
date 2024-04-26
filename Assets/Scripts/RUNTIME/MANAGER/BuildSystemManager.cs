using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystemManager : MonoBehaviour
{
    [SerializeField] private PlayerResourcesManager playerResourcesManager;
    [SerializeField] private List<ConstructionSO> constructions;
    [SerializeField] Transform constructionAnchor;
    
    private bool isBuilding = false;
    private int currentBuildingIndex;

    private GameObject ghostConstruction;

    // Getters
    public bool IsBuilding {
        get { return isBuilding; }
    }
    
    
    private void Update(){
        if(isBuilding){
            ProcessBuilding();
        }
    }

    public void StartBuilding(int constructionIndex)
    {
        if (!playerResourcesManager.Supply(constructions[constructionIndex].cost)) return;

        isBuilding = true;
        currentBuildingIndex = constructionIndex;
    }

    public void FinishBuilding()
    {
        Destroy(ghostConstruction.gameObject);
        isBuilding = false;
    }
    
    public void Build(){
        if (!isBuilding || !ghostConstruction) return;

        if(ghostConstruction.GetComponent<GhostConstruction>().IsBuildable){
            GameObject construction = Instantiate(
                constructions[ currentBuildingIndex ].finalPrefab,
                ghostConstruction.transform.position, 
                constructions[ currentBuildingIndex ].useIdentityRotation ? Quaternion.identity :ghostConstruction.transform.rotation
            );

            Connector[] connectors = ghostConstruction.GetComponentsInChildren<Connector>();
            foreach (Connector connector in connectors)
            {
                connector.updateConnectors(true);
            }
            FinishBuilding();
        }
    }

    public string GetConstructionInfo(int constructionIndex)
    {
        return constructions[constructionIndex].description;
    }

    private void ProcessBuilding(){
        CreateGhostContruction();
    }

    // create ghost construction
    private void CreateGhostContruction(){
        if(ghostConstruction == null)
        {
            ghostConstruction = Instantiate( constructions[currentBuildingIndex].modelBuildPrefab, constructionAnchor);
            if (constructions[currentBuildingIndex].useIdentityRotation)
                ghostConstruction.GetComponent<GhostConstruction>().UseIdentityRotation();
        }
            
    }

}
