using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayedInventoryItem
{
    public ItemObject ItemObject;
    public GameObject GameObject;
}

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public GameObject groundItemPrefab;
    public InventoryObject inventory;
    public GameObject player;
    public PlayerObject playerData;
    public RectTransform containerSpace;

    private MouseItem mouseItem = new MouseItem();

    Dictionary<InventoryItem, GameObject> itemsDisplayed = new Dictionary<InventoryItem, GameObject>();
    Dictionary<GameObject, InventoryItem> getInventoryItem = new Dictionary<GameObject, InventoryItem>();

    [SerializeField]
    float stepSize;

    float X_COORDINATE;
    float Y_COORDINATE;

    float x_start => containerSpace.localPosition.x + (stepSize - containerSpace.rect.width) / 2;
    float y_start => containerSpace.localPosition.y + (containerSpace.rect.height - stepSize) / 2;
    float x_max => containerSpace.localPosition.x + (containerSpace.rect.width - stepSize) / 2;
    float y_max => containerSpace.localPosition.y + (stepSize - containerSpace.rect.height) / 2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DisplayInventory.Start");
        X_COORDINATE = x_start;
        Y_COORDINATE = y_start;
        CreateDisplay();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            var invItem = inventory.Container.Items[i];
            CreateDisplayItem(invItem);
        }
    }

    void CreateDisplayItem(InventoryItem invItem)
    {
        var dbItem = inventory.database.GetItem[invItem.item.Id];
        var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<Image>().sprite = dbItem.Icon;
        obj.name = dbItem.name;

        if (invItem.position == null)
        {
            var position = GetPosition();
            invItem.position = (position.x, position.y, position.z);
        }

        AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
        AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
        AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

        obj.GetComponent<RectTransform>().localPosition = new Vector3(invItem.position.Value.Item1, invItem.position.Value.Item2, invItem.position.Value.Item3);
        var textMesh = obj.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = dbItem.Stackable ? invItem.amount.ToString("n0") : string.Empty;
        itemsDisplayed.Add(invItem, obj);
        getInventoryItem.Add(obj, invItem);
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        var trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    private void UpdateDisplay()
    {
        for (int i = itemsDisplayed.Count - 1; i >= 0; i--)
        {
            var item = itemsDisplayed.ElementAt(i);
            if (!inventory.Container.Items.Contains(item.Key))
            {
                getInventoryItem.Remove(item.Value);
                Destroy(item.Value);
                itemsDisplayed.Remove(item.Key);
            }
        }

        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            var invItem = inventory.Container.Items[i];
            if (itemsDisplayed.TryGetValue(invItem, out var obj))
            {
                var dbItem = inventory.database.GetItem[invItem.item.Id];
                var textMesh = obj.GetComponentInChildren<TextMeshProUGUI>();
                textMesh.text = dbItem.Stackable ? invItem.amount.ToString("n0") : string.Empty;
            }
            else
            {
                CreateDisplayItem(invItem);
            }
        }
    }

    void OnDrag(GameObject obj)
    {
        var position = Mouse.current.position.ReadValue();
        if (mouseItem.draggedImage != null)
            mouseItem.draggedImage.GetComponent<RectTransform>().position = position;
    }
    void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform);
        var img = mouseObject.AddComponent<Image>();
        img.sprite = obj.GetComponent<Image>().sprite;
        img.raycastTarget = false;

        mouseItem.draggedImage = mouseObject;
        mouseItem.item = obj;
    }
    void OnDragEnd(GameObject obj)
    {
        if (mouseItem.draggedImage != null)
        {
            var containerLocalRect = new Rect(x_start - 50, y_start - 30 - containerSpace.rect.height, containerSpace.rect.width, containerSpace.rect.height);
            var mousePosition = mouseItem.draggedImage.transform.position;

            var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            var distanceFromPlayer = Vector2.Distance(worldPosition, player.transform.position);
            if (!containerLocalRect.Contains(mousePosition) && distanceFromPlayer <= playerData.Reach)
            {
                var invItem = getInventoryItem[obj];
                var groundObj = Instantiate(groundItemPrefab, Vector3.zero, Quaternion.identity, transform);
                groundObj.transform.SetParent(player.transform.parent);
                groundObj.GetComponent<GroundItem>().item = invItem.item;
                groundObj.GetComponent<GroundItem>().Validate();
                groundObj.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);

                inventory.Container.Items.Remove(invItem);
            }
            else
            {
                var x = Math.Max(containerLocalRect.x, Math.Min(containerLocalRect.xMax, mousePosition.x));
                var y = Math.Max(containerLocalRect.y, Math.Min(containerLocalRect.yMax, mousePosition.y));
                mouseItem.item.transform.position = new Vector2(x, y);

            }
            Destroy(mouseItem.draggedImage);
        }
    }

    Vector3 GetPosition()
    {
        var position = new Vector3(X_COORDINATE, Y_COORDINATE);

        X_COORDINATE = X_COORDINATE + stepSize;
        if (X_COORDINATE > x_max)
        {
            X_COORDINATE = x_start;
            Y_COORDINATE = Y_COORDINATE - stepSize;
            if (Y_COORDINATE < y_max)
                Y_COORDINATE = y_start;
        }
        Debug.Log(position);
        return position;
    }
}

public class MouseItem
{
    public GameObject draggedImage;
    public GameObject item;
}
