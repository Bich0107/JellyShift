using UnityEngine;

public class InputHandler : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveManager.Instance.Reset();
        }
    }
#endif
}
