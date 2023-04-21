using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Required, Tooltip("Client's Hero")]
    private Hero _mainHero;

    [SerializeField, ReadOnly]
    private Unit _selectedUnit;

    [SerializeField]
    private TextMeshProUGUI radiantKillsText;

    [SerializeField]
    private TextMeshProUGUI direKillsText;

    [SerializeField]
    public Waypoint radiantHeroSpawnpoint;

    [SerializeField]
    public Waypoint direHeroSpawnpoint;

    public static List<Hero> radiantHeroes { set; get; } = new List<Hero>();
    public static List<Hero> direHeroes { set; get; } = new List<Hero>();
    public static int radiantKills { private set; get; }
    public static int direKills { private set; get; }
    public Hero mainHero { get { return _mainHero; } }
    public Unit selectedUnit { 
        set {
            gameEvents.OnUnselectedUnit?.Invoke(_selectedUnit);
            _selectedUnit = value;
            gameEvents.OnSelectedUnit?.Invoke(value);
        } 
        get { return _selectedUnit; } 
    }
    private InputActions playerAction;
    public static GameEvents gameEvents { set; get; } = new GameEvents();

    private void Awake()
    {
        mainHero.isAI = false;
        playerAction = new InputActions();
    }

    private void OnEnable()
    {
        playerAction.Player.SelectHero.performed 
            += _ => gameEvents.OnSelectedUnit?.Invoke(mainHero);
    }

    private void OnDisable()
    {
        playerAction.Player.SelectHero.performed 
            -= _ => gameEvents.OnSelectedUnit?.Invoke(mainHero);
    }

    private void Start()
    {
        selectedUnit = mainHero;
        //gameEvents.OnSelectedUnit?.Invoke(mainHero);
        AddDebugCommands();
        AudioManager.Instance.PlayBGM("Birth of a Hero");
    }

    private void Update()
    {
        if (radiantHeroes.Count > 0)
        {
            radiantKills = 0;
            for (int i = 0; i < radiantHeroes.Count; i++)
            {
                radiantKills += radiantHeroes[i].data.kills;
            }
            radiantKillsText.text = radiantKills.ToString();
        }

        if (direHeroes.Count > 0)
        {
            direKills = 0;
            for (int i = 0; i < direHeroes.Count; i++)
            {
                direKills += direHeroes[i].data.kills;
            }
            direKillsText.text = direKills.ToString();
        }
    }

    public void SelectMainHero()
    {
        selectedUnit = mainHero;
    }

    public static void AddHero(Hero heroUnit)
    {
        if (heroUnit.faction == Faction.Radiant)
        {
            radiantHeroes.Add(heroUnit);
            gameEvents.OnRadiantJoined?.Invoke(heroUnit,radiantHeroes.Count);
        } else
        {
            direHeroes.Add(heroUnit);
            gameEvents.OnDireJoined?.Invoke(heroUnit,direHeroes.Count);
        }

        gameEvents.OnHeroJoined?.Invoke(heroUnit);
    }

    private void AddDebugCommands()
    {
        #region Unit Manipulation
        DebugControl.AddCommand("kill", new DebugCommand
            ("Kills a selected unit by dealing 9999999 damage", "kill", () =>
            {
                if (selectedUnit)
                    selectedUnit.TakeDamage(9999999, DamageType.Physical, null);
                else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("mykill", new DebugCommand
            ("Kills a selected unit by dealing 9999999 damage and give it to the main", "mykill", () =>
            {
                if (selectedUnit)
                    selectedUnit.TakeDamage(9999999, DamageType.Physical, mainHero);
                else
                    Debug.Log("No unit selected");
             }));

        DebugControl.AddCommand("damage", new DebugCommand<float>
            ("Deals X amount of PHYSICAL damage to a selected unit", "damage <amount>", (x) =>
            {
                if (selectedUnit)
                    selectedUnit.TakeDamage(x, DamageType.Physical, null);
                else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("restore", new DebugCommand<float>
            ("Restores the X amount of health of the selected unit.", "restore <amount>", (x) =>
            {
                if (selectedUnit)
                    selectedUnit.Heal(x, null);
                else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("mana", new DebugCommand<float>
            ("Uses X amount of mana of selected unit", "mana <amount>", (x) =>
            {
                if (selectedUnit)
                    selectedUnit.ConsumeMana(x);
                else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("restoremana", new DebugCommand
            ("Restores mana of selected unit to full", "restoremana", () =>
            {
                if (selectedUnit)
                    selectedUnit.RestoreMana(9999);
                else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("cast", new DebugCommand<float>
            ("Deals X amount of MAGICAL damage to a selected unit", "cast <amount>", (x) =>
            {
                if (selectedUnit)
                    selectedUnit.TakeDamage(x, DamageType.Magical, null);
                else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("levelup", new DebugCommand
            ("Increase hero level", "levelup", () =>
            {
                if (selectedUnit)
                {
                    if(selectedUnit.data.unitType == UnitType.Hero)
                    {
                        selectedUnit.data.GainExp(999999999, true);
                    }
                    else
                        Debug.Log("Selected unit is not a hero");
                }
                else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("gainexp", new DebugCommand<float>
            ("Increase hero experience by X", "gainexp <amount>", (x) =>
            {
                if (selectedUnit)
                {
                    if (selectedUnit.data.unitType == UnitType.Hero)
                    {
                        selectedUnit.data.GainExp(x);
                    } else
                        Debug.Log("Selected unit is not a hero");
                } else
                    Debug.Log("No unit selected");
            }));

        DebugControl.AddCommand("refreshcooldown", new DebugCommand
            ("refreshAllCooldown", "refreshcooldown", () =>
            {
                if (selectedUnit)
                {
                    if (selectedUnit.data.qSkill)
                        selectedUnit.data.qSkill.RefreshCooldown();
                    if (selectedUnit.data.wSkill)
                        selectedUnit.data.wSkill.RefreshCooldown();
                    if (selectedUnit.data.eSkill)
                        selectedUnit.data.eSkill.RefreshCooldown();
                    if (selectedUnit.data.rSkill)
                        selectedUnit.data.rSkill.RefreshCooldown();

                } else
                Debug.Log("No unit selected");
            }));
        #endregion
    }

}

[System.Serializable]
public class GameEvents
{
    public UnityEvent<Unit> OnSelectedUnit= new UnityEvent<Unit>();
    public UnityEvent<Unit> OnUnselectedUnit = new UnityEvent<Unit>();

    public UnityEvent<Hero> OnHeroJoined = new UnityEvent<Hero>();

    public UnityEvent<Hero,int> OnRadiantJoined = new UnityEvent<Hero,int>();
    public UnityEvent<Hero,int> OnDireJoined = new UnityEvent<Hero,int>();
}