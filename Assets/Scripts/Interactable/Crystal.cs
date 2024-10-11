using UnityEngine;

public class Crystal : MonoBehaviour, ITriggerByPlayer
{


    public void TriggerByPlayer()
    {
        Bank.Instance.AddCrystal();
        gameObject.SetActive(false);
    }
}
