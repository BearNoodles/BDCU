using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float speed;
    Vector3 velocity;
    Vector3 startPos;

    float damage;

    float range = 10.0f;

	// Use this for initialization
	void Start ()
    {
        damage = 100.0f;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity = new Vector3(speed, 0, 0);
        transform.position += velocity * Time.deltaTime;

        if((Mathf.Abs(transform.position.x - startPos.x)) > range)
        {
            Debug.Log("destroy");
            Destroy(gameObject);
        }
	}

    public void SetSpeed(float sp)
    {
        speed = sp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<boatman>().health -= damage;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "EnemyD")
        {
            collision.gameObject.GetComponent<diverScript>().health -= damage;
            Destroy(gameObject);
        }
    }
}
