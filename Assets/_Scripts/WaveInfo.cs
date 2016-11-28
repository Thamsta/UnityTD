using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;

public class WaveInfo {

    Section section;

	public float enemySpawnInterval;
	public int waveSize;
	public int waveIndex;
    public int sectionIndex;
    public Vector2 indices;

    ArrayList spawnList;

	public WaveInfo (int waveIndex) {
        this.waveIndex = waveIndex;
        InitConfig();
    }

	public WaveInfo (int waveIndex, int sectionIndex) {
		this.waveIndex = waveIndex;
        this.sectionIndex = sectionIndex;
        InitConfig();
    }

    public Vector2 GetSectionIndicies()
    {
        return new Vector2(0,0);
    }

	public int GetSectionSize () {
        int a = 0;

        foreach (KeyValuePair<string, int> pair in spawnList)
        {
            //Add the amount of enemies to the accumulator
            a = a + pair.Value;
        }

        return a;
	}

    public int GetNumberOfSections()
    {
        return section["NUMBER_OF_SECTIONS"].IntValue;
    }

	public ArrayList ReadConfig () {

        ArrayList spawnList = new ArrayList();

       //Gets all the types for this section from this config
       string[] types = section[GetStringForSectionType(sectionIndex)].StringValueArray;

        //If the type array is not empty
       if (types.Length != 0)
       {
            //Gets amounts for this section from config
            int[] amounts = section[GetStringForSectionAmount(sectionIndex)].IntValueArray;
                
            //Iterates through all amounts given
            for (int j = 0; j < types.Length; j++)
            {
                //Placeholder for amount
                int a;
                //Checks wheter there is a value for given type if not then 0
                if (j > amounts.Length - 1) { a = 0; } else { a = amounts[j];}
                
                //Adds a pair of type and int value to the spawn list for this section.
                spawnList.Add(new KeyValuePair<string,int>(types[j],a));
            }
        }
        return spawnList;
	}

    private string GetStringForSectionType(int i)
    {
        return string.Concat("SECTION_", i, "_TYPE");
    }

    private string GetStringForSectionAmount(int i)
    {
        return string.Concat("SECTION_", i, "_AMOUNT");
    }

    private string GetStringForWave(int index)
    {
        return string.Concat("Wave#", index);
    }

    void InitConfig()
    {
        Configuration cfg = Configuration.LoadFromFile("wave.cfg");

        //Checks wheter or not this is a section or wave. If it is a wave then sectionIndex is smaller
        //than 0
        if (sectionIndex >= 0)
        {
            section = cfg[GetStringForWave(waveIndex)];
        }
        else
        {
            section = null;
        }
    }
}