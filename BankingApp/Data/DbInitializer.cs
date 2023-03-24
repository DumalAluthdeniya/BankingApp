using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;

namespace BankingApp.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            _modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Employer",
                    NormalizedName = "EMPLOYER".ToUpper()
                },
                new IdentityRole
                {
                    Id = "fd0ff8e2-f1a8-4a42-835e-d4aaff7e7b8d",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER".ToUpper()
                }
             );


            _modelBuilder.Entity<ApplicationUser>().HasData(

             new ApplicationUser
             {
                 Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                 UserName = "admin@sif.com",
                 Email = "admin@sif.com",
                 EmailConfirmed = true,
                 NormalizedUserName = "ADMIN@SIF.COM",
                 PasswordHash = hasher.HashPassword(null, "Admin@1234")

             }
          );

            _modelBuilder.Entity<IdentityUserRole<string>>().HasData(
              new IdentityUserRole<string>
              {
                  RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                  UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
              }
             );
            _modelBuilder.Entity<Permission>().HasData(
               new Permission() { Id=1, Name = "Manage Customers" },
               new Permission() { Id = 2, Name = "Manage Loan" },
               new Permission() { Id = 3, Name = "Manage Fee" },
               new Permission() { Id = 4, Name = "Manage Support" },
               new Permission() { Id = 5, Name = "Manage Exchange" },
               new Permission() { Id = 6, Name = "Manage Employees" },
               new Permission() { Id = 7, Name = "Manage Cards" },
               new Permission() { Id = 8, Name = "Manage Dashboard" },
               new Permission() { Id = 9, Name = "Manage Branches" },
               new Permission() { Id = 10, Name = "Manage Settlements" },
               new Permission() { Id = 11, Name = "Manage Post Updates" }
            );

            _modelBuilder.Entity<AccountFeeSetting>().HasData(
             new AccountFeeSetting() { Id = 1, Key = "Spending_Accounts_InterestRate" },
             new AccountFeeSetting() { Id = 2, Key = "Spending_Accounts_TXFeeDebit" },
             new AccountFeeSetting() { Id = 3, Key = "Spending_Accounts_TXFeeCredit" },
             new AccountFeeSetting() { Id = 4, Key = "Savings_Accounts_InterestRate" },
             new AccountFeeSetting() { Id = 5, Key = "Savings_Accounts_TXFeeDebit" },
             new AccountFeeSetting() { Id = 6, Key = "Savings_Accounts_TXFeeCredit" },
             new AccountFeeSetting() { Id = 7, Key = "Loans_Accounts_InterestRate" },
             new AccountFeeSetting() { Id = 8, Key = "Loans_Accounts_TXFeeDebit" },
             new AccountFeeSetting() { Id = 9, Key = "Loans_Accounts_TXFeeCredit" }
            );

		}
    }
}
