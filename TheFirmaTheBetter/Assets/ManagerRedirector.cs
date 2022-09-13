using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerRedirector : MonoBehaviour
{
    #region Not a Singleton
    public static ManagerRedirector Instance;

    private void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
        }
        partsCollectionManager = GetComponentInChildren<PartsCollectionManager>();
    }
    #endregion
    private PartsCollectionManager partsCollectionManager;

    public PartsCollectionManager PartsCollectionManager { get => partsCollectionManager; }
}
