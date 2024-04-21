using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] private ConnectorType type;
    
    public ConnectorPosition connectorPosition;
    public SelectedBuildType connectorParentType;

    [HideInInspector] public bool isConnectedToLake = false;
    [HideInInspector] public bool canConnectTo = true;

    [SerializeField] private bool canConnectToLake = true;

    //Getters
    public ConnectorType Type {
        get { return type; }
    }

    private void OnDrawGizmos() {
        Gizmos.color = isConnectedToLake ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(transform.position, transform.lossyScale.x / 2f);
    }

    public void updateConnectors(bool rootCall = false){
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.lossyScale.x / 2f);
        isConnectedToLake = !canConnectToLake;

        foreach (Collider collider in colliders)
        {
            
            if(collider.GetInstanceID() == GetComponent<Collider>().GetInstanceID()){
                continue;
            }
            
            if(collider.gameObject.layer == gameObject.layer)
            {
                Debug.Log(collider.gameObject.name);
                Connector foundConnector = collider.GetComponent<Connector>();
                if(foundConnector.connectorParentType == SelectedBuildType.lake){
                    isConnectedToLake = true;
                }
                if(rootCall)
                   foundConnector.updateConnectors();
            }
        }

        if(isConnectedToLake){
            canConnectTo = false;
        }
    }


}

[System.Serializable]
public enum ConnectorPosition{
    bottom,
}

[System.Serializable]
public enum ConnectorType {
    lake,
    surface,
}

