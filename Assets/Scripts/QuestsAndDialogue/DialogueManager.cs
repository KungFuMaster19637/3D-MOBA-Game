using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;


public class DialogueManager : MonoBehaviour
{

    public Queue<string> sentences;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;
    public GameObject abilityManager;
    private NavMeshAgent agent;

    void Start()
    {
        sentences = new Queue<string>();
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();

    }

    public void StartDialogue(Dialogue dialogue)
    {
        agent.enabled = false;

        abilityManager.SetActive(false);

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        //dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        agent.enabled = true;

        abilityManager.SetActive(true);

        animator.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; 
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
