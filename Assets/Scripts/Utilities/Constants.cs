using UnityEngine;

public class Constants
{
    #region Game Rules
    public static float DOTA_RATIO { private set; get; } = 0.025f;
    public static float goldBountyRange { private set; get; } = 1300f;
    public static float expBountyRange { private set; get; } = 1500f;
    #endregion

    #region Unit FSM parameters
    public static int STATE_ISMOVING { set; get; } = Animator.StringToHash("isMoving");
    public static int STATE_HASTARGET { set; get; } = Animator.StringToHash("hasTarget");
    public static int STATE_INRANGE { set; get; } = Animator.StringToHash("inRange");
    public static int STATE_STAGGER { set; get; } = Animator.StringToHash("stagger");
    public static int STATE_HASMANA { set; get; } = Animator.StringToHash("hasMana");
    public static int STATE_ATKSPDMULTIPLIER { set; get; } = Animator.StringToHash("atkSpdMultiplier");
    public static int STATE_ISSTUNNED { set; get; } = Animator.StringToHash("isStunned");
    #endregion
}