using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy
{
    [System.Serializable]
    public class PlayerEnergyType
    {
        public string energyID;
        public float energyAmount;
    }
    public float maxEnergy { get; private set; }
    public float currentEnergy { get; private set; }
    public FloatEvent onEnergyChangeEvent;
    public VoidEvent onEnergyZeroEvent;

    public PlayerEnergy(float curEnergy, float mEnergy, FloatEvent energyChangeEvent, VoidEvent energyZeroEvent)
    {
        currentEnergy = curEnergy;

        maxEnergy = mEnergy;

        CheckIfMax();

        onEnergyChangeEvent = energyChangeEvent;

        onEnergyZeroEvent = energyZeroEvent;
    }

    public float IncreaseEnergy(float increment)
    {
        currentEnergy += increment;

        if(onEnergyChangeEvent != null) onEnergyChangeEvent.Raise(currentEnergy);
        
        CheckIfMax();
        
        return currentEnergy;
    }

    public float DecreaseEnergy(float decrement)
    {
        currentEnergy -= decrement;

        if(onEnergyChangeEvent != null) onEnergyChangeEvent.Raise(currentEnergy);

        CheckIfZero();

        return currentEnergy;
    }

    private void CheckIfMax()
    {
        if(currentEnergy <= maxEnergy) return;

        currentEnergy = maxEnergy;

        if(onEnergyChangeEvent != null) onEnergyChangeEvent.Raise(currentEnergy);
    }

    private void CheckIfZero()
    {
        if(currentEnergy > 0) return;
        
        currentEnergy = 0f;

        if(onEnergyChangeEvent != null) onEnergyChangeEvent.Raise(currentEnergy);

        if(onEnergyZeroEvent != null) onEnergyZeroEvent.Raise();
    }
}
