//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/InputActions.inputactions
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

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""b7695500-b369-4584-8879-219eefc21843"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""56523df7-1f39-4fd6-b49f-2eb1c522d7b5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""Value"",
                    ""id"": ""34dcd682-5ea0-4963-b8e5-a82cbca759a2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""Value"",
                    ""id"": ""c23e1754-93a2-43a9-9e9c-242e4260f467"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryTap"",
                    ""type"": ""Button"",
                    ""id"": ""5fdf18c8-fddf-4da6-bc08-b5d36fdfa9c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,SlowTap(duration=0.21)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryHold"",
                    ""type"": ""Value"",
                    ""id"": ""f5721d2e-11f0-4876-96f4-b2caa19df584"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5e609b40-1a30-4f96-b847-095ee286bdd1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77d902ba-65e4-449a-8f88-f07195b0f317"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1dd927b-81bd-4370-ad81-0e38f6764974"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4583c7a7-f1bb-4bc9-9f7e-5a0e6357ac80"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dbedf27f-61a6-456a-9df0-f0e83107ebd9"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_Move = m_Touch.FindAction("Move", throwIfNotFound: true);
        m_Touch_PrimaryContact = m_Touch.FindAction("PrimaryContact", throwIfNotFound: true);
        m_Touch_PrimaryPosition = m_Touch.FindAction("PrimaryPosition", throwIfNotFound: true);
        m_Touch_PrimaryTap = m_Touch.FindAction("PrimaryTap", throwIfNotFound: true);
        m_Touch_PrimaryHold = m_Touch.FindAction("PrimaryHold", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private List<ITouchActions> m_TouchActionsCallbackInterfaces = new List<ITouchActions>();
    private readonly InputAction m_Touch_Move;
    private readonly InputAction m_Touch_PrimaryContact;
    private readonly InputAction m_Touch_PrimaryPosition;
    private readonly InputAction m_Touch_PrimaryTap;
    private readonly InputAction m_Touch_PrimaryHold;
    public struct TouchActions
    {
        private @InputActions m_Wrapper;
        public TouchActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Touch_Move;
        public InputAction @PrimaryContact => m_Wrapper.m_Touch_PrimaryContact;
        public InputAction @PrimaryPosition => m_Wrapper.m_Touch_PrimaryPosition;
        public InputAction @PrimaryTap => m_Wrapper.m_Touch_PrimaryTap;
        public InputAction @PrimaryHold => m_Wrapper.m_Touch_PrimaryHold;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void AddCallbacks(ITouchActions instance)
        {
            if (instance == null || m_Wrapper.m_TouchActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TouchActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @PrimaryContact.started += instance.OnPrimaryContact;
            @PrimaryContact.performed += instance.OnPrimaryContact;
            @PrimaryContact.canceled += instance.OnPrimaryContact;
            @PrimaryPosition.started += instance.OnPrimaryPosition;
            @PrimaryPosition.performed += instance.OnPrimaryPosition;
            @PrimaryPosition.canceled += instance.OnPrimaryPosition;
            @PrimaryTap.started += instance.OnPrimaryTap;
            @PrimaryTap.performed += instance.OnPrimaryTap;
            @PrimaryTap.canceled += instance.OnPrimaryTap;
            @PrimaryHold.started += instance.OnPrimaryHold;
            @PrimaryHold.performed += instance.OnPrimaryHold;
            @PrimaryHold.canceled += instance.OnPrimaryHold;
        }

        private void UnregisterCallbacks(ITouchActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @PrimaryContact.started -= instance.OnPrimaryContact;
            @PrimaryContact.performed -= instance.OnPrimaryContact;
            @PrimaryContact.canceled -= instance.OnPrimaryContact;
            @PrimaryPosition.started -= instance.OnPrimaryPosition;
            @PrimaryPosition.performed -= instance.OnPrimaryPosition;
            @PrimaryPosition.canceled -= instance.OnPrimaryPosition;
            @PrimaryTap.started -= instance.OnPrimaryTap;
            @PrimaryTap.performed -= instance.OnPrimaryTap;
            @PrimaryTap.canceled -= instance.OnPrimaryTap;
            @PrimaryHold.started -= instance.OnPrimaryHold;
            @PrimaryHold.performed -= instance.OnPrimaryHold;
            @PrimaryHold.canceled -= instance.OnPrimaryHold;
        }

        public void RemoveCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITouchActions instance)
        {
            foreach (var item in m_Wrapper.m_TouchActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TouchActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    public interface ITouchActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
        void OnPrimaryTap(InputAction.CallbackContext context);
        void OnPrimaryHold(InputAction.CallbackContext context);
    }
}
