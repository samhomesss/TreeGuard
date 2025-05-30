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

    private void Start()
    {
        type1WaveStart += ShowWaveUI;
        type2WaveStart += ShowWaveUI;
    }

    public float type1WaveTiming = 30f;
    public Action type1WaveStart;
    public bool stopType1Wave = false;

    public float type2WaveTiming = 50f;
    public Action type2WaveStart;
    public bool stopType2Wave = false;

    void Update()
    {
        if(!stopType1Wave)
        {
            type1WaveTiming -= Time.deltaTime;
            if (type1WaveTiming <= 0f)
            {
                type1WaveTiming = 30f; // Reset timing
                type1WaveStart?.Invoke(); // Trigger wave start event
            }
        }

        if (stopType2Wave) return; // If type 2 wave is stopped, skip the rest

        type2WaveTiming -= Time.deltaTime;
        if (type2WaveTiming <= 0f)
        {
            type2WaveTiming = 50f; // Reset timing
            type2WaveStart?.Invoke(); // Trigger wave start event
        }
    }

    public void ShowWaveUI()
    {
        Managers.Game.WaveComming();
    }
}
