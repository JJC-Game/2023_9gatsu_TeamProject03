using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingGameManager : BaseGameManager
{
    string answerWard;

    public override void Arrangements()
    {
        
    }

    public void Decision(string input)
    {
        if (answerWard == input)
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    public void Correct()
    {
        if (inGameEnable)
        {
            correctCount++;

            //WardChange();
        }
    }

    public void Incorrect()
    {
        LessTime();
    }
}
