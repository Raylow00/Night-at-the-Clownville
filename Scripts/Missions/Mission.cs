// The player can start and complete a mission or has yet to complete it
// but can never fail it
// unless the mission is a timed mission
using UnityEngine;

public class Mission : MonoBehaviour
{
    protected bool hasBegun = false;
    protected bool isCompleted = false;
    protected bool hasFailed = false;

    public bool Initiate()
    {
        if(hasBegun == false) hasBegun = true;
        return hasBegun;
    }

    public bool Complete()
    {
        if(!hasBegun) return false;

        isCompleted = true;
        return isCompleted;
    }

    public bool Fail()
    {
        if(!hasBegun) return false;

        hasFailed = true;
        return hasFailed;
    }
}
