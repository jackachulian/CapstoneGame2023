using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Cutscene : MonoBehaviour
{
    
    public TextMeshProUGUI textComponent;
    public float textSpeed = 0.04f;
    public TextAsset textFile;
    private string[] lines;
    public int index;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        lines = textFile.text.Split("\n");
        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if (textComponent.text == lines[index]){
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void resetDialog(){
        textComponent.text = string.Empty;
        lines = textFile.text.Split("\n");
    }

    void StartDialog(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        // If not at the last line, keep going
        if (index < lines.Length -1){
            index++;
            textComponent.text = string.Empty;

            // If the current line is empty, skip to the next line
            if (lines[index].Length == 0)
            {
                NextLine();
            } else {
                if (lines[index] == "$breath"){
                    SceneManager.LoadScene("BreathingMinigame");
                }
                else{
                    // Otherwise, parse and display the current line
                    StartCoroutine(TypeLine());
                }

            }
            
        } 
        // otherwise, stop the cutscene
        else {
            gameObject.SetActive(false);
        }
    }
}
