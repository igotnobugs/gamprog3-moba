// GENERATED AUTOMATICALLY FROM 'Assets/Controls/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5eefc7bb-64d8-44cc-9316-c4dc73b84c0c"",
            ""actions"": [
                {
                    ""name"": ""WASD"",
                    ""type"": ""PassThrough"",
                    ""id"": ""19c50b04-9acf-4519-8077-5bb849e5aa62"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c507c3c9-b93e-4a13-b573-3417253ddf14"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b1843627-e2ea-4825-8abf-b134fe4ca0a1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleDebug"",
                    ""type"": ""Button"",
                    ""id"": ""d4ce159f-6942-40eb-813d-630e27126a48"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftMouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""3656b799-1b8a-4346-85ce-cfee0b7b9f0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StopMoving"",
                    ""type"": ""Button"",
                    ""id"": ""b672c65a-d6de-45fa-9732-34be5c9d9070"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b955fbbc-301f-41b0-a775-32ea6669d468"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectHero"",
                    ""type"": ""Button"",
                    ""id"": ""52b350d9-73de-4ab5-a585-0568e567e7bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightMouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""f0f06d9d-d668-4b6a-9d61-63d4598855f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""66d7e9a7-e3c6-48f0-bfd8-64c8f3474379"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QSkill"",
                    ""type"": ""Button"",
                    ""id"": ""c727bd57-90f0-4efd-b0c8-730facbcf0b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WSkill"",
                    ""type"": ""Button"",
                    ""id"": ""53339150-cf84-4bce-af1e-b50fb117959b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ESkill"",
                    ""type"": ""Button"",
                    ""id"": ""6a64c433-1f16-454d-a7c4-74c594d4b285"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RSkill"",
                    ""type"": ""Button"",
                    ""id"": ""f8ac95eb-3485-457a-b296-99306a034d24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ViewScoreboard"",
                    ""type"": ""Button"",
                    ""id"": ""b9a12fce-a070-4c9c-815a-868bd773b983"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""55c5ab97-feba-4b5c-affc-87d53fd01c25"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""97b0e949-70e1-4512-9296-165d05d69f3f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6b110071-3686-4f3a-b931-812885b773f0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""68e8452b-88b6-4e72-892c-4202cbb495d0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""49b91626-c68a-4d80-8ab5-2f94d1372f8b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ea08f803-78e6-4589-a867-709928b8f5e9"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08778b78-503f-4661-a23e-932e3cf17299"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleDebug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c622e80-ea58-4695-91b8-b57706c886df"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ba1d657-a47e-4561-ad44-5413bb83f2de"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopMoving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5adedf28-2f84-4e5a-81d4-e017d107482f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowKeys"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""10ef607d-0d3c-4e06-b66c-bef9440194d5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c99bb20b-e550-4a94-b327-0bfc7295b885"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""88606dcb-cf21-456b-b9f8-0ab941846eb8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a6cb2126-d79d-4a20-8851-c631efdee626"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c81e73d1-7ea1-479d-adf6-edeeaf15e011"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abdcb94e-075f-4088-b862-5a23de3e6f23"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectHero"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b137ac6c-bfd5-4d70-9343-a85839d3f467"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""334b7ede-63a3-446c-b9d6-cfeb97b87875"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""659c0e7a-7d46-45f2-b3fe-2759b74393b3"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4676fe00-3d96-4ae2-b062-d3494d11c937"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc8f2d29-3ec6-40a1-ac24-0ad79e3a28d6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfe360c4-8ae7-495b-a19d-5d1c3835239b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fc52d52-835d-4265-ba3e-322f7892a532"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ViewScoreboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_WASD = m_Player.FindAction("WASD", throwIfNotFound: true);
        m_Player_ArrowKeys = m_Player.FindAction("ArrowKeys", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_ToggleDebug = m_Player.FindAction("ToggleDebug", throwIfNotFound: true);
        m_Player_LeftMouseClick = m_Player.FindAction("LeftMouseClick", throwIfNotFound: true);
        m_Player_StopMoving = m_Player.FindAction("StopMoving", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        m_Player_SelectHero = m_Player.FindAction("SelectHero", throwIfNotFound: true);
        m_Player_RightMouseClick = m_Player.FindAction("RightMouseClick", throwIfNotFound: true);
        m_Player_Submit = m_Player.FindAction("Submit", throwIfNotFound: true);
        m_Player_QSkill = m_Player.FindAction("QSkill", throwIfNotFound: true);
        m_Player_WSkill = m_Player.FindAction("WSkill", throwIfNotFound: true);
        m_Player_ESkill = m_Player.FindAction("ESkill", throwIfNotFound: true);
        m_Player_RSkill = m_Player.FindAction("RSkill", throwIfNotFound: true);
        m_Player_ViewScoreboard = m_Player.FindAction("ViewScoreboard", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_WASD;
    private readonly InputAction m_Player_ArrowKeys;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_ToggleDebug;
    private readonly InputAction m_Player_LeftMouseClick;
    private readonly InputAction m_Player_StopMoving;
    private readonly InputAction m_Player_Zoom;
    private readonly InputAction m_Player_SelectHero;
    private readonly InputAction m_Player_RightMouseClick;
    private readonly InputAction m_Player_Submit;
    private readonly InputAction m_Player_QSkill;
    private readonly InputAction m_Player_WSkill;
    private readonly InputAction m_Player_ESkill;
    private readonly InputAction m_Player_RSkill;
    private readonly InputAction m_Player_ViewScoreboard;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @WASD => m_Wrapper.m_Player_WASD;
        public InputAction @ArrowKeys => m_Wrapper.m_Player_ArrowKeys;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @ToggleDebug => m_Wrapper.m_Player_ToggleDebug;
        public InputAction @LeftMouseClick => m_Wrapper.m_Player_LeftMouseClick;
        public InputAction @StopMoving => m_Wrapper.m_Player_StopMoving;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputAction @SelectHero => m_Wrapper.m_Player_SelectHero;
        public InputAction @RightMouseClick => m_Wrapper.m_Player_RightMouseClick;
        public InputAction @Submit => m_Wrapper.m_Player_Submit;
        public InputAction @QSkill => m_Wrapper.m_Player_QSkill;
        public InputAction @WSkill => m_Wrapper.m_Player_WSkill;
        public InputAction @ESkill => m_Wrapper.m_Player_ESkill;
        public InputAction @RSkill => m_Wrapper.m_Player_RSkill;
        public InputAction @ViewScoreboard => m_Wrapper.m_Player_ViewScoreboard;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @WASD.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                @WASD.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                @WASD.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWASD;
                @ArrowKeys.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrowKeys;
                @ArrowKeys.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrowKeys;
                @ArrowKeys.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrowKeys;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @ToggleDebug.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @ToggleDebug.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @ToggleDebug.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleDebug;
                @LeftMouseClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMouseClick;
                @LeftMouseClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMouseClick;
                @LeftMouseClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMouseClick;
                @StopMoving.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopMoving;
                @StopMoving.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopMoving;
                @StopMoving.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopMoving;
                @Zoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @SelectHero.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectHero;
                @SelectHero.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectHero;
                @SelectHero.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectHero;
                @RightMouseClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMouseClick;
                @RightMouseClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMouseClick;
                @RightMouseClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMouseClick;
                @Submit.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubmit;
                @QSkill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQSkill;
                @QSkill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQSkill;
                @QSkill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQSkill;
                @WSkill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWSkill;
                @WSkill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWSkill;
                @WSkill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWSkill;
                @ESkill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESkill;
                @ESkill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESkill;
                @ESkill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESkill;
                @RSkill.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRSkill;
                @RSkill.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRSkill;
                @RSkill.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRSkill;
                @ViewScoreboard.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewScoreboard;
                @ViewScoreboard.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewScoreboard;
                @ViewScoreboard.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnViewScoreboard;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WASD.started += instance.OnWASD;
                @WASD.performed += instance.OnWASD;
                @WASD.canceled += instance.OnWASD;
                @ArrowKeys.started += instance.OnArrowKeys;
                @ArrowKeys.performed += instance.OnArrowKeys;
                @ArrowKeys.canceled += instance.OnArrowKeys;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @ToggleDebug.started += instance.OnToggleDebug;
                @ToggleDebug.performed += instance.OnToggleDebug;
                @ToggleDebug.canceled += instance.OnToggleDebug;
                @LeftMouseClick.started += instance.OnLeftMouseClick;
                @LeftMouseClick.performed += instance.OnLeftMouseClick;
                @LeftMouseClick.canceled += instance.OnLeftMouseClick;
                @StopMoving.started += instance.OnStopMoving;
                @StopMoving.performed += instance.OnStopMoving;
                @StopMoving.canceled += instance.OnStopMoving;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @SelectHero.started += instance.OnSelectHero;
                @SelectHero.performed += instance.OnSelectHero;
                @SelectHero.canceled += instance.OnSelectHero;
                @RightMouseClick.started += instance.OnRightMouseClick;
                @RightMouseClick.performed += instance.OnRightMouseClick;
                @RightMouseClick.canceled += instance.OnRightMouseClick;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @QSkill.started += instance.OnQSkill;
                @QSkill.performed += instance.OnQSkill;
                @QSkill.canceled += instance.OnQSkill;
                @WSkill.started += instance.OnWSkill;
                @WSkill.performed += instance.OnWSkill;
                @WSkill.canceled += instance.OnWSkill;
                @ESkill.started += instance.OnESkill;
                @ESkill.performed += instance.OnESkill;
                @ESkill.canceled += instance.OnESkill;
                @RSkill.started += instance.OnRSkill;
                @RSkill.performed += instance.OnRSkill;
                @RSkill.canceled += instance.OnRSkill;
                @ViewScoreboard.started += instance.OnViewScoreboard;
                @ViewScoreboard.performed += instance.OnViewScoreboard;
                @ViewScoreboard.canceled += instance.OnViewScoreboard;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnWASD(InputAction.CallbackContext context);
        void OnArrowKeys(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnToggleDebug(InputAction.CallbackContext context);
        void OnLeftMouseClick(InputAction.CallbackContext context);
        void OnStopMoving(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnSelectHero(InputAction.CallbackContext context);
        void OnRightMouseClick(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnQSkill(InputAction.CallbackContext context);
        void OnWSkill(InputAction.CallbackContext context);
        void OnESkill(InputAction.CallbackContext context);
        void OnRSkill(InputAction.CallbackContext context);
        void OnViewScoreboard(InputAction.CallbackContext context);
    }
}
