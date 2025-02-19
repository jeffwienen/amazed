using System;
using System.Collections;
using UnityEngine;

// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.Map.Weather {
    public class ClearSky : MonoBehaviour, IWeather {
        private const float ProgramDuration = 40f;

        public void Execute(Action callback) {
            StartCoroutine(Program(callback));
        }

        public bool CanBeChained() {
            return true;
        }

        private IEnumerator Program(Action callback) {
            yield return new WaitForSeconds(ProgramDuration);

            callback();
        }
    }
}