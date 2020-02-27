using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RCServerCli.Models
{
    public partial class RokonoControlContext : DbContext
    {
        public RokonoControlContext()
        {
        }

        public RokonoControlContext(DbContextOptions<RokonoControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssociatedBoardWorkItems> AssociatedBoardWorkItems { get; set; }
        public virtual DbSet<AssociatedBranchCommits> AssociatedBranchCommits { get; set; }
        public virtual DbSet<AssociatedCommitFiles> AssociatedCommitFiles { get; set; }
        public virtual DbSet<AssociatedProjectBoards> AssociatedProjectBoards { get; set; }
        public virtual DbSet<AssociatedProjectBuilds> AssociatedProjectBuilds { get; set; }
        public virtual DbSet<AssociatedProjectIterations> AssociatedProjectIterations { get; set; }
        public virtual DbSet<AssociatedProjectMemberRights> AssociatedProjectMemberRights { get; set; }
        public virtual DbSet<AssociatedProjectMembers> AssociatedProjectMembers { get; set; }
        public virtual DbSet<AssociatedRepositoryBranches> AssociatedRepositoryBranches { get; set; }
        public virtual DbSet<AssociatedWrorkItemChildren> AssociatedWrorkItemChildren { get; set; }
        public virtual DbSet<Boards> Boards { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<Builds> Builds { get; set; }
        public virtual DbSet<Commits> Commits { get; set; }
        public virtual DbSet<Efforts> Efforts { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Repository> Repository { get; set; }
        public virtual DbSet<Risks> Risks { get; set; }
        public virtual DbSet<UserAccounts> UserAccounts { get; set; }
        public virtual DbSet<UserRights> UserRights { get; set; }
        public virtual DbSet<ValueAreas> ValueAreas { get; set; }
        public virtual DbSet<WorkItem> WorkItem { get; set; }
        public virtual DbSet<WorkItemActivity> WorkItemActivity { get; set; }
        public virtual DbSet<WorkItemAreas> WorkItemAreas { get; set; }
        public virtual DbSet<WorkItemIterations> WorkItemIterations { get; set; }
        public virtual DbSet<WorkItemPriorities> WorkItemPriorities { get; set; }
        public virtual DbSet<WorkItemRealtionshipType> WorkItemRealtionshipType { get; set; }
        public virtual DbSet<WorkItemReasons> WorkItemReasons { get; set; }
        public virtual DbSet<WorkItemRelations> WorkItemRelations { get; set; }
        public virtual DbSet<WorkItemSeverities> WorkItemSeverities { get; set; }
        public virtual DbSet<WorkItemStates> WorkItemStates { get; set; }
        public virtual DbSet<WorkItemTypes> WorkItemTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
             }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssociatedBoardWorkItems>(entity =>
            {
                entity.HasOne(d => d.Board)
                    .WithMany(p => p.AssociatedBoardWorkItems)
                    .HasForeignKey(d => d.BoardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__Board__619B8048");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssociatedBoardWorkItems)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Associate__Proje__628FA481");

                entity.HasOne(d => d.WorkItem)
                    .WithMany(p => p.AssociatedBoardWorkItems)
                    .HasForeignKey(d => d.WorkItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__WorkI__6383C8BA");
            });

            modelBuilder.Entity<AssociatedBranchCommits>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AssociatedBranchCommits)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__Branc__6477ECF3");

                entity.HasOne(d => d.Commit)
                    .WithMany(p => p.AssociatedBranchCommits)
                    .HasForeignKey(d => d.CommitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__Commi__656C112C");
            });

            modelBuilder.Entity<AssociatedCommitFiles>(entity =>
            {
                entity.Property(e => e.DateOfCommit).HasColumnType("datetime");

                entity.HasOne(d => d.Commit)
                    .WithMany(p => p.AssociatedCommitFiles)
                    .HasForeignKey(d => d.CommitId)
                    .HasConstraintName("FK__Associate__Commi__66603565");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.AssociatedCommitFiles)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK__Associate__FileI__6754599E");
            });

            modelBuilder.Entity<AssociatedProjectBoards>(entity =>
            {
                entity.HasOne(d => d.Board)
                    .WithMany(p => p.AssociatedProjectBoards)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("FK__Associate__Board__68487DD7");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssociatedProjectBoards)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Associate__Proje__693CA210");
            });

            modelBuilder.Entity<AssociatedProjectBuilds>(entity =>
            {
                entity.HasOne(d => d.Build)
                    .WithMany(p => p.AssociatedProjectBuilds)
                    .HasForeignKey(d => d.BuildId)
                    .HasConstraintName("FK__Associate__Build__6A30C649");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssociatedProjectBuilds)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Associate__Proje__6B24EA82");

                entity.HasOne(d => d.Repository)
                    .WithMany(p => p.AssociatedProjectBuilds)
                    .HasForeignKey(d => d.RepositoryId)
                    .HasConstraintName("FK__Associate__Repos__6C190EBB");
            });

            modelBuilder.Entity<AssociatedProjectIterations>(entity =>
            {
                entity.HasOne(d => d.Iteration)
                    .WithMany(p => p.AssociatedProjectIterations)
                    .HasForeignKey(d => d.IterationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedProjectIterations_WorkItemIterations");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssociatedProjectIterations)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedProjectIterations_Projects");
            });

            modelBuilder.Entity<AssociatedProjectMemberRights>(entity =>
            {
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssociatedProjectMemberRights)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_AssociatedProjectMemberRights_Projects");

                entity.HasOne(d => d.Rights)
                    .WithMany(p => p.AssociatedProjectMemberRights)
                    .HasForeignKey(d => d.RightsId)
                    .HasConstraintName("FK_AssociatedProjectMemberRights_UserRights");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.AssociatedProjectMemberRights)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK_AssociatedProjectMemberRights_UserAccounts");
            });

            modelBuilder.Entity<AssociatedProjectMembers>(entity =>
            {
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssociatedProjectMembers)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__Proje__71D1E811");

                entity.HasOne(d => d.Repository)
                    .WithMany(p => p.AssociatedProjectMembers)
                    .HasForeignKey(d => d.RepositoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__Repos__72C60C4A");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.AssociatedProjectMembers)
                    .HasForeignKey(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__UserA__73BA3083");
            });

            modelBuilder.Entity<AssociatedRepositoryBranches>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AssociatedRepositoryBranches)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__Branc__74AE54BC");

                entity.HasOne(d => d.Repository)
                    .WithMany(p => p.AssociatedRepositoryBranches)
                    .HasForeignKey(d => d.RepositoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__Repos__75A278F5");
            });

            modelBuilder.Entity<AssociatedWrorkItemChildren>(entity =>
            {
                entity.HasOne(d => d.RelationTypeNavigation)
                    .WithMany(p => p.AssociatedWrorkItemChildren)
                    .HasForeignKey(d => d.RelationType)
                    .HasConstraintName("FK__Associate__Relat__76969D2E");

                entity.HasOne(d => d.WorkItemChild)
                    .WithMany(p => p.AssociatedWrorkItemChildrenWorkItemChild)
                    .HasForeignKey(d => d.WorkItemChildId)
                    .HasConstraintName("FK__Associate__WorkI__787EE5A0");

                entity.HasOne(d => d.WorkItem)
                    .WithMany(p => p.AssociatedWrorkItemChildrenWorkItem)
                    .HasForeignKey(d => d.WorkItemId)
                    .HasConstraintName("FK__Associate__WorkI__778AC167");
            });

            modelBuilder.Entity<Boards>(entity =>
            {
                entity.Property(e => e.BoardName).HasMaxLength(1000);
            });

            modelBuilder.Entity<Branches>(entity =>
            {
                entity.Property(e => e.DateOfCreation).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Branches__Projec__797309D9");
            });

            modelBuilder.Entity<Builds>(entity =>
            {
                entity.Property(e => e.DateOfBuild).HasColumnType("datetime");

                entity.Property(e => e.ReasonName)
                    .IsRequired()
                    .HasMaxLength(600);
            });

            modelBuilder.Entity<Commits>(entity =>
            {
                entity.Property(e => e.CommitData).IsRequired();

                entity.Property(e => e.DateOfCommit).HasColumnType("datetime");
            });

            modelBuilder.Entity<Efforts>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EffortName).HasMaxLength(300);
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.DateOfFile).HasColumnType("datetime");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Repository)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.RepositoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projects__Reposi__7A672E12");
            });

            modelBuilder.Entity<Repository>(entity =>
            {
                entity.Property(e => e.FolderPath).IsRequired();
            });

            modelBuilder.Entity<Risks>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RiskName).HasMaxLength(300);
            });

            modelBuilder.Entity<UserAccounts>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(400);

                entity.Property(e => e.GitUsername).HasMaxLength(300);

                entity.Property(e => e.LastName).HasMaxLength(400);

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<ValueAreas>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ValueAreaName).HasMaxLength(300);
            });

            modelBuilder.Entity<WorkItem>(entity =>
            {
                entity.Property(e => e.Compleated).HasMaxLength(300);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ItemPriority).HasColumnName("itemPriority");

                entity.Property(e => e.OriginEstitame).HasMaxLength(300);

                entity.Property(e => e.Remaining).HasMaxLength(300);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StoryPoints).HasMaxLength(400);

                entity.HasOne(d => d.ActivityNavigation)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.Activity)
                    .HasConstraintName("FK__WorkItem__Activi__7B5B524B");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK__WorkItem__AreaId__7C4F7684");

                entity.HasOne(d => d.AssignedAccountNavigation)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.AssignedAccount)
                    .HasConstraintName("FK__WorkItem__Assign__7D439ABD");

                entity.HasOne(d => d.IterationNavigation)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.Iteration)
                    .HasConstraintName("FK__WorkItem__Iterat__7E37BEF6");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK__WorkItem__Priori__7F2BE32F");

                entity.HasOne(d => d.ReasonNavigation)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.ReasonId)
                    .HasConstraintName("FK__WorkItem__Reason__00200768");

                entity.HasOne(d => d.Relation)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.RelationId)
                    .HasConstraintName("FK__WorkItem__Relati__01142BA1");

                entity.HasOne(d => d.Risk)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.RiskId)
                    .HasConstraintName("FK__WorkItem__RiskId__02084FDA");

                entity.HasOne(d => d.SeverityNavigation)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.Severity)
                    .HasConstraintName("FK__WorkItem__Severi__02FC7413");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__WorkItem__StateI__03F0984C");

                entity.HasOne(d => d.ValueArea)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.ValueAreaId)
                    .HasConstraintName("FK__WorkItem__ValueA__04E4BC85");

                entity.HasOne(d => d.WorkItemType)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.WorkItemTypeId)
                    .HasConstraintName("FK__WorkItem__WorkIt__05D8E0BE");
            });

            modelBuilder.Entity<WorkItemActivity>(entity =>
            {
                entity.Property(e => e.ActivityName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<WorkItemAreas>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<WorkItemIterations>(entity =>
            {
                entity.Property(e => e.IterationName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<WorkItemPriorities>(entity =>
            {
                entity.Property(e => e.PriorityName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<WorkItemRealtionshipType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<WorkItemReasons>(entity =>
            {
                entity.Property(e => e.ReasonName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<WorkItemRelations>(entity =>
            {
                entity.Property(e => e.RelationName).HasMaxLength(300);
            });

            modelBuilder.Entity<WorkItemSeverities>(entity =>
            {
                entity.Property(e => e.SeverityName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<WorkItemStates>(entity =>
            {
                entity.Property(e => e.StateName).HasMaxLength(300);
            });

            modelBuilder.Entity<WorkItemTypes>(entity =>
            {
                entity.Property(e => e.Icon).HasMaxLength(600);

                entity.Property(e => e.TypeName).HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
