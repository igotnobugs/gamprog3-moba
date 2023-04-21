using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    protected LayerMask groundLayer = 1 << 10;

    [SerializeField]
    protected LayerMask unitLayer = 1 << 7;

    [SerializeField]
    private SkillPanelUI skillPanelUI;

    [SerializeField]
    private GameObject scoreboardPanel;


    private Unit mainHeroUnit;
    private HeroController mainHeroController;
    private GameManager gameManager;
    private InputActions heroAction;  
    private bool isSelectedMainHero = true;
    

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        heroAction = new InputActions();
        GameManager.gameEvents.OnSelectedUnit.AddListener(CheckSelectedUnit);
    }

    private void CheckSelectedUnit(Unit newSelectedUnit)
    {
        isSelectedMainHero = gameManager.mainHero == newSelectedUnit;
    }

    private void OnEnable()
    {
        heroAction.Player.LeftMouseClick.performed += _ => LeftMouseClick_performed();
        heroAction.Player.RightMouseClick.performed += _ => RightMouseClick_performed();
        heroAction.Player.StopMoving.performed += _ => StopMoving_performed();

        heroAction.Player.QSkill.performed += _ => QKey_Performed();
        heroAction.Player.WSkill.performed += _ => WKey_Performed();
        heroAction.Player.ESkill.performed += _ => EKey_Performed();
        heroAction.Player.RSkill.performed += _ => RKey_Performed();

        heroAction.Player.ViewScoreboard.performed += ctx => OpenScoreboard(ctx);
        heroAction.Player.ViewScoreboard.canceled += ctx => OpenScoreboard(ctx);

        heroAction.Enable();
        Minimap.OnMinimapRightClick.AddListener(IssueMoveCommand);
    }

    private void OpenScoreboard(InputAction.CallbackContext ctx)
    {

        if (!scoreboardPanel) return;

        if (ctx.phase == InputActionPhase.Performed)
        {
            scoreboardPanel.SetActive(true);
        } else if (ctx.phase == InputActionPhase.Canceled)
        {
            scoreboardPanel.SetActive(false);
        }
    }

    private void OnDisable()
    {
        heroAction.Player.LeftMouseClick.performed -= _ => LeftMouseClick_performed();
        heroAction.Player.RightMouseClick.performed -= _ => RightMouseClick_performed();
        heroAction.Player.StopMoving.performed -= _ => StopMoving_performed();
        heroAction.Player.QSkill.performed -= _ => QKey_Performed();
        heroAction.Player.WSkill.performed -= _ => WKey_Performed();
        heroAction.Player.ESkill.performed -= _ => EKey_Performed();
        heroAction.Player.RSkill.performed -= _ => RKey_Performed();
        heroAction.Disable();

        Minimap.OnMinimapRightClick.RemoveListener(IssueMoveCommand);
    }

    private void Start()
    {
        heroAction.Enable();
        mainHeroUnit = gameManager.mainHero;
        mainHeroController = mainHeroUnit.controller as HeroController;
        
    }

    private void LeftMouseClick_performed()
    {
        Vector2 mpos = heroAction.Player.MousePosition.ReadValue<Vector2>();
        if (IsPointerOverUI(mpos)) return;

        Ray ray = Camera.main.ScreenPointToRay(mpos);

        if (Physics.Raycast(ray, out RaycastHit hit, 90000.0f, groundLayer))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                mainHeroController.SpawnMoveIndicator(hit.point);
                mainHeroController.AttemptCommand(() =>
                {
                    mainHeroController.StopAttacking();
                    mainHeroController.MoveToPosition(hit.point);
                });
            }
        }
    }

    private void RightMouseClick_performed()
    {
        Vector2 mpos = heroAction.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mpos);

        if (mainHeroUnit.selectedSkill && (mainHeroUnit.selectedSkill.targetingType == TargetingType.Ground 
            || mainHeroUnit.selectedSkill.targetingType == TargetingType.GroundSphere))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 90000.0f, groundLayer))
            {
                mainHeroController.RemoveMoveIndicator();

                mainHeroController.AttemptCommand(() => {
                    mainHeroController.AttackTarget(hit.point, mainHeroUnit.selectedSkill);
                });
            }
        } else
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 90000.0f, unitLayer))
            {
                if (hit.collider.TryGetComponent(out Unit targetUnit))
                {
                    // Cannot target allies - need to do targeting check here?
                    //if (targetUnit.faction == unit.faction) return;
                    if (targetUnit.isInvulnerable) return;
                    if (mainHeroUnit.selectedSkill)
                    {
                        if (!mainHeroUnit.selectedSkill.CheckSkillTargetIsValid(targetUnit)) return;
                    }
                    mainHeroController.RemoveMoveIndicator();

                    mainHeroController.AttemptCommand(() => {
                        mainHeroController.AttackTarget(targetUnit, mainHeroUnit.selectedSkill);
                    });
                }
            }
        }
    }

    private void StopMoving_performed()
    {
        mainHeroController.RemoveMoveIndicator();
        mainHeroController.CancelAction();
        mainHeroUnit.selectedSkill = null;
    }

    private void IssueMoveCommand(Vector3 position, bool checkRaycast = false)
    {
        if (checkRaycast)
        {
            Vector3 skyAbove = position + (Vector3.up * 2000);
            if (Physics.Raycast(skyAbove, Vector3.down, out RaycastHit hit, 90000.0f, groundLayer))
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    position = hit.point;
                }
            }
        }

        mainHeroController.SpawnMoveIndicator(position);
        mainHeroController.AttemptCommand(() =>
        {
            mainHeroController.StopAttacking();
            mainHeroController.MoveToPosition(position);
        });
    }

    #region Skilling
    // Make it as if its pressing the button in the UI
    private void QKey_Performed()
    {
        skillPanelUI.PressQSkillButton(() =>
        {
           // skillReady = gameManager.mainHero.stats.qSkill;
        });

    }

    private void WKey_Performed()
    {
        skillPanelUI.PressWSkillButton(() =>
        {
            //skillReady = gameManager.mainHero.stats.wSkill;
        });

    }

    private void EKey_Performed()
    {
        skillPanelUI.PressESkillButton(() =>
        {
           // skillReady = gameManager.mainHero.stats.eSkill;
        });

    }

    private void RKey_Performed()
    {
        skillPanelUI.PressRSkillButton(() =>
        {
            //skillReady = gameManager.mainHero.stats.rSkill;
        });

    }
    #endregion

    private bool IsPointerOverUI(Vector2 mpos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mpos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
