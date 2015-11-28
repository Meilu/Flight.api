using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight.DAL.Entities
{
    public class ApiUserEntity : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string Salt { get; set; }
        public ApiUserRole? Role { get; set; }
        public ApiPermission? Permission { get; set; }
    }
    // userentity configuration
    public class ApiUserEntityConfiguration : EntityTypeConfiguration<ApiUserEntity>
    {
        public ApiUserEntityConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
            Property(t => t.Password).HasMaxLength(300).IsRequired();
            Property(t => t.CreatedAt).IsOptional();
            Property(t => t.Email).HasMaxLength(255).IsRequired();
            Property(t => t.Salt).HasMaxLength(255);
            Property(t => t.Username).HasMaxLength(255).IsRequired();
            Property(t => t.Role).IsOptional();
            Property(t => t.Permission).IsOptional();

            // table
            ToTable("user");
        }
    }

    public enum ApiUserRole
    {
        User,
        Staff,
        Admin,
        Master
    }

    public enum ApiPermission
    {
        Read,
        Write,
        ReadWrite,
        None
    }
}
