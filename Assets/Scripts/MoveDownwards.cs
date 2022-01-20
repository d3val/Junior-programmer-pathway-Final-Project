using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownwards : MonoBehaviour
{
    private float speed = 2.5f;
    private float yLimit = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > yLimit)
            MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
