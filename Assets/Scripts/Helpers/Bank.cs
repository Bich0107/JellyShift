using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : MonoSingleton<Bank>
{
    [SerializeField] int crystalAmount = 0;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI amountText_2;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddCrystal(int _amount = 1)
    {
        crystalAmount += _amount;
        amountText.text = crystalAmount.ToString();
        amountText_2.text = crystalAmount.ToString();
    }

    public void TakeCrystal(int _amount)
    {
        if (crystalAmount < _amount) return;

        crystalAmount -= _amount;
        amountText.text = crystalAmount.ToString();
        amountText_2.text = crystalAmount.ToString();
    }

    public bool CheckAmount(int _amount)
    {
        return crystalAmount >= _amount;
    }
}
