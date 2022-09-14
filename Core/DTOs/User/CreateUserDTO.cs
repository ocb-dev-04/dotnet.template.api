using Core.Enums;

namespace Core.DTOs
{
    /// <summary>
    /// <see cref="CreateUserDTO"/> dto model
    /// </summary>
    public sealed class CreateUserDTO
    {
        /// <summary>
        /// The user name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User active status (Active, Inactive, Paused, etc...)
        /// </summary>
        public UserStatus Active { get; set; } = UserStatus.Active;

        /// <summary>
        /// Role assigned to user  
        /// </summary>
        public string Role { get; set; }
    }
}
