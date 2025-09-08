using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.Model;
using SiwanDoctorAPI.Model.EntityModel.AdminEntity;
using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using SiwanDoctorAPI.Model.EntityModel.PatientPrescriptionEntity.cs;
using SiwanDoctorAPI.Model.EntityModel.Payment_Entity;
using SiwanDoctorAPI.Model.EntityModel.SettingEntity;
using System.Reflection.Emit;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.DbConnection
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public class ApplicationUser : IdentityUser<int>
        {
            // Add your custom properties
            public UserType UserType { get; set; }
            public string? FirstName { get; set; } // f_name
            public string? LastName { get; set; } // l_name
            public string? ISDCode { get; set; } // isd_code
            public string? Gender { get; set; } // gender
            public DateTime DateOfBirth { get; set; } // dob
            public string? RegistrationNo { get; set; }
        }

        public DbSet<Doctor_Details> Doctor_Details { get; set; }
        public DbSet<Patient_Details> Patients_Details { get; set; }
        public DbSet<Department> Doctor_Departments { get; set; }
        public DbSet<Specialization> Doctor_Specializations { get; set; }
        public DbSet<DoctorTimeSlot> Doctor_TimeSlots { get; set; }
        public DbSet<UserFamilyMember> User_FamilyMembers { get; set; }
        public DbSet<FamilyMemberVitals> familyMemberVitals { get; set; }
        public DbSet<DoctorReview> doctorReviews { get; set; }
        public virtual DbSet<Appointment> appointments { get; set; }
        public DbSet<VideoDoctorTimeSlot> videoDoctorTimeSlots { get; set; }
        public DbSet<SocialMedia> socialMedias { get; set; }
        public DbSet<WebPage> webPages { get; set; }
        public DbSet<DoctorPrescribeMdicines> doctorPrescribeMdicines { get; set; }
        public DbSet<PatientPrescription> patientPrescriptions { get; set; }

        public DbSet<PatientMedicine> patientMedicines { get; set; }

        public DbSet<Coupon> coupons { get; set; }
        public DbSet<configurations> configurations { get; set; }
        public DbSet<PaymentDetails> paymentDetails {  get; set; }
        public DbSet<GetTestimonal> GetTestimonal { get; set; }
        public DbSet<GetSocialMedia> GetSocialMedia { get; set; }
        public enum UserType
        {
            Doctor = 1,
            Patient = 2,
            Staff = 3
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
            // Configure Identity tables to use int keys
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Auto-increment
            });

            builder.Entity<IdentityRole<int>>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
            });
            builder.Entity<Doctor_Details>()
        .Property(d => d.OpdFee)
        .HasColumnType("decimal(18,2)");

            builder.Entity<Doctor_Details>()
                .Property(d => d.VideoFee)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Doctor_Details>()
                .Property(d => d.EmergencyFee)
                .HasColumnType("decimal(18,2)");
            // Add any other model configurations here
            builder.Entity<GetTestimonal>().ToTable("get_testimonal");
            builder.Entity<GetSocialMedia>().ToTable("get_social_media");

        }

    }
}
