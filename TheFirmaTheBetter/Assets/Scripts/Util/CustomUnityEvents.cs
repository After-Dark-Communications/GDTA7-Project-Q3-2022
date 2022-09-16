using System;
using UnityEngine;
using UnityEngine.Events;
using Util;

/// <summary>unity event with a float value parameter.</summary>
public class UnityFloatEvent : UnityEvent<float> { }
/// <summary>unity event with a boolean value parameter.</summary>
public class UnityBoolEvent : UnityEvent<bool> { }
/// <summary>unity event with a boolean array parameter.</summary>
public class UnityBoolsEvent : UnityEvent<bool[]> { }
///<summary>unity event with a Vector2 value parameter</summary>
public class UnityVector2Event : UnityEvent<Vector2> { }
///<summary>unity event for the buttonstates flags</summary>
public class UnityButtonStateEvent : UnityEvent<ButtonStates> { }


