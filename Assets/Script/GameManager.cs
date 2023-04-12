
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private static int record = 0;
    private int score;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text recordTxt;

    [SerializeField] Snake snake;

    private void Start() {
        UpdateRecord();
        this.score = 0;
        this.scoreTxt.text = this.score.ToString();
        this.recordTxt.text = record.ToString();

    }


    public void AddScore(){
        this.scoreTxt.text = (this.score += 10).ToString();
    }


    public void OnLose(){
        SetRecord();
        ResetScore();
        UpdateRecord();
    }

    private void SetRecord(){
        if (this.score > record){
           PlayerPrefs.SetInt("record", score);
        }
    }
    private void UpdateRecord(){
        record = PlayerPrefs.GetInt("record");
        this.recordTxt.text = record.ToString();
    }

    private void ResetScore(){
        this.score = 0;
        this.scoreTxt.text = this.score.ToString();
    }

    public bool FoodCollidesWithSegment(Vector2 foodPosition) {
        
        return snake.IsCollidingWithFood(foodPosition);;
    }

}
