using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChecker : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField] Follower camFollower;
    [SerializeField] GravityEffector gravity;
    [SerializeField] LifeHandler lifeHandler;
    [SerializeField] float deathHeight = -0.03f;
    bool isActive = true;

    void Update()
    {
        HeightCheck();
    }

    public void SetActive(bool _status) => isActive = _status;

    void HeightCheck()
    {
        if (!isActive) return;

        if (targetTrans.position.y < deathHeight)
        {
            isActive = false;

            camFollower.Stop();
            gravity.SetActive(true);
            // kill player
            lifeHandler.DecreaseLife(999);
        }
    }

    public void Reset()
    {
        isActive = true;
    }
}
