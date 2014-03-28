using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Utils;

namespace PlatformerPrototype.Actors.Handlers
{
    using Weapons;

    public class WeaponManager
    {
        #region Declarations

        List<IWeapon> weapons;
        IWeapon activeWeapon;
        Enumerator weaponEnumerator;
 
        #endregion

        #region Constructor

        public WeaponManager()
        {
            Reset();
        }

        #endregion

        #region Properties

        bool HasWeapons
        {
            get
            {
                return (weapons.Count>0);
            }
        }

        #endregion

        #region Public Helper Methods

        public void Reset()
        {
            weapons = new List<IWeapon>();
            weaponEnumerator = new Enumerator(weapons.Count, 0);
            activeWeapon = null;
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.Add(weapon);
            activeWeapon = weapon;

            weaponEnumerator.Count = weapons.Count;
        }

        public void NextWeapon()
        {
            if (!this.HasWeapons) return;   
            weaponEnumerator.Next();
            activeWeapon = weapons[weaponEnumerator.Value];
        }

        public void Shoot(Vector2 location,Vector2 velocity)
        {
            if (!this.HasWeapons) return;   
            activeWeapon.Shoot(location, velocity);
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            foreach (IWeapon weapon in weapons)
                weapon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IWeapon weapon in weapons)
                weapon.Draw(spriteBatch);
        }

        #endregion
    }
}
