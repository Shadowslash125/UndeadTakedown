using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] PlayerState playerState;
    float elapsedTime;
    float StoppedTime;
    int minutes = 0;
    int seconds = 0;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        if (playerState == null)
            Debug.LogError("PlayerState component not found on GameObject!");
        if (timerText == null)
            Debug.LogError("TimerText (TextMeshProUGUI) is not assigned in the Inspector!");
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);
        if (timerText != null)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (playerState != null && !playerState.isAlive)
        {
            StoppedTime = elapsedTime;
            minutes = Mathf.FloorToInt(StoppedTime / 60);
            seconds = Mathf.FloorToInt(StoppedTime % 60);
            if (timerText != null)
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
