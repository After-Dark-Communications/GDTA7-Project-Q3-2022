using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    private TMP_Text statName;

    public TMP_Text StatName { get { return statName; } set { statName = value; } }
}
