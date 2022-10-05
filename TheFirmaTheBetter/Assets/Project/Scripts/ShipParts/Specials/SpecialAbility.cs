using ShipParts;
using ShipSelection.ShipBuilders.ConnectionPoints;
using System;
using UnityEngine;
using Util;

namespace ShipParts.Specials
{
    [AddComponentMenu("Parts/Special")]
    public abstract class SpecialAbility : Part
    {
        [SerializeField]
        private SpecialData specialData;

        protected bool CanDoSpecial = true;

        protected float currentCooldown = 0;

        public override string PartCategoryName => "Special";

        private void Update()
        {
            if (CanDoSpecial)
                return;

            currentCooldown += Time.deltaTime;

            if (currentCooldown >= specialData.AbilityCooldown)
            {
                CanDoSpecial = true;
                currentCooldown = 0;
            }
        }

        public override bool IsMyType(Part part)
        {
            if (part is SpecialAbility)
                return true;

            return false;
        }

        public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
        {
            if (connectionPoint is SpecialConnectionPoint)
                return true;

            return false;
        }

        protected override void Setup()
        {
            if (rootInputHandler != null)
            {
                rootInputHandler.OnPlayerSpecial.AddListener(PerformSpecial);
            }
        }

        private void PerformSpecial(ButtonStates state)
        {
            if (CanDoSpecial == false)
                return;

            if (state == ButtonStates.NONE)
                return;

            if (state.HasFlag(ButtonStates.PERFORMED) == false)
                return;

            CanDoSpecial = false;
            HandleSpecial();
        }

        protected abstract void HandleSpecial();

        public override PartData GetData()
        {
            return specialData;
        }
    }
}