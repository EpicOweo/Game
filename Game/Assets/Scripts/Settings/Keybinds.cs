//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Settings/Keybinds.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Settings
{
    public partial class @Keybinds: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Keybinds()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Keybinds"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""208de887-eda0-4cce-9828-2772909f6349"",
            ""actions"": [
                {
                    ""name"": ""Horizontal Movement"",
                    ""type"": ""Button"",
                    ""id"": ""fbbc8940-a63c-4d07-8a9c-0068f147014e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Vertical Movement"",
                    ""type"": ""Button"",
                    ""id"": ""ae62fbde-a9da-4ca4-bf2e-0ce262f0b3ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5791d056-cbff-46c5-ba09-965068155c3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fall Through"",
                    ""type"": ""Button"",
                    ""id"": ""c07077cd-a31f-43ea-ba2b-5514c472c93d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slow Down"",
                    ""type"": ""Button"",
                    ""id"": ""dcde3913-8245-43ba-be1b-a371d777362b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d63d69a0-d4fc-4e2b-af25-c479c7e25054"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""572b6bfa-d886-48b2-a115-090e71309388"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""967074f8-3838-42da-a5e8-5f79e12505aa"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Horizontal Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4b9f741e-7968-489c-8937-f40e60335e66"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d091d943-1522-485a-9851-2933603b2575"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Fall Through"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d717d2fd-5224-4240-a0d1-cbe27099bcdf"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""402d507b-87e9-43b5-a745-025744d1cf03"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d0fa23a2-dd58-469a-9cc0-ceb4c48914ed"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Vertical Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""40b05df6-31bf-45b9-a55d-afce237cadd4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Slow Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KBM"",
            ""bindingGroup"": ""KBM"",
            ""devices"": []
        }
    ]
}");
            // Player Movement
            m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
            m_PlayerMovement_HorizontalMovement = m_PlayerMovement.FindAction("Horizontal Movement", throwIfNotFound: true);
            m_PlayerMovement_VerticalMovement = m_PlayerMovement.FindAction("Vertical Movement", throwIfNotFound: true);
            m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
            m_PlayerMovement_FallThrough = m_PlayerMovement.FindAction("Fall Through", throwIfNotFound: true);
            m_PlayerMovement_SlowDown = m_PlayerMovement.FindAction("Slow Down", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Player Movement
        private readonly InputActionMap m_PlayerMovement;
        private List<IPlayerMovementActions> m_PlayerMovementActionsCallbackInterfaces = new List<IPlayerMovementActions>();
        private readonly InputAction m_PlayerMovement_HorizontalMovement;
        private readonly InputAction m_PlayerMovement_VerticalMovement;
        private readonly InputAction m_PlayerMovement_Jump;
        private readonly InputAction m_PlayerMovement_FallThrough;
        private readonly InputAction m_PlayerMovement_SlowDown;
        public struct PlayerMovementActions
        {
            private @Keybinds m_Wrapper;
            public PlayerMovementActions(@Keybinds wrapper) { m_Wrapper = wrapper; }
            public InputAction @HorizontalMovement => m_Wrapper.m_PlayerMovement_HorizontalMovement;
            public InputAction @VerticalMovement => m_Wrapper.m_PlayerMovement_VerticalMovement;
            public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
            public InputAction @FallThrough => m_Wrapper.m_PlayerMovement_FallThrough;
            public InputAction @SlowDown => m_Wrapper.m_PlayerMovement_SlowDown;
            public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerMovementActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Add(instance);
                @HorizontalMovement.started += instance.OnHorizontalMovement;
                @HorizontalMovement.performed += instance.OnHorizontalMovement;
                @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                @VerticalMovement.started += instance.OnVerticalMovement;
                @VerticalMovement.performed += instance.OnVerticalMovement;
                @VerticalMovement.canceled += instance.OnVerticalMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @FallThrough.started += instance.OnFallThrough;
                @FallThrough.performed += instance.OnFallThrough;
                @FallThrough.canceled += instance.OnFallThrough;
                @SlowDown.started += instance.OnSlowDown;
                @SlowDown.performed += instance.OnSlowDown;
                @SlowDown.canceled += instance.OnSlowDown;
            }

            private void UnregisterCallbacks(IPlayerMovementActions instance)
            {
                @HorizontalMovement.started -= instance.OnHorizontalMovement;
                @HorizontalMovement.performed -= instance.OnHorizontalMovement;
                @HorizontalMovement.canceled -= instance.OnHorizontalMovement;
                @VerticalMovement.started -= instance.OnVerticalMovement;
                @VerticalMovement.performed -= instance.OnVerticalMovement;
                @VerticalMovement.canceled -= instance.OnVerticalMovement;
                @Jump.started -= instance.OnJump;
                @Jump.performed -= instance.OnJump;
                @Jump.canceled -= instance.OnJump;
                @FallThrough.started -= instance.OnFallThrough;
                @FallThrough.performed -= instance.OnFallThrough;
                @FallThrough.canceled -= instance.OnFallThrough;
                @SlowDown.started -= instance.OnSlowDown;
                @SlowDown.performed -= instance.OnSlowDown;
                @SlowDown.canceled -= instance.OnSlowDown;
            }

            public void RemoveCallbacks(IPlayerMovementActions instance)
            {
                if (m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerMovementActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerMovementActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
        private int m_KBMSchemeIndex = -1;
        public InputControlScheme KBMScheme
        {
            get
            {
                if (m_KBMSchemeIndex == -1) m_KBMSchemeIndex = asset.FindControlSchemeIndex("KBM");
                return asset.controlSchemes[m_KBMSchemeIndex];
            }
        }
        public interface IPlayerMovementActions
        {
            void OnHorizontalMovement(InputAction.CallbackContext context);
            void OnVerticalMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnFallThrough(InputAction.CallbackContext context);
            void OnSlowDown(InputAction.CallbackContext context);
        }
    }
}
