using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsidenceManager : MonoBehaviour
{
    /* 
    Subsidence Manager: (vn) -> Quản lý sụt lún 
    - Subsidence Levels 
    - Tree Objects
    - Surface Water 

    ----------------------------------
    Message By Hồng Sơn: 
    We are processing subsidence data from Gamma
    
    */
    private bool isSubsidence = false;
    private float currentWaterLevel = 1f;
    private float currentWaterLevelGlobal = 1f;
    public static float currentSubsidenceLevel = 0f;

    [SerializeField] private float subsidenceLevel1 = 2f;
    [SerializeField] private float subsidenceLevel2 = 5f;
    [SerializeField] private float subsidenceLevel3 = 7f;
    [SerializeField] private float subsidenceLevelRatio = 0.2f;

    private List<GameObject> subsidenceLevels = new List<GameObject>();
    private List<Tree> treeObjects = new List<Tree>();
    private GameObject waterSurface;

    [SerializeField] private float waterRiseSpeed = 0.01f;
    [SerializeField] private float waterHeight = 1f;
    [SerializeField] private float waterLevelRatio = 0.2f;

    [SerializeField] private Vector3 rotationModifier = Vector3.one;

    //Getter
    public float RemainingWaterLevelLocal
    {
        get { return currentWaterLevel; }
        set { currentWaterLevel = value; }
    }
    public float RemainingWaterLevelGlobal
    {
        get { return currentWaterLevelGlobal; }
        set { currentWaterLevelGlobal = value; }
    }
    public float SubsidenceScore
    {
        get { return currentSubsidenceLevel; } 
        set { currentSubsidenceLevel = value; }
    }

    public void IncreaseSubsidenceLevel()
    {
        currentSubsidenceLevel += subsidenceLevelRatio;
    }

    public void DecreaseWaterLevel()
    {
        currentWaterLevel -= waterLevelRatio;
    }

    void Start()
    {
        InitializeSubsidenceLevels();
        InitializeTreeObjects();
        InitializeSurfaceWater();
    }

    void InitializeSubsidenceLevels()
    {
        currentSubsidenceLevel = 0;
        for (int i = 1; i <= 3; i++)
        {
            GameObject subsidenceLevel = transform.Find("Subsidence_Lvl_" + i)?.gameObject;
            if (subsidenceLevel != null)
            {
                subsidenceLevels.Add(subsidenceLevel);
                subsidenceLevel.SetActive(false);
            }
        }
    }

    void InitializeTreeObjects()
    {
        Tree[] trees = FindObjectsOfType<Tree>();
        foreach (Tree tree in trees)
        {
            treeObjects.Add(tree);
        }
    }

    void InitializeSurfaceWater()
    {
        if (transform.Find("SF_Water") != null)
        {
            waterSurface = transform.Find("SF_Water").gameObject;
        //     waterSurface.SetActive(false);
        }
    }


    int tick = 0;
    void Update()
    {
        HandleSubsidence();
        ActivateSubsidenceLevels();
        ApplyWaterLevelEffect();
        Flooded(SubsidenceScore); //Kiểm tra mức độ lũ lụt 
        //Debug.Log("SubsidenceScore: " + SubsidenceScore);
        GameManager gg = FindObjectOfType<GameManager>();
        if (gg != null && gg.CurrentGameStatus() == GameStatus.InProgress)
        {
            tick++;
            //Debug.Log("tick: " + tick);
            if (tick >= 1000)
            {
                //Debug.Log("Ask GAMA");
                tick = 0;
            }
        }
    }

    void HandleSubsidence()
    {
        isSubsidence = currentWaterLevel == 0 || currentSubsidenceLevel >= 1;
    }

    void RotateTrees()
    {
        foreach (Tree tree in treeObjects)
        {
            if (tree != null)
            {
                Vector3 rotationDelta = rotationModifier;
                tree.transform.Rotate(rotationDelta);
            }
        }
    }

    void ActivateSubsidenceLevels()
    {
        if (currentSubsidenceLevel >= subsidenceLevel3)
        {
            if (subsidenceLevels[2]?.activeSelf == false)
            {
                ActivateSubsidenceLevel(3);
                RotateTrees();
                //Flooded(-30);
                Debug.Log("subsidenceLevel3");
            }
        }
        else if (currentSubsidenceLevel >= subsidenceLevel2)
        {
            if (subsidenceLevels[1]?.activeSelf == false)
            {
                ActivateSubsidenceLevel(2);
                RotateTrees();
                //Flooded(-20);
                Debug.Log("subsidenceLevel2");
            }
        }
        else if (currentSubsidenceLevel >= subsidenceLevel1)
        {
            if (subsidenceLevels[0]?.activeSelf == false)
            {
                ActivateSubsidenceLevel(1);
                RotateTrees();
                //Flooded(-10);
                Debug.Log("subsidenceLevel1");
            }
        }
    }

    void ActivateSubsidenceLevel(int level)
    {
        for (int i = 0; i < subsidenceLevels.Count; i++)
        {
            if (i == level - 1)
            {
                subsidenceLevels[i].SetActive(true);
            }
        }
    }

    public void Flooded(float level)
    {
        // Xử lý tạm chờ ghép với GAMA
        Vector3 waterSurfacePosition = waterSurface.transform.position;
        // if (level == -10 && waterSurfacePosition.y < 0.1)
        // {
        //     waterSurfacePosition.y = waterSurface.transform.position.y + 0.01f;
        //     waterSurface.transform.position = waterSurfacePosition;
        // }
        // if (level == -20 && waterSurfacePosition.y < 0.6)
        // {
        //     waterSurfacePosition.y = waterSurface.transform.position.y + 0.01f;
        //     waterSurface.transform.position = waterSurfacePosition;
        // }
        //  if (level == -30 && waterSurfacePosition.y < 1.1)
        // {
        //     waterSurfacePosition.y = waterSurface.transform.position.y + 0.01f;
        //     waterSurface.transform.position = waterSurfacePosition;
        // }
        if (SubsidenceScore < 2.5)
        {
            if (level == SubsidenceScore && waterSurfacePosition.y < (SubsidenceScore - 1.0f))
            {
                waterSurfacePosition.y = waterSurface.transform.position.y + 0.005f;
                waterSurface.transform.position = waterSurfacePosition;
            }
        }
        else 
        {
            if (level == SubsidenceScore && waterSurfacePosition.y < 1.8f)
            {
                waterSurfacePosition.y = waterSurface.transform.position.y + 0.005f;
                waterSurface.transform.position = waterSurfacePosition;
            }
        }
    }

    void ApplyWaterLevelEffect()
    {
        if (currentWaterLevel <= 0f)
        {
            if (waterSurface.activeSelf == false)
            {
                waterSurface.SetActive(true);
            }
            if (waterSurface.transform.position.y < waterHeight)
            {
                Vector3 waterSurfacePosition = waterSurface.transform.position;
                waterSurfacePosition.y = waterSurface.transform.position.y + waterRiseSpeed;
                waterSurface.transform.position = waterSurfacePosition;
            }
        }
    }
}

