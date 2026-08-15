using UnityEngine;
using TMPro;

public class InteractTextTrigger : MonoBehaviour
{
    [Header("表示するUI")]
    public TextMeshProUGUI interactText;

    [Header("懐中電灯本体スクリプト")]
    public FlashlightPickup flashlightPickup;

    private void Start()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !flashlightPickup.pickedUp)
        {
            interactText.text = "Eキーで懐中電灯を拾う";
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !flashlightPickup.pickedUp)
        {
            if (!interactText.gameObject.activeSelf)
            {
                interactText.text = "Eキーで懐中電灯を拾う";
                interactText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 拾った瞬間に消す
        if (flashlightPickup != null &&
            flashlightPickup.pickedUp &&
            interactText != null &&
            interactText.gameObject.activeSelf)
        {
            interactText.gameObject.SetActive(false);
        }
    }
}