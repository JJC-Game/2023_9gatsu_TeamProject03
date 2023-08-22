using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingGameManager : BaseGameManager
{
    string answerWard;
    string[] questionWard;

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

    void WardChange()
    {

    }

    public void Correct()
    {
        if (inGameEnable)
        {
            AddScore();

            WardChange();
        }
    }

    public void Incorrect()
    {
        LessTime();
    }
}
