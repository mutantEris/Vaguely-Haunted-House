using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MP3Player : MonoBehaviour
{
    #region MP3 click
    [SerializeField] public Material red_glow;
    [SerializeField] public Material green_glow;
    [SerializeField] public Material black_matte;
    [SerializeField] public int mp3_player_state = 2; // start black. 0: green 1: red 2: black
    private int last_on_state = 0;
    [SerializeField] public KeyCode mp3_click_key = KeyCode.Mouse0;
    [SerializeField] public KeyCode mp3_off_key = KeyCode.Mouse1;
    [SerializeField] public AudioSource audio_source;
    [SerializeField] public AudioClip green_audio;
    [SerializeField] public AudioClip red_audio;
    [SerializeField] public AudioClip beep_audio;
    [SerializeField] public AudioClip click_off;
    [SerializeField] public MeshRenderer screen_mat;
    [SerializeField] public bool enableMP3Player = true;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        if(enableMP3Player){
            audio_source.loop = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        # region MP3 Click
        if(enableMP3Player){
            if(Input.GetKeyDown(mp3_off_key) && mp3_player_state != 2){ // turn mp3 player off
                audio_source.Stop();
                audio_source.PlayOneShot(click_off);
                last_on_state = mp3_player_state;
                mp3_player_state = 2;
                screen_mat.material = black_matte;
            }
            else if(Input.GetKeyDown(mp3_click_key)){
                audio_source.Stop();
                audio_source.PlayOneShot(beep_audio);

                if(mp3_player_state == 2){ // black, set to last on state
                    mp3_player_state = 1 - last_on_state;
                }
                if(mp3_player_state == 0){ // green, set to red
                    audio_source.clip = red_audio;
                    screen_mat.material = red_glow;
                    mp3_player_state = 1;
                }
                else{ // red, set to green
                    audio_source.clip = green_audio;
                    screen_mat.material = green_glow;
                    mp3_player_state = 0;
                }
                audio_source.time = Random.Range(0f, audio_source.clip.length);
                audio_source.Play();
            }
        }
        #endregion
    }
}
