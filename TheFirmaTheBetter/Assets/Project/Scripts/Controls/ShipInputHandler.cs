using Collisions;
using EventSystem;
using ShipParts;
using ShipParts.Ship;
using ShipSelection;
using ShipSelection.ShipBuilders;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

/*
 * Steef is right :)
 * -Luc
 */
namespace Controls
{
    public class ShipInputHandler : MonoBehaviour
    {
        public UnityVector2Event OnPlayerMove = new UnityVector2Event();
        public UnityFloatEvent OnPlayerForwardBackward = new UnityFloatEvent();
        public UnityFloatEvent OnPlayerLeftRight = new UnityFloatEvent();
        public UnityFloatEvent OnPlayerAim = new UnityFloatEvent();
        public UnityButtonStateEvent OnPlayerShoot = new UnityButtonStateEvent();
        public UnityButtonStateEvent OnPlayerPause = new UnityButtonStateEvent();
        public UnityButtonStateEvent OnPlayerSpecial = new UnityButtonStateEvent();
        public UnityButtonStateEvent OnPlayerMoveUp = new UnityButtonStateEvent();
        public UnityButtonStateEvent OnPlayerMoveDown = new UnityButtonStateEvent();

        private Rigidbody _Rb;
        private InputAction _Move;
        private InputAction _Aim;

        private ShipInfo _ShipInfo;

        private void Start()
        {
            //Enable input events
            SetupInputEvents();
            //ensure rigidbody exists
            //_Rb = GetComponent<Rigidbody>();
            //if (_Rb == null)
            //{
            //    //_Rb = gameObject.AddComponent<Rigidbody>();
            //    _Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            //    _Rb.drag = 7;
            //}

            SetupParts();

        }

        private void OnDisable()
        {
            PlayerControlls.PlayerActions actions = new PlayerControlls().Player;
            InputActionMap controls = GetInputActions();
            _Move = controls.FindAction(actions.Move.name);
            _Aim = controls.FindAction(actions.Aim.name);

            _Move.Disable();
            _Aim.Disable();

            controls.FindAction(actions.MoveUp.name).started -= OnMoveUp;
            controls.FindAction(actions.MoveDown.name).started -= OnMoveDown;
            controls.FindAction(actions.Pause.name).started -= OnPause;
            controls.FindAction(actions.Special.name).started -= OnSpecial;

            controls.FindAction(actions.Fire.name).performed -= OnFire;
        }

        private void SetupParts()
        {
            _ShipInfo = GetComponentInParent<ShipInfo>();

            if (_ShipInfo != null)
            {
                foreach (ShipBuilder shipBuilder in ShipBuildManager.Instance.ShipBuilders)
                {
                    if (shipBuilder.PlayerNumber != _ShipInfo.PlayerNumber)
                        continue;
                    _Rb = shipBuilder.transform.parent.GetComponent<Rigidbody>();
                    _Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                    foreach (Part part in shipBuilder.SelectedParts)
                    {
                        part.SetupPart(shipBuilder.transform.parent.transform, this, _Rb, shipBuilder.PlayerDevice, shipBuilder.GetComponent<ShipCollision>());
                    }
                }
            }
        }



        private void Update()
        {
            if (_Move.activeControl != null)
            {
                Vector2 move = _Move.ReadValue<Vector2>();
                if (move != null)
                { OnPlayerMove?.Invoke(move); }

            }
            if (_Aim.activeControl != null)
            {
                Vector2 aim = _Aim.ReadValue<Vector2>();
                if (aim != null)
                {
                    OnPlayerAim?.Invoke(aim.x);
                }
            }
        }

        private void SetupInputEvents()
        {
            PlayerControlls.PlayerActions actions = new PlayerControlls().Player;
            InputActionMap controls = GetInputActions();
            _Move = controls.FindAction(actions.Move.name);
            _Aim = controls.FindAction(actions.Aim.name);

            _Move.Enable();
            _Aim.Enable();

            controls.FindAction(actions.MoveUp.name).started += OnMoveUp;
            controls.FindAction(actions.MoveDown.name).started += OnMoveDown;
            controls.FindAction(actions.Pause.name).started += OnPause;
            controls.FindAction(actions.Special.name).performed += OnSpecial;

            controls.FindAction(actions.Fire.name).performed += OnFire;
            controls.FindAction(actions.Fire.name).canceled += OnFire;
        }

        private InputActionMap GetInputActions()
        {
            PlayerInput input = GetComponent<PlayerInput>();
            return input.actions.FindActionMap("Player");
        }

        public void OnFire(InputAction.CallbackContext ctx)
        {
            OnPlayerShoot.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
        }

        public void OnPause(InputAction.CallbackContext ctx)
        {
            OnPlayerPause.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
        }

        public void OnSpecial(InputAction.CallbackContext ctx)
        {
            OnPlayerSpecial.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
        }

        public void OnMoveUp(InputAction.CallbackContext ctx)
        {
            OnPlayerMoveUp.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
        }

        public void OnMoveDown(InputAction.CallbackContext ctx)
        {
            OnPlayerMoveDown.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
        }
    }
}