using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EZMusic
{
    public class EZMMusicTrack : ScriptableObject
    {
        
#if UNITY_EDITOR
        public EZMMusicTrack Create(Object parent)
        {
            var newTrack = ScriptableObject.CreateInstance<EZMMusicTrack>();
            AssetDatabase.AddObjectToAsset(newTrack, parent);
            return newTrack;
        }
#endif

        public List<EZMTrackVariation> Variations;
    }
}