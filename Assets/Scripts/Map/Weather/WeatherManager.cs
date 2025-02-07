﻿using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;
using Random = System.Random;


// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.Map.Weather {
    public class WeatherManager : MonoBehaviour {
        private readonly Random _random = new Random();

        private readonly List<IWeather> _weatherOptions = new List<IWeather>();

        private IWeather _activeWeather;
        private ParticleSystem _rainSystem;
        private ParticleSystem.ShapeModule _rainSystemShape;

        public bool Start;

        public IWeather ActiveWeather {
            get { return _activeWeather; }
            set {
                _activeWeather = value;
                _activeWeather.Execute(WeatherProgramDone);
            }
        }

        private void Awake() {
            _weatherOptions.Add(gameObject.AddComponent<Storm>());
            _weatherOptions.Add(gameObject.AddComponent<ClearSky>());

            _rainSystem = GetComponentInChildren<ParticleSystem>();
            _rainSystemShape = _rainSystem.shape;
        }

        public void Init() {
            InitEnv();
            SetRandomWeather();
        }

        private void InitEnv() {
            // Set cloud (particleSystem) size to fit the map
            var size = GameManager.Instance.Size * 12;
            _rainSystemShape.box = new Vector3(size + 80, size + 80);
        }

        private int GetNewRandom() {
            while (true) {
                var i = _random.Next(0, _weatherOptions.Count);
                if (_weatherOptions[i] != ActiveWeather || _activeWeather.CanBeChained())
                    return i;
            }
        }

        private void SetRandomWeather() {
            ActiveWeather = _weatherOptions[GetNewRandom()];
        }

        private void WeatherProgramDone() {
            SetRandomWeather();
        }
    }
}