using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    Vector3 baseScale;
    [SerializeField] Transform targetTrans;
    [Header("Max and min scale values")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] float baseXTValue;
    [SerializeField] float baseYTValue;
    [SerializeField] float scaleSpeed;
    float xTValue;
    float yTValue;
    Vector3 scale = new Vector3();

    void Awake()
    {
        baseScale = targetTrans.localScale;
        scale = baseScale;
        xTValue = baseXTValue;
        yTValue = baseYTValue;
    }

    public void Scale(float _distance)
    {
        if (_distance > 0)
        {
            xTValue = Mathf.Clamp(xTValue - _distance * scaleSpeed, 0f, 1f);
            yTValue = Mathf.Clamp(yTValue + _distance * scaleSpeed, 0f, 1f);

            scale.x = Mathf.Lerp(minX, maxX, xTValue);
            scale.y = Mathf.Lerp(minY, maxY, yTValue);

            targetTrans.localScale = scale;

        }
        else if (_distance < 0)
        {
            _distance = Mathf.Abs(_distance);

            xTValue = Mathf.Clamp(xTValue + _distance * scaleSpeed, 0f, 1f);
            yTValue = Mathf.Clamp(yTValue - _distance * scaleSpeed, 0f, 1f);

            scale.x = Mathf.Lerp(minX, maxX, xTValue);
            scale.y = Mathf.Lerp(minY, maxY, yTValue);

            targetTrans.localScale = scale;
        }
    }

    public void Reset()
    {
        targetTrans.localScale = baseScale;
        xTValue = baseXTValue;
        yTValue = baseYTValue;
    }
}
