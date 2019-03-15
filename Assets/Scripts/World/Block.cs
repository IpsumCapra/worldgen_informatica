using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void Update()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (!IsVisible())
            renderer.enabled = false;
        else if (!renderer.enabled)
            renderer.enabled = true;
    }

    public bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds))
            return true;
        else
            return false;
    }

    //Niet nodig als er maar 1 laag wordt gerendered.
    /*public void ActivateBlocks()
    {
        RaycastHit hit;
        int cubeLayerIndex = LayerMask.NameToLayer("Blocks");
        int layerMask = (1 << cubeLayerIndex);
        float distance = .5f;
        for (int x = -1; x < 2; x += 2)
        {
            if (Physics.Raycast(transform.position, new Vector3(x, 0, 0), out hit, distance, layerMask) && hit.transform.tag != "Player")
                CheckBlock(hit.transform.GetComponent<Block>());
        }
        for (int x = -1; x < 2; x += 2)
        {
            if (Physics.Raycast(transform.position, new Vector3(0, x, 0), out hit, distance, layerMask) && hit.transform.tag != "Player")
                CheckBlock(hit.transform.GetComponent<Block>());
        }
        for (int x = -1; x < 2; x += 2)
        {
            if (Physics.Raycast(transform.position, new Vector3(0, 0, x), out hit, distance, layerMask) && hit.transform.tag != "Player")
                CheckBlock(hit.transform.GetComponent<Block>());
                
        }
    }
    public bool IsHidden()
    {
        RaycastHit hit;
        int cubeLayerIndex = LayerMask.NameToLayer("Blocks");
        int layerMask = (1 << cubeLayerIndex);
        float distance = .5f;
        for (int x = -1; x < 2; x += 2)
        {
            if (!Physics.Raycast(transform.position, new Vector3(x, 0, 0), out hit, distance, layerMask) && hit.transform.tag != "Player")
                return false;
        }
        for (int x = -1; x < 2; x += 2)
        {
            if (!Physics.Raycast(transform.position, new Vector3(0, x, 0), out hit, distance, layerMask) && hit.transform.tag != "Player")
                return false;
        }
        for (int x = -1; x < 2; x += 2)
        {
            if (!Physics.Raycast(transform.position, new Vector3(0, 0, x), out hit, distance, layerMask) && hit.transform.tag != "Player")
                return false;
        }
        return true;
    }

    public void SetEnabled()
    {
        if (IsHidden())
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Block>().enabled = false;
        }
        else
            GetComponent<Renderer>().enabled = true;
    }

    void CheckBlock(Block block)
    {
        if (block.enabled == false)
            block.enabled = true;
        block.SetEnabled();
    }*/
}
