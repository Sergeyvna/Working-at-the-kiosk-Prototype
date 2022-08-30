using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject levelLoader;
    [SerializeField]
    private GameObject inventoryDropSlot;

    private GameObject customerSpawner;
    private bool canSellItem;
    private float droppedItemID;
    private float customerItemID;

    [SerializeField]
    private float fightChance;
    
    private void Start() {
        canSellItem = false;
    }

    private void FixedUpdate() {
        SellItem();       
    }

    private void SellItem()
    {
        droppedItemID = GetItemID();
        if(canSellItem && droppedItemID!=0)
        {
            if(droppedItemID == customerItemID)
            {
                if(Random.Range(0f,1f) > fightChance)
                {
                    customerSpawner = GameObject.Find("Customer Spawner(Clone)");
                    customerSpawner.GetComponent<CustomerSpawner>().UpdateQueue();
                    canSellItem = false;
                }
                else
                    StartBattleScene();    
            }
            else
                StartBattleScene();
        }

    }

    private void StartBattleScene()
    {
        levelLoader.GetComponent<LevelLoader>().LoadFightScene();
    }

    private float GetItemID()
    {
        float id = inventoryDropSlot.GetComponent<DropItem>().GetItemID();
        inventoryDropSlot.GetComponent<DropItem>().SetItemID(0);
        return id;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Customer")
        {
            customerItemID = other.gameObject.GetComponent<Customer>().reqItemID;
            other.gameObject.GetComponent<Customer>().showReqItem = true;
            canSellItem = true;
        }
    }
}
