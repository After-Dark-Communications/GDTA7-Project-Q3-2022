using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Parts
{
    [AddComponentMenu("Parts/Weapon")]
    public class Weapon : Part
    {
        [SerializeField]
        private WeaponData weaponData;

        public override string PartName => throw new System.NotImplementedException();

        public override void Setup()
        {
            if (RootInputHanlder != null)
            {
                RootInputHanlder.OnPlayerAim.AddListener(AimWeapon);
                RootInputHanlder.OnPlayerShoot.AddListener(ShootWeapon);
            }
        }

        private void ShootWeapon(ButtonStates state)
        {
            Debug.LogError("ShootWeapon is not implemented!");
        }

        private void AimWeapon(float rotation)
        {
            //rotation is for left/right
            Debug.LogError("AimWeapon is not implemented!");
        }
    }
}