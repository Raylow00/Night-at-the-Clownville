using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private MissionScriptableObject missionSO;
    [SerializeField] private VoidEvent onMissionInitiateEvent;
    [SerializeField] private VoidEvent onMissionOngoingSendEvent;
    [SerializeField] private VoidEvent onMissionCompleteEvent;
    [SerializeField] private VoidEvent onMissionFailEvent;
    [SerializeField] private VoidEvent onMissionResetEvent;
    #endregion

    #region Private Fields
    private Mission mission;
    #endregion

    void Start()
    {
        mission = new Mission(missionSO.missionName, MissionState.MISSION_IDLE);  
    }

    #region Public Methods
    /// <summary>
    ///     Get the mission scriptable object
    /// </summary>
    /// <returns></returns>
    public MissionScriptableObject GetMissionScriptableObject()
    {
        return missionSO;
    }

    /// <summary>
    ///     Get the mission attached to this manager
    /// </summary>
    /// <returns></returns>
    public Mission GetMission()
    {
        return mission;
    }

    /// <summary>
    ///     Process the new mission state and raise the necessary event
    ///     Then store the new mission state into the scriptable object
    /// </summary>
    /// <param name="arg_newState"></param>
    public void ProcessMissionState(MissionState arg_newState)
    {
        MissionState processedMissionState = mission.SetMissionState(arg_newState);
        if (processedMissionState == MissionState.MISSION_INIT)
        {
            onMissionInitiateEvent.Raise();
        }
        else if (processedMissionState == MissionState.MISSION_ONGOING)
        {
            onMissionOngoingSendEvent.Raise();
        }
        else if (processedMissionState == MissionState.MISSION_COMPLETE)
        {
            onMissionCompleteEvent.Raise();
        }
        else if (processedMissionState == MissionState.MISSION_FAILED)
        {
            onMissionFailEvent.Raise();
        }
        else if (processedMissionState == MissionState.MISSION_RESET)
        {
            onMissionResetEvent.Raise();
        }
        else
        {
            // Do nothing
        }

        missionSO.currMissionState = processedMissionState;
    }
    #endregion
}
