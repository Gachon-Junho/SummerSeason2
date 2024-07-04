using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Result;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    private CSVStorage csvs;
    private List<PlayerScore> playerScores;

    [SerializeField]
    private GameObject scoreCard;

    [SerializeField] 
    private GameObject scrollContent;

    [SerializeField]
    private GameObject resultLayer;
    
    [SerializeField]
    private GameObject leaderboardLayer;
    
    
    void Awake()
    {
        csvs = new CSVStorage();
        playerScores = csvs.Read<PlayerScore>(CSVStorage.SCORES_FILE_NAME).ToList();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SubmitScore(TMP_InputField textbox)
    {
        var newScore = new PlayerScore() { Name = textbox.text, Score = GameStateCache.Score, Time = GameStateCache.Time };
        var duplicating = playerScores.FirstOrDefault(p => p.Name == newScore.Name);
        
        if (duplicating != null)
        {
            duplicating.Score = newScore.Score;
            duplicating.Time = newScore.Time;
        }
        else
        {
            playerScores.Add(newScore);
        }
        
        csvs.Write(playerScores, CSVStorage.SCORES_FILE_NAME);
        
        resultLayer.SetActive(false);
        leaderboardLayer.SetActive(true);
        
        createItem();
    }

    private void createItem()
    {
        foreach (var score in playerScores.OrderByDescending(p => p.Score).ThenBy(p => p.Time))
        {
            var newScore = Instantiate(scoreCard);
                    
            newScore.transform.SetParent(scrollContent.transform);
            newScore.GetComponent<ScoreCard>().PlayerScore = score;
        }
    }
}