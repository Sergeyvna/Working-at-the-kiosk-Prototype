using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterShop : MonoBehaviour
{
    [SerializeField]
    private GameObject enterButtonText;
    [SerializeField]
    private GameObject levelLoader;

    private GameObject player;
    private Player playerScript;
    private LevelLoader levelLoaderScript;
    private bool canEnter;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        levelLoaderScript = levelLoader.GetComponent<LevelLoader>();
        canEnter = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enterButtonText.SetActive(true);
        canEnter = true;
           
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canEnter = false;
        enterButtonText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(canEnter && Input.GetKeyDown(KeyCode.E))
        {
            levelLoaderScript.LoadMainScene();
            Invoke("DestroyPlayer", 0.5f);  
        }
                
    }

    void DestroyPlayer()
    {
        Destroy(player);    
    }
}
