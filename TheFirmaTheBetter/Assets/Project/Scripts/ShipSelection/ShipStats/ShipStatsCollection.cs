using ShipSelection.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipSelection
{
    public class ShipStatsCollection : MonoBehaviour
    {
        [SerializeField]
        private List<ShipStat> shipStats = new List<ShipStat>();
        [SerializeField]
        private List<WeaponStat> weaponStats = new List<WeaponStat>();
        [SerializeField]
        private List<SpecialStat> specialStats = new List<SpecialStat>();


        private void Start()
        {
            //foreach (Stat stat in gameObject.GetComponentsInChildren<Stat>())
            //{
            //    switch (stat)
            //    {
            //        case ShipStat shipStat:
            //            shipStats.Add(shipStat);
            //            break;
            //        case WeaponStat weaponStat:
            //            weaponStats.Add(weaponStat);
            //            break;
            //        case SpecialStat specialStat:
            //            specialStats.Add(specialStat);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        public List<ShipStat> ShipStats { get => shipStats; }

        public List<WeaponStat> WeaponStats { get => weaponStats; }

        public List<SpecialStat> SpecialStats { get => specialStats; }


    }
}