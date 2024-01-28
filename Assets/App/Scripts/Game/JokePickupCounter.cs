using UnityEngine;

public class JokePickupCounter : MonoBehaviour
{
    private int jokeCount = 0;
    public int jokesForWin = 1;
    public int AddJokeCount(out bool isReactWin)
    {
        jokeCount++;
        isReactWin = jokeCount == jokesForWin;
        if (isReactWin)
        {
            EventManager.Instance.ReachedNeededJokeNumberEvent();
        }
        return jokeCount;
    }
}