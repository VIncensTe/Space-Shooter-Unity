using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerC : MonoBehaviour {



    #region
    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;
    public GameObject projectilePrefab3;

    public float projectileOffset;


    public static int missed = 0;

    public float playerSpeed;


    #endregion

    #region
    public GameObject explosionPrefab;
    public static int score = 0;
    public static int lives = 3;
    public static Text playerStats;
    #endregion
    #region
    private float shipInvisibleTime = 1.5f;
    private float shipMoveOnToScreenSpeed = 5f;
    private float blinkRate = .1f;
    private int numberOfTimesToBlink = 10;
    private int blinkCount;
    #endregion
    enum State
    {
        Playing,
        Explosion,
        Invincible
    };

    private State state = State.Playing;

    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<Text>();
    }
    
    void Update()
    {
        if (state != State.Explosion)
        { 

            // Move player depending on input
            float movePh = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        transform.Translate(movePh * Vector3.right);

        float movePv = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        transform.Translate(movePv * Vector3.up);

        // Screen wrap up/down

        if (transform.position.y < -7.1f)
        {
            transform.position = new Vector3(transform.position.x, 7.1f, transform.position.z);
        }
        if (transform.position.y > 7.1f)
        {
            transform.position = new Vector3(transform.position.x, -7.1f, transform.position.z);
        }


        // Screen wrap left/right
        if (transform.position.x < -7.4f)
        {
            transform.position = new Vector3(7.4f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 7.4f)
        {
            transform.position = new Vector3(-7.4f, transform.position.y, transform.position.z);
        }

        // Fire projectile on Spacebar
        if (Input.GetKeyDown("space") & score < 1000)
        {
            // Set position of Instantiate
            Vector3 position = new Vector3(transform.position.x, transform.position.y + projectileOffset,
            transform.position.z);
            // Create projectile 
            Instantiate(projectilePrefab, position, Quaternion.identity);

        }

        if (Input.GetKeyDown("space") & score >= 1000 & score < 2000)
        {
            // Set position of Instantiate
            Vector3 position1 = new Vector3(0.6f + transform.position.x, transform.position.y + projectileOffset,
            transform.position.z);
            Vector3 position2 = new Vector3(transform.position.x - 0.6f, transform.position.y + projectileOffset,
            transform.position.z);

            // Create projectile 
            Instantiate(projectilePrefab2, position1, Quaternion.identity);
            Instantiate(projectilePrefab2, position2, Quaternion.identity);
        }

            if (Input.GetKeyDown("space") & score >= 2000)
            {

                // Set position of Instantiate
                Vector3 position1 = new Vector3(transform.position.x + 0.5f, transform.position.y + projectileOffset,
                transform.position.z);
                Vector3 position2 = new Vector3(transform.position.x, transform.position.y + (-0.1f * transform.localScale.y),
               transform.position.z);
                Vector3 position3 = new Vector3(transform.position.x - 0.5f, transform.position.y + projectileOffset,
               transform.position.z);
                // Create projectile 
                Instantiate(projectilePrefab3, position1, Quaternion.identity);
                Instantiate(projectilePrefab3, position2, Quaternion.identity);
                Instantiate(projectilePrefab3, position3, Quaternion.identity);

            }
        }

    }
       

        

    

    //Coroutine 
    IEnumerator DestroyShip()
    {
        blinkCount = 0;
        state = State.Explosion;
        
        transform.position = new Vector3(0f, -5.5f, transform.position.z);
        yield return new WaitForSeconds(shipInvisibleTime);
        if (lives > 0)
        {
            while (transform.position.y < -2.2)
            {
                // Move the ship up
                float amtToMove = shipMoveOnToScreenSpeed * Time.deltaTime;
                transform.position = new Vector3(0, transform.position.y + amtToMove,
                transform.position.z);
                yield return 0;
            }
            state = State.Invincible;

            while (blinkCount < numberOfTimesToBlink)
            {
                gameObject.GetComponent<Renderer>().enabled
                = !gameObject.GetComponent<Renderer>().enabled;
                if (gameObject.GetComponent<Renderer>().enabled)
                    blinkCount++;
                yield return new WaitForSeconds(blinkRate);

                
            }

            state = State.Playing;

        }
        else
            Application.LoadLevel("Lose");
    }


    public static void UpdateStats()
    {
        playerStats.text = "Score: " + score.ToString()
        + "\nLives: " + lives.ToString()
        +"\nMissed: " + missed.ToString();
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Enemy" && state == State.Playing)
        {
            
            lives--;
            score = 0;

            UpdateStats();
            // Set a new position and speed for the hit enemy
            EnemyC enemy = otherObject.gameObject.GetComponent<EnemyC>();
            enemy.maxSpeed = 7.0f;
            enemy.minSpeed = 3.0f;
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            enemy.SetPositionAndSpeed();
            // Instantiate the explosion
            StartCoroutine(DestroyShip());
        }
    }

}
