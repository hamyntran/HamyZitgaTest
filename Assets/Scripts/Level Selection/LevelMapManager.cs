using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

public class LevelMapManager : SingletonMonoBehaviour<LevelMapManager>
{
   public int totalLevel;
   public int unlockedLevel;


   public void GenerateRandomStar()
   {
      for (int i = 1; i <= unlockedLevel; i++)
      {
         int randomStar = GetRandomStar();
         Debug.Log($"Level {i} star: {randomStar}");
         PlayerPrefs.SetInt("Level star" +i,  randomStar);
      }

      int GetRandomStar()
      {
         return Random.Range(1, 4);
      }
   }

   public void ResetAllStage()
   {
      for (int i = 1; i <= unlockedLevel; i++)
      {
         PlayerPrefs.DeleteKey("Level star" +i);
      }

      unlockedLevel = 0;
   }
   
}


[CustomEditor(typeof(LevelMapManager))]
public class LevelMapManagerEditor : Editor
{
public override void OnInspectorGUI()
{
   LevelMapManager _target = (LevelMapManager)target;
   DrawDefaultInspector();
   
   if (GUILayout.Button("Generate Random Star"))
   {
      _target.GenerateRandomStar();
      
   }  if (GUILayout.Button("Reset Stage"))
   {
      _target.ResetAllStage();
   }
}
}