using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainManager : MonoBehaviour
{

    public static UIMainManager instance;

    private Vector3 worldPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Set a gameObject's position in the world game depending on the mouse position.
    public void SetPositionInWorld(GameObject gameObject)
    {

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            worldPosition = hit.point;
        }

        if (hit.collider != null)
        {
            gameObject.transform.position = worldPosition;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

}
