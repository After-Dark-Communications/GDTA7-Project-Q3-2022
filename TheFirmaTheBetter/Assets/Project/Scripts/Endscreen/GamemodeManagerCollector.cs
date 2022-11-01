using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeManagerCollector : MonoBehaviour
{
    private void Awake()
    {
        GameModeManager.Instance.gameObject.transform.parent = this.gameObject.transform;
        Managers.ResultsManager.Instance.gameObject.transform.parent = this.gameObject.transform;
    }
}
