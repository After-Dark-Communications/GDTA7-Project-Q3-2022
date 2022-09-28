using Controls;
using ShipSelection;
using ShipSelection.ShipBuilders.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ShipParts
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
        public abstract PartData GetData();

        //protected Transform ShipRoot { get; private set; }
        public Transform ShipRoot;
        protected ShipInputHandler RootInputHandler { get; private set; }
        protected Rigidbody ShipRigidBody { get; private set; }
        protected InputDevice MyInputDevice { get; private set; }

        /// <summary>Sets the events and any other things that the part needs</summary>
        protected abstract void Setup();

        public void SetupPart(Transform root, ShipInputHandler shipInputHandler, Rigidbody rigidbody, InputDevice playerDevice)
        {
            //not virtual because ShipRoot and RootInputHanlder MUST be set before regular setup is called
            ShipRoot = root;
            RootInputHandler = shipInputHandler;
            ShipRigidBody = rigidbody;
            MyInputDevice = playerDevice;
            Setup();
        }
        public ConnectionPointsCollection ConnectionPointCollection => connectionPointCollection;
    }
}
