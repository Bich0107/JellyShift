using UnityEngine;

[System.Serializable]
public class CustomAnimation : MonoBehaviour
{
    public float playDelay;
    public float rewindDelay;
    public WaitForSecondsRealtime playWait;
    public WaitForSecondsRealtime rewindWait;

    protected virtual void Start()
    {
        playWait = new WaitForSecondsRealtime(playDelay);
        rewindWait = new WaitForSecondsRealtime(rewindDelay);
    }

    public virtual void Play() { }
    public virtual void Rewind() { }
}