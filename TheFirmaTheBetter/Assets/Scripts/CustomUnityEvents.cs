using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>unity event with a float value parameter.</summary>
public class UnityFloatEvent: UnityEvent<float>{ }
/// <summary>unity event with a boolean value parameter.</summary>
public class UnityBoolEvent : UnityEvent<bool> { }
/// <summary>
/// <para>unity event with a boolean array parameter.</para>
/// <para>If this event is used with shipInputHandler, it will contain three bools:started,performed,canceled. in that order.</para>
/// </summary>
public class UnityBoolsEvent : UnityEvent<bool[]> { }
///<summary>unity event with a Vector2 value parameter</summary>
public class UnityVector2Event : UnityEvent<Vector2> { }

