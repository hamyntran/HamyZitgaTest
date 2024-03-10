using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelMapManager : SingletonMonoBehaviour<LevelMapManager>
{
    public int totalLevel;
    [SerializeField] private int unlockedLevel;
    public string inGameScene;

    public int UnlockedLevel
    {
        get => PlayerPrefs.GetInt("UnlockLevel", unlockedLevel);
        set
        {
            PlayerPrefs.SetInt("UnlockLevel", value);
            unlockedLevel = value;
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(inGameScene);
    }

    private int GeneratedStar
    {
        get => PlayerPrefs.GetInt("GenerateStar", 0);
        set => PlayerPrefs.SetInt("GenerateStar", value);
    }

    private void Start()
    {
        unlockedLevel = UnlockedLevel;

        if (GeneratedStar == 0)
        {
            GeneratedStar = 1;
            GenerateRandomStar();
        }
    }

    public void GenerateRandomStar()
    {
        for (int i = 1; i <= UnlockedLevel; i++)
        {
            int randomStar = GetRandomStar();
            PlayerPrefs.SetInt("Level star" + i, randomStar);
        }

        int GetRandomStar()
        {
            return Random.Range(1, 4);
        }
    }


    public void ResetAllStage()
    {
        PlayerPrefs.DeleteKey("Level star" + 1);

        for (int i = 2; i <= UnlockedLevel; i++)
        {
            if (LevelMapGenerator.buttonByLevel.ContainsKey(i))
            {
                LevelMapGenerator.buttonByLevel[i].SetLockStatus(true);
            }

            PlayerPrefs.DeleteKey("Level star" + i);
        }

        UnlockedLevel = 1;
    }
}

#if UNITY_EDITOR

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
        }

        if (GUILayout.Button("Reset Stage"))
        {
            _target.ResetAllStage();
        }
    }
}

#endif