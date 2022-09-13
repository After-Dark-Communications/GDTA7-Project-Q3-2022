using System;
using UnityEngine;

public class MovementChannel
{
    public EventHandler<Vector2> OnNavigateUI_Up;
    public EventHandler<Vector2> OnNavigateUI_Down;
    public EventHandler<Vector2> OnNavigateUI_Left;
    public EventHandler<Vector2> OnNavigateUI_Right;
}