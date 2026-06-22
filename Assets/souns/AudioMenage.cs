using UnityEngine;

public class AudioMenage : MonoBehaviour
{
    public static AudioMenage instanceSound;

     public AudioSource audioSource;

     public AudioClip soundattack;
    public AudioClip soundrun;
    public AudioClip sounddeathPlayer;
    public AudioClip soundenemy;
    public AudioClip soundenviroment;
    public AudioClip soundblow;



    void Awake()
    {
        
        if (instanceSound == null){

            // Create a instance
            instanceSound = this;

            // Set to don't destroy
            DontDestroyOnLoad(gameObject);

        }else{

            // Destroy the instance
            Destroy(gameObject);

        }

    }

    public void     
    {

        // Play sound point collect
        audioSource.PlayOneShot(soundblow);

    }


}
