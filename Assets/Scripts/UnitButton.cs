using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject unitPrefab;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Le pique");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Toi arrastrando");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Deje de arratrar");
        Spawner.SpawnWithRaycast(unitPrefab);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Empece a arrastrar");
    }
}