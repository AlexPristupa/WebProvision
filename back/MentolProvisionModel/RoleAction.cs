using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель доступа роли к обьектам
    /// </summary>
    [Table("RoleAction")]
    public class RoleAction
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Идентификатор обьекта
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Идентификатор действия
        /// </summary>
        public int? ActionId { get; set; }
    }

    public class ActionRoleSameRoleIdObjectId : EqualityComparer<RoleAction>
    {
        public override bool Equals(RoleAction rl1, RoleAction rl2)
        {
            if (rl1 == null && rl2 == null)
                return true;
            else if (rl1 == null || rl2 == null)
                return false;

            return rl1.RoleId == rl2.RoleId && rl1.ObjectId == rl2.ObjectId;
        }

        public override int GetHashCode(RoleAction rl)
        {
            return (rl.RoleId, rl.ObjectId).GetHashCode();
        }
    }
}
