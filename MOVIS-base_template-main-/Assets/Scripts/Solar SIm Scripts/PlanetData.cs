using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{
    public TextAsset planetSheet;
    public Planet[] PlanetList;

    public static int changeDuration = 100;
    public static int ViewTypeCount = 2;

    void Start()
    {
        PlanetList = new Planet[9];
        ReadCSV();
    }

    private void Update()
    {

    }

    void ReadCSV()
    {
        int planetID = 0;
        string[] data = planetSheet.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        for (int i = 7; i < data.Length - (ViewTypeCount * 2);)
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

    void ParseRow(int i, string[] data, float[] output)
    {
        for (int x = 0; x < ViewTypeCount; x++)
        {
            output[x] = float.Parse(data[i + x]);
        }
    }


    public class Planet
    { //Index in Array is the Viewtype
        public float[] Mass;
        public float[] InitVelocityZ;
        public float[] Diameter;
        public float[] XPos;
        public float[] YPos;
        public float[] OrbitScale;

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
