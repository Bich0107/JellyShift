using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : MonoSingleton<Bank>
{
    // amount controlled by save file value
    int crystalAmount = 0;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI amountText_2;

    protected override void Awake()
    {
        base.Awake();
    }

    void UpdateUI()
    {
        amountText.text = crystalAmount.ToString();
        amountText_2.text = crystalAmount.ToString();
    }

    public void SetCrystalAmount(int _amount)
    {
        crystalAmount = _amount;
        UpdateUI();
    }

    public void AddCrystal(int _amount = 1)
    {
        crystalAmount += _amount;
        UpdateUI();

        SaveManager.Instance.currentSaveFile.Crystal = crystalAmount;
    }

    public void TakeCrystal(int _amount)
    {
        crystalAmount -= _amount;
        UpdateUI();

        SaveManager.Instance.currentSaveFile.Crystal = crystalAmount;
    }

    public bool CheckAmount(int _amount)
    {
        if (crystalAmount < _amount) return false;
        return true;
    }
}
