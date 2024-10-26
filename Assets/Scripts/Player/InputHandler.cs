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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveManager.Instance.currentSaveFile.Life = 3;
        }
    }
#endif
}
