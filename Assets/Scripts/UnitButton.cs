using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject unitPrefab;
    public GameObject unitIndicator;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Le pique");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Toi arrastrando");
        UIMainManager.SetPositionInWorld(unitIndicator);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Deje de arratrar");
        Spawner.SpawnWithRaycast(unitPrefab);
        unitIndicator.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        unitIndicator.SetActive(true);
        Debug.Log("Empece a arrastrar");
    }
}