using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnPlayerInputUp;
    public static UnityEvent OnPlayerInputDown;
    public static UnityEvent OnPlayerInputRight;
    public static UnityEvent OnPlayerInputLeft;
    public static UnityEvent OnPlayerSelect;
}
