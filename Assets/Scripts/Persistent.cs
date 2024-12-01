using UnityEngine;

public class Persistent : MonoBehaviour
{
    void Awake()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");

        if (music.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
