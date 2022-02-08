using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    [SerializeField]
    private InputAction saveAction;

    [SerializeField]
    private InputAction loadAction;

    private void OnEnable()
    {
        saveAction.Enable();
        saveAction.performed += (s) => inventory.Save();
        loadAction.Enable();
        loadAction.performed += (s) => inventory.Load();
    }

    private void OnDisable()
    {
        saveAction.Disable();
        saveAction.performed -= (s) => inventory.Save();
        loadAction.Disable();
        loadAction.performed -= (s) => inventory.Load();
    }

    public void OnMouseDrop(GameObject other)
    {
        Debug.Log("Player.OnMouseDrop");
        if (other.gameObject.TryGetComponent<GroundItem>(out var item))
        {
            inventory.AddItem(item.item);
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("Unable to find ItemComponent");
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
}
