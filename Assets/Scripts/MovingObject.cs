using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float currentSpeed;
    [SerializeField] Vector3 direction;
    bool isMoving = false;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        if (isMoving) transform.position += speed * direction * Time.deltaTime;
    }

    public void Move() => isMoving = true;

    public void Stop() => isMoving = false;

    public void Reset()
    {
        currentSpeed = speed;
        Stop();
    }
}
