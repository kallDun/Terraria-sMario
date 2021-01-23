using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations
{
    class EntityWeaponAnimation
    {
        public EntityTypes entityType { get; protected set; }
        public List<EntityAnimation> animations { get; protected set; }

        public EntityWeaponAnimation(EntityTypes entityType, List<EntityAnimation> animations)
        {
            this.entityType = entityType;
            this.animations = animations;
        }

        public static EntityWeaponAnimation getAnimationsToEntity(List<EntityWeaponAnimation> entityWeaponAnimations, EntityTypes entityType)
        {
            foreach (var animation in entityWeaponAnimations)
            {
                if (animation.entityType == entityType) return animation;
            }
            return null;
        }
    }
}