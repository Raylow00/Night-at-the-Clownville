using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour, IInteractable
{
    [SerializeField] private bool onStartOff;
    [SerializeField] private GameObject[] lights;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private VoidEvent onLightsOffEvent;
    [SerializeField] private VoidEvent onLightsOnEvent;

    public void PressInteractiveButton()
    {
        // Not used
    }

    public void HoldInteractiveButton()
    {
        TurnOnLights();
    }

    public void CancelHoldInteractiveButton()
    {
        // not used
    }

    public void TurnOffLights()
    {
        foreach(GameObject light in lights)
        {
            light.SetActive(false);
        }

        onLightsOffEvent.Raise();
    }

    public void Mission2_Event_TurnOffLights()
    {
        // Debug.Log("[LightsManager] Turning off lights");

        audioManager.PlayAudio("Turn_Off_Lights");

        StartCoroutine(Coroutine_TurnOffLights_2f55());
    }

    public void TurnOnLights()
    {
        foreach(GameObject light in lights)
        {
            // Debug.Log("[LightsManager] Turning on lights: " + light.gameObject.name);
            light.SetActive(true);
        }

        onLightsOnEvent.Raise();
    }

    private IEnumerator Coroutine_TurnOffLights_2f55()
    {
        yield return new WaitForSeconds(2.55f);

        TurnOffLights();
    }
}
