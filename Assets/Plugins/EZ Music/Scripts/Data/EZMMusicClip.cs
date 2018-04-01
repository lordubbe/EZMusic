using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZMusic
{
    public abstract class EZMMusicClip
    {
        public AudioClip Source;
        public double position;

        public abstract double GetLength();
    }
}