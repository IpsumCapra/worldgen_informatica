using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void BlockUpdate()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (!IsVisible())
            renderer.enabled = false;
        else
        {
            if (IsHidden())
                renderer.enabled = false;
            else if (!renderer.enabled)
                renderer.enabled = true;
        }
    }

    public bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds))
            return true;
        else
            return false;
    }

    public bool IsHidden()
    {
        RaycastHit hit;
        float distance = .5f;
        for (int x = -1; x < 2; x += 2)
        {
            if (!Physics.Raycast(transform.position, new Vector3(x, 0, 0), out hit, distance) || hit.transform.tag == "Player")
                return false;
        }
        for (int x = -1; x < 2; x += 2)
        {
            if (!Physics.Raycast(transform.position, new Vector3(0, x, 0), out hit, distance) || hit.transform.tag == "Player")
                return false;
        }
        for (int x = -1; x < 2; x += 2)
        {
            if (!Physics.Raycast(transform.position, new Vector3(0, 0, x), out hit, distance) || hit.transform.tag == "Player")
                return false;
        }
        return true;
    }
}
