namespace QingFa.EShop.Domain.Users.Enums
{
    /// <summary>
    /// Represents the role of a user in the system.
    /// </summary>
    public enum UserRoleEnum
    {
        /// <summary>
        /// User with administrative privileges.
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Regular user with standard access.
        /// </summary>
        User = 2,

        /// <summary>
        /// User with limited access, typically read-only.
        /// </summary>
        Guest = 3
    }

}
