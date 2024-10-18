using UnityEngine;

public class SoundButton : MonoBehaviour
{
    bool isOn = true;

    public void OnClick()
    {
        if (isOn)
        {
            isOn = false;
            Debug.Log("turn off sound");
        }
        else
        {
            isOn = true;
            Debug.Log("turn on sound");
        }
    }
}
