using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wave : MonoBehaviour
{
    //Index of this wave
    public int waveIndex;

    //Section Index
    public int currentSectionIndex;

    //Contains all the information about the wave
    WaveInfo waveInfo;
    //Array of all sections contained in the wave
    WaveSection[] sections;
    //Waypoints on which the enemy is walking along
    GameObject[] waypoints;

    //When did the last speical enemy spawn?
    float lastSpecialEnemySpawnTime;

    public Wave(int waveIndex, GameObject[] waypoints)
    {
        this.waveIndex = waveIndex;

        //Get Information about the wave
        waveInfo = new WaveInfo(waveIndex);

        //Set waypointa
        this.waypoints = waypoints;

        //Generates wave sections
        GenerateWaveSections();
    }

    /// <summary>
    /// Spawn the next enemy if possible
    /// </summary>
    public void SpawnNext()
    {
        //If the current wave has not yet finished
        if (!sections[currentSectionIndex].HasSectionFinished())
        {
            //Spawns next enemy in spawn list of given section
            sections[currentSectionIndex].SpawnNext();
        }
        else
        {
            //If there are still sections to go through
            if(!HasWaveFinished())
            {
                currentSectionIndex++;
            }
        }
    }

    /// <summary>
    /// Generates the sections which compose a wave
    /// </summary>
    public void GenerateWaveSections()
    {
        //Declare an array of size of the total number of sections
        sections = new WaveSection[waveInfo.GetNumberOfSections() - 1];

        for (int i = 0; i < sections.Length; i++)
        {
            //Generates a new wave section and sets the inidicies for first and last enemy of this section.
            sections[i] = new WaveSection(new WaveInfo(waveIndex, i));
        }
    }

    /// <summary>
    /// This function can be used manually to spawn outside of the spawnList
    /// </summary>
    /// <param name="obj">The enemies to be spawned</param>
    public void SpecialEnemySpawn(GameObject[] list)
    {
        foreach (GameObject obj in list)
        {
            GameObject enemy = Instantiate(obj, waypoints[0].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>().waypoints = waypoints;

            lastSpecialEnemySpawnTime = Time.time;
        }
    }

    //For later use.
    /*
    bool CanSpecialEnemySpawn()
    {
        return (specialEnemySpawnList.Count != 0) && !isSpecialEnemySpawnEnabled;
    }*/

    /// <summary>
    /// Checks wheter the whole wave has finished.
    /// </summary>
    /// <returns></returns>
    public bool HasWaveFinished()
    {
        //If the last wave has finished then return true otherwise false.
       return (currentSectionIndex == sections.Length - 1 && sections[currentSectionIndex].HasSectionFinished()) ? true : false;
    }
}