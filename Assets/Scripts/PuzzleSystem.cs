using UnityEngine;
using UnityEngine.UI;

public class PuzzleSystem : MonoBehaviour
{
    public GameObject wirePuzzle;
    public GameObject buttonSequencePuzzle;
    public float puzzleTimeLimit = 10f;
    private float puzzleTimer;
    private bool isPuzzleActive = false;

    void Update()
    {
        if (isPuzzleActive)
        {
            puzzleTimer -= Time.deltaTime;
            if (puzzleTimer <= 0)
            {
                FailPuzzle();
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) // Example key to start puzzle
        {
            StartPuzzle();
        }
    }

    void StartPuzzle()
    {
        // Randomly choose a puzzle
        isPuzzleActive = true;
        puzzleTimer = puzzleTimeLimit;

        int puzzleType = Random.Range(0, 2); // 0 = wire, 1 = button sequence
        if (puzzleType == 0)
        {
            wirePuzzle.SetActive(true);
        }
        else
        {
            buttonSequencePuzzle.SetActive(true);
        }
    }

    void CompletePuzzle()
    {
        isPuzzleActive = false;
        wirePuzzle.SetActive(false);
        buttonSequencePuzzle.SetActive(false);

        // Unlock buff or power-up
        // Example: unlock boost rocket shoes
    }

    void FailPuzzle()
    {
        isPuzzleActive = false;
        wirePuzzle.SetActive(false);
        buttonSequencePuzzle.SetActive(false);

        // Respawn player or other consequence
    }
}
