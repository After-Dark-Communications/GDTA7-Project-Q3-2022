using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionScreensData : MonoBehaviour
{
    private PartsCollectionManager collectionManager;
    private CamPreviewManager camPreviewManager;

    public PartsCollectionManager CollectionManager => collectionManager;
    public CamPreviewManager CamPreviewManager => camPreviewManager;

    private void Awake()
    {
        Channels.OnManagerInitialized += OnManagerInitialize;
    }

    private void OnManagerInitialize(object sender, Manager manager)
    {
        if (manager.GetType() == typeof(PartsCollectionManager))
        {
            collectionManager = manager as PartsCollectionManager;
        }

        if (manager.GetType() == typeof(CamPreviewManager))
        {
            camPreviewManager = manager as CamPreviewManager;
        }
    }
}
