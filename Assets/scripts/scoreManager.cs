using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;
    AudioSource audioSource;

    [SerializeField]
    private TextMeshProUGUI scoreText , maxScore , menu_score , menu_max_score;

    [SerializeField]
    private AudioSource buffManager , BGM;

    [SerializeField]
    private float stepPitch = 0.5f;

    [SerializeField]
    private GameObject dead_timeline , menu;

    [SerializeField]
    private AudioClip coin , buff;

    public int score = 0 , jump_buff_t_count = 0 , run_speed_buff_t_count = 0;

    public Movement movement;

    float init_jump , init_speed;

    [SerializeField]
    private int jump_buff_time = 10 , speed_buff_time = 10, jump_buff = 2 , speed_buff = 2;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        init_jump = movement.jump;
        init_speed = movement.runSpeed;

        if (PlayerPrefs.HasKey("max_score"))
        {
            maxScore.text = $"best score = {PlayerPrefs.GetInt("max_score")}";
        }
        else
        {
            maxScore.text = $"best score = 0";
        }

        StartCoroutine(scoreRunTime());
    }

    IEnumerator scoreRunTime()
    {
        yield return new WaitForSeconds(5.0f);

        while (true)
        {
            score += 1;
            scoreText.text = score.ToString();

            if (movement.IsDead)
            {
                break;
            }

            yield return new WaitForSeconds(1.6f / (movement.runSpeed + movement.calculate_speedByscore()));
        }
    }


    public void addScore(int s)
    {
        StopCoroutine(resetPitchDelay());
        score += s;
        scoreText.text = score.ToString();
        audioSource.pitch += stepPitch;
        audioSource.pitch = Mathf.Clamp(audioSource.pitch , 1.0f , 5.0f);
        audioSource.PlayOneShot(coin);
        StartCoroutine(resetPitchDelay());
    }

    IEnumerator resetPitchDelay()
    {
        yield return new WaitForSeconds(1.0f);
        resetPicth();
    }

    public void buff_jump()
    {
        buffManager.PlayOneShot(buff);
        movement.setJumpBuff(jump_buff);
        StartCoroutine(buff_time_jump());
        
    }

    public void buff_speed()
    {
        buffManager.PlayOneShot(buff);
        movement.setSpeedBuff(speed_buff);
        StartCoroutine(buff_time_speed());
        
    }

    IEnumerator buff_time_jump()
    {
        jump_buff_t_count = jump_buff_time;

        while (jump_buff_t_count > 0)
        {
            yield return new WaitForSeconds(1.0f);
            jump_buff_t_count -= 1;
            
        }

        reset_jump_buff();

    }

    IEnumerator buff_time_speed()
    {
        run_speed_buff_t_count = speed_buff_time;

        while (run_speed_buff_t_count > 0)
        {
            yield return new WaitForSeconds(1.0f);
            run_speed_buff_t_count -= 1;
            
        }

        reset_speed_buff();

    }

    void reset_jump_buff()
    {
        movement.resetJumpBuff(init_jump);
    }

    void reset_speed_buff()
    {
        movement.resetSpeedBuff(init_speed);
    }

    void resetPicth()
    {
        audioSource.pitch = 1;
    }

    public void IsDead()
    {
        BGM.Stop();
        Time.timeScale = 0.4f;
        dead_timeline.SetActive(true);
        menu.SetActive(true);

        menu_score.text = score.ToString();

        if (PlayerPrefs.HasKey("max_score"))
        {
            int max_score = PlayerPrefs.GetInt("max_score");

            if (score > max_score)
            {
                PlayerPrefs.SetInt("max_score", score);
                menu_max_score.text = "new best score";
            }
            else
            {
                menu_max_score.text = $"best score = {max_score}";
            }
        }
        else
        {
            PlayerPrefs.SetInt("max_score", score);
            menu_max_score.text = "new best score";
        }
        
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
