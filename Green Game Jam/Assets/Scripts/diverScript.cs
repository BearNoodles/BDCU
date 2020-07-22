using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diverScript : MonoBehaviour
{

    Vector3 startPos;
    Vector3 targetPos;
    Rigidbody2D rb;
    GameObject childSprite;
    public GameObject[] trashToFling;
    public float health = 50.0f;
    GameController gameController;
    public float movementRangeFromStart = 30.0f;
    public float moveSpeedMultiplier = 10.0f;

    //public AK.Wwise.

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        startPos = new Vector3(0.0f, gameController.diverAvgHeight + Random.Range(-gameController.diverHeightVariation, gameController.diverHeightVariation) , 0.0f);
        targetPos = startPos;
        rb = GetComponent<Rigidbody2D>();
        childSprite = transform.GetChild(0).gameObject;
        if (targetPos.x < transform.position.x)
        {
            Vector3 scale = childSprite.transform.localScale;
            scale.x = -1.0f;
            childSprite.transform.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0.0f)
        {
            gameController.pollutersSleepingWithTheFishes++;
            Destroy(gameObject);
        }

        if (transform.position.x - targetPos.x < 0.1f && transform.position.x - targetPos.x > -0.1f)
        {
            targetPos = new Vector3(startPos.x - Random.Range(-movementRangeFromStart, movementRangeFromStart), startPos.y, startPos.z);
            if (targetPos.x < transform.position.x)
            {
                Vector3 scale = childSprite.transform.localScale;
                scale.x = -1.0f;
                childSprite.transform.localScale = scale;
            }
            else
            {
                Vector3 scale = childSprite.transform.localScale;
                scale.x = 1.0f;
                childSprite.transform.localScale = scale;
            }
            Instantiate(trashToFling[Random.Range(0, trashToFling.Length)], transform.position, Quaternion.identity);
        }

        Vector3 direction = targetPos - transform.position;
        direction.Normalize();
        rb.velocity = direction * moveSpeedMultiplier;
    }
}
