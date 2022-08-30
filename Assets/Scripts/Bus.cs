using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bus : MonoBehaviour
{
    [SerializeField]
    private float busSpawnTime;
    [SerializeField]
    private float busWaitTime;
    [SerializeField]
    private float busSpeed;
    [SerializeField]
    private GameObject customerSpawner;

    private float waitTime;
    private int xPos;
    private bool hasSpawned;

    private static Bus instance;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            if(instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }   
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }    
        }        
    }

    private void Start() {
        hasSpawned = false;
        waitTime = busWaitTime;
        xPos = 6;
    }


    private void Update() 
    {
        if(busSpawnTime <=0)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xPos, transform.position.y), busSpeed * Time.deltaTime);
        }
        else
            busSpawnTime -= Time.deltaTime;
        
        if(Vector2.Distance(transform.position, new Vector2(6f, transform.position.y)) < 0.9f)
        {
            if(waitTime <=0)
            {
                xPos = -16;
            }
                
            else
            {
                SpawnCustomers();
                waitTime -= Time.deltaTime;
            }
                
        }      
    }

    private void MoveBus()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(xPos, transform.position.y), busSpeed * Time.deltaTime);    
    }

    private void SpawnCustomers()
    {
        if(!hasSpawned)
        {
            Instantiate(customerSpawner,transform.position,Quaternion.identity);
            hasSpawned = true;
        }
        
    }
}
