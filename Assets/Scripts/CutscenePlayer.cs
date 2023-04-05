using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutscenePlayer : MonoBehaviour
{
    // The cutscene currently being played
    public Cutscene cutscene;
    public TextMeshProUGUI textComponent;
    public float textSpeed = 0.04f;
    private string[] lines;
    private int lineIndex;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        lines = cutscene.textFile.text.Split("\n");
        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            if (textComponent.text == lines[lineIndex]){
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[lineIndex];
            }
        }
    }

    void StartDialog(){
        lineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach (char c in lines[lineIndex].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        // If not at the last line, keep going
        if (lineIndex < lines.Length -1){
            lineIndex++;
            textComponent.text = string.Empty;

            // If the current line is empty, skip to the next line
            if (lines[lineIndex].Length == 0)
            {
                NextLine();
            } else {
                // Otherwise, parse and display the current line
                StartCoroutine(TypeLine());
            }
        } 
        // otherwise, stop the cutscene
        else {
            gameObject.SetActive(false);
        }
    }
}
