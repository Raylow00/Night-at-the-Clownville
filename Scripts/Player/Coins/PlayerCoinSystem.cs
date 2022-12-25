using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinSystem : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Events")]
    [SerializeField] private VoidEvent onCoinCollectEvent;
    [SerializeField] private IntEvent onCoinChangeEvent;
    [SerializeField] private VoidEvent onCoinZeroEvent;

    [Header("SFX")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string coinCollectClipName;

    public PlayerCoin playerCoin;

    void Awake()
    {
        playerCoin = new PlayerCoin(playerStats.playerCurrentCoins, onCoinChangeEvent, onCoinZeroEvent);
    }

    void Start()
    {
        if(onCoinChangeEvent != null) onCoinChangeEvent.Raise(playerCoin.currentCoins);
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Coin")
        {
            // Debug.Log("[PlayerCoinSystem] Collecting R coin");

            if(onCoinCollectEvent != null) onCoinCollectEvent.Raise();

            audioManager.PlayAudio(coinCollectClipName);

            playerStats.playerCurrentCoins = playerCoin.CollectCoins(1);

            if(col.gameObject.transform.parent != null) Destroy(col.gameObject.transform.parent.gameObject);
            else Destroy(col.gameObject);
        }
    }
}
