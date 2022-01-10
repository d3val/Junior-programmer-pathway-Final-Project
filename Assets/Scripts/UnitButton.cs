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
        Debug.Log("Le pique");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Toi arrastrando");
        UIMainManager.SetPositionInWorld(unitIndicator);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (indicatorData.enableBuild)
        {
            Debug.Log("Deje de arratrar");
            Spawner.SpawnWithRaycast(unitPrefab);
            unitIndicator.SetActive(false);
        }
        unitIndicator.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        unitIndicator.SetActive(true);
        Debug.Log("Empece a arrastrar");
    }
}