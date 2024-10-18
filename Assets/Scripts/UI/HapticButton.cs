using UnityEngine;

public class HapticButton : MonoBehaviour
{
    bool isOn = true;

    public void OnClick()
    {
        if (isOn)
        {
            isOn = false;
            Debug.Log("turn off haptic");
        }
        else
        {
            isOn = true;
            Debug.Log("turn on haptic");
        }
    }
}
