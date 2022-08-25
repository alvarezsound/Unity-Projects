# Unity-Projects
This is the repository for all of my Unity course projects! Click the links below to view the files.
## Projects:
- [Frogger](#Frogger)
- <a href="https://github.com/alvarezsound/Unity-Projects/tree/main/Run" target="_blank">Run!!</a>
- <a href="https://github.com/alvarezsound/Unity-Projects/tree/main/Mini_Solar_System" target="_blank">Mini Solar System</a>
- <a href="https://github.com/alvarezsound/Unity-Projects/tree/main/Unity_Fundamentals" target="_blank">Unity Fundamentals</a>
- <a href="https://github.com/alvarezsound/Unity-Projects/tree/main/Ball_Motion" target="_blank">Ball Motion</a>

## Frogger
Completion time: 12 days

After completing the Unity course, I participated in a two-week sprint working on a small dev team. We were each tasked with remaking a classic arcade game. As a fan, I chose Frogger and used the original Frogger as a guide. The game is complete with a title, main game, victory, and game over scenes (pictured below). I recreated and modified the original pixel art to fit a brighter theme. I added some simple animations, created custom sound effects, and added some copyright free music.

Gameplay:
The player starts at the bottom of the screen and must guide five frogs to each of the five homes at the top of the screen while avoiding obstacles on the road and staying on platforms over the water. Die three times and its game over or bring all five frog’s home and you win! The player can use either WASD or arrow keys to move the frog one block at a time.

Score:
Every forward step scores 10 points, and every frog arriving safely home scores 50 points. 10 points are also awarded per each unused ½ second of time. When all five frogs reach home to end the level the player earns 1,000 points

Code highlights - below are just a few code snippets within the numerous scripts that were created for the game:

I created a script to control the movement of the obstacles and platforms. Once attaching the script to the game object, the speed and direction can easily be modified.
MoveCycle.cs 
```cs
private void Update()
{
    // Check if the object is past the right edge of the screen
    if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
    {
        transform.position = new Vector3(leftEdge.x - size, transform.position.y, transform.position.z);
    }
    // Check if the object is past the left edge of the screen
    else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
    {
        transform.position = new Vector3(rightEdge.x + size, transform.position.y, transform.position.z);
    }
    // Move object
    else
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
```

Within the player controller script, i added movement controls (added cooldown to fix teleportation), death, respawning, leap animation, and how the player object interacts with other objects. 
PlayerController.cs
```cs
private void Move(Vector3 direction)
    {
        if (cooldown)
        {
            return;
        }

        Vector3 destination = transform.position + direction;

        // Check for collision
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));

        // Prevent any movement at barrier
        if (barrier != null)
        {
            return;
        }

        // Allows frogger to walk on platforms
        if (platform != null)
        {
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        // Frogger dies when colliiding with obstacle
        if (obstacle != null && platform == null)
        {
            transform.position = destination;
            Death();
        }
        // If conditions pass, move to the destination
        else
        {
            // Check if we have advanced to a farther row
            if (destination.y > farthestRow)
            {
                farthestRow = destination.y;
                FindObjectOfType<GameManager>().AdvancedRow();
            }

            // Leap animation
            StopAllCoroutines();
            StartCoroutine(Leap(destination));

            // Play leap sound
            audioSource.clip = froggerLeaps[Random.Range(0, froggerLeaps.Length)];
            audioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
```

I also created a Game Manager script which includes the scoring system and functions like new game, restart, timer, game over, and victorty.
GameManager.cs
```cs
public void HomeOccupied()
    {
        frogger.gameObject.SetActive(false);

        int bonusPoints = time * 20;
        SetScore(score + bonusPoints + 50);

        if (Cleared())
        {
            SetLives(lives + 1);
            SetScore(score + 1000);
            Invoke(nameof(Victory), 1f);
        }
        else
        {
            Invoke(nameof(Respawn), 1f);
        }
    }
```

Finally, I created an audio manager script as a singleton to attach to an empty that game object/audio source so it never gets destroyed when entering/exiting different scenes. Once the first audio manager object was set up. I made it a prefab and added it to every scene.
AudioManager.cs
```cs
public class AudioManager : MonoBehaviour
{
    public AudioSource audioManager;

    public static AudioMngr instance;
    private static AudioMngr _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        instance = _instance;

        DontDestroyOnLoad(this);
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        audioManager.volume = volume;
        audioManager.PlayOneShot(clip);
    }
}
```

![Frogger](/Images/Frogger_Title.png)
![Frogger](/Images/Frogger_Game.png)
![Frogger](/Images/Frogger_Victory.png)
![Frogger](/Images/Frogger_GameOver.png)

## Run!!
Completion time: 2 days

As a challenge, I was given 2 days to create an original game, complete with art and a full gameplay loop. I created Run!!, a game of tag between a cute duck and a jealous penguin. Players take control of the duck (using mouse clicks for movement) and have to keep away from the penguin for as long as possible. The game is complete with a title and ending screen. The game starts with a 3 second countdown (player and enemy movement is frozen) and then the game starts and the penguin begins to hunt the player. There is a timer that starts at the end of the countdown and stops as soon as you are captured. Your final time is displayed on the end screen. I also added a few UI sounds, countdown sounds, music, and ambience.

![Run](/Images/Run_Title.png)

![Run](/Images/Run_Gameplay.png)

![Run](/Images/Run_GameOver.png)
## Mini Solar System
This is an animated model of the Sun, Earth, and Moons orbit using assets I found on the Unity assets store. I used a point light and area light to recreate the lighting of the Sun and how it shines on the Earth and Moon as they orbit. The objects rotate as they orbit around each other to demonstrate how our solar system operates.

![Solar System](/Images/MiniSolarSystem.png)

## Unity Fundamentals
This project was a part of the Unity 2018 Fundamentals course on Pluralsight. It started off with prototyping the general game ideas based on a game design document. From there, I created a basic level using ProBuilder. Finally I imported the final art assets, configured prefabs, added various lighting, created VFX, character animations, UI, and implemented audio.

## Ball Motion
This was a rapid unity assignment to create a simple maze game from scratch using Unity's built-in 3D game objects. The player takes control of a ball and navigates through a 3D maze course to find the exit.

Back to [top](#Unity-Projects)

