using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    static Vector3 spawnPosition;

   public static void SpawnWithRaycast(GameObject prefab)
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            spawnPosition = hit.point;
        }

        if(hit.collider != null)
            Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
