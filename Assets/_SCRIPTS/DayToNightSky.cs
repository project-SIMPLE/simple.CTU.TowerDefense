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
    public AudioClip newClip;

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
        audioSound = GetComponent<AudioSource>();
        skyboxMaterial.SetColor("_Tint", dayTint);
    }

    void Update()
    {
        // if (Input.GetKeyDown("5")) // Trigger
        // {
        //     if (!isTransitioning)
        //     {
        //         //audioSound.Play();
        //         ChangeAudioClip(newClip);
        //         timer = 0f;
        //         startTint = skyboxMaterial.GetColor("_Tint");
        //         isTransitioning = true;
        //     }
        // }

        SubsidenceScore = SubsidenceManager.currentSubsidenceLevel;

        if (SubsidenceScore > 1.7f)
        {
            if (!isTransitioning)
            {
                //audioSound.Play();
                ChangeAudioClip(newClip);
                timer = 0f;
                startTint = skyboxMaterial.GetColor("_Tint");
                isTransitioning = true;
            }
        }

        if (isTransitioning)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionDuration);

            // Lerp SkyTint
            Color currentTint = Color.Lerp(startTint, nightTint, t);
            skyboxMaterial.SetColor("_Tint", currentTint);

            // Lerp ánh sáng
            directionalLight.intensity = Mathf.Lerp(2.0f, 0.2f, t);

            // if (t >= 1f)
            //     isTransitioning = false;
        }
    }

    void ChangeAudioClip(AudioClip newClip)
    {
        audioSound.Stop(); // Dừng clip hiện tại
        audioSound.clip = newClip; // Gán clip mới
        audioSound.Play(); // Phát clip mới
    }
}
