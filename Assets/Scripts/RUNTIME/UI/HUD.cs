using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerResourcesManager playerResourcesManager;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private Image timeBar;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private TextMeshProUGUI step;
    [SerializeField] private TextMeshProUGUI resoucre;
    
    // Update is called once per frame
    void Update()
    {
        UpdateLevelUI();
        UpdateResourcesUI();
    }

    void UpdateLevelUI()
    {
        if (!levelManager) return;

        if (levelManager.Finished)
        {
            time.text = "FINISHED";
        }
        else
        {
            timeBar.fillAmount = (levelManager.CurrentWaveStepTime - levelManager.CurrentTime) / levelManager.CurrentWaveStepTime;

            int minutes = Mathf.FloorToInt(levelManager.CurrentTime / 60F);
            int seconds = Mathf.FloorToInt(levelManager.CurrentTime - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            time.text = niceTime;
        }
        wave.text = "WAVE " + levelManager.CurrentWave + "/" + levelManager.MaxWave;
        step.text = levelManager.CurrentWaveStep;
    }

    void UpdateResourcesUI()
    {
        if (playerResourcesManager)
            resoucre.text = playerResourcesManager.CurrentAmount.ToString();
    }
}
