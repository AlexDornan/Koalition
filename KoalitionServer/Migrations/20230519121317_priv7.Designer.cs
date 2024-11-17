﻿// <auto-generated />
using System;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230519121317_priv7")]
    partial class priv7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KoalitionServer.Models.GroupChat", b =>
                {
                    b.Property<int>("GroupChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupChatId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupChatId");

                    b.ToTable("GroupChats");
                });

            modelBuilder.Entity("KoalitionServer.Models.GroupChatsToUsers", b =>
                {
                    b.Property<int>("GroupChatId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("bit");

                    b.HasKey("GroupChatId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupChatsToUsers");
                });

            modelBuilder.Entity("KoalitionServer.Models.GroupMessage", b =>
                {
                    b.Property<int>("GroupMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupMessageId"), 1L, 1);

                    b.Property<int>("GroupChatId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GroupMessageId");

                    b.HasIndex("GroupChatId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMessages");
                });

            modelBuilder.Entity("KoalitionServer.Models.PrivateChat", b =>
                {
                    b.Property<int>("PrivateChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrivateChatId"), 1L, 1);

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PrivateChatId");

                    b.HasIndex("UserId");

                    b.ToTable("PrivateChats");
                });

            modelBuilder.Entity("KoalitionServer.Models.PrivateChatsToUsers", b =>
                {
                    b.Property<int>("PrivateChatId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PrivateChatId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PrivateChatsToUsers");
                });

            modelBuilder.Entity("KoalitionServer.Models.PrivateMessage", b =>
                {
                    b.Property<int>("PrivateMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrivateMessageId"), 1L, 1);

                    b.Property<int>("PrivateChatId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PrivateMessageId");

                    b.HasIndex("PrivateChatId");

                    b.HasIndex("UserId");

                    b.ToTable("PrivateMessages");
                });

            modelBuilder.Entity("KoalitionServer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastRescent")
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Online")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KoalitionServer.Models.GroupChatsToUsers", b =>
                {
                    b.HasOne("KoalitionServer.Models.GroupChat", "GroupChat")
                        .WithMany("GroupChatsToUsers")
                        .HasForeignKey("GroupChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KoalitionServer.Models.User", "User")
                        .WithMany("GroupChatsToUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupChat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KoalitionServer.Models.GroupMessage", b =>
                {
                    b.HasOne("KoalitionServer.Models.GroupChat", null)
                        .WithMany("Messages")
                        .HasForeignKey("GroupChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KoalitionServer.Models.User", null)
                        .WithMany("GroupMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KoalitionServer.Models.PrivateChat", b =>
                {
                    b.HasOne("KoalitionServer.Models.User", null)
                        .WithMany("PrivateChats")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("KoalitionServer.Models.PrivateChatsToUsers", b =>
                {
                    b.HasOne("KoalitionServer.Models.PrivateChat", "PrivateChat")
                        .WithMany("PrivateChatsToUsers")
                        .HasForeignKey("PrivateChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KoalitionServer.Models.User", "User")
                        .WithMany("PrivateChatsToUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrivateChat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KoalitionServer.Models.PrivateMessage", b =>
                {
                    b.HasOne("KoalitionServer.Models.PrivateChat", "PrivateChat")
                        .WithMany("Messages")
                        .HasForeignKey("PrivateChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KoalitionServer.Models.User", null)
                        .WithMany("PrivateMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrivateChat");
                });

            modelBuilder.Entity("KoalitionServer.Models.GroupChat", b =>
                {
                    b.Navigation("GroupChatsToUsers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("KoalitionServer.Models.PrivateChat", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("PrivateChatsToUsers");
                });

            modelBuilder.Entity("KoalitionServer.Models.User", b =>
                {
                    b.Navigation("GroupChatsToUsers");

                    b.Navigation("GroupMessages");

                    b.Navigation("PrivateChats");

                    b.Navigation("PrivateChatsToUsers");

                    b.Navigation("PrivateMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
