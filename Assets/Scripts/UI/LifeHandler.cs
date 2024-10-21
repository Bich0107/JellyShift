using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoSingleton<LifeHandler>
{
    [SerializeField] GameObject[] lifes;
    int currentLife;

    void Start()
    {
        currentLife = SaveManager.Instance.currentSaveFile.Life;
        Display();
    }

    public void IncreaseLife(int _value = 1)
    {
        currentLife += _value;
        if (currentLife > SaveFile.s_MaxLife) currentLife = SaveFile.s_MaxLife;

        Display();
        SaveManager.Instance.currentSaveFile.Life = currentLife;
    }

    public void DecreaseLife(int _value = 1)
    {
        currentLife -= _value;
        if (currentLife <= 0)
        {
            currentLife = 0;
            GameManager.Instance.GameOver();
        }

        Display();
        SaveManager.Instance.currentSaveFile.Life = currentLife;
    }

    void Display()
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(i < currentLife);
        }
    }
}
