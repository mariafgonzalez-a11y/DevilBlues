using UnityEngine;

public class AudioMenage : MonoBehaviour
{
    public static AudioMenage instanceSound;

    public AudioSource audioSource;
    public  AudioClip soundportal;
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

       public void PlayPortal()
       {
             audioSource.PlayOneShot(soundportal);

       }

         public void PlayEnemy()  

        {
        audioSource.PlayOneShot(soundenemy);

        }

    public void PlayDeathPlayer()
    {

     audioSource.PlayOneShot(sounddeathPlayer);
    }
    
    public void PlayAttack()
    {
       audioSource.PlayOneShot(soundattack);
        
    }
      
      public void PlayRun()
      {
           audioSource.PlayOneShot(soundrun);
      }

      public void PlayEnviroment()
      {
         audioSource.PlayOneShot(soundenviroment);

     }

    public void PlayBlow()
    {

       audioSource.PlayOneShot(soundblow);
    }

}
