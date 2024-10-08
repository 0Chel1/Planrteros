using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZoom : MonoBehaviour
{
    public CinemachineVirtualCamera Virtualcamera;
    public float zoomSens;
    public float minZoom;
    public float maxZoom;
    void Start()
    {
        Virtualcamera.m_Lens.OrthographicSize = 8f;
    }

    void Update()
    {
        zoomSens = Input.mouseScrollDelta.y;
        if (zoomSens < 0 && Virtualcamera.m_Lens.OrthographicSize <= maxZoom)
        {
            Virtualcamera.m_Lens.OrthographicSize++;
        }
        else if(zoomSens > 0 && Virtualcamera.m_Lens.OrthographicSize >= minZoom)
        {
            Virtualcamera.m_Lens.OrthographicSize--;
        }

        if(Virtualcamera.m_Lens.OrthographicSize > maxZoom)
        {
            Virtualcamera.m_Lens.OrthographicSize = maxZoom;
        }
        
        if(Virtualcamera.m_Lens.OrthographicSize < minZoom)
        {
            Virtualcamera.m_Lens.OrthographicSize = minZoom;
        }
    }
}
