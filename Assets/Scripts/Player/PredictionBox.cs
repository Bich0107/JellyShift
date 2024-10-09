using UnityEngine;

public class PredictionBox : MonoBehaviour
{
    [Tooltip("Layer of the obstacle frame for the raycast to determine the distance from the base of the prediction box to the obstacle")]
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] Transform predictionBoxTrans;
    [SerializeField] Transform raycastPos;
    [SerializeField] float baseScale;
    [SerializeField] float minDistanceToFrame;
    [SerializeField] float maxRayDistance;
    RaycastHit hit;
    float distance;
    Vector3 scale;

    void Update()
    {
        FindObstacle();
    }

    void FindObstacle()
    {
        Physics.Raycast(raycastPos.position, predictionBoxTrans.forward, out hit, maxRayDistance, obstacleLayer, QueryTriggerInteraction.Collide);
        if (hit.collider == null)
        {
            SetActive(false);
        }
        else
        {
            SetActive(true);
            ScaleToObstacle(hit.point);
        }
    }

    void ScaleToObstacle(Vector3 _hitPos)
    {
        // set the position of the prediction box to the middle of the raycast start point and hit point
        distance = Vector3.Distance(_hitPos, raycastPos.position);
        Vector3 midPoint = (_hitPos + raycastPos.position) / 2f;
        midPoint.y = predictionBoxTrans.position.y;
        predictionBoxTrans.position = midPoint;

        // scale the prediction box to make it connect two points
        scale = predictionBoxTrans.localScale;
        scale.z = distance / baseScale;
        predictionBoxTrans.localScale = scale;
    }

    public void SetActive(bool _status)
    {
        predictionBoxTrans.gameObject.SetActive(_status);
    }

    public void Reset()
    {
        SetActive(false);
    }

    void OnDisable()
    {
        SetActive(false);
    }
}
