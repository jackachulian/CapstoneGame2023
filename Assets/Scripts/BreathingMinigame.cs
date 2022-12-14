using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingMinigame : MonoBehaviour
{

    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 120f;
    bool breathingIn = true;
    bool difficultyChangeMade = false;
    public GameObject thumbsUp;
    public GameObject thumbsDown;

    

    void Update(){

        if(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f){
            currentWaypointIndex ++;
            StartCoroutine(ExecuteAfterTime(0.15f));
            if (currentWaypointIndex >= waypoints.Length){
                currentWaypointIndex = 0;
            }
        }
        //MAKE DIFFICULTY RISE IF YOU DO NOTHING, ADD USER FEEDBACK TEXT (GOOD JOB KEEP GOING, NO THATS NOT IT, ETC.), END GAME TEXT
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Random.Range(speed, speed + 2) * Time.deltaTime);
    
        //if they hold the space key down while the circle is expanding it will decrease the speed,
        // holding it down while the circle is contracting will increase the speed and difficulty back up to where it began.
        if (Input.GetKey("space")){
            if (!difficultyChangeMade){
                if (breathingIn){
                    speed -= 5f;
                    print("difficulty lowered");
                    difficultyChangeMade = true;
                    thumbsUp.SetActive(true);
                    StartCoroutine(DelayedTurnOff(0.2f));
                } else {
                    if (speed < 120){
                        speed += 5f;
                    print("difficulty increased");
                    difficultyChangeMade = true;
                    } else {
                        print("difficulty already max");
                        difficultyChangeMade = true;
                    }
                    thumbsDown.SetActive(true);
                    StartCoroutine(DelayedTurnOff(0.2f));
                }
            }
        }

        if (speed <= 25){
            speed = 15;
            //end game
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

}
