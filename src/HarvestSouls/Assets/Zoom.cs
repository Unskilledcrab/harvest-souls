using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class Zoom : MonoBehaviour
{
    [Range(0, 1)]
    public float ZoomIncrement = .5f;
    public float MinZoom = 3f;
    public float MaxZoom = 20f;
    public float DefaultZoom = 13f;

    private CinemachineVirtualCamera camera;
    private InputMaster input;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        input = new InputMaster();
        input.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseZoom();
    }

    private void ResetZoom()
    {
        camera.m_Lens.OrthographicSize = DefaultZoom;
    }

    private void HandleMouseZoom()
    {
        var zoom = input.Player.Zoom.ReadValue<float>();
        var zoomState = ZoomState.None;
        if (zoom < 0)
            zoomState = ZoomState.ZoomOut;
        else if (zoom > 0)
            zoomState = ZoomState.ZoomIn;

        TryZoom(zoomState);
    }

    private void TryZoom(ZoomState zoomState)
    {
        if (zoomState == ZoomState.ZoomIn && camera.m_Lens.OrthographicSize > MinZoom)
        {
            camera.m_Lens.OrthographicSize -= ZoomIncrement;

        }
        else if (zoomState == ZoomState.ZoomOut && camera.m_Lens.OrthographicSize < MaxZoom)
        {
            camera.m_Lens.OrthographicSize += ZoomIncrement;
        }
    }

    enum ZoomState
    {
        None,
        ZoomIn,
        ZoomOut
    }
}
