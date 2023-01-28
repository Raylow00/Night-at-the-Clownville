public enum MissionState
{
    MISSION_IDLE,       // no mission is ongoing
    MISSION_INIT,       // mission initiated
    MISSION_ONGOING,    // mission ongoing
    MISSION_COMPLETE,   // mission completed
    MISSION_FAILED,     // mission failed
    MISSION_RESET       // mission reset coming from ONGOING
}

public class Mission
{
    #region Private Fields
    private string missionName;
    private MissionState currMissionState = MissionState.MISSION_IDLE;
    #endregion

    #region Public Methods
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="arg_missionName"></param>
    /// <param name="arg_missionState"></param>
    public Mission(string arg_missionName, MissionState arg_missionState)
    {
        missionName = arg_missionName;
        currMissionState = arg_missionState;
    }

    /// <summary>
    ///     Get the mission name
    /// </summary>
    /// <returns></returns>
    public string GetMissionName()
    {
        return missionName;
    }

    /// <summary>
    ///     Get the current mission state
    /// </summary>
    /// <returns></returns>
    public MissionState GetMissionState()
    {
        return currMissionState;
    }

    /// <summary>
    ///     Set mission state according to the preconditions
    /// </summary>
    /// <param name="arg_newState"></param>
    public void SetMissionState(MissionState arg_newState)
    {
        switch (arg_newState)
        {
            case MissionState.MISSION_IDLE:
                if (currMissionState == MissionState.MISSION_COMPLETE ||
                    currMissionState == MissionState.MISSION_FAILED)
                {
                    currMissionState = MissionState.MISSION_IDLE;
                }
                break;
            case MissionState.MISSION_INIT:
                if (currMissionState == MissionState.MISSION_IDLE ||
                    currMissionState == MissionState.MISSION_RESET)
                {
                    currMissionState = MissionState.MISSION_INIT;
                }
                break;
            case MissionState.MISSION_ONGOING:
                if (currMissionState == MissionState.MISSION_INIT)
                {
                    currMissionState = MissionState.MISSION_ONGOING;
                }
                break;
            case MissionState.MISSION_COMPLETE:
                if (currMissionState == MissionState.MISSION_ONGOING)
                {
                    currMissionState = MissionState.MISSION_COMPLETE;
                }
                break;
            case MissionState.MISSION_FAILED:
                if (currMissionState == MissionState.MISSION_ONGOING)
                {
                    currMissionState = MissionState.MISSION_FAILED;
                }
                break;
            case MissionState.MISSION_RESET:
                if (currMissionState == MissionState.MISSION_ONGOING)
                {
                    currMissionState = MissionState.MISSION_RESET;
                }
                break;
            default:
                currMissionState = MissionState.MISSION_IDLE;
                break;
        }
    }
    #endregion
}
