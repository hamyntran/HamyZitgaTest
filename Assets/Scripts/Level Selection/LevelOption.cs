using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelStr;
    [SerializeField] private GameObject _lockIMG, starHolder;
    [SerializeField] private GameObject[] starsIMG;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _tutorialPrefab;

    private int _level;
    private bool unlocked = false;
    private GameObject _tutorialGO;

    public void SetLevel(int lvl)
    {
        _level = lvl;
        LevelMapGenerator.buttonByLevel.Add(_level, this);
        _levelStr.gameObject.SetActive(true);
        _levelStr.text = lvl.ToString();

        if (_level == 1)
        {
            _tutorialGO = Instantiate(_tutorialPrefab, transform);
            _levelStr.gameObject.SetActive(false);
        }
    }

    public void UnsetLevel()
    {
        LevelMapGenerator.buttonByLevel.Remove(_level);
        if (_tutorialGO != null)
        {
            Destroy(_tutorialGO);
        }
    }

    public void SetLockStatus(bool lockStatus)
    {
        _lockIMG.SetActive(lockStatus);
        starHolder.SetActive(!lockStatus);
        unlocked = !lockStatus;
    }

    public void SetStarStatus(int level)
    {
        int star = PlayerPrefs.GetInt("Level star" + level);

        int i = 0;
        for (; i < star; i++)
        {
            starsIMG[i].SetActive(true);
        }

        for (; i < starsIMG.Length; i++)
        {
            starsIMG[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeScene);
    }

    private void ChangeScene()
    {
        if (unlocked)
            LevelMapManager.Instance.ChangeScene();
    }
}