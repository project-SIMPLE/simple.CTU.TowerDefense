using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SimulationManager simulationManager;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private Transform head;
    [SerializeField] private float spawnDistance;

    [SerializeField] private GameObject startContent;
    [SerializeField] private GameObject finalContent;
    [SerializeField] private GameObject finalContent_Win;
    [SerializeField] private GameObject finalContent_Lose;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI finalText;
    [SerializeField] private string winText = "YOU WON!!!";
    [SerializeField] private string loseText = "YOU LOST!!!";

    [SerializeField] private PlayerResourcesManager playerResourcesManager;
    private SubsidenceManager subsidenceManager;
    

    private WebSocket socket;
//    private bool connected = false;
    public static GameUI Instance = null;

    private string reportText = "";
    [SerializeField] private TextMeshProUGUI reportTextMeshPro;

    // Son: Update final Menu
    [SerializeField] private TextMeshProUGUI reportLivingTreesNumber;
    [SerializeField] private TextMeshProUGUI reportDeadTreesNumber;
    [SerializeField] private TextMeshProUGUI reportLakeNumber;
    [SerializeField] private TextMeshProUGUI reportPumpNumber;
    [SerializeField] private TextMeshProUGUI reportWaterGateNumber;
    [SerializeField] private TextMeshProUGUI reportEnemiesNumber;
    [SerializeField] private TextMeshProUGUI reportRemainingGroundwaterLevelLocal;
    [SerializeField] private TextMeshProUGUI reportRemainingGroundwaterLevelGlobal;

    // Son: Update Win and Lose report 
    [SerializeField] private TextMeshProUGUI win_reportLivingTreesNumber;
    [SerializeField] private TextMeshProUGUI win_reportDeadTreesNumber;
    [SerializeField] private TextMeshProUGUI win_reportPumpNumber;
    [SerializeField] private TextMeshProUGUI win_reportEnemiesNumber;
    [SerializeField] private TextMeshProUGUI win_reportSubsidenceScore;

    [SerializeField] private TextMeshProUGUI lose_reportLivingTreesNumber;
    [SerializeField] private TextMeshProUGUI lose_reportDeadTreesNumber;
    [SerializeField] private TextMeshProUGUI lose_reportPumpNumber;
    [SerializeField] private TextMeshProUGUI lose_reportEnemiesNumber;
    [SerializeField] private TextMeshProUGUI lose_reportSubsidenceScore;

    public bool endDone = false;

    public float SubsidenceScore = 0;
    public float LiveTreeNumber = 0;
    private float dtree = 0;
    public float DeadTreeNumber = 0;
    public float TotalTree = 0;
    public float NumberPumper = 0;
    public float TotalNeutralWater = 0;
    public float TotalMiningWater = 0;
    public float ScoreGame = 0;

    void Start()
    {
        //string ip = PlayerPrefs.GetString("IP");
        //if (NotValid(ip))
         //   ip = "127.0.0.1";
        // Son: turn_off IP
        // playerTextOutput = GameObject.FindGameObjectWithTag("textIP").GetComponentInChildren<TextMeshProUGUI>();
        //playerTextOutput.text = ip;

        ready = false;
        transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        startContent.SetActive(true);
        finalContent.SetActive(false);
        Instance = this;
        TotalTree = playerResourcesManager.TotalTree;
        Debug.Log("Start Total Tree:"+ TotalTree);
        Debug.Log("Start Live Tree:"+ LiveTreeNumber);
        Debug.Log("Start Dead Tree:"+ DeadTreeNumber);
    }
    void Awake()
    {
        Instance = this;
        subsidenceManager = FindObjectOfType<SubsidenceManager>();
    }
    public void computeScore()
    {

        
        SubsidenceScore = subsidenceManager.SubsidenceScore;
        
        //DeadTreeNumber = dtree;
        LiveTreeNumber =  playerResourcesManager.CurrentRefillSources; //TotalTree - DeadTreeNumber;//
        DeadTreeNumber = playerResourcesManager.TotalTree - playerResourcesManager.CurrentRefillSources;
        NumberPumper = StatisticsManager.Instance.WaterPumpCount;
        TotalNeutralWater = StatisticsManager.Instance.EnemyKillCount;
        TotalMiningWater = 100 - subsidenceManager.RemainingWaterLevelLocal;


        // SubsidenceScore = 1;
        // TotalTree = 199;
        // DeadTreeNumber = 199;
        // LiveTreeNumber = TotalTree-DeadTreeNumber;// playerResourcesManager.CurrentRefillSources;
        // NumberPumper = 10;
        // TotalNeutralWater = 100;
        // TotalMiningWater = 100;
        ScoreGame = ((1 - (SubsidenceScore / 10)) + ((LiveTreeNumber+1) / (TotalTree+1)) + (1 - ((NumberPumper+1) / 10)) + (((TotalNeutralWater+1)/200+1) / (TotalMiningWater+1))) * 100;
        
        //ScoreGame = ((1 - (SubsidenceScore / 10)) + (LiveTreeNumber / TotalTree) + (1 - (NumberPumper / 10)) + ((TotalNeutralWater/200+1) / (TotalMiningWater+1))) * 100;
        
        ScoreGame = Mathf.Round(ScoreGame);
        //ScoreGame = Mathf.Round(ScoreGame * 100.0f) * 0.01f;
    }

    void Update()
    {
        ready = true;
        
       
        if (!endDone && (gameManager.CurrentGameStatus() == GameStatus.Win || gameManager.CurrentGameStatus() == GameStatus.Lose))
        {
            endDone = true;
            transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            startContent.SetActive(false);
            //finalContent.SetActive(true);
            computeScore();
            // Debug.Log("Total Tree:"+ TotalTree);
            // Debug.Log("Dead Tree:"+ DeadTreeNumber);
            // Debug.Log("Live Tree:"+ LiveTreeNumber);
            // Debug.Log("SubsidenceScore:"+ SubsidenceScore);
            // Debug.Log("ScoreGame:"+ ScoreGame);
            // Son : update menu win and lose 
            if (LiveTreeNumber > 0 && SubsidenceScore < 1.25f) //  GAMA < 2.75f          (DeadTreeNumber/LiveTreeNumber) < (0.4)
            {
                finalContent_Win.SetActive(true);
            }
            else finalContent_Lose.SetActive(true);

            

            if (gameManager.CurrentGameStatus() == GameStatus.Win)
            {
                finalText.text = winText;
            }
            if (gameManager.CurrentGameStatus() == GameStatus.Lose)
            {
                finalText.text = loseText;
            }



            // Add Report Text Here
            // reportText = "Living Trees: " + playerResourcesManager.CurrentRefillSources + "\n" +
            //              "Dead Trees: " + (playerResourcesManager.TotalTree - playerResourcesManager.CurrentRefillSources) + "\n" +
            //              "Lake Structures Built: " + StatisticsManager.Instance.LakeCount + "\n" +
            //              "WaterPump Structures Built: " + StatisticsManager.Instance.WaterPumpCount + "\n" +
            //              "SluiceGate Structures Built: " + StatisticsManager.Instance.SluiceGateCount + "\n" +
            //              "Enemies Neutralized: " + StatisticsManager.Instance.EnemyKillCount + "\n" +
            //              "Remaining Groundwater Level (Local): " + subsidenceManager.RemainingWaterLevelLocal + "\n" +
            //              "Remaining Groundwater Level (Global): " + subsidenceManager.RemainingWaterLevelGlobal + "\n" +
            //              "Subsidence Score: " + subsidenceManager.SubsidenceScore;


            // Son: Setup Final Report
            // reportTextMeshPro.text = reportText;
            // reportLivingTreesNumber.text = "" + playerResourcesManager.CurrentRefillSources;
            // reportDeadTreesNumber.text = "" + (playerResourcesManager.TotalTree - playerResourcesManager.CurrentRefillSources);
            // reportLakeNumber.text = "" + StatisticsManager.Instance.LakeCount;
            // reportPumpNumber.text = "" + StatisticsManager.Instance.WaterPumpCount;
            // reportWaterGateNumber.text = "" + StatisticsManager.Instance.SluiceGateCount;
            // reportEnemiesNumber.text = "" + StatisticsManager.Instance.EnemyKillCount;
            // reportRemainingGroundwaterLevelLocal.text = "Remaining Groundwater Level (Local): " + subsidenceManager.RemainingWaterLevelLocal;
            // reportRemainingGroundwaterLevelGlobal.text = "Remaining Groundwater Level (Global): " + subsidenceManager.RemainingWaterLevelGlobal;
        



            // Son: Update Win and Lose
             
            win_reportLivingTreesNumber.text = "" + LiveTreeNumber;
            win_reportDeadTreesNumber.text = "" + DeadTreeNumber;
            win_reportPumpNumber.text = "" + NumberPumper;
            win_reportEnemiesNumber.text = "" + TotalNeutralWater;
            win_reportSubsidenceScore.text = "" + ScoreGame;

            lose_reportLivingTreesNumber.text = "" + LiveTreeNumber;
            lose_reportDeadTreesNumber.text = "" + DeadTreeNumber;
            lose_reportPumpNumber.text = "" + NumberPumper;
            lose_reportEnemiesNumber.text = "" + TotalNeutralWater;
            lose_reportSubsidenceScore.text = "" + ScoreGame;

        }

        transform.LookAt(new Vector3(head.position.x, transform.position.y, head.position.z));
        transform.forward *= -1;
    }

    public void StartTutorialUI()
    {
        gameManager.StartTutorial();
        startContent.gameObject.SetActive(false);
    }

    public void ReConnect()
    {
        ConnectionManager.Instance.UpdateConnectionState(ConnectionState.DISCONNECTED);
    }

    public void StartUI()
    {
       /* // PlayerPrefs.SetString("IP", "localhost");
        PlayerPrefs.SetString("PORT", "1000");
        PlayerPrefs.SetString("IP", "127.0.0.1");
        // Son: turn_off IP
        //PlayerPrefs.SetString("IP", playerTextOutput.text);
        PlayerPrefs.Save();

        port = PlayerPrefs.GetString("PORT");
        host = PlayerPrefs.GetString("IP");
        // socket = new WebSocket("ws://" + host + ":" + port + "/");
        // socket.OnOpen += HandleConnectionOpen;
        // socket.Connect();*/

        startContent.gameObject.SetActive(false);
        gameManager.StartLevel();
        
        simulationManager.sendTrees();
        simulationManager.createEnemySpawner();

    }

    public void RetryUI()
    {
        gameManager.RestartLevel();
        Restart();
    }

    public List<float> toGAMACRS3D(Vector3 pos)
    {
        List<float> position = new List<float>();
        // position.Add((int)((pos.x - GamaCRSOffsetX) / GamaCRSCoefX * precision));
        // position.Add((int)((pos.z - GamaCRSOffsetY) / GamaCRSCoefY * precision));
        // position.Add((int)((pos.y - GamaCRSOffsetZ) / GamaCRSCoefZ * precision));
        position.Add((float)(pos.x * precision));
        position.Add((float)(pos.z * precision));
        position.Add((float)(pos.y * precision));

        return position;
    }

    // optional: define a scale between GAMA and Unity for the location given
    public float GamaCRSCoefX = 1.0f;
    public float GamaCRSCoefY = 1.0f;
    public float GamaCRSCoefZ = 1.0f;
    public float GamaCRSOffsetX = 0.001f;
    public float GamaCRSOffsetY = 0.001f;
    public float GamaCRSOffsetZ = 0.001f;

    public int precision = 1;


    public void Restart()
    {
        SimulationManager.Instance.RestartGame();
    }

    public void CountDeadTree()
    {
        dtree++;
    }

    public void DeletePlayer(GameObject obj)
    {
        // if (SimulationManager.Instance.IsGameState(GameState.GAME))
        // {
        //     int instanceId = obj.GetInstanceID();

        //     Dictionary<string, string> args = new Dictionary<string, string> {
        //     {"idP",ConnectionManager.Instance.GetConnectionId() },
        //     {"id", ""+  obj },
        //     {"iid",  ""+instanceId },
        //     };

        //     // Debug.Log("DeletePlayer: " + obj);

        //     // SendExecutableAsk("simulation[0]", "DeletePlayer", args);

        //     ConnectionManager.Instance.SendExecutableAsk("DeletePlayer", args);
        // }
    }
    public void UpdateConstructionPosition(GameObject obj)
    {
        // if (GetSocket() == null || !connected || finalContent.activeSelf) return;

 
            // Debug.Log("sent to GAMA: " + SimulationManager.Instance.currentState);
        // if (SimulationManager.Instance.IsGameState(GameState.GAME))// && UnityEngine.Random.Range(0.0f, 1.0f) < 0.002f)
        // {


        //     Vector2 vF = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        //     Vector2 vR = new Vector2(transform.forward.x, transform.forward.z);
        //     vF.Normalize();
        //     vR.Normalize();
        //     float c = vF.x * vR.x + vF.y * vR.y;
        //     float s = vF.x * vR.y - vF.y * vR.x;
        //     int angle = (int)(((s > 0) ? -1.0 : 1.0) * (180 / Math.PI) * Math.Acos(c) * precision);

        //     List<float> p = toGAMACRS3D(obj.transform.position);
        //     float instanceId = obj.GetInstanceID();

        //     // Vector3 v = new Vector3(Camera.main.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
        //     // List<float> p = toGAMACRS3D(v);
        //     Dictionary<string, string> args = new Dictionary<string, string> {
        //     {"idP",ConnectionManager.Instance.GetConnectionId() },
        //     {"id", ""+  obj },
        //     {"iid",  ""+instanceId },
        //     {"x", "" +p[0]},
        //     {"y", "" +p[1]},
        //     {"z", "" +p[2]},
        //     {"angle", "" +angle}
        //     };

        //     // Debug.Log("move_player_external: " + player + " " + p[0] + "," + p[1] + "," + p[2]);


        //     // Debug.Log("sent to GAMA: " + obj);
        //     // ConnectionManager.Instance.SendExecutableAsk("construction_message", args);
        //     // SendExecutableAsk("simulation[0]", "move_player_external", args);
        // }
    }
    protected string host;
    protected string port;
    private static bool ready = false;
    private TextMeshProUGUI playerTextOutput;

    private static bool NotValid(string ip)
    {
        if (ip == null || ip.Length == 0) return false;
        string[] ipb = ip.Split(".");
        return (ipb.Length != 4);
    }


    public void OnTriggerEnterBtn(Text text)
    {
        string t = text.text;

        if (ready)
        {
            playerTextOutput.text += t;

        }
    }

    public void OnTriggerEnterDelete()
    {

        if (ready && playerTextOutput.text.Length > 0)
        {
            playerTextOutput.text = playerTextOutput.text.Substring(0, playerTextOutput.text.Length - 1);

        }
    }

    public void OnTriggerEnterCancel()
    {

        if (ready && playerTextOutput.text.Length > 0)
        {
            playerTextOutput.text = "";

        }
    }
    protected void HandleConnectionOpen(object sender, System.EventArgs e)
    {
      //  connected = true;
        Debug.Log("ConnectionManager: Connection opened");

    }

}
