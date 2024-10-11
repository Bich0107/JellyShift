using UnityEngine;

public class PagePoint : MonoBehaviour
{
    [SerializeField] GameObject activeImage;
    [SerializeField] GameObject deactiveImage;

    public void SetActive(bool _value)
    {
        activeImage.SetActive(_value);
        deactiveImage.SetActive(!_value);
    }
}
