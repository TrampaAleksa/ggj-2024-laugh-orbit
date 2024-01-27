using UnityEngine;

public class JokePickupCounter : MonoBehaviour
{
    private int jokeCount = 0;
    public int jokesForWin = 5;
    public void AddJokeCount()
    {
        jokeCount++;
        if ( jokeCount == jokesForWin )
        {
            EventManager.Instance.ReachedNeededJokeNumberEvent();
        }
    }
}