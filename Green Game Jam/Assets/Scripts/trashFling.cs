using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashFling : MonoBehaviour {
    public float throwScale = 1.0f;
    public float waterSlowMultiplier = 0.5f;

    Rigidbody2D rb;
    bool hitWater = false;
    float startY;
    public AK.Wwise.Event splashSound = new AK.Wwise.Event();
    public AK.Wwise.Event throwSound = new AK.Wwise.Event();

    public GameObject splashObject;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(Random.Range(-3.0f, 3.0f) * throwScale, 3.0f * throwScale);
        //rb.AddTorque(Random.Range(-10.0f, 10.0f));
        rb.angularVelocity = Random.Range(-50.0f, 50.0f);
        startY = transform.position.y;
        rb.gravityScale = rb.gravityScale * throwScale;
        throwSound.Post(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < startY && !hitWater)
        {
            //rb.drag *= 10;
            rb.drag = (0.5f);// * waterSlowMultiplier);
            //rb.angularDrag *= 2;
            rb.angularDrag = 0.05f;

            splashSound.Post(gameObject);

            Instantiate(splashObject, gameObject.transform.position, Quaternion.identity);

            hitWater = true;
        }
        if (transform.position.y < startY)
        {
            //rb.gravityScale = 0.1f;
            rb.velocity *= (0.2f * waterSlowMultiplier);
        }
	}
}
