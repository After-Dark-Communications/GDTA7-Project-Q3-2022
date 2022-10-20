using EventSystem;
using UnityEngine;

namespace Managers
{
    public abstract class Manager : MonoBehaviour
    {
        public virtual void Start()
        {
            Channels.OnManagerInitialized?.Invoke(this);
        }
    }
}