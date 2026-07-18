using UnityEngine;
using UnityEngine.InputSystem; // New Input Systemを使用

public class FPSCameraController : MonoBehaviour
{
    [Header("設定")]
    public float mouseSensitivity = 100f; // マウス感度
    public Transform playerBody;         // プレイヤー本体のTransform

    private float xRotation = 0f;

    void Start()
    {
        // プレイ中にマウスカーソルを画面中央にロックして非表示にする
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // New Input Systemからマウスの移動量を取得
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        // 上下の視点移動（カメラ自体をローカルX軸で回転）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 真上・真下を向いたときに反転しないよう制限

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 左右の視点移動（プレイヤーの体をY軸で回転）
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
