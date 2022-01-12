using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Domain.Models
{
    public class AppRole : IdentityRole
    {
        public string CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public string? LastUpdatedBy { get; private set; }
        public DateTimeOffset? LastUpdatedAt { get; private set; }
        public bool? Active { get; private set; }

        public AppRole(string name, string createdBy)
        {
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = DateTimeOffset.Now;
            Active = true;
        }

        public void AtualizarNome(string name, string updatedBy)
        {
            Name = name;
            LastUpdatedBy = updatedBy;
            LastUpdatedAt = DateTimeOffset.Now;
        }

        public void Inativar(string updatedBy)
        {
            Active = false;
            LastUpdatedBy = updatedBy;
            LastUpdatedAt = DateTimeOffset.Now;
        }
    }
}
