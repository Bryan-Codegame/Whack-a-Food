using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private Rigidbody rb;
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;
    public GameObject FloatingTextPrefab;

    public float timeOnScreen = 1.0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

        //transform.position = RandomSpawnPosition(); 
        StartCoroutine(RemoveObjectRoutine()); // begin timer before target leaves screen

    }

    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)
        {
            if (gameObject.CompareTag("Bad")) 
            {
                gameManagerX.GameOver();
            }
            
            if (FloatingTextPrefab)
            {
                ShowTextFloating();
            }
            Destroy(gameObject);
            gameManagerX.UpdateScore(pointValue);
            Explode();
        }
               
    }

    // If target that is NOT the bad object collides with sensor, trigger game over
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Sensor")&& !gameObject.CompareTag("Bad"))
        {
            gameManagerX.GameOver();
        } 

    }

    // Display explosion particle at object's position
    void Explode ()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }

    void ShowTextFloating ()
    {
       var textPrefab = Instantiate(FloatingTextPrefab, transform.position, FloatingTextPrefab.transform.rotation);
       textPrefab.GetComponent<TextMesh>().text = pointValue.ToString();
    }
    // After a delay, Moves the object behind background so it collides with the Sensor object
    IEnumerator RemoveObjectRoutine ()
    {
        yield return new WaitForSeconds(timeOnScreen);
        if (gameManagerX.isGameActive)
        {
            transform.Translate(Vector3.forward * 5, Space.World);
        }

    }

}
