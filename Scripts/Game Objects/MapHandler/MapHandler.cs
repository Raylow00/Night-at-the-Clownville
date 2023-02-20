using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    MAP_ACTIVATED,
    MAP_DEACTIVATED,
    MAP_FLIPPED
}

public class MapHandler : MonoBehaviour
{
    #region Serialized Fields
    [Header("Map Prefab")]
    [SerializeField] private GameObject mapGO;

    [Header("Events")]
    [SerializeField] private VoidEvent onMapActivateEvent;
    [SerializeField] private VoidEvent onMapDeactivateEvent;
    [SerializeField] private VoidEvent onMapFlipEvent;
    #endregion

    #region Private Fields
    private MapState currentMapState = MapState.MAP_DEACTIVATED;
    #endregion

    void Start()
    {
        InitMap();    
    }

    #region Public Methods
    public MapState GetCurrentMapState()
    {
        return currentMapState;
    }

    /// <summary>
    ///     Activate, flip and deactivate map according to the current map state
    ///     The events raised are for animation purposes. Attach a VoidEventListener 
    ///     to listen to the map state, referenc Animator to SetTrigger if necessary
    /// </summary>
    public void UseMap()
    {
        switch (currentMapState)
        {
            case MapState.MAP_DEACTIVATED:
                currentMapState = MapState.MAP_ACTIVATED;
                mapGO.SetActive(true);

                // Send map activated event
                onMapActivateEvent.Raise();
                
                break;

            case MapState.MAP_ACTIVATED:
                currentMapState = MapState.MAP_FLIPPED;

                // Send map flip event
                onMapFlipEvent.Raise();

                break;

            case MapState.MAP_FLIPPED:
                InitMap();
                mapGO.SetActive(false);

                // Send map deactivate event
                onMapDeactivateEvent.Raise();

                break;

            default:
                InitMap();
                break;
        }
    }
    #endregion

    #region Private Methods
    private void InitMap()
    {
        currentMapState = MapState.MAP_DEACTIVATED;
    }
    #endregion
}
