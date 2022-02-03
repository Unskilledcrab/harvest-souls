using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayedInventoryItem
{
    public ItemObject ItemObject;
    public GameObject GameObject;
}

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public RectTransform containerSpace;

    Dictionary<InventoryItem, GameObject> itemsDisplayed = new Dictionary<InventoryItem, GameObject>();

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
        obj.GetComponent<RectTransform>().localPosition = GetPosition();
        var textMesh = obj.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = dbItem.Stackable ? invItem.amount.ToString("n0") : string.Empty;
        itemsDisplayed.Add(invItem, obj);
    }

    private void UpdateDisplay()
    {
        for (int i = itemsDisplayed.Count - 1; i >= 0; i--)
        {
            var item = itemsDisplayed.ElementAt(i);
            if (!inventory.Container.Items.Contains(item.Key))
            {
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
