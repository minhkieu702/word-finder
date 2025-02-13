using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class SelectPuzzleButton : MonoBehaviour
{
    public  GameData gameData;
    public static GameData cloneGameData;
    public  GameLevelData levelData;
    public static GameLevelData cloneLevelData;
    public Text categoryText;
    public Image progressBarFilling;
    private AddCategory addCategory;

    private string gameSceneName = "GameScene";

    private bool _levelLocked;
    private static int flag = 0;

    private void Awake()
    {
      addCategory = new AddCategory();
        
    }
    void Start()
    {
        _levelLocked = false;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        UpdateButtonInformation();
        if(_levelLocked)
        {
            button.interactable = false;
        } 
        else
        {
            button.interactable = true;
        }
    }

    private void OnEnable()
    {
        //AdManager.OnInterstitialAdsClosed += IntersialAdsClosed;
    }

    private void OnDisable()
    {
        //AdManager.OnInterstitialAdsClosed -= IntersialAdsClosed;

    }

    private void IntersialAdsClosed()
    {
        
    }

    private void UpdateButtonInformation()
    {
        var currentIndex = -1;
        var totalBoards = 0;
        if (flag == 0)
        {
            cloneGameData = gameData;
            cloneLevelData = levelData;
            //levelData.data.RemoveAt(levelData.data.Count - 1);
            levelData = addCategory.ConfigurePrefab();
            cloneLevelData = null;
            flag++;
        }

        foreach (var data in levelData.data)
        {
            if(data.categoryName == gameObject.name)
            {
                currentIndex = DataSaver.ReadCategoryCurrentIndexValues(gameObject.name);
                totalBoards = data.boardData.Count;
                if (levelData.data[0].categoryName == gameObject.name && currentIndex < 0)
                {
                    DataSaver.SaveCategoryData(levelData.data[0].categoryName, 0);
                    currentIndex = DataSaver.ReadCategoryCurrentIndexValues(gameObject.name);
                    totalBoards = data.boardData.Count;
                }
            }
        }

      

        if(currentIndex == -1)
        {
            _levelLocked = true;
        }
        categoryText.text = _levelLocked ? string.Empty : (currentIndex.ToString() + "/" + totalBoards.ToString());
        progressBarFilling.fillAmount = (currentIndex > 0 && totalBoards > 0) ? ((float)currentIndex / (float)totalBoards) : 0f;

    }

    private void OnButtonClick()
    {
        gameData.selectedCategoryName = gameObject.name;
        //AdManager.Instance.ShowInterstitialAd();
        SceneManager.LoadScene(gameSceneName);

    }
}
