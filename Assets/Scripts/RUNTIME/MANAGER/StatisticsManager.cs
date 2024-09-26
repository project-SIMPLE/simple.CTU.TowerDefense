using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionHistory
{
    public string datetime;
    public string action;
    public string construction;
    public Vector2 position;

    public ActionHistory(string action, string construction, Vector2 position)
    {
        datetime = DateTime.Now.ToString("M/d/yyyy hh:mm:ss");
        this.action = action;
        this.construction = construction;
        this.position = position;
    }
}

public class StatisticsManager : MonoBehaviour
{
    /* 
    Statistics Manager: (v) -> Quản lý thống kê
    Manage and compile final data information of objects in the game screen.

    ----------------------------------
    Message By Hồng Sơn: 
    We are processing information adjustments to accommodate educational programs.

     */
    public static StatisticsManager Instance = null;

    private int currentLakeCount = 0;
    private int currentWaterPumpCount = 0;
    private int currentSluiceGateCount = 0;
    private int currentEnemyCount = 0;
    
    [HideInInspector] public List<ActionHistory> histories;

    private void Start()
    {
        histories = new List<ActionHistory>();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void IncreateLakeCount() 
    {
        currentLakeCount += 1;
    }

    public void IncreateWaterPumpCount()
    {
        currentWaterPumpCount += 1;
    }
    public void IncreateSluiceGateCount()
    {
        currentSluiceGateCount += 1;
    }

    public void IncreaseEnemyKillCount()
    {
        currentEnemyCount += 1;
    }

    public void AddActionHistory(string action, string construction, Vector2 position)
    {
        histories.Add(
            new ActionHistory(action,construction,position)
        );
        Debug.Log(histories[histories.Count-1].datetime);
        Debug.Log(histories[histories.Count-1].action);
        Debug.Log(histories[histories.Count-1].construction);
        Debug.Log(histories[histories.Count-1].position);
    }


    //Getter
    public int LakeCount
    {
        get { return currentLakeCount; }
    }

    public int WaterPumpCount
    {
        get { return currentWaterPumpCount; }
    }

    public int SluiceGateCount
    {
        get { return currentSluiceGateCount; }
    }
    public int EnemyKillCount
    { 
        get { return currentEnemyCount; } 
    }
}
