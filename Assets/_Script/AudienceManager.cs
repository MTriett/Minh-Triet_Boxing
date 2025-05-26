using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public AudiencePool pool;
    public Transform audienceRoot; 
    void Start()
    {
        SpawnAudience();
    }

    void SpawnAudience()
    {
        foreach (Transform spot in audienceRoot)
        {
            GameObject audience = pool.Get();
            if (audience != null)
            {
                audience.transform.position = spot.position;
                audience.transform.rotation = spot.rotation * Quaternion.Euler(0, 90, 0);
                audience.transform.SetParent(null); 
            }
        }
    }
}
