using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileC : MonoBehaviour {

    #region
    public float projectileSpeed;
    public GameObject explosionPrefab;
    private EnemyC enemy;
    #endregion
    
    void Start () {
        enemy = GameObject.Find("Enemy").GetComponent<EnemyC>();
    }
	
	void Update ()
    {

        // Move projectile
        float amtToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * amtToMove);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.tag=="Enemy")
        {
            PlayerC.score += 100;
            PlayerC.UpdateStats();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            enemy.SetPositionAndSpeed();
            enemy.minSpeed += 0.25f;
            enemy.maxSpeed += 0.5f;

            Destroy(gameObject);
        }

        if (PlayerC.score >= 3000)
            Application.LoadLevel("Win");

        //Debug.Log("We hit: " + otherObject.name);
    }

}
