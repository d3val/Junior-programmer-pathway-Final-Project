using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject unitPrefab;
    public GameObject unitIndicator;
    Indicator indicatorData;

    private void Awake()
    {
        indicatorData = unitIndicator.GetComponent<Indicator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        indicatorData.EnableBuild();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIMainManager.SetPositionInWorld(unitIndicator);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (indicatorData.enableBuild)
        {
            Spawner.spawnManager.SpawnWithRaycast(unitPrefab);
            unitIndicator.SetActive(false);
        }
        unitIndicator.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        unitIndicator.SetActive(true);
    }
}