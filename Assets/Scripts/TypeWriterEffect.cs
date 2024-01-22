using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Threading;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed = 50f;

    public bool isRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>(){'.','!','?'}, 0.6f),
        new Punctuation(new HashSet<char>(){',',';',':'}, 0.3f)
    };

    private Coroutine typingCoroutine;

    public void Run(string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        isRunning = true;

        textLabel.text = string.Empty;

        /*yield return new WaitForSeconds(1); // wait 1 sec before starting to type out the text*/

        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typeWriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for(int i = lastCharIndex; i<charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                textLabel.text = textToType.Substring(0, i +1);

                if (isPunctuation(textToType[i], out float waitTime) && !isLast && !isPunctuation(textToType[i+1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }
            yield return null;
        }

        isRunning = false;
    }

    private bool isPunctuation(char character, out float waitTime)
    {
        foreach(Punctuation punctuationCategory in punctuations)
        {
            if(punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }
        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
