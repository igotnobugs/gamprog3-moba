using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Counts time and does absolutely nothing else

//[RequireComponent(typeof(PlayableDirector))]
public class TimeSystem : MonoBehaviour
{  
    [SerializeField]
    private TextMeshProUGUI timeText;

    private float totalTime = 0.0f;


    private void Start()
    {
        if (timeText == null)
        {
            Debug.Log("Text to display time not set");
            enabled = false;
            return;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        totalTime += Time.deltaTime;

        SetTimeText(totalTime);
    }

    private void SetTimeText(float newText)
    {
        if (timeText == null) return;

        timeText.text = Mathf.Floor(newText).ToString();
    }
    
    public void SetTimeSpeed(float timeScale)
    {
        if (timeScale < 0) return;

        Time.timeScale = timeScale;
    }

}
