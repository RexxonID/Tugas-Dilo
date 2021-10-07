using UnityEngine;

public class SideWall : MonoBehaviour
{
    public PlayerControl player;
    [SerializeField]
    private GameManager gameManager;

    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        if(anotherCollider.name == "Ball")
        {
            player.IncrementScore();
            if(player.Score < gameManager.maxScore)
            {
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
