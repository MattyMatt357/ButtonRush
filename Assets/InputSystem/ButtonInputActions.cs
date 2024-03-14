//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputSystem/ButtonInputActions.inputactions
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

public partial class @ButtonInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ButtonInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ButtonInputActions"",
    ""maps"": [
        {
            ""name"": ""Buttons"",
            ""id"": ""570b9978-f4d6-419b-a4dd-b2eaff0a1bc0"",
            ""actions"": [
                {
                    ""name"": ""UseButton1"",
                    ""type"": ""Button"",
                    ""id"": ""7f8a84c9-ff21-4245-8460-cd04e57561c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseButton2"",
                    ""type"": ""Button"",
                    ""id"": ""fcb2f035-634a-48ef-9cfd-7312537d09fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseButton3"",
                    ""type"": ""Button"",
                    ""id"": ""6182eb61-dbb9-4234-a267-afc78de09a3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseButton4"",
                    ""type"": ""Button"",
                    ""id"": ""54dbeda3-f5f5-4dcb-a20c-ba52bc938b95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cf5f0c36-d911-4204-84dd-0500e7882682"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""968d6bb9-1f9a-4540-99c6-0214d8523a5e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd9cc62b-fc01-4efd-8c18-d708284bc81a"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""977100c6-3c0f-4b96-8833-961307ec8a0b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0e77eae-c483-49fa-884f-2b7582af0c40"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""c698bc2c-ac28-406e-bf49-39f0e2cd8dd4"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton3"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""e7eedef9-9c21-44e4-9dbe-e0593efaf95d"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Binding"",
                    ""id"": ""62142081-0ea4-4b13-a426-9550606e56f2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""09ecca4e-16c0-4f0e-8740-a5d4d0cca8ec"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""830135e2-a5b6-41dc-9128-067ce79606da"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton4"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""944c48aa-6ea0-448c-b2ac-5efb35c30242"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""642107fd-c7c3-453b-8f61-43fdd9145a12"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseButton4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Buttons
        m_Buttons = asset.FindActionMap("Buttons", throwIfNotFound: true);
        m_Buttons_UseButton1 = m_Buttons.FindAction("UseButton1", throwIfNotFound: true);
        m_Buttons_UseButton2 = m_Buttons.FindAction("UseButton2", throwIfNotFound: true);
        m_Buttons_UseButton3 = m_Buttons.FindAction("UseButton3", throwIfNotFound: true);
        m_Buttons_UseButton4 = m_Buttons.FindAction("UseButton4", throwIfNotFound: true);
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

    // Buttons
    private readonly InputActionMap m_Buttons;
    private List<IButtonsActions> m_ButtonsActionsCallbackInterfaces = new List<IButtonsActions>();
    private readonly InputAction m_Buttons_UseButton1;
    private readonly InputAction m_Buttons_UseButton2;
    private readonly InputAction m_Buttons_UseButton3;
    private readonly InputAction m_Buttons_UseButton4;
    public struct ButtonsActions
    {
        private @ButtonInputActions m_Wrapper;
        public ButtonsActions(@ButtonInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @UseButton1 => m_Wrapper.m_Buttons_UseButton1;
        public InputAction @UseButton2 => m_Wrapper.m_Buttons_UseButton2;
        public InputAction @UseButton3 => m_Wrapper.m_Buttons_UseButton3;
        public InputAction @UseButton4 => m_Wrapper.m_Buttons_UseButton4;
        public InputActionMap Get() { return m_Wrapper.m_Buttons; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ButtonsActions set) { return set.Get(); }
        public void AddCallbacks(IButtonsActions instance)
        {
            if (instance == null || m_Wrapper.m_ButtonsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ButtonsActionsCallbackInterfaces.Add(instance);
            @UseButton1.started += instance.OnUseButton1;
            @UseButton1.performed += instance.OnUseButton1;
            @UseButton1.canceled += instance.OnUseButton1;
            @UseButton2.started += instance.OnUseButton2;
            @UseButton2.performed += instance.OnUseButton2;
            @UseButton2.canceled += instance.OnUseButton2;
            @UseButton3.started += instance.OnUseButton3;
            @UseButton3.performed += instance.OnUseButton3;
            @UseButton3.canceled += instance.OnUseButton3;
            @UseButton4.started += instance.OnUseButton4;
            @UseButton4.performed += instance.OnUseButton4;
            @UseButton4.canceled += instance.OnUseButton4;
        }

        private void UnregisterCallbacks(IButtonsActions instance)
        {
            @UseButton1.started -= instance.OnUseButton1;
            @UseButton1.performed -= instance.OnUseButton1;
            @UseButton1.canceled -= instance.OnUseButton1;
            @UseButton2.started -= instance.OnUseButton2;
            @UseButton2.performed -= instance.OnUseButton2;
            @UseButton2.canceled -= instance.OnUseButton2;
            @UseButton3.started -= instance.OnUseButton3;
            @UseButton3.performed -= instance.OnUseButton3;
            @UseButton3.canceled -= instance.OnUseButton3;
            @UseButton4.started -= instance.OnUseButton4;
            @UseButton4.performed -= instance.OnUseButton4;
            @UseButton4.canceled -= instance.OnUseButton4;
        }

        public void RemoveCallbacks(IButtonsActions instance)
        {
            if (m_Wrapper.m_ButtonsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IButtonsActions instance)
        {
            foreach (var item in m_Wrapper.m_ButtonsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ButtonsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ButtonsActions @Buttons => new ButtonsActions(this);
    public interface IButtonsActions
    {
        void OnUseButton1(InputAction.CallbackContext context);
        void OnUseButton2(InputAction.CallbackContext context);
        void OnUseButton3(InputAction.CallbackContext context);
        void OnUseButton4(InputAction.CallbackContext context);
    }
}
