using ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Parts
{
    public abstract class Part : MonoBehaviour
    {
        [SerializeField]
        private Image partIcon;
        [SerializeField]
        private Vector3 connectionPoint;

        [SerializeField]
        private ConnectionPointsCollection connectionPointCollection;

        public virtual string PartCategoryName => "part";

        public abstract bool IsMyType(Part part);
        public abstract bool IsMyConnectionType(ConnectionPoint connectionPoint);

        public ConnectionPointsCollection ConnectionPointCollection => connectionPointCollection;
        protected Transform ShipRoot { get; private set; }
        protected ShipInputHandler RootInputHanlder { get; private set; }

        /// <summary>Sets the events and any other things that the part needs</summary>
        protected abstract void Setup();

        public void SetupPart(Transform root)
        {
            //not virtual because ShipRoot and RootInputHanlder MUST be set before regular setup is called
            ShipRoot = root;//TODO: fix
            RootInputHanlder = ShipRoot.GetComponent<ShipInputHandler>();
            Setup();
        }
    }
}
