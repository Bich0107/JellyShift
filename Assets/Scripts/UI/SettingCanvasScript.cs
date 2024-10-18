using UnityEngine;

public class SettingCanvasScript : MonoBehaviour
{
    [SerializeField] GameObject canvasBody;

    public void Toggle()
    {
        canvasBody.SetActive(!canvasBody.activeSelf);
    }
}
