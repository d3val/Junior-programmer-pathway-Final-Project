using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject unitPrefab;
    public GameObject unitIndicator;
    Indicator indicatorData;

    [SerializeField] int unitPrice;

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
        UIMainManager.instance.SetPositionInWorld(unitIndicator);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (indicatorData.enableBuild && UIMainManager.instance.money >= unitPrice)
        {
            Spawner.spawnManager.SpawnWithRaycast(unitPrefab);
            UIMainManager.instance.UpdateMoney(-unitPrice);
        }
        unitIndicator.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (UIMainManager.instance.money >= unitPrice)
            unitIndicator.SetActive(true);
    }
}