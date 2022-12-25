using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapHandler : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private GameObject mapGO;
    private bool isMapActivated;
    [HideInInspector] public bool IsMapActivated => isMapActivated;
    private bool isMapFlipped;
    [HideInInspector] public bool IsMapFlipped => isMapFlipped;

    [Header("Weapon Handler")]
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    void Awake()
    {
        mapGO.SetActive(false);
        isMapActivated = false;
    }

    public void UseMap()
    {
        if(!isMapActivated)
        {
            ViewMap();
        }
        else if(isMapActivated && !isMapFlipped)
        {
            FlipMap();
        }
        else if(isMapFlipped)
        {
            ExitMap();
        }
    }

    public void ViewMap()
    {
        weaponHandler.CurrentWeaponGO.SetActive(false);

        mapGO.SetActive(true);
        mapGO.GetComponent<Animator>().SetTrigger("view");

        isMapActivated = true;

        // Debug.Log("Viewing map");
    }

    public void FlipMap()
    {
        mapGO.GetComponent<Animator>().SetTrigger("flip");

        isMapFlipped = true;

        // Debug.Log("Flipping map");
    }

    public void ExitMap()
    {
        mapGO.GetComponent<Animator>().SetTrigger("exit");

        weaponHandler.CurrentWeaponGO.SetActive(true);

        isMapActivated = false;
        isMapFlipped = false;

        // Debug.Log("Exiting map");
    }
}
