using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour {

    public GameObject bullet;

    GameObject bulletObject;
    Vector3 bulletPosition;
    float bulletScale;

    float bulletSpeed;
    float currentBulletSpeed;
    Vector3 bulletDist;
    float bulletOffsetX;

    int maxAmmo;
    int currentAmmo;

    int maxPollution;
    int currentPollution;
    int pollutionToNextAmmo;
    int pollutionPerAmmo;

    float score;
    int stopped;


    Vector3 velocity;
    float speedIncrease;
    float scale;

    float maxSpeed;


    Camera cam;
    Vector3 camPosition;

    float camWidth;
    float camHeight;

    GameObject background;
    float boundsX;
    float boundsY;

    bool isJumping;

    Vector3 scaleVector;

    float playerHeight;

    public GameObject splashObject;

    GameController controller;

    public AK.Wwise.Event musicEvent = new AK.Wwise.Event();
    public AK.Wwise.Event atmosEvent = new AK.Wwise.Event();


    public AK.Wwise.Event gunSoundWater = new AK.Wwise.Event();
    public AK.Wwise.Event gunSoundAir = new AK.Wwise.Event();
    public AK.Wwise.Event pickupTrashSound = new AK.Wwise.Event();
    public AK.Wwise.Event depositTrashSound = new AK.Wwise.Event();
    public AK.Wwise.Event splashSound = new AK.Wwise.Event();
    public AK.Wwise.RTPC heightRTPC;

    // Use this for initialization
    void Start ()
    {
        cam = transform.parent.GetComponentInChildren<Camera>();
        background = GameObject.FindGameObjectWithTag("Background");
        boundsX = background.GetComponentInChildren<BoxCollider2D>().bounds.size.x;
        boundsY = background.GetComponentInChildren<BoxCollider2D>().bounds.size.y;
        camWidth = 18.0f;
        camHeight = 5.0f;
        camPosition = new Vector3(0.0f, 0.0f, -10.0f);

        bulletSpeed = 20.0f;
        currentBulletSpeed = bulletSpeed;
        bulletOffsetX = 0.2f;
        bulletDist = new Vector3(bulletOffsetX, 0, 0);

        maxAmmo = 8;
        currentAmmo = 0;


        maxPollution = 10;
        currentPollution = 0;

        pollutionPerAmmo = 1;
        pollutionToNextAmmo = pollutionPerAmmo;

        speedIncrease = 15.7f;
        scale = 0.4f;
        
        maxSpeed = 24.0f;

        velocity = new Vector3(0, 0, 0);

        isJumping = false;

        scaleVector = new Vector3(scale, scale, 1);

        playerHeight = transform.position.y + 40.0f;
        playerHeight = Mathf.Lerp(0.0f, 100.0f, playerHeight);
        heightRTPC.SetGlobalValue(playerHeight);

        score = 0;
        stopped = 0;

        controller = GameObject.FindObjectOfType<GameController>();
        //bulletPosition = new Vector3(-bulletDist, 0, 0) + transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerHeight = (transform.parent.position.y + 40) / 80.0f;
        playerHeight = Mathf.Lerp(0, 90.0f, playerHeight);

        heightRTPC.SetGlobalValue(playerHeight);


        //Vector3 newCamPosition = Vector3.zero;
        if (transform.parent.position.x < (-boundsX / 2.0f) + camWidth)
        {
            camPosition.x = camWidth + (-boundsX / 2.0f - transform.parent.position.x);
        }
        else if (transform.parent.position.x > (boundsX / 2.0f) - camWidth)
        {
            camPosition.x = -camWidth + (boundsX / 2.0f - transform.parent.position.x);
        }
        else
        {
            camPosition.x = 0;
        }

        if (transform.parent.position.y < (-boundsY / 2.0f) + camHeight)
        {
            camPosition.y = camHeight + (-boundsY / 2.0f - transform.parent.position.y);
        }
        //else if (transform.parent.position.y > (boundsY / 2.0f) - camHeight)
        //{
        //    camPosition.y = -camHeight + (boundsY / 2.0f - transform.parent.position.y);
        //}
        else
        {
            camPosition.y = 0;
        }
        
        cam.transform.localPosition = camPosition;

        

        //CONTROLS
        if (Input.GetKeyDown(KeyCode.Z) && currentAmmo > 0)
        {
            bulletObject = Instantiate<GameObject>(bullet, bulletPosition, Quaternion.identity);
            bulletObject.transform.localScale *= bulletScale;
            bulletObject.GetComponent<Bullet>().SetSpeed(currentBulletSpeed);
            currentAmmo--;

            if(isJumping)
            {
                gunSoundAir.Post(gameObject);
            }
            else
            {
                gunSoundWater.Post(gameObject);
            }
        }

        

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }



        //DIRECTIONAL CONTROLS
        

        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity.y -= speedIncrease * Time.deltaTime;
        }



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!isJumping)
            {
                velocity.x -= speedIncrease * Time.deltaTime;
            }

            scaleVector = new Vector3(scale, scale, 1);
            bulletScale = 1;
            bulletDist.x = -bulletOffsetX;
            currentBulletSpeed = -bulletSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!isJumping)
            {
                velocity.x += speedIncrease * Time.deltaTime;
            }

            scaleVector = new Vector3(-scale, scale, 1);
            bulletScale = -1;
            bulletDist.x = bulletOffsetX;
            currentBulletSpeed = bulletSpeed;
        }

        bulletPosition = bulletDist + transform.parent.position;

        transform.localScale = scaleVector;

        if (isJumping)
        {
            velocity.y -= speedIncrease * Time.deltaTime / 1.0f;

            transform.parent.position += velocity * Time.deltaTime;
            bulletPosition = bulletDist + transform.parent.position;


            if (transform.parent.position.y < (boundsY / 2.0f))
            {
                isJumping = false;
                Instantiate<GameObject>(splashObject, transform.parent.position, Quaternion.identity);
                splashSound.Post(gameObject);
            }

            return;
        }

        if (Input.GetKey(KeyCode.UpArrow) && transform.parent.position.y < (boundsY / 2.0f))
        {
            velocity.y += speedIncrease * Time.deltaTime;
        }


        //CHECK FOR LEVEL BOUNDS
        if (transform.parent.position.y > (boundsY / 2.0f))
        {
            isJumping = true;
            Instantiate<GameObject>(splashObject, transform.parent.position, Quaternion.identity);
            splashSound.Post(gameObject);
        }
        else if (transform.parent.position.y < (-boundsY / 2.0f))
        {
            velocity.y = 10 * speedIncrease * Time.deltaTime;
        }
        else if (transform.parent.position.x < (-boundsX / 2.0f))
        {
            velocity.x = 10 * speedIncrease * Time.deltaTime;
        }
        else if (transform.parent.position.x > (boundsX / 2.0f))
        {
            velocity.x = -10 * speedIncrease * Time.deltaTime;
        }


        //LIMIT MAX SPEED
        if (velocity.x > maxSpeed)
        {
            velocity.x = maxSpeed;
        }
        if (velocity.x < -maxSpeed)
        {
            velocity.x = -maxSpeed;
        }
        if (velocity.y > maxSpeed)
        {
            velocity.y = maxSpeed;
        }
        if (velocity.y < -maxSpeed)
        {
            velocity.y = -maxSpeed;
        }

        velocity.x /= 1.01f;
        velocity.y /= 1.01f;
        
        transform.parent.position += velocity * Time.deltaTime;
        
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetCurrentPollution()
    {
        return currentPollution;
    }

    public int GetCurrentStopped()
    {
        return controller.pollutersSleepingWithTheFishes;
    }
    public int GetCurrentScore()
    {
        return (int)score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pollution")
        {
            //Play pickup sound
            pickupTrashSound.Post(gameObject);

            Destroy(collision.gameObject);
            //if(currentAmmo < maxAmmo)
            //{
            //    currentAmmo++;
            //}
            if (currentPollution < maxPollution)
            {
                currentPollution++;
            }
        }

        else if (collision.gameObject.tag == "Net" && currentPollution > 0)
        {
            depositTrashSound.Post(gameObject);

            while (currentPollution > 0)
            {
                score += (1173);
                score *= 1.005f;
                currentPollution--;
                pollutionToNextAmmo--;
                if (pollutionToNextAmmo == 0)
                {
                    currentAmmo++;
                    pollutionToNextAmmo = pollutionPerAmmo;
                }
            }
        }

        else if (collision.gameObject.tag == "Net2" && currentPollution > 0)
        {
            depositTrashSound.Post(gameObject);

            while (currentPollution > 0)
            {
                score += (1173);
                score *= 1.005f;
                currentPollution--;
                pollutionToNextAmmo--;
                if (pollutionToNextAmmo == 0)
                {
                    currentAmmo++;
                    pollutionToNextAmmo = pollutionPerAmmo;
                }
            }
        }

        else if (collision.gameObject.tag == "Toxic")
        {
            Debug.Log("asdsafdsgs");
            musicEvent.Post(gameObject); ;
            atmosEvent.Post(gameObject);

            SceneManager.LoadScene("Daryl");
        }
    }
}
