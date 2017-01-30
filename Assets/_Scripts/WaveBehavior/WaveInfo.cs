using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Text.RegularExpressions;
using UnityEngine;

public static class WaveInfo {

    public static SortedList<int, KeyValuePair<int, int>[]> ReadFromJSONFile(int waveIndex)
    {
        TextAsset targetFile = Resources.Load<TextAsset>("WaveConfig") as TextAsset;
        var Config = JSON.Parse(targetFile.text);

        int numberOfSections = Config["data"][GetWaveIndexString(waveIndex)]["Sections"].AsInt;

        Debug.Log(numberOfSections);

        //Here the final list is going to be stored
        SortedList<int, KeyValuePair<int, int>[]> generatedJSONList = new SortedList<int, KeyValuePair<int, int>[]>();

        for (int sec = 0; sec < numberOfSections; sec++)
        {

            Debug.Log("Section" + sec);

            //Checking wheter a wave is referenced as section or it is locally declared
            if (Regex.IsMatch(Config["data"][GetWaveIndexString(waveIndex)][GetSectionsIndexString(sec)].Value, @"^\d+$"))
            {
                SortedList<int, KeyValuePair<int, int>[]>  tempList = ReadFromJSONFile(Config["data"][GetWaveIndexString(waveIndex)][GetSectionsIndexString(sec)].AsInt);

                //Store pairs without key from SortedList
                ArrayList pairList = new ArrayList();

                //Extract all pairs into the pairList
                foreach(KeyValuePair<int, int>[] pair in tempList.Values)
                {
                    pairList.Add(pair);
                }

                //Convert ArrayList into Array to fit into SortedList
                KeyValuePair<int, int>[] sectionList = (KeyValuePair < int, int>[])pairList.ToArray(typeof( KeyValuePair<int, int>[]));

                //Section is put into final list
                generatedJSONList.Add(sec, sectionList);
            }
            else
            {
                KeyValuePair<int, int>[] sectionList = new KeyValuePair<int, int>[Config["data"][GetWaveIndexString(waveIndex)][GetSectionsIndexString(sec)].Count];

                for (int i = 0; i < Config["data"][GetWaveIndexString(waveIndex)][GetSectionsIndexString(sec)].Count; i++)
                {
                    sectionList[i] = new KeyValuePair<int, int>(Config["data"][GetWaveIndexString(waveIndex)][GetSectionsIndexString(sec)][i][0].AsInt, Config["data"][GetWaveIndexString(waveIndex)][GetSectionsIndexString(sec)][i][1].AsInt);
                }

                //Section is put into final list
                generatedJSONList.Add(sec, sectionList);
            }
        }

        return generatedJSONList;
    }

    private static string GetWaveIndexString(int waveIndex)
    {
        return string.Concat("Wave#", waveIndex);
    }

    private static string GetSectionsIndexString(int sectionIndex)
    {
        return string.Concat("#", sectionIndex);
    }
}