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
///<summary>unity event with a <see cref="Vector2"/> value parameter</summary>
public class UnityVector2Event : UnityEvent<Vector2> { }
///<summary>unity event with a <see cref="Vector3"/> value parameter</summary>
public class UnityVector3Event : UnityEvent<Vector3> { }
///<summary>unity event for the <see cref="ButtonStates"/> flags</summary>
public class UnityButtonStateEvent : UnityEvent<ButtonStates> { }
///<summary>unity event with a <see cref="GameObject"/> parameter</summary>
public class UnityGameObjectEvent : UnityEvent<GameObject> { }
/// <summary>unity event with the collision impulse <see cref="Vector3"/> value parameter and the <see cref="GameObject"/> that it collided with</summary>
public class UnityCollisionEvent : UnityEvent<Vector3, GameObject> { }

