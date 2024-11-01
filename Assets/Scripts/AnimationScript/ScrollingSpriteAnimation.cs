using UnityEngine;

public class ScrollingSpriteAnimation : MonoBehaviour
{
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;
    [SerializeField] Material material;
    [SerializeField] bool playOnEnable;
    bool isPlaying = false;
    Vector2 newOffset = new Vector2();

    void OnEnable()
    {
        if (playOnEnable) isPlaying = true;
    }

    void Update()
    {
        if (isPlaying)
        {
            newOffset.x = Time.time * xSpeed;
            newOffset.y = Time.time * ySpeed;
            material.mainTextureOffset = newOffset;
        }
    }

    void OnDisable()
    {
        Reset();
    }

    public void Reset()
    {
        newOffset = Vector2.zero;
        material.mainTextureOffset = newOffset;
    }
}
