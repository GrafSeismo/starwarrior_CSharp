#region File description

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpirationSystem.cs" company="GAMADU.COM">
//     Copyright � 2013 GAMADU.COM. All rights reserved.
//
//     Redistribution and use in source and binary forms, with or without modification, are
//     permitted provided that the following conditions are met:
//
//        1. Redistributions of source code must retain the above copyright notice, this list of
//           conditions and the following disclaimer.
//
//        2. Redistributions in binary form must reproduce the above copyright notice, this list
//           of conditions and the following disclaimer in the documentation and/or other materials
//           provided with the distribution.
//
//     THIS SOFTWARE IS PROVIDED BY GAMADU.COM 'AS IS' AND ANY EXPRESS OR IMPLIED
//     WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GAMADU.COM OR
//     CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
//     ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//     NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
//     ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//     The views and conclusions contained in the software and documentation are those of the
//     authors and should not be interpreted as representing official policies, either expressed
//     or implied, of GAMADU.COM.
// </copyright>
// <summary>
//   The expiration system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion File description

namespace StarWarrior.Systems
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.System;

    using StarWarrior.Components;

    #endregion

    /// <summary>The expiration system.</summary>
    [ArtemisEntitySystem]
    public class ExpirationSystem : EntityProcessingSystem
    {
        /// <summary>The expires mapper.</summary>
        private ComponentMapper<ExpiresComponent> expiresMapper;

        /// <summary>Initializes a new instance of the <see cref="ExpirationSystem" /> class.</summary>
        public ExpirationSystem()
            : base(typeof(ExpiresComponent))
        {
        }

        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.expiresMapper = new ComponentMapper<ExpiresComponent>(this.EntityWorld);
        }

        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity)
        {
            ExpiresComponent expiresComponent = this.expiresMapper.Get(entity);
            if (expiresComponent != null)
            {
                float ms = TimeSpan.FromTicks(this.EntityWorld.Delta).Milliseconds;
                expiresComponent.ReduceLifeTime(ms);

                if (expiresComponent.IsExpired)
                {
                    entity.Delete();
                }
            }
        }
    }
}