using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public bool buildEnable = true;

    public Material enableMaterial;
    public Material disableMaterial;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        buildEnable = false;
        meshRenderer.material = disableMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        buildEnable = true;
        meshRenderer.material = enableMaterial;
    }
}
