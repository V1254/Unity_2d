using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class NumberWizard : MonoBehaviour
{

    private static int minGuess;
    private static int maxGuess;
    private static int currentGuess;
    private static bool CR_RUNNING = false; // Keep track of when our courotine is running.

    // Start is called before the first frame update
    void Start()
    {
        minGuess = 1;
        maxGuess = 1000;
 
        Debug.Log("Welcome to this cancer console game..");
        Debug.Log(string.Format("Think of a number between {0} and {1}", minGuess, maxGuess));

        StartCoroutine(SleepThenStart(2, UpdateAndShowCurrentGuess)); // I am fully aware that StartCourtine can execute a method, but needed to see how they work.
        StartCoroutine(SleepThenStart(4, AskInput));
    }

    // Update is called once per frame
    void Update()
    {

        if (!CR_RUNNING)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ClearLogConsole();
                Start();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Thanks for playing. Cheerio...");
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit(0);
            } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                minGuess = currentGuess;
                UpdateAndShowCurrentGuess();
                AskInput();
            } else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
                maxGuess = currentGuess;
                UpdateAndShowCurrentGuess();
                AskInput();
            } else if(Input.GetKeyDown(KeyCode.Return)) {
                Debug.Log("Hahaha, i win nerd..");
                Debug.Log("Press r to play again or Quit by pressing q");
            }
        }



    }

    IEnumerator SleepThenStart(float secondsToWait, Action postWait)
    {
        CR_RUNNING = true;
        //yield on a new YieldInstruction that waits for seconds.
        yield return new WaitForSecondsRealtime(secondsToWait);

        //After we have waited x seconds, do the next action..
        CR_RUNNING = false;
        if(postWait != null) {
            postWait();
        }
    }

    void UpdateAndShowCurrentGuess()
    {
        currentGuess = (minGuess + maxGuess) / 2;
        Debug.Log(string.Format("My intituition tells me your number is: {0}", currentGuess));
    }

    void AskInput()
    {
        Debug.Log("Push up arrow key to Indicate Higher,down arrow key to indicate lower and enter key to indicate my guess was correct.");
    }

    void ClearLogConsole()
    {
        Type logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        MethodInfo clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }
}
