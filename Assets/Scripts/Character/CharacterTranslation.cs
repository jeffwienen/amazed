﻿using UnityEngine;

namespace Assets.Scripts.Character {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    // Jordi Wolthuis
    // S1085303
    public class CharacterTranslation : ICharacterTransformation {
        private readonly Character _character;
        private Vector3 _momentum;

        public CharacterTranslation(Character character) {
            _character = character;
            Airborne = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public bool Airborne { get; set; }

        //Handles the basic movement of the player + the jumping
        public void Update() {
            var vertical = Input.GetAxis("Vertical") * _character.Speed * Time.deltaTime;
            var horizontal = Input.GetAxis("Horizontal") * _character.Speed * Time.deltaTime;

            //Allow movement if the player is not in the air
            var translation = Airborne ? _momentum : new Vector3(horizontal, 0, vertical);
            //Store momentum (so the player isn't frozen when he jumps)
            _momentum = translation;
            _character.transform.Translate(translation);


            if (Input.GetKeyDown(KeyCode.L)) {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !Airborne) {
                //Space is pressed and the player is NOT in the air
                Airborne = true;
                _character.PlayJumpSound();
                _character.GetComponent<Rigidbody>().velocity += _character.JumpForce * Vector3.up;
            }
        }
    }
}