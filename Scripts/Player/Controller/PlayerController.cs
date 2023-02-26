// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Controller/PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""GameplayControls"",
            ""id"": ""2837c2e8-d31e-4bf7-aa34-0054889fcebf"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""57be6f3c-64d8-4079-9702-85368a6ddd42"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fa99cc75-e6c9-478d-880d-0194cefc231a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e4afd591-4373-40c4-b100-449e9706ec7f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f2e7689c-b936-4c57-aadb-810c7373871a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""44141984-5add-4291-9459-d405670e2913"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon1"",
                    ""type"": ""Button"",
                    ""id"": ""c612d0c3-c4cc-467d-8331-89954c6f3dd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon2"",
                    ""type"": ""Button"",
                    ""id"": ""23969124-1bce-4fc0-b16b-2e8561b7dca4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon3"",
                    ""type"": ""Button"",
                    ""id"": ""e1a3705f-8c13-4b38-a786-0643749ee660"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon4"",
                    ""type"": ""Button"",
                    ""id"": ""139d1663-2428-4273-b067-110f63e4ed88"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon5"",
                    ""type"": ""Button"",
                    ""id"": ""88373227-4b83-491b-846a-755fd2780e60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleWeaponSkin"",
                    ""type"": ""Button"",
                    ""id"": ""2795a3b6-e19a-46ad-8b66-d19873c0faa6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""Button"",
                    ""id"": ""c46689b4-e33a-4d32-9b56-571a3b14a226"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Flashlight"",
                    ""type"": ""Button"",
                    ""id"": ""fde092e4-ee5f-49a5-ac6d-d0f5dcc25fa7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchEquipment"",
                    ""type"": ""Button"",
                    ""id"": ""733db6dc-0bd0-4cc1-a9de-1a2248bf72d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseMap"",
                    ""type"": ""Button"",
                    ""id"": ""553e98c1-ad20-4a75-ab6a-fbaa9a5292c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PressInteractive"",
                    ""type"": ""Button"",
                    ""id"": ""bd869091-023d-4d32-9714-261e3448b34e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=2),Press""
                },
                {
                    ""name"": ""PauseAction"",
                    ""type"": ""Button"",
                    ""id"": ""c8df8fd7-ff2e-4852-a7ee-5eee21f5da27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""146d0898-fecf-4a66-8f00-de4abb589454"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inspect"",
                    ""type"": ""Button"",
                    ""id"": ""4fe70128-be54-4c95-ba74-672a29f49f6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""48dbd056-180f-4d3d-aa90-f8e72ac4a111"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""55ec3b5e-5831-45b7-bae6-d9a656851600"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c986809b-d2be-47da-a481-46e411bbfd95"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""90cc33b4-a2bb-4fa4-aaa8-37bad8f30f19"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""39361344-1c55-4428-8bf4-c55c70d3d883"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f62a48b3-225c-4aa4-8be7-dc4eb27b9153"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e7b3787-d32f-4e88-979a-facf025a46c6"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ad785f4-2ab2-4fa1-aec6-b90ee097d0cc"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""089fbefe-7163-4a5c-b9cc-8444e4501553"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07783b6b-5ecf-45c5-81da-51167ce1bea6"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc4ff614-4aa6-4ef3-b1a5-da533eb073d6"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd157a9c-8a4c-468c-85e8-522780973235"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d30ebeb-6f0a-4911-a104-315752e2220e"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb65fd92-7b3e-42de-af98-463d4d06e23f"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8650747-fc4c-4a14-ba95-41adfc0af3ac"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d446ae15-d147-4a61-931f-3af708a02cfe"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchEquipment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c8847a4-cbaf-408c-b1ef-a3cf6b58c787"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c11ee1b-662d-48f1-9a82-4920e02323b8"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseMap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79405c0b-6fd8-434f-9350-6853509f7f3d"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleWeaponSkin"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd33a060-33e4-4bfe-9da9-14e2520b7771"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressInteractive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9dcd979-37b1-457f-8415-521accb93084"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""554bc3b9-e015-4e46-a15e-4a1e3b91584f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b8bce8d-53a3-486f-85ed-10ebfdd21f3f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c7e52cb-dab5-41ce-9285-5fe1031e40f4"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inspect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GameplayControls
        m_GameplayControls = asset.FindActionMap("GameplayControls", throwIfNotFound: true);
        m_GameplayControls_HorizontalMovement = m_GameplayControls.FindAction("HorizontalMovement", throwIfNotFound: true);
        m_GameplayControls_Jump = m_GameplayControls.FindAction("Jump", throwIfNotFound: true);
        m_GameplayControls_MouseX = m_GameplayControls.FindAction("MouseX", throwIfNotFound: true);
        m_GameplayControls_MouseY = m_GameplayControls.FindAction("MouseY", throwIfNotFound: true);
        m_GameplayControls_Crouch = m_GameplayControls.FindAction("Crouch", throwIfNotFound: true);
        m_GameplayControls_Weapon1 = m_GameplayControls.FindAction("Weapon1", throwIfNotFound: true);
        m_GameplayControls_Weapon2 = m_GameplayControls.FindAction("Weapon2", throwIfNotFound: true);
        m_GameplayControls_Weapon3 = m_GameplayControls.FindAction("Weapon3", throwIfNotFound: true);
        m_GameplayControls_Weapon4 = m_GameplayControls.FindAction("Weapon4", throwIfNotFound: true);
        m_GameplayControls_Weapon5 = m_GameplayControls.FindAction("Weapon5", throwIfNotFound: true);
        m_GameplayControls_ToggleWeaponSkin = m_GameplayControls.FindAction("ToggleWeaponSkin", throwIfNotFound: true);
        m_GameplayControls_Camera = m_GameplayControls.FindAction("Camera", throwIfNotFound: true);
        m_GameplayControls_Flashlight = m_GameplayControls.FindAction("Flashlight", throwIfNotFound: true);
        m_GameplayControls_SwitchEquipment = m_GameplayControls.FindAction("SwitchEquipment", throwIfNotFound: true);
        m_GameplayControls_UseMap = m_GameplayControls.FindAction("UseMap", throwIfNotFound: true);
        m_GameplayControls_PressInteractive = m_GameplayControls.FindAction("PressInteractive", throwIfNotFound: true);
        m_GameplayControls_PauseAction = m_GameplayControls.FindAction("PauseAction", throwIfNotFound: true);
        m_GameplayControls_Reload = m_GameplayControls.FindAction("Reload", throwIfNotFound: true);
        m_GameplayControls_Inspect = m_GameplayControls.FindAction("Inspect", throwIfNotFound: true);
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

    // GameplayControls
    private readonly InputActionMap m_GameplayControls;
    private IGameplayControlsActions m_GameplayControlsActionsCallbackInterface;
    private readonly InputAction m_GameplayControls_HorizontalMovement;
    private readonly InputAction m_GameplayControls_Jump;
    private readonly InputAction m_GameplayControls_MouseX;
    private readonly InputAction m_GameplayControls_MouseY;
    private readonly InputAction m_GameplayControls_Crouch;
    private readonly InputAction m_GameplayControls_Weapon1;
    private readonly InputAction m_GameplayControls_Weapon2;
    private readonly InputAction m_GameplayControls_Weapon3;
    private readonly InputAction m_GameplayControls_Weapon4;
    private readonly InputAction m_GameplayControls_Weapon5;
    private readonly InputAction m_GameplayControls_ToggleWeaponSkin;
    private readonly InputAction m_GameplayControls_Camera;
    private readonly InputAction m_GameplayControls_Flashlight;
    private readonly InputAction m_GameplayControls_SwitchEquipment;
    private readonly InputAction m_GameplayControls_UseMap;
    private readonly InputAction m_GameplayControls_PressInteractive;
    private readonly InputAction m_GameplayControls_PauseAction;
    private readonly InputAction m_GameplayControls_Reload;
    private readonly InputAction m_GameplayControls_Inspect;
    public struct GameplayControlsActions
    {
        private @PlayerController m_Wrapper;
        public GameplayControlsActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_GameplayControls_HorizontalMovement;
        public InputAction @Jump => m_Wrapper.m_GameplayControls_Jump;
        public InputAction @MouseX => m_Wrapper.m_GameplayControls_MouseX;
        public InputAction @MouseY => m_Wrapper.m_GameplayControls_MouseY;
        public InputAction @Crouch => m_Wrapper.m_GameplayControls_Crouch;
        public InputAction @Weapon1 => m_Wrapper.m_GameplayControls_Weapon1;
        public InputAction @Weapon2 => m_Wrapper.m_GameplayControls_Weapon2;
        public InputAction @Weapon3 => m_Wrapper.m_GameplayControls_Weapon3;
        public InputAction @Weapon4 => m_Wrapper.m_GameplayControls_Weapon4;
        public InputAction @Weapon5 => m_Wrapper.m_GameplayControls_Weapon5;
        public InputAction @ToggleWeaponSkin => m_Wrapper.m_GameplayControls_ToggleWeaponSkin;
        public InputAction @Camera => m_Wrapper.m_GameplayControls_Camera;
        public InputAction @Flashlight => m_Wrapper.m_GameplayControls_Flashlight;
        public InputAction @SwitchEquipment => m_Wrapper.m_GameplayControls_SwitchEquipment;
        public InputAction @UseMap => m_Wrapper.m_GameplayControls_UseMap;
        public InputAction @PressInteractive => m_Wrapper.m_GameplayControls_PressInteractive;
        public InputAction @PauseAction => m_Wrapper.m_GameplayControls_PauseAction;
        public InputAction @Reload => m_Wrapper.m_GameplayControls_Reload;
        public InputAction @Inspect => m_Wrapper.m_GameplayControls_Inspect;
        public InputActionMap Get() { return m_Wrapper.m_GameplayControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayControlsActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayControlsActions instance)
        {
            if (m_Wrapper.m_GameplayControlsActionsCallbackInterface != null)
            {
                @HorizontalMovement.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnHorizontalMovement;
                @Jump.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnJump;
                @MouseX.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMouseX;
                @MouseX.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMouseX;
                @MouseX.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMouseX;
                @MouseY.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMouseY;
                @MouseY.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMouseY;
                @MouseY.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnMouseY;
                @Crouch.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCrouch;
                @Weapon1.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon1;
                @Weapon1.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon1;
                @Weapon1.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon1;
                @Weapon2.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon2;
                @Weapon2.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon2;
                @Weapon2.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon2;
                @Weapon3.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon3;
                @Weapon3.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon3;
                @Weapon3.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon3;
                @Weapon4.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon4;
                @Weapon4.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon4;
                @Weapon4.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon4;
                @Weapon5.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon5;
                @Weapon5.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon5;
                @Weapon5.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnWeapon5;
                @ToggleWeaponSkin.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnToggleWeaponSkin;
                @ToggleWeaponSkin.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnToggleWeaponSkin;
                @ToggleWeaponSkin.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnToggleWeaponSkin;
                @Camera.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnCamera;
                @Flashlight.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnFlashlight;
                @Flashlight.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnFlashlight;
                @Flashlight.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnFlashlight;
                @SwitchEquipment.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnSwitchEquipment;
                @SwitchEquipment.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnSwitchEquipment;
                @SwitchEquipment.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnSwitchEquipment;
                @UseMap.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnUseMap;
                @UseMap.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnUseMap;
                @UseMap.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnUseMap;
                @PressInteractive.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnPressInteractive;
                @PressInteractive.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnPressInteractive;
                @PressInteractive.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnPressInteractive;
                @PauseAction.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnPauseAction;
                @PauseAction.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnPauseAction;
                @PauseAction.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnPauseAction;
                @Reload.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnReload;
                @Inspect.started -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnInspect;
                @Inspect.performed -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnInspect;
                @Inspect.canceled -= m_Wrapper.m_GameplayControlsActionsCallbackInterface.OnInspect;
            }
            m_Wrapper.m_GameplayControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMovement.started += instance.OnHorizontalMovement;
                @HorizontalMovement.performed += instance.OnHorizontalMovement;
                @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MouseX.started += instance.OnMouseX;
                @MouseX.performed += instance.OnMouseX;
                @MouseX.canceled += instance.OnMouseX;
                @MouseY.started += instance.OnMouseY;
                @MouseY.performed += instance.OnMouseY;
                @MouseY.canceled += instance.OnMouseY;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Weapon1.started += instance.OnWeapon1;
                @Weapon1.performed += instance.OnWeapon1;
                @Weapon1.canceled += instance.OnWeapon1;
                @Weapon2.started += instance.OnWeapon2;
                @Weapon2.performed += instance.OnWeapon2;
                @Weapon2.canceled += instance.OnWeapon2;
                @Weapon3.started += instance.OnWeapon3;
                @Weapon3.performed += instance.OnWeapon3;
                @Weapon3.canceled += instance.OnWeapon3;
                @Weapon4.started += instance.OnWeapon4;
                @Weapon4.performed += instance.OnWeapon4;
                @Weapon4.canceled += instance.OnWeapon4;
                @Weapon5.started += instance.OnWeapon5;
                @Weapon5.performed += instance.OnWeapon5;
                @Weapon5.canceled += instance.OnWeapon5;
                @ToggleWeaponSkin.started += instance.OnToggleWeaponSkin;
                @ToggleWeaponSkin.performed += instance.OnToggleWeaponSkin;
                @ToggleWeaponSkin.canceled += instance.OnToggleWeaponSkin;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Flashlight.started += instance.OnFlashlight;
                @Flashlight.performed += instance.OnFlashlight;
                @Flashlight.canceled += instance.OnFlashlight;
                @SwitchEquipment.started += instance.OnSwitchEquipment;
                @SwitchEquipment.performed += instance.OnSwitchEquipment;
                @SwitchEquipment.canceled += instance.OnSwitchEquipment;
                @UseMap.started += instance.OnUseMap;
                @UseMap.performed += instance.OnUseMap;
                @UseMap.canceled += instance.OnUseMap;
                @PressInteractive.started += instance.OnPressInteractive;
                @PressInteractive.performed += instance.OnPressInteractive;
                @PressInteractive.canceled += instance.OnPressInteractive;
                @PauseAction.started += instance.OnPauseAction;
                @PauseAction.performed += instance.OnPauseAction;
                @PauseAction.canceled += instance.OnPauseAction;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Inspect.started += instance.OnInspect;
                @Inspect.performed += instance.OnInspect;
                @Inspect.canceled += instance.OnInspect;
            }
        }
    }
    public GameplayControlsActions @GameplayControls => new GameplayControlsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IGameplayControlsActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnWeapon1(InputAction.CallbackContext context);
        void OnWeapon2(InputAction.CallbackContext context);
        void OnWeapon3(InputAction.CallbackContext context);
        void OnWeapon4(InputAction.CallbackContext context);
        void OnWeapon5(InputAction.CallbackContext context);
        void OnToggleWeaponSkin(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnFlashlight(InputAction.CallbackContext context);
        void OnSwitchEquipment(InputAction.CallbackContext context);
        void OnUseMap(InputAction.CallbackContext context);
        void OnPressInteractive(InputAction.CallbackContext context);
        void OnPauseAction(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnInspect(InputAction.CallbackContext context);
    }
}
