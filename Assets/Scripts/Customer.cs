using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    public int reqItemID;
    public bool showReqItem;
    public int spriteID;

    private Animator animator;

    [SerializeField]
    private float speed;
    [SerializeField]
    private string[] itemText;
    [SerializeField]
    private List<GameObject> customer_list = new List<GameObject>();

    private Vector2 movingPosition;
    private GameObject itemInfo, itemInfoText;
    private TextMeshProUGUI textMeshP;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        
        showReqItem = false;
        itemInfo = transform.GetChild(0).gameObject;
        itemInfoText = itemInfo.transform.GetChild(0).gameObject;

        itemInfo.GetComponent<TextMesh>().text = itemText[reqItemID-1];
        itemInfoText.GetComponent<TextMesh>().text = itemText[reqItemID-1];

        GetComponent<SpriteRenderer>().sprite = customer_list[spriteID].GetComponent<SpriteRenderer>().sprite;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = customer_list[spriteID].GetComponent<Animator>().runtimeAnimatorController;

    }

    public void SetMovingPosition(Vector2 position)
    {
        movingPosition = position;
    }

    Vector2 lastPos = new Vector2(0f, 0f);

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, movingPosition, speed * Time.deltaTime);

        Vector2 curPos = transform.position;


        if(curPos == lastPos) 
            animator.SetBool("customerWalking",false);
        else
            animator.SetBool("customerWalking",true);
        
        lastPos = curPos;

        if(!showReqItem)
            itemInfo.SetActive(false);
        else
            itemInfo.SetActive(true);

    }


}
