using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    public virtual void Start()
    {
        Channels.OnManagerInitialized.Invoke(gameObject, this);
    }
}
