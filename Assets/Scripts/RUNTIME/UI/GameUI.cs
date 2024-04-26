using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform head;
    [SerializeField] private float spawnDistance;

    [SerializeField] private GameObject startContent;
    [SerializeField] private GameObject finalContent;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI finalText;
    [SerializeField] private string winText = "YOU WON!!!";
    [SerializeField] private string loseText = "YOU LOST!!!";

    void Start()
    {
        transform.position = head.position + new Vector3(head.forward.x, 0 , head.forward.z).normalized * spawnDistance;
        startContent.SetActive(true);
        finalContent.SetActive(false);
    }

    void Update()
    {
        if (gameManager.CurrentGameStatus() == GameStatus.Win || gameManager.CurrentGameStatus() == GameStatus.Lose)
        {
            transform.position = head.position + new Vector3(head.forward.x, 0 , head.forward.z).normalized * spawnDistance;
            startContent.SetActive(false);
            finalContent.SetActive(true);

            if (gameManager.CurrentGameStatus() == GameStatus.Win)
            {
                finalText.text = winText;
                finalText.text += "\nSCORE: " + gameManager.Score;
            }
            if (gameManager.CurrentGameStatus() == GameStatus.Lose)
            {
                finalText.text = loseText;
            }
        }

        transform.LookAt(new Vector3(head.position.x, transform.position.y, head.position.z));
        transform.forward *= -1;
    }
    
    public void StartUI()
    {
        gameManager.StartLevel();
        startContent.SetActive(false);
    }

    public void RetryUI()
    {
        gameManager.RestartLevel();
    }
}
