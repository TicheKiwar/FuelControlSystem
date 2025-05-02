namespace AuthService.AuthService.Application.Common.Constants
{
    public class RoleConstants
    {
        public const string Admin = "Admin";
        public const string Operador = "Operador";
        public const string Supervisor = "Supervisor";

        public static readonly string[] AllRoles = { Admin, Operador, Supervisor };
    }
}
