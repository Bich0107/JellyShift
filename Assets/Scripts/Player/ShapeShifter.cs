using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    [SerializeField] GameObject[] shapes;
    [SerializeField] Transform targetTrans;
    [SerializeField] float scaleChangeDuration;
    GameObject currentShape;
    ShapeType currentShapeType;
    bool isBusy;

    void Start()
    {
        currentShape = shapes[0];
        currentShapeType = ShapeType.Cube;
    }

    public void ShapeShift(ShapeType _shapeType)
    {
        if (isBusy || currentShapeType == _shapeType) return;

        StopAllCoroutines();
        StartCoroutine(CR_ShapeShift(_shapeType));
    }

    IEnumerator CR_ShapeShift(ShapeType _shapeType)
    {
        isBusy = true;
        Vector3 baseScale = targetTrans.localScale;
        float tick = 0f;

        while (tick < scaleChangeDuration)
        {
            tick += Time.deltaTime;
            targetTrans.localScale = Vector3.Lerp(baseScale, Vector3.zero, tick / scaleChangeDuration);
            yield return null;
        }

        currentShape.SetActive(false);
        currentShape = shapes[(int)_shapeType];
        currentShape.SetActive(true);
        currentShapeType = _shapeType;

        tick = 0f;
        while (tick < scaleChangeDuration)
        {
            tick += Time.deltaTime;
            targetTrans.localScale = Vector3.Lerp(Vector3.zero, baseScale, tick / scaleChangeDuration);
            yield return null;
        }

        isBusy = false;
    }
}
