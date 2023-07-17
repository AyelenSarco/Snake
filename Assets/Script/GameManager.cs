
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private static int record = 0;
    private int score;
    private bool unmute = true;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text recordTxt;

    [SerializeField] Snake snake;
    [SerializeField] AudioSource pointSource;
    [SerializeField] AudioSource loseSource;
    


    private void Start() {
        UpdateRecord();
        this.score = 0;
        this.scoreTxt.text = this.score.ToString();
        this.recordTxt.text = record.ToString();

    }


    public void AddScore(){
        this.scoreTxt.text = (this.score += 100).ToString();
        pointSource.Play();
        if (score % 30 == 0){
            snake.increaseSpeed();
        }
    }


    public void OnLose(){
        SetRecord();
        ResetScore();
        UpdateRecord();
        loseSource.Play();
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

    public void Mute(){
        if (unmute){
            pointSource.volume = 0f;
            loseSource.volume = 0f;
            unmute = false;
        } else {
            pointSource.volume = 1f;
            loseSource.volume = 1f;
            unmute = true;
        }
        
    }

}
