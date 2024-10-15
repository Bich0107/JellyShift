using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : MonoSingleton<Bank>
{
    [SerializeField] int crystalAmount = 0;
    [SerializeField] TextMeshProUGUI amountText;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddCrystal(int _amount = 1)
    {
        crystalAmount += _amount;
        amountText.text = crystalAmount.ToString();
    }

    public bool TakeCrystal(int _amount)
    {
        if (crystalAmount < _amount) return false;

        crystalAmount -= _amount;
        amountText.text = crystalAmount.ToString();
        return true;
    }
}
