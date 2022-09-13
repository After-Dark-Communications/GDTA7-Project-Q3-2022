using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ShipSelection
{
    public static class AnchorPointsSetter
    {
        private static Vector2 player1Position = Vector2.left + Vector2.up;
        private static Vector2 player2Position = Vector2.right + Vector2.up;
        private static Vector2 player3Position = Vector2.right + Vector2.down;
        private static Vector2 player4Position = Vector2.left + Vector2.down;

        public static void SetAnchorPoints(PlayerInput playerInput)
        {
            RectTransform rectTransform = playerInput.transform.GetComponent<RectTransform>();

            switch (playerInput.playerIndex)
            {
                case 1:
                    SetAnchorPoint(player1Position, rectTransform);
                    break;
                case 2:
                    SetAnchorPoint(player2Position, rectTransform);
                    break;
                case 3:
                    SetAnchorPoint(player3Position, rectTransform);
                    break;
                case 4:
                    SetAnchorPoint(player4Position, rectTransform);
                    break;
                default:
                    break;
            }

            rectTransform.ForceUpdateRectTransforms();
        }

        private static void SetAnchorPoint(Vector2 NewValue, RectTransform toSet)
        {
            toSet.anchorMax = NewValue;
            toSet.anchorMin = NewValue;
            toSet.pivot = NewValue;
        }
    }
}
