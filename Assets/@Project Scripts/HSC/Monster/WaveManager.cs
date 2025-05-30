using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float type1WaveTiming = 30f;
    public Action type1WaveStart;

    public float type2WaveTiming = 50f;
    public Action type2WaveStart;

    void Update()
    {
        type1WaveTiming -= Time.deltaTime;
        if (type1WaveTiming <= 0f)
        {
            type1WaveTiming = 30f; // Reset timing
            type1WaveStart?.Invoke(); // Trigger wave start event
        }

        type2WaveTiming -= Time.deltaTime;
        if (type2WaveTiming <= 0f)
        {
            type2WaveTiming = 50f; // Reset timing
            type2WaveStart?.Invoke(); // Trigger wave start event
        }
    }
}
