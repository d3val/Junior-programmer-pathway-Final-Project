using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public bool enableBuild = true;

    public Material enableMaterial;
    public Material disableMaterial;
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //meshRenderers = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            DisableBuild();
        }
    }

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
        if (other.CompareTag("Building"))
        {
            EnableBuild();
        }
    }

    public void EnableBuild()
    {
        enableBuild = true;
        foreach (MeshRenderer render in meshRenderers)
        {
            render.material = enableMaterial;

        }
    }
}
