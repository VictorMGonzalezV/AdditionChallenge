using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Reference for the Addition prefab so the coroutine can spawn them
    [SerializeField]
    private GameObject AdditionPrefab;
    //Reference to the empty object that marks the spawning position of the new Additions
    [SerializeField]
    private GameObject AdditionSpawnLocation;
    private Transform AdditionSpawnTransform;
    [SerializeField]
    private TMP_InputField InputField;
    private InputField NumberInputField;
    //I'm storing the new Additions in a Queue since the oldest entry is processed first (FIFO)
    //I made this variable public so the Addition GameObjects can access it, but it's not needed in the Editor
    [HideInInspector]
    public Queue<Addition> AdditionQueue = new Queue<Addition>();
    //Caching the coroutine
    private IEnumerator SpawnAdditionRef;
    //Having these as variables will make increasing the level of difficulty easier later on
    [SerializeField]
    private int MaxAdditions=3;
    [SerializeField]
    private float AdditionSpawnRate = 1.0f;
    //Set after how many correct answers the player sees a particle burst as a small reward
    [SerializeField]
    private int RewardThreshold=3;
    //Set after how  many correct answers the game slightly increases difficulty
    [SerializeField]
    private int DifficultyThreshold=6;
    //Correct answer counter, keeps track of the total correct answers, not the current streak (I'd add that if this was an actual product)
    private int CorrectAnswers = 0;
    //Every RewardThreshold correct answers the player sees particles popping up on the screen
    [SerializeField]
    private ParticleSystem Particles;
    //I'm storing the owl image as a game object so I can activate/deactivate without needing more complex code for fade in/out effects
    [SerializeField]
    private GameObject RewardImage;

    public static GameManager Instance;

    //I'm setting a static instance of the GameManager so when a new Addition prefab is spawned, it can enqueue itself into AdditionQueue
    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //I'm using this quick solution to prevent division by 0, in an actual project I'd use setter validation or backing fields
        if(RewardThreshold<=0||DifficultyThreshold<=0)
        {
            Debug.Log("Averting division by zero");
            RewardThreshold = 3;
            DifficultyThreshold = 6;
        }
        NumberInputField = InputField.GetComponent<InputField>();
        AdditionSpawnTransform = AdditionSpawnLocation.transform;
        StartCoroutine(SpawnAddition());
    }



    private IEnumerator SpawnAddition()
    {
        while(true)
        {
            if (AdditionQueue.Count < MaxAdditions)
            {
                Instantiate(AdditionPrefab, AdditionSpawnTransform);
                //Debug.Log("Can into spawn");
            }
            yield return new WaitForSeconds(AdditionSpawnRate);
        }
  
    }
    //This is a quick solution to display a small reward for the player, this should be replaced with a script that fades the image in and out
    private IEnumerator DisplayRewardImage()
    {
        RewardImage.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        RewardImage.SetActive(false);
    }

    //I placed the logic to process answers here since the GameManager also stores the Additions queue
    public void CheckAnswer()
    {
        //This setup with a local variable ensures the steps follow the challenge instructions 1st clear, then check
        int UserAnswer = NumberInputField.GetInputValue();
        //Debug.Log(UserAnswer);
        NumberInputField.ClearInputField();
        //I compare the answers using Peek() so the Addition isn't removed from the queue by mistake
        if (UserAnswer == AdditionQueue.Peek().GetAdditionValue())
        {
            //Debug.Log("Correct");
            CorrectAnswers++;
            if(CorrectAnswers%RewardThreshold==0)
            {
                //If the total of correct answers is divisible by RewardThreshold, we spawn the particles at the location of the correct answer
                //Peek() is a fast operation so it's not necessary to store the Addition object in a local variable, we can just call it again here
                Instantiate(Particles, AdditionQueue.Peek().gameObject.transform);
                Debug.Log("Imagine particles being spawned");
                StartCoroutine(DisplayRewardImage());
            }
            //in case the answer is correct we can remove the oldest addition from the queue and destroy the whole GameObject as well
            Destroy(AdditionQueue.Dequeue().gameObject);
            //After destroying the answer, we can check whether it's time to increase difficulty
            if(CorrectAnswers%DifficultyThreshold==0)
            {
                IncreaseDifficulty();
            }
        }
        //else
            //Debug.Log("Wrong");
    
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void IncreaseDifficulty()
    {
        //It is more logical to stop the SpawnAddition coroutine before altering the Addition prefab
        StopAllCoroutines();
        //Both the change in gravity scale and spawning rate should be variables in an actual product if this feature is implemented,
        //but for prototyping purposes this proves the feature works
        //This makes the upcoming additions fall faster, it may need an upper limit to keep the difficulty in check but this works for
        //prototyping purposes. Altering the global gravity through Physics2D is also a possibility but doesn't seem prudent to me
        AdditionPrefab.GetComponent<Rigidbody2D>().gravityScale += 0.25f;
        //I'm also decreasing the SpawnRate so they are spawned faster. I placed validation code to prevent it from dropping to 0 seconds
        if (AdditionSpawnRate > 2.0f)
        {
            AdditionSpawnRate = -1.0f;
        }
        Debug.Log("Increasing difficulty");
        //Now we can restart the spawning with the updated values
        StartCoroutine(SpawnAddition());
    }
}
