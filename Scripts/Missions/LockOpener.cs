using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOpener : MonoBehaviour, IInteractable
{
    [Header("Mission SO")]
    [SerializeField] private MissionSO missionData;

    [Header("Message when lock is unopenable")]
    [SerializeField] private string message;

    [Header("Events to raise")]
    [SerializeField] private VoidEvent onLockOpenedEvent;
    [SerializeField] private StringEvent onLockStillLockedEvent;

    void Start()
    {
        if(missionData.lockOpened) OpenLock();
    }

    public void PressInteractiveButton()
    {
        OpenLock();
    }

    public void HoldInteractiveButton()
    {
        // Not used
    }

    public void CancelHoldInteractiveButton()
    {
        // not used
    }

    public void OpenLock()
    {
        if(missionData.keyObtained)
        {
            onLockOpenedEvent.Raise();
            missionData.lockOpened = true;
            // Debug.Log("Lock opened!");
        }
        else
        {
            onLockStillLockedEvent.Raise(message);
            // Debug.Log(message);
        }
    }
}
