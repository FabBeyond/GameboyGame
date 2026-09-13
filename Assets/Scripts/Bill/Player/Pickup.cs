using System;
using UnityEngine;
using static UnityEditor.Progress;

public class Pickup : MonoBehaviour
{
    [Header("Pickup")]
    public GameObject itemCheck;
    public bool canPickup = true;
    public LayerMask ItemLayer;

    [Header("Use")]
    private Action curAction = null;
    private string curItemName = "none";


    InputActions input;

    private void Awake()
    {
        input = new();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Checks();
        PickupItem();
        UseItem();
    }

    public void Checks()
    {
        if(curAction == null)
        {
            canPickup = true;
        }
        else
        {
            canPickup = false;
        }
    }

    public void PickupItem()
    {
        Collider2D item = Physics2D.OverlapBox(itemCheck.transform.position, itemCheck.transform.lossyScale * 2, 0, ItemLayer);

        if(item != null)
        {
            if (canPickup && input.Player.B.WasPerformedThisFrame())
            {
                curAction = item.GetComponent<Essential_Items>().itemFunction;
                Destroy(item.gameObject);
            }
        }    
    }

    public void UseItem()
    {
            if (!canPickup && input.Player.B.WasPerformedThisFrame())
            {
                curAction();
                curAction = null;
            }
    }
}
