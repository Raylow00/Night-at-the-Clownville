using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEventTrigger : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private Sprite spriteToSend;
    [SerializeField] private SpriteEvent spriteEvent;
    #endregion

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent(spriteToSend);
        }
    }

    /// <summary>
    ///     Triggers the sprite event and send the sprite out
    /// </summary>
    /// <param name="arg_spriteToSend"></param>
    public void TriggerEvent(Sprite arg_spriteToSend)
    {
        spriteEvent.Raise(arg_spriteToSend);
    }

}
