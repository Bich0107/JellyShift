using UnityEngine;

public class LifeHandler : MonoSingleton<LifeHandler>
{
    [SerializeField] AnimationSequence gameOverPanelAnimation;
    [SerializeField] SoundHandler soundHandler;
    [SerializeField] LifeDisplayer lifeDisplayer;
    int currentLife;

    void Start()
    {
        currentLife = SaveManager.Instance.currentSaveFile.Life;
    }

    public void IncreaseLife(int _value = 1)
    {
        currentLife += _value;
        if (currentLife > SaveFile.s_MaxLife) currentLife = SaveFile.s_MaxLife;

        lifeDisplayer.Display(currentLife);
        SaveManager.Instance.currentSaveFile.Life = currentLife;
    }

    public void DecreaseLife(int _value = 1)
    {
        currentLife -= _value;
        if (currentLife <= 0)
        {
            currentLife = 0;
            soundHandler.GameOver();
            PlayerScoreHandler.Instance.CheckHighScore();
            GameManager.Instance.GamePause();
            gameOverPanelAnimation.Play();
        }

        lifeDisplayer.Display(currentLife);
        SaveManager.Instance.currentSaveFile.Life = currentLife;
    }
}
