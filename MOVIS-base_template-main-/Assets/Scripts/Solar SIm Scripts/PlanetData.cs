using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The class that handles reading the CSV data sheet and setting up the ability to change view types
 */
public class PlanetData : MonoBehaviour
{
    public TextAsset planetSheet; //The CSV file
    public Planet[] PlanetList; //The list of subclass planets used to store the data

    //public static int changeDuration = 1000; //The value related to how long it takes for the planets to change views
    public static int ViewTypeCount = 3; //If adding a viewtype, change this value AND the one in VRController

    //Start method
    void Start()
    {
        PlanetList = new Planet[10];
        ReadCSV();
    }

    /*
     * Reads the CSV file and stores the information into the subclass planets
     */
    void ReadCSV()
    {
        int planetID = 0;
        string[] data = planetSheet.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        for (int i = 9; i < data.Length - (ViewTypeCount * 2);)
        {
            Planet planet = new Planet();
            ParseRow(i, data, planet.Mass);
            i += (ViewTypeCount + 1);
            ParseRow(i, data, planet.InitVelocityZ);
            i += (ViewTypeCount + 1);
            ParseRow(i, data, planet.Diameter);
            i += (ViewTypeCount + 1);
            ParseRow(i, data, planet.XPos);
            i += (ViewTypeCount + 1);
            ParseRow(i, data, planet.YPos);
            i += (ViewTypeCount + 1);
            ParseRow(i, data, planet.OrbitScale);
            i += (ViewTypeCount + 1) * 2;
            PlanetList[planetID] = planet;
            planetID++;
        }
    }

    /*
     * Used to seperate the information into the data files
     */
    void ParseRow(int i, string[] data, float[] output)
    {
        for (int x = 0; x < ViewTypeCount; x++)
        {
            output[x] = float.Parse(data[i + x]);
        }
    }

    /*
     * This is a subclass used to store the information of a planet from the CSV file
     */
    public class Planet
    { //Index in Array is the Viewtype
        public float[] Mass; //The possible masses
        public float[] InitVelocityZ; //The possible velocity
        public float[] Diameter; //The possible diameter
        public float[] XPos; //The possible x position
        public float[] YPos; //The possible y position
        public float[] OrbitScale; //The possible scale of the orbit
        /*
         * Note: Mass, InitVelocity, Xpos, and YPos should not change when view types change.
         * Orbit scale and diameter are able and should change
         */

        /*
         * The constructor that takes in the data
         */
        public Planet()
        {
            Mass = new float[ViewTypeCount];
            InitVelocityZ = new float[ViewTypeCount];
            Diameter = new float[ViewTypeCount];
            XPos = new float[ViewTypeCount];
            YPos = new float[ViewTypeCount];
            OrbitScale = new float[ViewTypeCount];
        }
    }
}
