using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hintable : MonoBehaviour, IHintable
{
    [Header("Interactive Hint")]
    [SerializeField] private string hint;

    public string GetHint()
    {
        return hint;
    }

    public void SetHint(string value)
    {
        hint = value;
    }
}
