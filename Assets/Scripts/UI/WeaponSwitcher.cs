
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [Header("武器モデル")]
    public GameObject swordModel;
    public GameObject bowModel;

    [Header("武器スクリプト")]
    public SwordAttack swordAttack;
    public BowAttack bowAttack;

    [Header("カメラ")]
    public Camera playerCamera;

    [Header("キー設定")]
    public Key swordKey = Key.Digit1;
    public Key bowKey = Key.Digit3;

    [Header("初期武器")]
    public int startingWeapon = 1;

    private int currentWeapon = 1;

    void Start()
    {
        SwitchWeapon(startingWeapon);
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current[swordKey].wasPressedThisFrame)
        {
            SwitchWeapon(1);
        }

        if (Keyboard.current[bowKey].wasPressedThisFrame)
        {
            SwitchWeapon(2);
        }
    }

    public void SwitchWeapon(int weapon)
    {
        currentWeapon = weapon;

        bool useSword = weapon == 1;
        bool useBow = weapon == 2;

        if (!useSword && !useBow) return;

        if (swordModel != null)
            swordModel.SetActive(useSword);

        if (bowModel != null)
            bowModel.SetActive(useBow);

        if (swordAttack != null)
            swordAttack.enabled = useSword;

        if (bowAttack != null)
            bowAttack.enabled = useBow;

        if (playerCamera != null)
            playerCamera.fieldOfView = 60f;

        Debug.Log(
            useSword ? "剣に切り替えました！" :
            "弓に切り替えました！"
        );
    }
}
