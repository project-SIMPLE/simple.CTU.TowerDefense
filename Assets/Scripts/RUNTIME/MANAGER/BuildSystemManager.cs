using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystemManager : MonoBehaviour
{
    [SerializeField] private PlayerResourcesManager playerResourcesManager;
    [SerializeField] private List<ConstructionSO> constructions;
    [SerializeField] Transform constructionAnchor;
    [SerializeField] private BuildUI buildIU;
    
    private bool isBuilding = false;
    private int currentBuildingIndex;
    private GameObject ghostConstruction;

    [SerializeField] private SubsidenceManager subsidenceManager;

    // Getters
    public bool IsBuilding {
        get { return isBuilding; }
    }
  
    public List<ConstructionSO> Constructions
    {
        get { return constructions; }
    }
    private void Update(){
        UpdateCooldowns(Time.deltaTime);
        if(isBuilding){
            ProcessBuilding();
        }
    }

    public void StartBuilding(int constructionIndex)
    {
        if (!IsBuildable(constructionIndex)) return;

        isBuilding = true;
        currentBuildingIndex = constructionIndex;
    }
    private void UpdateCooldowns(float deltaTime)
    {
        foreach(var construction in constructions)
        {
            construction.DecreaseCooldown(deltaTime);
        }
    }
    public bool IsBuildable(int constructionIndex)
    {
        ConstructionSO construction = constructions[constructionIndex];
        if (construction.CurrentTime <= 0 && construction.CurrentQuantity > 0)
        {
            construction.ResetCooldown();
            construction.DecreaseQuantity();
            buildIU.ImageCooldownList[constructionIndex].fillAmount = 1;
            return true;
        }else return false;
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
            construction.GetComponent<ConstructionRemover>().buildSystemManager = this;

            Connector[] connectors = ghostConstruction.GetComponentsInChildren<Connector>();
            foreach (Connector connector in connectors)
            {
                connector.UpdateConnector(false);
            }
            subsidenceManager.IncreaseSubsidenceLevel();
            subsidenceManager.DecreaseWaterLevel();
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
