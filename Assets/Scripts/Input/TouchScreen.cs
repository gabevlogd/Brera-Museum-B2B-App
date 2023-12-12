//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/TouchScreen.inputactions
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

public partial class @TouchScreen: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchScreen()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchScreen"",
    ""maps"": [
        {
            ""name"": ""PuzzleActions"",
            ""id"": ""e1edfaa6-3765-4d65-95bf-0dfa660ac77e"",
            ""actions"": [
                {
                    ""name"": ""TouchPos"",
                    ""type"": ""Value"",
                    ""id"": ""e17834cd-b684-4532-8df3-2b3a95cdaab2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c4d9dcab-7def-4064-9c2d-39a6262f6c2d"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SightActions"",
            ""id"": ""a98caf49-1cd2-42ef-8036-b89a4a888251"",
            ""actions"": [
                {
                    ""name"": ""PinchZoom"",
                    ""type"": ""Value"",
                    ""id"": ""fc661637-2275-423e-84d6-3f7008851afa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseZoom"",
                    ""type"": ""Value"",
                    ""id"": ""212084c9-4661-438c-9e3c-428abd5cdf8c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateSight"",
                    ""type"": ""Value"",
                    ""id"": ""a4bd5622-da3a-4d52-bac5-5f865f58b044"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73c98b7d-6458-43cc-be19-fe99c60f747e"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PinchZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48f6e71c-4c5e-4cb0-9a88-138ab5b525a8"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a57a428e-6326-4879-9730-47e589c22750"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateSight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PuzzleActions
        m_PuzzleActions = asset.FindActionMap("PuzzleActions", throwIfNotFound: true);
        m_PuzzleActions_TouchPos = m_PuzzleActions.FindAction("TouchPos", throwIfNotFound: true);
        // SightActions
        m_SightActions = asset.FindActionMap("SightActions", throwIfNotFound: true);
        m_SightActions_PinchZoom = m_SightActions.FindAction("PinchZoom", throwIfNotFound: true);
        m_SightActions_MouseZoom = m_SightActions.FindAction("MouseZoom", throwIfNotFound: true);
        m_SightActions_RotateSight = m_SightActions.FindAction("RotateSight", throwIfNotFound: true);
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

    // PuzzleActions
    private readonly InputActionMap m_PuzzleActions;
    private List<IPuzzleActionsActions> m_PuzzleActionsActionsCallbackInterfaces = new List<IPuzzleActionsActions>();
    private readonly InputAction m_PuzzleActions_TouchPos;
    public struct PuzzleActionsActions
    {
        private @TouchScreen m_Wrapper;
        public PuzzleActionsActions(@TouchScreen wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchPos => m_Wrapper.m_PuzzleActions_TouchPos;
        public InputActionMap Get() { return m_Wrapper.m_PuzzleActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PuzzleActionsActions set) { return set.Get(); }
        public void AddCallbacks(IPuzzleActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_PuzzleActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PuzzleActionsActionsCallbackInterfaces.Add(instance);
            @TouchPos.started += instance.OnTouchPos;
            @TouchPos.performed += instance.OnTouchPos;
            @TouchPos.canceled += instance.OnTouchPos;
        }

        private void UnregisterCallbacks(IPuzzleActionsActions instance)
        {
            @TouchPos.started -= instance.OnTouchPos;
            @TouchPos.performed -= instance.OnTouchPos;
            @TouchPos.canceled -= instance.OnTouchPos;
        }

        public void RemoveCallbacks(IPuzzleActionsActions instance)
        {
            if (m_Wrapper.m_PuzzleActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPuzzleActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_PuzzleActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PuzzleActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PuzzleActionsActions @PuzzleActions => new PuzzleActionsActions(this);

    // SightActions
    private readonly InputActionMap m_SightActions;
    private List<ISightActionsActions> m_SightActionsActionsCallbackInterfaces = new List<ISightActionsActions>();
    private readonly InputAction m_SightActions_PinchZoom;
    private readonly InputAction m_SightActions_MouseZoom;
    private readonly InputAction m_SightActions_RotateSight;
    public struct SightActionsActions
    {
        private @TouchScreen m_Wrapper;
        public SightActionsActions(@TouchScreen wrapper) { m_Wrapper = wrapper; }
        public InputAction @PinchZoom => m_Wrapper.m_SightActions_PinchZoom;
        public InputAction @MouseZoom => m_Wrapper.m_SightActions_MouseZoom;
        public InputAction @RotateSight => m_Wrapper.m_SightActions_RotateSight;
        public InputActionMap Get() { return m_Wrapper.m_SightActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SightActionsActions set) { return set.Get(); }
        public void AddCallbacks(ISightActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_SightActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SightActionsActionsCallbackInterfaces.Add(instance);
            @PinchZoom.started += instance.OnPinchZoom;
            @PinchZoom.performed += instance.OnPinchZoom;
            @PinchZoom.canceled += instance.OnPinchZoom;
            @MouseZoom.started += instance.OnMouseZoom;
            @MouseZoom.performed += instance.OnMouseZoom;
            @MouseZoom.canceled += instance.OnMouseZoom;
            @RotateSight.started += instance.OnRotateSight;
            @RotateSight.performed += instance.OnRotateSight;
            @RotateSight.canceled += instance.OnRotateSight;
        }

        private void UnregisterCallbacks(ISightActionsActions instance)
        {
            @PinchZoom.started -= instance.OnPinchZoom;
            @PinchZoom.performed -= instance.OnPinchZoom;
            @PinchZoom.canceled -= instance.OnPinchZoom;
            @MouseZoom.started -= instance.OnMouseZoom;
            @MouseZoom.performed -= instance.OnMouseZoom;
            @MouseZoom.canceled -= instance.OnMouseZoom;
            @RotateSight.started -= instance.OnRotateSight;
            @RotateSight.performed -= instance.OnRotateSight;
            @RotateSight.canceled -= instance.OnRotateSight;
        }

        public void RemoveCallbacks(ISightActionsActions instance)
        {
            if (m_Wrapper.m_SightActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISightActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_SightActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SightActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SightActionsActions @SightActions => new SightActionsActions(this);
    public interface IPuzzleActionsActions
    {
        void OnTouchPos(InputAction.CallbackContext context);
    }
    public interface ISightActionsActions
    {
        void OnPinchZoom(InputAction.CallbackContext context);
        void OnMouseZoom(InputAction.CallbackContext context);
        void OnRotateSight(InputAction.CallbackContext context);
    }
}
