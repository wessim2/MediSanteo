﻿// <auto-generated />
using System;
using MediSanteo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediSanteo.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediSanteo.Domain.Consultations.Consultation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("appointment_time");

                    b.Property<DateTime?>("ConfirmedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("confirmed_on_utc");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uuid")
                        .HasColumnName("doctor_id");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid")
                        .HasColumnName("patient_id");

                    b.Property<DateTime?>("RejectedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("rejected_on_utc");

                    b.Property<DateTime?>("RescheduledOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("rescheduled_on_utc");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_consultations");

                    b.HasIndex("DoctorId")
                        .HasDatabaseName("ix_consultations_doctor_id");

                    b.HasIndex("PatientId")
                        .HasDatabaseName("ix_consultations_patient_id");

                    b.ToTable("consultations", (string)null);
                });

            modelBuilder.Entity("MediSanteo.Domain.Doctors.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("speciality");

                    b.HasKey("Id")
                        .HasName("pk_doctor");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_doctor_email");

                    b.ToTable("doctor", (string)null);
                });

            modelBuilder.Entity("MediSanteo.Domain.Patients.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.HasKey("Id")
                        .HasName("pk_patient");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_patient_email");

                    b.ToTable("patient", (string)null);
                });

            modelBuilder.Entity("MediSanteo.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("identity_id");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("IdentityId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_identity_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MediSanteo.Domain.Consultations.Consultation", b =>
                {
                    b.HasOne("MediSanteo.Domain.Doctors.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_consultations_doctor_doctor_id");

                    b.HasOne("MediSanteo.Domain.Patients.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_consultations_patient_patient_id");

                    b.OwnsOne("MediSanteo.Domain.Consultations.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ConsultationId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("price_currency");

                            b1.HasKey("ConsultationId");

                            b1.ToTable("consultations");

                            b1.WithOwner()
                                .HasForeignKey("ConsultationId")
                                .HasConstraintName("fk_consultations_consultations_id");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("MediSanteo.Domain.Doctors.Doctor", b =>
                {
                    b.OwnsOne("MediSanteo.Domain.Doctors.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("DoctorId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_country");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address_street");

                            b1.HasKey("DoctorId");

                            b1.ToTable("doctor");

                            b1.WithOwner()
                                .HasForeignKey("DoctorId")
                                .HasConstraintName("fk_doctor_doctor_id");
                        });

                    b.OwnsOne("MediSanteo.Domain.Shared.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("DoctorId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_last_name");

                            b1.HasKey("DoctorId");

                            b1.ToTable("doctor");

                            b1.WithOwner()
                                .HasForeignKey("DoctorId")
                                .HasConstraintName("fk_doctor_doctor_id");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("FullName")
                        .IsRequired();
                });

            modelBuilder.Entity("MediSanteo.Domain.Patients.Patient", b =>
                {
                    b.OwnsMany("MediSanteo.Domain.Patients.Medication", "Medications", b1 =>
                        {
                            b1.Property<Guid>("PatientId")
                                .HasColumnType("uuid")
                                .HasColumnName("patient_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<decimal>("Dosage")
                                .HasColumnType("numeric")
                                .HasColumnName("dosage");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("end_date");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.Property<string>("Notes")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("notes");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("start_date");

                            b1.HasKey("PatientId", "Id")
                                .HasName("pk_medication");

                            b1.ToTable("medication", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PatientId")
                                .HasConstraintName("fk_medication_patient_patient_id");
                        });

                    b.OwnsOne("MediSanteo.Domain.Shared.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("PatientId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("full_name_last_name");

                            b1.HasKey("PatientId");

                            b1.ToTable("patient");

                            b1.WithOwner()
                                .HasForeignKey("PatientId")
                                .HasConstraintName("fk_patient_patient_id");
                        });

                    b.OwnsMany("MediSanteo.Domain.Patients.VitalSigns", "VitalSignHistory", b1 =>
                        {
                            b1.Property<Guid>("PatientId")
                                .HasColumnType("uuid")
                                .HasColumnName("patient_id");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<decimal>("BloodPressure")
                                .HasColumnType("numeric")
                                .HasColumnName("blood_pressure");

                            b1.Property<decimal>("HeartRate")
                                .HasColumnType("numeric")
                                .HasColumnName("heart_rate");

                            b1.Property<decimal>("Tempertature")
                                .HasColumnType("numeric")
                                .HasColumnName("tempertature");

                            b1.Property<DateTime>("Timestamp")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("timestamp");

                            b1.HasKey("PatientId", "Id")
                                .HasName("pk_vital_signs");

                            b1.ToTable("vital_signs", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PatientId")
                                .HasConstraintName("fk_vital_signs_patient_patient_id");
                        });

                    b.Navigation("FullName")
                        .IsRequired();

                    b.Navigation("Medications");

                    b.Navigation("VitalSignHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
