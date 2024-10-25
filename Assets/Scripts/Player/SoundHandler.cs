using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip collideWithObstacleSFX;
    [SerializeField] AudioClip activeFeverSFX;
    [SerializeField] AudioClip reachGoalSFX;
    [SerializeField] AudioClip floatSFX;
    [SerializeField] AudioClip descendSFX;
    [SerializeField] AudioClip gameOverSFX;

    public void CollideWIthObstacle() => audioSource.PlayOneShot(collideWithObstacleSFX);
    public void ActiveFever() => audioSource.PlayOneShot(activeFeverSFX);
    public void ReachGoal() => audioSource.PlayOneShot(reachGoalSFX);
    public void Float() => audioSource.PlayOneShot(floatSFX);
    public void Descend() => audioSource.PlayOneShot(descendSFX);
    public void GameOver() => audioSource.PlayOneShot(gameOverSFX);

}
