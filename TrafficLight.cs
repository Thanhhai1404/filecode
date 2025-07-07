using System;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject greenLight;
    public GameObject yellowLight;
    public GameObject redLight;

    private float timer;
    private enum LightState { Green, Yellow, Red }
    private LightState currentState;

    void Start()
    {
        currentState = LightState.Green;
        timer = 5f;
        UpdateLights();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SwitchLight();
        }
    }

    void SwitchLight()
    {
        if (currentState == LightState.Green)
        {
            currentState = LightState.Yellow;
            timer = 2f;
        }
        else if (currentState == LightState.Yellow)
        {
            currentState = LightState.Red;
            timer = 5f;
        }
        else
        {
            currentState = LightState.Green;
            timer = 5f;
        }

        UpdateLights();
    }

    void UpdateLights()
    {
        greenLight.SetActive(currentState == LightState.Green);
        yellowLight.SetActive(currentState == LightState.Yellow);
        redLight.SetActive(currentState == LightState.Red);
    }

    public bool CurrentLightIsRed()
    {
        return currentState == LightState.Red;
    }
}
