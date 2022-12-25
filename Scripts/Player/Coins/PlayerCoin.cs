using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : ICollectable
{
    public int currentCoins { get; private set; }
    public IntEvent onCoinChangeEvent;
    public VoidEvent onCoinZeroEvent;

    public PlayerCoin(int coins, IntEvent coinChangeEvent, VoidEvent coinZeroEvent)
    {
        currentCoins = coins;

        onCoinChangeEvent = coinChangeEvent;

        onCoinZeroEvent = coinZeroEvent;
    }

    public int CollectCoins(int increment)
    {
        currentCoins += increment;

        if(onCoinChangeEvent != null) onCoinChangeEvent.Raise(currentCoins);

        return currentCoins;
    }

    public int UseCoins(int decrement)
    {
        if(CheckIfSufficient(decrement))
        {
            currentCoins -= decrement;

            if(onCoinChangeEvent != null) onCoinChangeEvent.Raise(currentCoins);

            CheckIfZero();
        }

        return currentCoins;
    }

    private bool CheckIfSufficient(int dec)
    {
        if(currentCoins >= dec) return true;

        else return false;
    }

    private void CheckIfZero()
    {
        if(currentCoins > 0) return;

        currentCoins = 0;

        if(onCoinChangeEvent != null) onCoinChangeEvent.Raise(currentCoins);

        if(onCoinZeroEvent != null) onCoinZeroEvent.Raise();
    }
}
