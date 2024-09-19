using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    // Staticのデータクラスとか香ばしすぎるだろ
    public static int s_stageNum = 1;
    public static int s_zankiNum = 3;

    private const int kCornNum = 4;
    public static bool[] s_OneUpCornFlag = new bool[kCornNum];
}
