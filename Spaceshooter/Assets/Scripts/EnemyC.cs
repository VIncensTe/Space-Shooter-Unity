using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour {

    #region
    public float maxSpeed;
    public float minSpeed;
    public float maxSpeeds;
    public float minSpeeds;
    public float enemySpeed;
    public float enemySpeedSide;
    private float x, y, z;

    private float MinRotateSpeed = 60f;
    private float MaxRotateSpeed = 120f;
    private float MinScale = .8f;
    private float MaxScale = 2f;
    private float currentRotationSpeed;
    private float currentScaleX;
    private float currentScaleY;
    private float currentScaleZ;
    #endregion

    
    void Start () {
        SetPositionAndSpeed();
	}
	
	void Update () {

        float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed);


        //Enemy moves down
        float moveDown = enemySpeed * Time.deltaTime;
        transform.Translate(Vector3.down * moveDown, Space.World);

        //Enemy moves left/right
        float moveSideways = enemySpeedSide * Time.deltaTime;
        transform.Translate(Vector3.right * moveSideways, Space.World);

        //Moves Enemy after its on the bottom
        if (transform.position.y <= -5)
        {
            SetPositionAndSpeed();
            PlayerC.missed++;
            PlayerC.UpdateStats();
        }
    }

    public void SetPositionAndSpeed()
    {
        currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);
        currentScaleX = Random.Range(MinScale, MaxScale);
        currentScaleY = Random.Range(MinScale, MaxScale);
        currentScaleZ = Random.Range(MinScale, MaxScale);

        //Set enemy speed at random between 2 defined values
        enemySpeed = Random.Range(minSpeed, maxSpeed);
        enemySpeedSide = Random.Range(minSpeeds, maxSpeeds);

        //Set Position with random x range set y and z

        x = Random.Range(-6f , 6f);
        y = 7f;
        z = 0f;

        transform.position = new Vector3(x, y, z);

        transform.localScale = new Vector3(currentScaleX, currentScaleY,
        currentScaleZ);

    }

   

}
