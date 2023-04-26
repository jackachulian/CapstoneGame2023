using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreathingMinigame : MonoBehaviour
{

    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 100f;
    bool breathingIn = true;
    bool difficultyChangeMade = false;
    bool gameOver = false;
    public GameObject thumbsUp;
    public GameObject thumbsDown;
    public GameObject FinishedText;
    public GameObject BottomText;

    

    void Update(){

        if(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f){
            currentWaypointIndex ++;
            StartCoroutine(ExecuteAfterTime(0.15f));
            if (currentWaypointIndex >= waypoints.Length){
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Random.Range(speed, speed + 2) * Time.deltaTime);
    
        //if they hold the space key down while the circle is expanding it will decrease the speed,
        // holding it down while the circle is contracting will increase the speed and difficulty back up to where it began.
        
        if (!gameOver){
            if (Input.GetKeyDown("space")){
                if (!difficultyChangeMade){
                    if (breathingIn){
                        speed -= 3f;
                        difficultyChangeMade = true;
                        thumbsUp.SetActive(true);
                        StartCoroutine(DelayedTurnOff(0.25f));
                    } else {
                        if (speed < 120){
                            speed += 5f;
                            difficultyChangeMade = true;
                        } else {
                            print("difficulty already max");
                            difficultyChangeMade = true;
                        }
                        thumbsDown.SetActive(true);
                        StartCoroutine(DelayedTurnOff(0.25f));
                    }
                }
            }
        }
        

        if (speed <= 40){
            //end game
            speed = 15;
            gameOver = true;
            FinishedText.SetActive(true);
            BottomText.SetActive(false);
            StartCoroutine(ReturnToGame(5f));
            
        }

    }



     IEnumerator ExecuteAfterTime(float time){
        yield return new WaitForSeconds(time);
            breathingIn = !breathingIn;
            difficultyChangeMade = false;
        }

    IEnumerator DelayedTurnOff(float time){
        yield return new WaitForSeconds(time);
            thumbsUp.SetActive(false);
            thumbsDown.SetActive(false);
        }

    IEnumerator ReturnToGame(float time){
        yield return new WaitForSeconds(time);
            DONTPLAYCUTSCENE.playCutscene = false;
            SceneManager.LoadScene("Hallway");
        }

}
