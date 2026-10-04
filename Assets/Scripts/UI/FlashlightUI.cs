using UnityEngine;
using UnityEngine.UI;

public class FlashlightUI : MonoBehaviour
{
    // シングルトン（どこからでもこのUIを呼び出せるようにする設定）
    public static FlashlightUI Instance { get; private set; }

    private Image myImage;

    private void Awake()
    {
        // インスタンスの登録
        if (Instance == null) { Instance = this; }
        
        myImage = GetComponent<Image>();
        
        // 最初はUI画像を非表示にしておく
        if (myImage != null) { myImage.enabled = false; }
    }

    // 懐中電灯が拾われた時に、3Dオブジェクト側からこれを呼び出す
    public void ShowIcon()
    {
        if (myImage != null)
        {
            myImage.enabled = true; // 画像を表示する
        }
    }
}
