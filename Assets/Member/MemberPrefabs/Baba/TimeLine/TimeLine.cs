using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLine : MonoBehaviour
{
    // ここにインスペクター上であらかじめ複数のセット
    public PlayableDirector[] timelines;
    private PlayableDirector director;

    void Start()
    {

    }
    //イベント再生メソッド ボタンに割り当てる
    public void EventPlay(int id)
    {
        //ボタンの引数によってタイムラインを指定して再生
        switch (id)
        {
            case 0:
                timelines[0].Play();
                break;
            case 1:
                timelines[1].Play();
                break;
            case 2:
                timelines[2].Play();
                break;
        }
    }

}
