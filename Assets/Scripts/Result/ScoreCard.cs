using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace Result
{
    public class ScoreCard : MonoBehaviour
    {
        public PlayerScore PlayerScore;
        
        [SerializeField] 
        private TMP_Text name;
        
        [SerializeField] 
        private TMP_Text score;
        
        [SerializeField] 
        private TMP_Text time;

        public void SetData(PlayerScore playerScore)
        {
            PlayerScore = playerScore;
            
            name.text = $"Name: {playerScore.Name}";
            score.text = $"Score: {playerScore.Score}";
            time.text = $"Time: {playerScore.Time}";
        }
    }
}