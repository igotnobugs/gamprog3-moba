using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool chooseHeroAtStart = true;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private UnitData vengefulSpiritData;

    [SerializeField]
    private UnitData phantomAssassinData;

    [SerializeField]
    private UnitData slardarData;

    [SerializeField]
    private UnitData necrophos;

    [SerializeField]
    private UnitData crystalMaiden;


    private void Start()
    {
        AddDebugCommands();
        playerManager.enabled = !chooseHeroAtStart;
    }

    public void SetVengefulData()
    {
        GameManager.Instance.mainHero.SetNewData(vengefulSpiritData);
        EnableHeroControl();
    }

    public void SetPhantomAssassinData()
    {
        GameManager.Instance.mainHero.SetNewData(phantomAssassinData);
        EnableHeroControl();
    }

    public void SetSlardarData()
    {
        GameManager.Instance.mainHero.SetNewData(slardarData);
        EnableHeroControl();
    }

    public void SetNecrophosData()
    {
        GameManager.Instance.mainHero.SetNewData(necrophos);
        EnableHeroControl();
    }

    public void SetCrystalMaiden()
    {
        GameManager.Instance.mainHero.SetNewData(crystalMaiden);
        EnableHeroControl();
    }

    private void EnableHeroControl()
    {
        GameManager.Instance.selectedUnit = GameManager.Instance.mainHero;
        playerManager.enabled = true;
        gameObject.SetActive(false);
    }


    private void AddDebugCommands()
    {
        #region Unit Manipulation
        DebugControl.AddCommand("switchvs", new DebugCommand
            ("Set Data to Vengeful", "switchvs", () =>
            {
                SetVengefulData();
            }));

        DebugControl.AddCommand("switchpa", new DebugCommand
            ("Set Data to Phantom Assassin", "switchpa", () =>
            {
                SetPhantomAssassinData();
            }));

        DebugControl.AddCommand("switchsl", new DebugCommand
            ("Set Data to Slardar", "switchsl", () =>
            {
                SetSlardarData();
            }));

        DebugControl.AddCommand("switchnc", new DebugCommand
            ("Set Data to Necrophos", "switchnc", () =>
            {
                SetNecrophosData();
            }));

        DebugControl.AddCommand("switchcm", new DebugCommand
            ("Set Data to Phantom Assassin", "switchcm", () =>
            {
                SetCrystalMaiden();
            }));

        #endregion
    }
}
