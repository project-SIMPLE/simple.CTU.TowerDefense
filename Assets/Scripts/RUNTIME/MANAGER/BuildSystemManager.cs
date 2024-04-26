using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystemManager : MonoBehaviour
{
    [SerializeField] private PlayerResourcesManager playerResourcesManager;
    [SerializeField] private List<ConstructionSO> constructions;
    [SerializeField] private float connectorRadius = 1;
    [SerializeField] private LayerMask connectorLayerMask;
    [SerializeField] Transform constructionAnchor;
    
    [Header("Ghost Construction Material Settings")]
    [SerializeField] private Material validMaterial;
    [SerializeField] private Material invalidMaterial;

    private bool isBuilding = false;
    private int currentBuildingIndex;

    private GameObject ghostConstruction;
    private bool validPosition = false;

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
        validPosition = false;
    }

    public void FinishBuilding()
    {
        Destroy(ghostConstruction);
        isBuilding = false;
    }
    
    public void Build(){
        if (!isBuilding) return;

        if(ghostConstruction && validPosition){
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
        UpdateGhostContruction();
        UpdateConstructionValidity();
    }

    // create ghost construction
    private void UpdateGhostContruction(){
        if(ghostConstruction == null){
            ghostConstruction = Instantiate( constructions[currentBuildingIndex].modelBuildPrefab, constructionAnchor);
            ghostifyModel(ghostConstruction, invalidMaterial);
            
        }
        else
        {
            if (validPosition) ghostifyModel(ghostConstruction, validMaterial);
            else ghostifyModel(ghostConstruction, invalidMaterial);
        }
    }

    // update ghost construction material
    private void ghostifyModel(GameObject model, Material ghostMaterial = null){
        if(ghostMaterial != null){
            MeshRenderer[] meshRenderers = model.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer meshRenderer in meshRenderers){
                meshRenderer.material = ghostMaterial;
            }
        }
    }


    private void UpdateConstructionValidity(){
        Collider[] colliders = Physics.OverlapSphere(ghostConstruction.transform.position, connectorRadius, connectorLayerMask);
        if(colliders.Length > 0){
            FindConnector(colliders);
        }
        else{
            validPosition = false;
        }
    }

    private void FindConnector(Collider[] colliders){
        Connector bestsurfaceConnector = null;
        foreach(Collider collider in colliders){
            Connector connector = collider.GetComponent<Connector>();
            if(connector.canConnectTo && !connector.transform.IsChildOf(ghostConstruction.transform)){
                bestsurfaceConnector = connector;
                break;
            }
        }
        if (bestsurfaceConnector)
        {
            var constructionConnector = ghostConstruction.GetComponentInChildren<Connector>();
            SnapTwoConnector(bestsurfaceConnector, constructionConnector);
        }
        else
        {
            validPosition = false;
        }
    }


    private void SnapTwoConnector(Connector surfaceConnector, Connector constructionConnector){  
        if (surfaceConnector.Type == constructionConnector.Type)
        {
            ghostConstruction.transform.position = surfaceConnector.transform.position - (constructionConnector.transform.position - ghostConstruction.transform.position);
            ghostConstruction.transform.rotation = constructions[ currentBuildingIndex ].useIdentityRotation ? Quaternion.identity: surfaceConnector.transform.rotation;
            validPosition = true;
        }
    }
}
