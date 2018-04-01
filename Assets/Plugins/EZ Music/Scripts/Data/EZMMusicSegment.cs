using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZMusic
{
    public class EZMMusicSegment
    {
        public float BPM;

        public double EntryMarker;
        public double ExitMarker;

        public List<EZMMusicTrack> Tracks;
    }
}
