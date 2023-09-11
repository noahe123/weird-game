using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        HideSpeechBubble();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }*/

    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; // Show the entire line immediately
            }
        }
    }*/



    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    /*
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }*/

    /*
    IEnumerator TypeLine()
    {
        string[] words = lines[index].Split(' '); // Split the line into words
        textComponent.text = string.Empty;

        for (int i = 0; i < words.Length; i++)
        {
            textComponent.text += words[i] + " "; // Add the word and a space
            yield return new WaitForSeconds(textSpeed);
        }
    }*/

    IEnumerator TypeLine()
    {
        Debug.Log("Typing Line...");


        string[] words = lines[index].Split(' '); // Split the line into words
        textComponent.text = string.Empty;

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            textComponent.text += word + " "; // Add the word and a space

            float wordSpeed = textSpeed * word.Length; // Calculate typing speed based on word length
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void HideSpeechBubble()
    {
        transform.parent.gameObject.GetComponent<Image>().enabled = false;
        textComponent.enabled = false;
        Debug.Log("Hiding Speech Bubble...");
    }

    public void ShowSpeechBubble()
    {
        transform.parent.gameObject.GetComponent<Image>().enabled = true;
        textComponent.enabled = true;
        Debug.Log("Showing Speech Bubble...");

    }

    public void RefreshFirstLine(string myLine)
    {
        ShowSpeechBubble();

        lines[0] = myLine;
        if (index < lines.Length - 1)
        {
            //index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            Invoke("HideSpeechBubble", 5f);
        }
        else
        {
            textComponent.enabled = false;
        }
    }


}
