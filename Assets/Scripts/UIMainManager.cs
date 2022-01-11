using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainManager : MonoBehaviour
{
    static Vector3 worldPosition;

    // Set a gameObject's position in the world game depending on the mouse position.
    public static void SetPositionInWorld(GameObject gameObject)
    {

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            worldPosition = hit.point;
        }

        if(hit.collider != null)
        {
            gameObject.transform.position = worldPosition;
        }
    }
    
}
