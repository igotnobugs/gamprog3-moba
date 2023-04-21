using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

// Sets transistion based on time

public class DayNightCycle : MonoBehaviour
{
    [Header("Skybox")]
    [SerializeField, Tooltip("Use a material that has a blend transistion")]
    private Material skyboxMaterial;

    [SerializeField, Tooltip("The variable to transistion in the skybox material.")]
    private string transistionVariable = "_CubemapTransition";

    [Header("Lighting")]
    [SerializeField, Tooltip("Light to modify")]
    private Light mainLight;

    [SerializeField, Tooltip("Intensity during the peak of the day")]
    private float dayLightIntensity = 1.0f;

    [SerializeField, Tooltip("Intensity during the peak of night")]
    private float nightLightIntensity = 0.0f;

    [Header("Used by the Timeline")]
    [SerializeField, Tooltip("Automatically set by the Timeline")]
    private float transition = 0.0f;

    [Space]

    public UnityEvent OnDayStarted;
    public UnityEvent OnNightStarted;

    private bool isDayNightActivated = true;

    // Start is called before the first frame update
    private void Start()
    {
        mainLight.intensity = dayLightIntensity;
        RenderSettings.skybox = skyboxMaterial;
        RenderSettings.skybox.SetFloat(transistionVariable, transition);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isDayNightActivated) return;

        RenderSettings.skybox.SetFloat(transistionVariable, transition);
        
    }

    private void OnDestroy()
    {
        //Reset it
        RenderSettings.skybox.SetFloat(transistionVariable, 0);
    }

    #region Used in Signal Reciever
    public void DayTimeReached()
    {
        mainLight.intensity = dayLightIntensity;
        OnDayStarted?.Invoke();
        //Debug.Log("Day!");
    }

    public void NightTimeReached()
    {
        mainLight.intensity = nightLightIntensity;
        OnNightStarted?.Invoke();
        //Debug.Log("Night!");
    }
    #endregion

    #region Debug Functions
    public void ToggleDayNightCycle()
    {
        isDayNightActivated = !isDayNightActivated;
    }
    #endregion
}
