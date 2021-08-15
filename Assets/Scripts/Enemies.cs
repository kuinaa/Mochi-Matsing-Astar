using UnityEngine;


public class Enemies : MonoBehaviour
{
    public Movement movement {get; private set; }
    public EnemiesHome home {get; private set; }
    public EnemiesScatter scatter {get; private set; }
    public EnemiesChase chase {get; private set; }
    public EnemiesFrightened frightened {get; private set; }

    public EnemiesBehavior initialBehavior;

    public Transform target;
    public int points = 200;


    public void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<EnemiesHome>();
        this.scatter = GetComponent<EnemiesScatter>();
        this.chase = GetComponent<EnemiesChase>();
        this.frightened = GetComponent<EnemiesFrightened>();
    }

    public void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }

        if (this.initialBehavior != null){
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (this.frightened.enabled) {
                FindObjectOfType<GameManager>().EnemiesKilled(this);
            } else{
                FindObjectOfType<GameManager>().PlayerKilled();
            }
        }
    }

}

