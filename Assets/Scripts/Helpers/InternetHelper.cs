using System.Collections;
using UnityEngine;

public class InternetHelper : MonoBehaviour
{
    [Tooltip("Repeat Checking internet with this interval")]
    [SerializeField] float internetCheckInterval = 5f;
    WaitForSeconds internetCheckWait;
    static bool s_internetAvailable = false;
    public static bool s_InternetAvailable => s_internetAvailable;

    void Start()
    {
        internetCheckWait = new WaitForSeconds(internetCheckInterval);

        StartCoroutine(CR_CheckInternet());
    }

    IEnumerator CR_CheckInternet()
    {
        do
        {
            if (IsConnectedToInternet() && !s_internetAvailable)
            {
                s_internetAvailable = true;
            }
            else if (!IsConnectedToInternet() && s_internetAvailable)
            {
                s_internetAvailable = false;
            }

            yield return internetCheckWait;
        } while (true);
    }

    bool IsConnectedToInternet()
    {
        // Check the internet reachability
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
