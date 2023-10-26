using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum animationState { none, idle, walk, run, eat, howl, attack, die, jump, swim, reaction };
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    private bool _activated;
    public bool Activated { get { return _activated; } set { _activated = value; } }

    protected abstract void interaction();
    public void OnMouseDown()
    {
        OnTouchDetected();
    }

    public void OnTouchDetected()
    {
        if(!_activated)
        {
            interaction();
        }
    }
}
