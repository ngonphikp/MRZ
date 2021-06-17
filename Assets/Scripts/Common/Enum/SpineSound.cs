using Spine;
using Spine.Unity;
using UnityEngine;

public class SpineSound : MonoBehaviour
{
    SkeletonAnimation anim;

    private void Awake()
    {
        anim = this.GetComponent<SkeletonAnimation>();
        if (anim != null && anim.state != null)
        {
            anim.state.Event += OnEvent;
        }
        else
        {
            foreach (Transform child in this.gameObject.transform)
            {
                SkeletonAnimation animC = child.GetComponent<SkeletonAnimation>();
                if (animC != null && animC.gameObject.GetComponent<SpineSound>() == null && animC.state != null)
                    animC.gameObject.AddComponent<SpineSound>();
            }
        }
    }

    private void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        string path = e.Data.AudioPath.Replace(".ogg", "");
        path = path.Replace(".wav", "");
        path = "Sounds/" + path;
        SoundManager.instance.PlayOneShot(ResourceManager.instance.LoadAudioClip(path), e.Volume);
    }
}
