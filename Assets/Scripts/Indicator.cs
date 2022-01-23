using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public bool enableBuild = true;

    [SerializeField] Material enableMaterial;
    [SerializeField] Material disableMaterial;
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building") || other.CompareTag("Enemy"))
        {
            DisableBuild();
        }
    }

    // Change the current material and the state enableBuild to false
    public void DisableBuild()
    {
        enableBuild = false;
        foreach (MeshRenderer render in meshRenderers)
        {
            render.material = disableMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building") || other.CompareTag("Enemy"))
        {
            EnableBuild();
        }
    }

    // Change the current material and the state enableBuild to true
    public void EnableBuild()
    {
        if (!enableBuild)
        {
            foreach (MeshRenderer render in meshRenderers)
            {
                render.material = enableMaterial;
            }
        }
        enableBuild = true;
    }
}
