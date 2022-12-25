using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAchievementsSystem : MonoBehaviour
{
    [SerializeField] private PlayerAchievementsSO achievementsSO;

    public void SetNumberOfKills(string enemyType, int n)
    {
        achievementsSO.kills[enemyType] = n;
    }

    public void SetNumberOfCriticalHitKills(string enemyType, int n)
    {
        achievementsSO.criticalHitKills[enemyType] = n;
    }

    public void SetNumberOfKillStreaks(int n)
    {
        achievementsSO.killStreaks = n;
    }

    public void SetNumberOfEasterEggsUnlocked(int n)
    {
        achievementsSO.easterEggsUnlocked = n;
    }

    public void SetTimeTakenToCompleteEachMission(float t)
    {
        achievementsSO.timeTakenToCompleteEachMission = t;
    }

    public void SetTimeTakenToCompleteAllMissions(float t)
    {
        achievementsSO.timeTakenToCompleteAllMissions = t;
    }

    public void SetDistanceCoveredOnFeet(float d)
    {
        achievementsSO.distanceCoveredOnFeet = d;
    }

    public void SetDistanceCoveredOnWheels(float d)
    {
        achievementsSO.distanceCoveredOnWheels = d;
    }

    public void SetOverallGameScore(float s)
    {
        achievementsSO.overallGameScore = s;
    }

    // Private functions that do all calculations for the metrics above
}
