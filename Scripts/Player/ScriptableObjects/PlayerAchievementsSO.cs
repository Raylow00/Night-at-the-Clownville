using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAchievementsSystem", menuName = "Player/Achievements System")]
public class PlayerAchievementsSO : ScriptableObject
{
    public Dictionary<string, int> kills = new Dictionary<string, int>{{"santa_claus", 0}, 
                                                                        {"elf", 0}, 
                                                                        {"giant_snowball", 0},
                                                                        {"gingerbread", 0},
                                                                        {"snowman", 0},
                                                                        {"reindeer", 0},
                                                                        {"winding_toy", 0}
                                                                        }; 

    public Dictionary<string, int> criticalHitKills = new Dictionary<string, int>{{"santa_claus", 0}, 
                                                                                    {"elf", 0}, 
                                                                                    {"giant_snowball", 0},
                                                                                    {"gingerbread", 0},
                                                                                    {"snowman", 0},
                                                                                    {"reindeer", 0},
                                                                                    {"winding_toy", 0}
                                                                                    }; 
    public int killStreaks;                         // Number of kills without taking damage
    public int easterEggsUnlocked;

    public float timeTakenToCompleteEachMission;    // Unlock trophies at each station
    public float timeTakenToCompleteAllMissions;    // Unlock badges of levels as a theme park player

    public float distanceCoveredOnFeet;
    public float distanceCoveredOnWheels;

    public float overallGameScore;
}
