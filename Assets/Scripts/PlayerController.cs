using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; //Controls velocity multiplier
    private int score = 0;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text WinLoseText;
    public Image WinLoseBG;
    public int health = 5;
    bool collision;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            //Debug.Log("Score: " + score); //Displays score

            Destroy(other.gameObject); //Removes coins when collected
        }

        void SetScoreText ()
        {
            scoreText.text = "Score: " + score;
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            //Debug.Log("Health: " + health); //Displays health
        }
        if (other.CompareTag("Goal"))
        {
            WinLoseBG.color = Color.green;
            WinLoseText.text = ($"You Win!");
            WinLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }

        void SetHealthText()
        {
            healthText.text = "Health: " + health;
        }

        if (health == 0)
        {
            WinLoseBG.color = Color.red;
            WinLoseText.text = ($"Game Over!");
            WinLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    IEnumerator LoadScene(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            SceneManager.LoadScene(0);
        }
    
   Rigidbody rb; //Tells script there is a rigidbody, we can use variable rb to reference it in further script

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //rb equals the rigidbody on the player

    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        Vector3 move = new Vector3(xMove, 0, zMove);
        move *= speed * Time.deltaTime;
        transform.Translate(move);

        
    }
}
