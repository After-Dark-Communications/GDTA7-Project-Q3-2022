using EventSystem;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    public virtual void Start()
    {
        Channels.OnManagerInitialized?.Invoke(this);
    }
}
