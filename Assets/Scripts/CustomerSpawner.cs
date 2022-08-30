using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject customer;
    [SerializeField]
    private int custNum;
    [SerializeField]
    private float spawnerInterval = 1f;

    private GameObject newCustomer;
    private List<GameObject> customerList;
    private GameObject dropItem;
    private Vector2 orderPos,nextPos;
    private List<Vector2> waitingQueuePosList;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        customerList = new List<GameObject>();

        waitingQueuePosList = new List<Vector2>();
        orderPos = new Vector2(-3.5f, -0.2f);
        nextPos = orderPos + new Vector2(2f, 0);

        waitingQueuePosList.Add(orderPos);

        for (int i = 0; i <= custNum; i++)
        {
            waitingQueuePosList.Add(nextPos + new Vector2(1f, 0) * i);
        }

        StartCoroutine(SpawnCustomer(spawnerInterval));
    }


    private IEnumerator SpawnCustomer(float spawnerInterval)
    {
        for(int i = 0; i <= custNum; i++)
        {
            newCustomer = Instantiate(customer, transform.position, Quaternion.identity);
            Customer customerScript = newCustomer.GetComponent<Customer>();

            customerScript.SetMovingPosition(waitingQueuePosList[i]);
            customerScript.reqItemID = Random.Range(1, 9);

            customerScript.spriteID = i;

            customerList.Add(newCustomer);
            yield return new WaitForSeconds(spawnerInterval);

        }

    }

    public Sprite returnCustomerSprite()
    {
        return customerList[0].GetComponent<SpriteRenderer>().sprite;
    }

    private void Update() {
 
        if(customerList.Count == 0)
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadGoingHomeScene();
            Destroy(gameObject);
            Destroy(GameObject.Find("Bus"));
        }          
    }


    public void UpdateQueue()
    {
        customerList[0].GetComponent<Customer>().SetMovingPosition(new Vector2(-16,customerList[0].transform.position.y));
        customerList[0].GetComponent<Customer>().showReqItem = false;
        RelocateQueue();

        Invoke("RemoveCustomer",1f);
    }

    private void RemoveCustomer()
    {
        Destroy(customerList[0]);
        customerList.Remove(customerList[0]);
    }

    private void RelocateQueue()
    {
        for(int i = 1; i < customerList.Count; i++)
        {
            customerList[i].GetComponent<Customer>().SetMovingPosition(waitingQueuePosList[i-1]);
        }
    }


}
