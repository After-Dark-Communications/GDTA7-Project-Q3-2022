using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Channels
{
    public static MovementChannel Movement = new MovementChannel();
    public static EventHandler<Manager> OnManagerInitialized;
}
