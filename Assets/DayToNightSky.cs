using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayToNightSky : MonoBehaviour
{
    private float SubsidenceScore = 0.0f;
    public Light directionalLight;
    public Material skyboxMaterial; // Gán skybox hiện tại (phải là dạng Procedural Skybox)
    public float transitionDuration = 5f;
    public Color dayTint = Color.cyan;
    public Color nightTint = Color.gray;

    private float timer = 0f;
    private bool isTransitioning = false;
    private Color startTint;
    
    public AudioSource audioSound;

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
        audioSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Trigger
        {
            if (!isTransitioning)
            {
                audioSound.Play();
                timer = 0f;
                //startTint = skyboxMaterial.GetColor("_Tint");
                isTransitioning = true;
            }
        }

        SubsidenceScore = SubsidenceManager.currentSubsidenceLevel;

        if (SubsidenceScore > 0.9f)
        {
            if (!isTransitioning)
            {
                audioSound.Play();
                timer = 0f;
                //startTint = skyboxMaterial.GetColor("_Tint");
                isTransitioning = true;
            }
        }

        if (isTransitioning)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionDuration);

            // Lerp SkyTint
            //Color currentTint = Color.Lerp(startTint, nightTint, t);
            //skyboxMaterial.SetColor("_Tint", currentTint);

            // Lerp ánh sáng
            directionalLight.intensity = Mathf.Lerp(1.8f, 0.33f, t);

            if (t >= 1f)
                isTransitioning = false;
        }
    }
}
