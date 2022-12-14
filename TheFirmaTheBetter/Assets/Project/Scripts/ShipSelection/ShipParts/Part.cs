using Collisions;
using Controls;
using EventSystem;
using ShipParts.Ship;
using ShipSelection;
using ShipSelection.ShipBuilders.ConnectionPoints;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ShipParts
{
    public abstract class Part : MonoBehaviour
    {
        [SerializeField]
        private Sprite partIcon;
        [SerializeField]
        private Vector3 connectionPoint;
        [SerializeField]
        private ConnectionPointsCollection connectionPointCollection;

        public virtual string PartCategoryName => "part";

        public abstract bool IsMyType(Part part);
        public abstract bool IsMyConnectionType(ConnectionPoint connectionPoint);
        public abstract PartData GetData();

        protected Transform shipRoot { get; private set; }
        protected ShipInputHandler rootInputHandler { get; private set; }
        protected Rigidbody shipRigidBody { get; private set; }
        protected InputDevice myInputDevice { get; private set; }
        protected ShipCollision thisCollision { get; private set; }
        protected ShipStats Stats { get; private set; }

        private void Awake()
        {
            CalculateHighestAndLowest();
        }


        /// <summary>Sets the events and any other things that the part needs</summary>
        protected abstract void Setup();

        public void SetupPart(Transform root, ShipInputHandler shipInputHandler, Rigidbody rigidbody, InputDevice playerDevice, ShipCollision shipCollision)
        {
            //not virtual because ShipRoot and RootInputHanlder MUST be set before regular setup is called
            shipRoot = root;
            rootInputHandler = shipInputHandler;
            shipRigidBody = rigidbody;
            myInputDevice = playerDevice;
            thisCollision = shipCollision;
            this.Stats = thisCollision.GetComponent<ShipResources>()?.ShipStats;
            Setup();
        }

        protected virtual void CalculateHighestAndLowest() { }

        public ConnectionPointsCollection ConnectionPointCollection => connectionPointCollection;

        public Sprite PartIcon { get => partIcon; set => partIcon = value; }
    }
}
