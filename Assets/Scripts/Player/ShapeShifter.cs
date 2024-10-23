using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    [SerializeField] Mesh[] meshes;
    [SerializeField] Vector3[] scales;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Transform targetTrans;
    [SerializeField] float scaleChangeDuration;

    public void ShapeShift(ShapeType _shapeType)
    {
        StartCoroutine(CR_ShapeShift(_shapeType));
    }

    IEnumerator CR_ShapeShift(ShapeType _shapeType)
    {
        Vector3 baseScale = targetTrans.localScale;
        float tick = 0f;

        while (tick < scaleChangeDuration)
        {
            tick += Time.deltaTime;
            targetTrans.localScale = Vector3.Lerp(baseScale, Vector3.zero, tick / scaleChangeDuration);
            yield return null;
        }

        meshFilter.mesh = meshes[(int)_shapeType];
        Vector3 endScale = scales[(int)_shapeType];

        tick = 0f;
        while (tick < scaleChangeDuration)
        {
            tick += Time.deltaTime;
            targetTrans.localScale = Vector3.Lerp(Vector3.zero, endScale, tick / scaleChangeDuration);
            yield return null;
        }
    }
}
