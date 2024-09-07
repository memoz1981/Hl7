﻿// <auto-generated />
using System;
using Hl7.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hl7.DAL.Migrations
{
    [DbContext(typeof(Hl7DbContext))]
    partial class Hl7DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Hl7.DAL.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Aetitle")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("AppointmentFile")
                        .HasColumnType("TEXT");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AppointmentType")
                        .HasColumnType("TEXT");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EquipmentName")
                        .HasColumnType("TEXT");

                    b.Property<string>("EstablishmentCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("EstablishmentName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileExtension")
                        .HasMaxLength(3)
                        .HasColumnType("TEXT");

                    b.Property<string>("MedicalRegistration")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModalityId")
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServiceName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("StudyCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ThirdPartyName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.MedicalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReportText")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReportURL")
                        .HasColumnType("TEXT");

                    b.Property<int>("SendMedicalRecordRequestId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServiceName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudyCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudyName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("SendMedicalRecordRequestId");

                    b.ToTable("MedicalRecord");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("DocumentType")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Locality")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaternalSurname")
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentSurname")
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .HasMaxLength(1)
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.HasKey("PatientId");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.SendAppointmentResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SuiMessage")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.ToTable("SendAppointmentResponse");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.SendMedicalRecordRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("MdmMessage")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SendMedicalRecordRequest");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.Doctor", b =>
                {
                    b.HasOne("Hl7.DAL.Entities.Appointment", "Appointment")
                        .WithOne("Doctor")
                        .HasForeignKey("Hl7.DAL.Entities.Doctor", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.MedicalRecord", b =>
                {
                    b.HasOne("Hl7.DAL.Entities.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("Hl7.DAL.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("Hl7.DAL.Entities.SendMedicalRecordRequest", "SendMedicalRecordRequest")
                        .WithMany()
                        .HasForeignKey("SendMedicalRecordRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("SendMedicalRecordRequest");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.Patient", b =>
                {
                    b.HasOne("Hl7.DAL.Entities.Appointment", "Appointment")
                        .WithOne("Patient")
                        .HasForeignKey("Hl7.DAL.Entities.Patient", "AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.SendAppointmentResponse", b =>
                {
                    b.HasOne("Hl7.DAL.Entities.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Hl7.DAL.Entities.Appointment", b =>
                {
                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
