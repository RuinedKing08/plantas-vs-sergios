using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    public event Action<int> OnMinutePassed; 
    public event Action<float> OnSecondTick; 

    private float elapsedTime = 0f;
    public float ElapsedTime => elapsedTime;    
    private int minutesPassed = 0;


    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        OnSecondTick?.Invoke(elapsedTime);

        int currentMinutes = Mathf.FloorToInt(elapsedTime / 60f);
        if (currentMinutes > minutesPassed)
        {
            minutesPassed = currentMinutes;
            OnMinutePassed?.Invoke(minutesPassed);
        }
    }


}
