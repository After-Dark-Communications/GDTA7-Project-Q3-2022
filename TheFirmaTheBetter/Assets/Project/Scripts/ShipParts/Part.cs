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
        public abstract PartData GetData();

        protected Transform ShipRoot { get; private set; }
        protected ShipInputHandler RootInputHandler { get; private set; }
        protected Rigidbody ShipRigidBody { get; private set; }

        /// <summary>Sets the events and any other things that the part needs</summary>
        protected abstract void Setup();

        public void SetupPart(Transform root, ShipInputHandler shipInputHandler, Rigidbody rigidbody)
        {
            //not virtual because ShipRoot and RootInputHanlder MUST be set before regular setup is called
            ShipRoot = root;//TODO: fix
            RootInputHandler = shipInputHandler;
            ShipRigidBody = rigidbody;
            Setup();
        }
        public ConnectionPointsCollection ConnectionPointCollection => connectionPointCollection;
    }
}
