using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;

    [Header("Dialogue Settings")]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private bool startOnAwake = true;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip typeSound;
    [SerializeField][Range(0.1f, 2f)] private float pitchVariation = 0.2f;

    [Header("Timing Settings")]
    [SerializeField] private float charsPerSecond = 30f;

    [Header("Input Settings")]
    [SerializeField] private KeyCode continueKey = KeyCode.Space;

    private AudioSource audioSource;
    private Queue<string> sentences = new Queue<string>();
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool waitForInput = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        ToggleDialoguePanel(false);
    }

    void Start()
    {
        if (startOnAwake && dialogueLines.Length > 0)
            StartDialogue(dialogueLines);
    }

    public void StartDialogue(string[] lines)
    {
        Cursor.lockState = CursorLockMode.None;
        sentences.Clear();
        foreach (string line in lines)
            sentences.Enqueue(line);

        ToggleDialoguePanel(true);
        ShowNextSentence();
    }

    void ToggleDialoguePanel(bool state)
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(state);

        if (continueIcon != null)
            continueIcon.SetActive(false);
    }

    void ShowNextSentence()
    {
        // Если идет печать - завершить текущую строку
        if (isTyping)
        {
            FinishTyping();
            return;
        }

        // Если нет больше реплик - завершить диалог
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Начать печать новой реплики
        string sentence = sentences.Dequeue();
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        waitForInput = false;
        dialogueText.text = "";
        float delay = 1f / charsPerSecond;

        if (continueIcon != null)
            continueIcon.SetActive(false);

        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text += sentence[i];
            PlayTypeSound();

            // Обработка ускорения печати
            float timer = 0;
            while (timer < delay)
            {
                if (Input.GetKey(continueKey))
                    timer += Time.deltaTime * 5f;
                else
                    timer += Time.deltaTime;

                yield return null;
            }
        }

        CompleteSentence();
    }

    void CompleteSentence()
    {
        isTyping = false;
        waitForInput = true;

        if (continueIcon != null)
            continueIcon.SetActive(true);
    }

    void FinishTyping()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = sentences.Peek();
            CompleteSentence();
        }
    }

    void PlayTypeSound()
    {
        if (typeSound == null) return;

        audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation);
        audioSource.PlayOneShot(typeSound);
    }

    void EndDialogue()
    {
        ToggleDialoguePanel(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(continueKey))
        {
            if (waitForInput)
            {
                waitForInput = false;
                ShowNextSentence();
            }
            else
            {
                ShowNextSentence();
            }
        }
    }
}