using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NewLevelGeneration
{
    public abstract class Randomizer: MonoBehaviour
    {
        public int GetRandomBetween(int one, int two)
        {
            return UnityEngine.Random.Range(one, two);
        }
    }
}
