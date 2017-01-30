using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class WaveInfo  {

    WaveInfo(int waveIndex)
    {

        TextAsset targetFile = Resources.Load<TextAsset>("WaveConfig") as TextAsset;
        var Node = JSON.Parse(targetFile.text);
    }
}
