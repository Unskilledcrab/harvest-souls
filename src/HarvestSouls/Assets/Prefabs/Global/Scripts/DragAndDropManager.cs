using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDropManager : MonoBehaviour
{
    public PlayerObject PlayerData;

    [SerializeField]
    private InputAction mouseClick;

    [SerializeField]
    private float mouseDragPhysicsSpeed = 10;

    [SerializeField]
    private float mouseDragSpeed = .1f;

    Player player;
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        player = GetComponent<Player>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.GetRayIntersectionAll(ray)
            .FirstOrDefault(x => 
                x.collider.gameObject.TryGetComponent<IDraggable>(out var draggable) && 
                draggable.InDragAnchor(x.point - (Vector2)x.collider.transform.position));
        
        if (hit.collider?.gameObject.GetComponent<IDraggable>() != null)
        {
            var distanceFromPlayer = Vector2.Distance(hit.point, player.transform.position);

            if (distanceFromPlayer <= PlayerData.Reach)
                StartCoroutine(DragUpdate(hit.collider.gameObject));
        } 
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        var originalPosition = clickedObject.transform.position;
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        clickedObject.TryGetComponent<IDraggable>(out var iDraggable);
        iDraggable?.onStartDrag();
        Ray ray;

        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);

        while(mouseClick.ReadValue<float>() != 0)
        {
            ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if(rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * mouseDragPhysicsSpeed;
                transform.position = ray.GetPoint(initialDistance);
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(
                    clickedObject.transform.position, 
                    ray.GetPoint(initialDistance), 
                    ref velocity, 
                    mouseDragSpeed);
                yield return null;
            }
        }
        iDraggable?.onEndDrag();

        var distanceFromPlayer = Vector2.Distance(clickedObject.transform.position, player.transform.position);

        if (distanceFromPlayer > PlayerData.Reach)
        {
            clickedObject.transform.position = originalPosition;
        }
        else
        {
            ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaycastHit2D hitContainer = Physics2D.GetRayIntersectionAll(ray)
                .SingleOrDefault(x => x.collider?.GetComponent<Player>() != null);

            if(hitContainer.collider != null)
            {
                player.OnMouseDrop(clickedObject);
            }
        }
    }
}

