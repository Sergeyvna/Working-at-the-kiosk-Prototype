using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingQueue : MonoBehaviour
{
    
    private Vector2 firstPos,lastEnteringPos,orderPos;
    private List<Vector2> waitingQueuePosList;

    // Start is called before the first frame update
    void Start()
    {
        waitingQueuePosList = new List<Vector2>();
        orderPos = new Vector2(-1.25f, -1.7f);
        firstPos = new Vector2(3f, -1.7f);

        for (int i = 0; i < 5; i++)
        {
            waitingQueuePosList.Add(firstPos + new Vector2(1f, 0) * i);
        }

        lastEnteringPos = waitingQueuePosList[waitingQueuePosList.Count - 1];
        print(lastEnteringPos);
    }

    public List<Vector2> GetWaitingList()
    {
        return waitingQueuePosList;
    }
   
}
