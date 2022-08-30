using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum FightState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class FightHandler : MonoBehaviour
{
    private LevelLoader levelLoader;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyPos;
    [SerializeField]
    private GameObject csv;
    [SerializeField]
    private GameObject inputField;
    [SerializeField]
    private GameObject damageTxt;

    public TextMeshProUGUI dialogText;
    public FightState state;

    private EnemyStats enemyStats;
    private CustomerSpawner customerSpawner;
    private string playerInput;
    private CSVReader csvReader;
    private Animator damageTxtAnimator;

    
    // Start is called before the first frame update
    void Start()
    {
        state = FightState.START;

        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        customerSpawner = GameObject.Find("Customer Spawner(Clone)").GetComponent<CustomerSpawner>();
        csvReader = csv.GetComponent<CSVReader>();

        damageTxtAnimator = damageTxt.GetComponent<Animator>();


        StartCoroutine(SetupFight());
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(DoDamage());
            state = FightState.ENEMYTURN;
            EnemyAttack();
        }      

        if(state != FightState.WON)
        {
            CheckEnemyHealth();  
        }
        
    }

    IEnumerator DoDamage()
    {
        playerInput = inputField.GetComponent<TMP_InputField>().text;
        float dmg = csvReader.GetDamageAmount(playerInput);

        damageTxtAnimator.SetBool("DamageTxt", true);
        damageTxt.GetComponent<TMP_Text>().text = $"- {dmg}";
        enemyStats.TakeDamage(dmg);

        inputField.GetComponent<TMP_InputField>().text = "";

        yield return new WaitForSeconds(0.5f);
        damageTxtAnimator.SetBool("DamageTxt", false);
    }

    IEnumerator SetupFight()
    {
        GameObject enemyGO = Instantiate(enemy, enemyPos.transform.position,Quaternion.identity);
        enemyGO.GetComponent<SpriteRenderer>().sprite = customerSpawner.returnCustomerSprite();

        enemyStats = enemyGO.GetComponent<EnemyStats>();

        dialogText.text = enemyStats.reasonForFight[Random.Range(0,enemyStats.reasonForFight.Length)];

        yield return new WaitForSeconds(2f);

        state = FightState.PLAYERTURN;
        PlayerTurn();
    }

 

    private void PlayerTurn()
    {
        if(state != FightState.PLAYERTURN)
        {
            return;
        }
        dialogText.text = "Enter your response";
        StartCoroutine(PlayerAttack());
            
    }

    IEnumerator PlayerAttack()
    {

        inputField.SetActive(true);        
        yield return new WaitForSeconds(10f);

        state = FightState.ENEMYTURN;
        StartCoroutine(EnemyAttack());
    }

    private void CheckEnemyHealth()
    {
        bool isBeaten = enemyStats.CheckHealth();

        if(isBeaten)
        {
            state = FightState.WON;
            dialogText.text = "You won";
            customerSpawner.UpdateQueue();
            levelLoader.LoadMainScene();
            isBeaten = false;

        }

    }


    IEnumerator EnemyAttack()
    {
        inputField.SetActive(false);
        dialogText.text = enemyStats.enemyResponse[Random.Range(0, enemyStats.enemyResponse.Length)];
        yield return new WaitForSeconds(3f);

        state = FightState.PLAYERTURN;
        PlayerTurn();
    }
}
