using System.Collections;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public int World = 1;
    public int Stage = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.LoadLevel(World, Stage);
    }
}
