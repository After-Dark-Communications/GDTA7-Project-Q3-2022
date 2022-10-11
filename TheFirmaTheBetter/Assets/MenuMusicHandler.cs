using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicHandler : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter fmodEvent;
    private FMOD.Studio.EventInstance buildingTheme;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        fmodEvent = GetComponent<FMODUnity.StudioEventEmitter>();
        buildingTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_BuildTheme"); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadBuildingScene()
    {
        fmodEvent.Stop();
        buildingTheme.start();
    }
}
