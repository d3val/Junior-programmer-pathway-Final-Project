using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitButton : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] GameObject unitIndicator;
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
        UIMainManager.Instance.SetPositionInWorld(unitIndicator);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (indicatorData.enableBuild && UIMainManager.Instance.Money >= unitPrice)
        {
            Spawner.spawnManager.SpawnWithRaycast(unitPrefab);
            UIMainManager.Instance.UpdateMoney(-unitPrice);
        }
        unitIndicator.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (UIMainManager.Instance.Money >= unitPrice)
            unitIndicator.SetActive(true);
    }
}