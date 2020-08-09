using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbScaffold.Models
{
    public partial class WWContext : DbContext
    {
        public WWContext()
        {
        }

        public WWContext(DbContextOptions<WWContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abilities> Abilities { get; set; }
        public virtual DbSet<AbilityAfterEffect> AbilityAfterEffect { get; set; }
        public virtual DbSet<AbilityCombos> AbilityCombos { get; set; }
        public virtual DbSet<ActiveMovementSpeeds> ActiveMovementSpeeds { get; set; }
        public virtual DbSet<ActuveRotationSpeed> ActuveRotationSpeed { get; set; }
        public virtual DbSet<AssociatedAbilityCombos> AssociatedAbilityCombos { get; set; }
        public virtual DbSet<AssociatedAccountCharacters> AssociatedAccountCharacters { get; set; }
        public virtual DbSet<AssociatedAreaBuildings> AssociatedAreaBuildings { get; set; }
        public virtual DbSet<AssociatedBlogPosts> AssociatedBlogPosts { get; set; }
        public virtual DbSet<AssociatedBookPages> AssociatedBookPages { get; set; }
        public virtual DbSet<AssociatedBuildingFunctionality> AssociatedBuildingFunctionality { get; set; }
        public virtual DbSet<AssociatedCcpermutations> AssociatedCcpermutations { get; set; }
        public virtual DbSet<AssociatedCharacterAbilities> AssociatedCharacterAbilities { get; set; }
        public virtual DbSet<AssociatedCharacterBooks> AssociatedCharacterBooks { get; set; }
        public virtual DbSet<AssociatedCharacterCompletedQuests> AssociatedCharacterCompletedQuests { get; set; }
        public virtual DbSet<AssociatedCharacterMounts> AssociatedCharacterMounts { get; set; }
        public virtual DbSet<AssociatedCharacterQuestStates> AssociatedCharacterQuestStates { get; set; }
        public virtual DbSet<AssociatedCharacterQuests> AssociatedCharacterQuests { get; set; }
        public virtual DbSet<AssociatedCharacterTraits> AssociatedCharacterTraits { get; set; }
        public virtual DbSet<AssociatedDialogOptionNcc> AssociatedDialogOptionNcc { get; set; }
        public virtual DbSet<AssociatedEquipmentItemWeapons> AssociatedEquipmentItemWeapons { get; set; }
        public virtual DbSet<AssociatedEquippedCharacterItems> AssociatedEquippedCharacterItems { get; set; }
        public virtual DbSet<AssociatedInventoryItems> AssociatedInventoryItems { get; set; }
        public virtual DbSet<AssociatedItemSubCategories> AssociatedItemSubCategories { get; set; }
        public virtual DbSet<AssociatedMobs> AssociatedMobs { get; set; }
        public virtual DbSet<AssociatedNpcConversations> AssociatedNpcConversations { get; set; }
        public virtual DbSet<AssociatedNpcMovingPoints> AssociatedNpcMovingPoints { get; set; }
        public virtual DbSet<AssociatedPotionIngridients> AssociatedPotionIngridients { get; set; }
        public virtual DbSet<AssociatedProductsForSale> AssociatedProductsForSale { get; set; }
        public virtual DbSet<AssociatedQuestBookReadings> AssociatedQuestBookReadings { get; set; }
        public virtual DbSet<AssociatedQuestCollectable> AssociatedQuestCollectable { get; set; }
        public virtual DbSet<AssociatedQuestQuestions> AssociatedQuestQuestions { get; set; }
        public virtual DbSet<AssociatedQuestRewards> AssociatedQuestRewards { get; set; }
        public virtual DbSet<AssociatedSaleProducts> AssociatedSaleProducts { get; set; }
        public virtual DbSet<AssociatedTradedBussinessItems> AssociatedTradedBussinessItems { get; set; }
        public virtual DbSet<AssociatedWeaponPart> AssociatedWeaponPart { get; set; }
        public virtual DbSet<AssociatedZoneEntrancePoints> AssociatedZoneEntrancePoints { get; set; }
        public virtual DbSet<AssociatedZoneQuests> AssociatedZoneQuests { get; set; }
        public virtual DbSet<BlogCategories> BlogCategories { get; set; }
        public virtual DbSet<BlogPost> BlogPost { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<BookReadingQuest> BookReadingQuest { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<BuildingTypes> BuildingTypes { get; set; }
        public virtual DbSet<BusienssTypes> BusienssTypes { get; set; }
        public virtual DbSet<BusinessItems> BusinessItems { get; set; }
        public virtual DbSet<CharacterGearItems> CharacterGearItems { get; set; }
        public virtual DbSet<CharacterRaces> CharacterRaces { get; set; }
        public virtual DbSet<CharacterRecepies> CharacterRecepies { get; set; }
        public virtual DbSet<Characters> Characters { get; set; }
        public virtual DbSet<CombotEffectTypes> CombotEffectTypes { get; set; }
        public virtual DbSet<DialogOptions> DialogOptions { get; set; }
        public virtual DbSet<EquipmentItem> EquipmentItem { get; set; }
        public virtual DbSet<EyeColors> EyeColors { get; set; }
        public virtual DbSet<FeaturedModels> FeaturedModels { get; set; }
        public virtual DbSet<GearEffects> GearEffects { get; set; }
        public virtual DbSet<GearTypes> GearTypes { get; set; }
        public virtual DbSet<InventoryItems> InventoryItems { get; set; }
        public virtual DbSet<ItemCategories> ItemCategories { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<LandingPage> LandingPage { get; set; }
        public virtual DbSet<Mobs> Mobs { get; set; }
        public virtual DbSet<Mounts> Mounts { get; set; }
        public virtual DbSet<NpcBaseAbilities> NpcBaseAbilities { get; set; }
        public virtual DbSet<NpcConversations> NpcConversations { get; set; }
        public virtual DbSet<NpcMovingPoints> NpcMovingPoints { get; set; }
        public virtual DbSet<Npcs> Npcs { get; set; }
        public virtual DbSet<OwableBuildings> OwableBuildings { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<PotionIngridients> PotionIngridients { get; set; }
        public virtual DbSet<PotionRecepie> PotionRecepie { get; set; }
        public virtual DbSet<QuestAreaSizes> QuestAreaSizes { get; set; }
        public virtual DbSet<QuestItems> QuestItems { get; set; }
        public virtual DbSet<QuestQuestistions> QuestQuestistions { get; set; }
        public virtual DbSet<QuestState> QuestState { get; set; }
        public virtual DbSet<QuestTypes> QuestTypes { get; set; }
        public virtual DbSet<Quests> Quests { get; set; }
        public virtual DbSet<Rewards> Rewards { get; set; }
        public virtual DbSet<Slots> Slots { get; set; }
        public virtual DbSet<Stories> Stories { get; set; }
        public virtual DbSet<TradingCenters> TradingCenters { get; set; }
        public virtual DbSet<Traits> Traits { get; set; }
        public virtual DbSet<UnAllocatedPledgeSupport> UnAllocatedPledgeSupport { get; set; }
        public virtual DbSet<UnassignedPledges> UnassignedPledges { get; set; }
        public virtual DbSet<UserAccounts> UserAccounts { get; set; }
        public virtual DbSet<WeaponPart> WeaponPart { get; set; }
        public virtual DbSet<Weapons> Weapons { get; set; }
        public virtual DbSet<ZoneEntrances> ZoneEntrances { get; set; }
        public virtual DbSet<Zones> Zones { get; set; }

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abilities>(entity =>
            {
                entity.Property(e => e.HitTime).HasColumnName("HitTIme");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AbilityTypeNavigation)
                    .WithMany(p => p.Abilities)
                    .HasForeignKey(d => d.AbilityType)
                    .HasConstraintName("FK_Abilities_CombotEffectTypes");

                entity.HasOne(d => d.Effect)
                    .WithMany(p => p.Abilities)
                    .HasForeignKey(d => d.EffectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abilities_AbilityAfterEffect");
            });

            modelBuilder.Entity<AbilityAfterEffect>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<AbilityCombos>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.AbilityCombos)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AbilityCombos_CombotEffectTypes");
            });

            modelBuilder.Entity<ActiveMovementSpeeds>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<AssociatedAbilityCombos>(entity =>
            {
                entity.HasOne(d => d.Ability)
                    .WithMany(p => p.AssociatedAbilityCombos)
                    .HasForeignKey(d => d.AbilityId)
                    .HasConstraintName("FK_AssociatedAbilityCombos_Abilities");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.AssociatedAbilityCombos)
                    .HasForeignKey(d => d.ComboId)
                    .HasConstraintName("FK_AssociatedAbilityCombos_AbilityCombos");
            });

            modelBuilder.Entity<AssociatedAccountCharacters>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AssociatedAccountCharacters)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_AssociatedAccountCharacters_UserAccounts");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedAccountCharacters)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK_AssociatedAccountCharacters_Characters");
            });

            modelBuilder.Entity<AssociatedAreaBuildings>(entity =>
            {
                entity.HasOne(d => d.Area)
                    .WithMany(p => p.AssociatedAreaBuildings)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Associate__AreaI__477199F1");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.AssociatedAreaBuildings)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedAreaBuildings_OwableBuildings");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.AssociatedAreaBuildings)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_AssociatedAreaBuildings_Characters");
            });

            modelBuilder.Entity<AssociatedBlogPosts>(entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.AssociatedBlogPosts)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__Associate__BlogI__0D44F85C");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AssociatedBlogPosts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Associate__Categ__1209AD79");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.AssociatedBlogPosts)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Associate__PostI__0F2D40CE");
            });

            modelBuilder.Entity<AssociatedBookPages>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AssociatedBookPages)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_AssociatedBookPages_Books");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.AssociatedBookPages)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("FK_AssociatedBookPages_Pages");
            });

            modelBuilder.Entity<AssociatedBuildingFunctionality>(entity =>
            {
                entity.HasOne(d => d.BuildingFunctionalityNavigation)
                    .WithMany(p => p.AssociatedBuildingFunctionality)
                    .HasForeignKey(d => d.BuildingFunctionality)
                    .HasConstraintName("FK_AssociatedBuildingFunctionality_BuildingTypes");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.AssociatedBuildingFunctionality)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_AssociatedBuildingFunctionality_OwableBuildings");

                entity.HasOne(d => d.BusinessTypeNavigation)
                    .WithMany(p => p.AssociatedBuildingFunctionality)
                    .HasForeignKey(d => d.BusinessType)
                    .HasConstraintName("FK_AssociatedBuildingFunctionality_BusienssTypes");
            });

            modelBuilder.Entity<AssociatedCcpermutations>(entity =>
            {
                entity.ToTable("AssociatedCCPermutations");

                entity.HasOne(d => d.AssociatedNpcConversation)
                    .WithMany(p => p.AssociatedCcpermutations)
                    .HasForeignKey(d => d.AssociatedNpcConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCCPermutations_AssociatedNpcConversations");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCcpermutations)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCCPermutations_Characters");

                entity.HasOne(d => d.LastConversation)
                    .WithMany(p => p.AssociatedCcpermutations)
                    .HasForeignKey(d => d.LastConversationId)
                    .HasConstraintName("FK_AssociatedCCPermutations_NpcConversations");
            });

            modelBuilder.Entity<AssociatedCharacterAbilities>(entity =>
            {
                entity.Property(e => e.BoundButton).HasMaxLength(40);

                entity.HasOne(d => d.Ability)
                    .WithMany(p => p.AssociatedCharacterAbilities)
                    .HasForeignKey(d => d.AbilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterAbilities_Abilities");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCharacterAbilities)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterAbilities_Characters");
            });

            modelBuilder.Entity<AssociatedCharacterBooks>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AssociatedCharacterBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_AssociatedCharacterBooks_Books");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCharacterBooks)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK_AssociatedCharacterBooks_Characters");
            });

            modelBuilder.Entity<AssociatedCharacterCompletedQuests>(entity =>
            {
                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCharacterCompletedQuests)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK_AssociatedCharacterCompletedQuests_Characters");

                entity.HasOne(d => d.CharacterNavigation)
                    .WithMany(p => p.AssociatedCharacterCompletedQuests)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK_AssociatedCharacterCompletedQuests_Quests");
            });

            modelBuilder.Entity<AssociatedCharacterMounts>(entity =>
            {
                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCharacterMounts)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterMounts_Characters");

                entity.HasOne(d => d.Mount)
                    .WithMany(p => p.AssociatedCharacterMounts)
                    .HasForeignKey(d => d.MountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterMounts_Mounts");
            });

            modelBuilder.Entity<AssociatedCharacterQuestStates>(entity =>
            {
                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCharacterQuestStates)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK__Associate__Chara__382F5661");

                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedCharacterQuestStates)
                    .HasForeignKey(d => d.QuestId)
                    .HasConstraintName("FK__Associate__Quest__39237A9A");

                entity.HasOne(d => d.QuestState)
                    .WithMany(p => p.AssociatedCharacterQuestStates)
                    .HasForeignKey(d => d.QuestStateId)
                    .HasConstraintName("FK__Associate__Quest__373B3228");
            });

            modelBuilder.Entity<AssociatedCharacterQuests>(entity =>
            {
                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCharacterQuests)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterQuests_Characters");

                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedCharacterQuests)
                    .HasForeignKey(d => d.QuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterQuests_Quests");
            });

            modelBuilder.Entity<AssociatedCharacterTraits>(entity =>
            {
                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedCharacterTraits)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterTraits_Characters");

                entity.HasOne(d => d.Traid)
                    .WithMany(p => p.AssociatedCharacterTraits)
                    .HasForeignKey(d => d.TraidId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedCharacterTraits_Traits");
            });

            modelBuilder.Entity<AssociatedDialogOptionNcc>(entity =>
            {
                entity.ToTable("AssociatedDialogOptionNCC");

                entity.HasOne(d => d.DiloagOption)
                    .WithMany(p => p.AssociatedDialogOptionNcc)
                    .HasForeignKey(d => d.DiloagOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedDialogOptionNCC_DialogOptions");

                entity.HasOne(d => d.NpcConversation)
                    .WithMany(p => p.AssociatedDialogOptionNcc)
                    .HasForeignKey(d => d.NpcConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedDialogOptionNCC_NpcConversations");

                entity.HasOne(d => d.StoryLine)
                    .WithMany(p => p.AssociatedDialogOptionNcc)
                    .HasForeignKey(d => d.StoryLineId)
                    .HasConstraintName("FK_AssociatedDialogOptionNCC_Stories");
            });

            modelBuilder.Entity<AssociatedEquipmentItemWeapons>(entity =>
            {
                entity.HasOne(d => d.EquipmentItem)
                    .WithMany(p => p.AssociatedEquipmentItemWeapons)
                    .HasForeignKey(d => d.EquipmentItemId)
                    .HasConstraintName("FK_AssociatedEquipmentItemWeapons_EquipmentItem");

                entity.HasOne(d => d.Weapon)
                    .WithMany(p => p.AssociatedEquipmentItemWeapons)
                    .HasForeignKey(d => d.WeaponId)
                    .HasConstraintName("FK_AssociatedEquipmentItemWeapons_Weapons");
            });

            modelBuilder.Entity<AssociatedEquippedCharacterItems>(entity =>
            {
                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedEquippedCharacterItems)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK_AssociatedEquippedCharacterItems_Characters");

                entity.HasOne(d => d.GearItem)
                    .WithMany(p => p.AssociatedEquippedCharacterItems)
                    .HasForeignKey(d => d.GearItemId)
                    .HasConstraintName("FK_AssociatedEquippedCharacterItems_CharacterGearItems");
            });

            modelBuilder.Entity<AssociatedInventoryItems>(entity =>
            {
                entity.HasOne(d => d.Character)
                    .WithMany(p => p.AssociatedInventoryItems)
                    .HasForeignKey(d => d.CharacterId)
                    .HasConstraintName("FK_AssociatedInventoryItems_Characters");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.AssociatedInventoryItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_AssociatedInventoryItems_InventoryItems");
            });

            modelBuilder.Entity<AssociatedItemSubCategories>(entity =>
            {
                entity.HasOne(d => d.Main)
                    .WithMany(p => p.AssociatedItemSubCategoriesMain)
                    .HasForeignKey(d => d.MainId)
                    .HasConstraintName("FK_AssociatedItemSubCategories_ItemCategories");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.AssociatedItemSubCategoriesSubCategory)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK_AssociatedItemSubCategories_ItemCategories1");
            });

            modelBuilder.Entity<AssociatedMobs>(entity =>
            {
                entity.HasOne(d => d.Mob)
                    .WithMany(p => p.AssociatedMobs)
                    .HasForeignKey(d => d.MobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedMobs_Mobs");

                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedMobs)
                    .HasForeignKey(d => d.QuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedMobs_Quests");
            });

            modelBuilder.Entity<AssociatedNpcConversations>(entity =>
            {
                entity.HasOne(d => d.NpcConversation)
                    .WithMany(p => p.AssociatedNpcConversations)
                    .HasForeignKey(d => d.NpcConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedNpcConversations_NpcConversations");

                entity.HasOne(d => d.Npc)
                    .WithMany(p => p.AssociatedNpcConversations)
                    .HasForeignKey(d => d.NpcId)
                    .HasConstraintName("FK_AssociatedNpcConversations_Npcs");

                entity.HasOne(d => d.StoryLine)
                    .WithMany(p => p.AssociatedNpcConversations)
                    .HasForeignKey(d => d.StoryLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedNpcConversations_Stories");
            });

            modelBuilder.Entity<AssociatedNpcMovingPoints>(entity =>
            {
                entity.HasOne(d => d.Npc)
                    .WithMany(p => p.AssociatedNpcMovingPoints)
                    .HasForeignKey(d => d.NpcId)
                    .HasConstraintName("FK_AssociatedNpcMovingPoints_Npcs");

                entity.HasOne(d => d.Point)
                    .WithMany(p => p.AssociatedNpcMovingPoints)
                    .HasForeignKey(d => d.PointId)
                    .HasConstraintName("FK_AssociatedNpcMovingPoints_NpcMovingPoints");
            });

            modelBuilder.Entity<AssociatedPotionIngridients>(entity =>
            {
                entity.HasOne(d => d.Ingridient)
                    .WithMany(p => p.AssociatedPotionIngridients)
                    .HasForeignKey(d => d.IngridientId)
                    .HasConstraintName("FK__Associate__Ingri__14B10FFA");

                entity.HasOne(d => d.PotionRecepie)
                    .WithMany(p => p.AssociatedPotionIngridients)
                    .HasForeignKey(d => d.PotionRecepieId)
                    .HasConstraintName("FK__Associate__Potio__13BCEBC1");
            });

            modelBuilder.Entity<AssociatedProductsForSale>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.AssociatedProductsForSale)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK_AssociatedProductsForSale_OwableBuildings");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.AssociatedProductsForSale)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedProductsForSale_BusinessItems");
            });

            modelBuilder.Entity<AssociatedQuestBookReadings>(entity =>
            {
                entity.HasOne(d => d.BookReading)
                    .WithMany(p => p.AssociatedQuestBookReadings)
                    .HasForeignKey(d => d.BookReadingId)
                    .HasConstraintName("FK__Associate__BookR__1E6F845E");

                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedQuestBookReadings)
                    .HasForeignKey(d => d.QuestId)
                    .HasConstraintName("FK__Associate__Quest__1D7B6025");
            });

            modelBuilder.Entity<AssociatedQuestCollectable>(entity =>
            {
                entity.HasOne(d => d.AreaSizeNavigation)
                    .WithMany(p => p.AssociatedQuestCollectable)
                    .HasForeignKey(d => d.AreaSize)
                    .HasConstraintName("FK_AssociatedQuestCollectable_QuestAreaSizes");

                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedQuestCollectable)
                    .HasForeignKey(d => d.QuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedQuestCollectable_Quests");

                entity.HasOne(d => d.QuestItem)
                    .WithMany(p => p.AssociatedQuestCollectable)
                    .HasForeignKey(d => d.QuestItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedQuestCollectable_QuestItems");
            });

            modelBuilder.Entity<AssociatedQuestQuestions>(entity =>
            {
                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedQuestQuestions)
                    .HasForeignKey(d => d.QuestId)
                    .HasConstraintName("FK__Associate__Quest__308E3499");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssociatedQuestQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Associate__Quest__32767D0B");
            });

            modelBuilder.Entity<AssociatedQuestRewards>(entity =>
            {
                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedQuestRewards)
                    .HasForeignKey(d => d.QuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedQuestRewards_Quests");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.AssociatedQuestRewards)
                    .HasForeignKey(d => d.RewardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedQuestRewards_Rewards");
            });

            modelBuilder.Entity<AssociatedSaleProducts>(entity =>
            {
                entity.HasOne(d => d.Building)
                    .WithMany(p => p.AssociatedSaleProducts)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedSaleProducts_OwableBuildings");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.AssociatedSaleProducts)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedSaleProducts_BusinessItems");
            });

            modelBuilder.Entity<AssociatedTradedBussinessItems>(entity =>
            {
                entity.HasOne(d => d.BusinessItem)
                    .WithMany(p => p.AssociatedTradedBussinessItems)
                    .HasForeignKey(d => d.BusinessItemId)
                    .HasConstraintName("FK_AssociatedTradedBussinessItems_BusinessItems");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AssociatedTradedBussinessItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_AssociatedTradedBussinessItems_ItemCategories");

                entity.HasOne(d => d.TradingCenter)
                    .WithMany(p => p.AssociatedTradedBussinessItems)
                    .HasForeignKey(d => d.TradingCenterId)
                    .HasConstraintName("FK_AssociatedTradedBussinessItems_TradingCenters");
            });

            modelBuilder.Entity<AssociatedWeaponPart>(entity =>
            {
                entity.HasOne(d => d.Part)
                    .WithMany(p => p.AssociatedWeaponPart)
                    .HasForeignKey(d => d.PartId)
                    .HasConstraintName("FK_AssociatedWeaponPart_WeaponPart");

                entity.HasOne(d => d.Weapon)
                    .WithMany(p => p.AssociatedWeaponPart)
                    .HasForeignKey(d => d.WeaponId)
                    .HasConstraintName("FK_AssociatedWeaponPart_Weapons");
            });

            modelBuilder.Entity<AssociatedZoneEntrancePoints>(entity =>
            {
                entity.HasOne(d => d.Point)
                    .WithMany(p => p.AssociatedZoneEntrancePoints)
                    .HasForeignKey(d => d.PointId)
                    .HasConstraintName("FK__Associate__Point__4589517F");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.AssociatedZoneEntrancePoints)
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK__Associate__ZoneI__44952D46");
            });

            modelBuilder.Entity<AssociatedZoneQuests>(entity =>
            {
                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.AssociatedZoneQuests)
                    .HasForeignKey(d => d.QuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedZoneQuests_Quests");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.AssociatedZoneQuests)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatedZoneQuests_Zones");
            });

            modelBuilder.Entity<BlogCategories>(entity =>
            {
                entity.Property(e => e.CategoryName).IsRequired();
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.Property(e => e.DateOfPost).HasColumnType("datetime");

                entity.Property(e => e.Heading).IsRequired();
            });

            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.Property(e => e.BannerHeading).IsRequired();

                entity.Property(e => e.Heading).IsRequired();
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.Property(e => e.ModelPath).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BuildingTypes>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<BusienssTypes>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<BusinessItems>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.WeaponPart)
                    .WithMany(p => p.BusinessItems)
                    .HasForeignKey(d => d.WeaponPartId)
                    .HasConstraintName("FK__BusinessI__Weapo__4E1E9780");
            });

            modelBuilder.Entity<CharacterGearItems>(entity =>
            {
                entity.HasOne(d => d.Effect)
                    .WithMany(p => p.CharacterGearItems)
                    .HasForeignKey(d => d.EffectId)
                    .HasConstraintName("FK_CharacterGearItems_GearEffects");

                entity.HasOne(d => d.SlotNavigation)
                    .WithMany(p => p.CharacterGearItems)
                    .HasForeignKey(d => d.Slot)
                    .HasConstraintName("FK_CharacterGearItems_Slots");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.CharacterGearItems)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CharacterGearItems_GearTypes");
            });

            modelBuilder.Entity<CharacterRaces>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CharacterRecepies>(entity =>
            {
                entity.Property(e => e.Adult).IsRequired();

                entity.Property(e => e.Grown).IsRequired();

                entity.Property(e => e.Teenager).IsRequired();
            });

            modelBuilder.Entity<Characters>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CharacterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ActiveMovementSpeedNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.ActiveMovementSpeed)
                    .HasConstraintName("FK_Characters_ActiveMovementSpeeds");

                entity.HasOne(d => d.ActiveRecepieNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.ActiveRecepie)
                    .HasConstraintName("FK__Character__Activ__214BF109");

                entity.HasOne(d => d.ActiveRotationSpeedNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.ActiveRotationSpeed)
                    .HasConstraintName("FK_Characters_ActuveRotationSpeed");

                entity.HasOne(d => d.CharacterRaceNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.CharacterRace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Characters_CharacterRaces");

                entity.HasOne(d => d.EquipedWeapon)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.EquipedWeaponId)
                    .HasConstraintName("FK_Characters_Weapons");

                entity.HasOne(d => d.EyeColorNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.EyeColor)
                    .HasConstraintName("FK_Characters_EyeColors");
            });

            modelBuilder.Entity<CombotEffectTypes>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<DialogOptions>(entity =>
            {
                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.DialogOptions)
                    .HasForeignKey(d => d.QuestId)
                    .HasConstraintName("FK_DialogOptions_Quests");
            });

            modelBuilder.Entity<EquipmentItem>(entity =>
            {
                entity.Property(e => e.SlotName).HasMaxLength(300);
            });

            modelBuilder.Entity<GearEffects>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<GearTypes>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<InventoryItems>(entity =>
            {
                entity.Property(e => e.InGameModel).IsRequired();

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.EquipmentItemNavigation)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.EquipmentItem)
                    .HasConstraintName("FK_InventoryItems_EquipmentItem");
            });

            modelBuilder.Entity<ItemCategories>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.Property(e => e.ItemImage).IsRequired();

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<LandingPage>(entity =>
            {
                entity.Property(e => e.CurrentMonthFunding).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.EndDate).HasMaxLength(12);

                entity.Property(e => e.TargetFunding).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<Mobs>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Npc)
                    .WithMany(p => p.Mobs)
                    .HasForeignKey(d => d.NpcId)
                    .HasConstraintName("FK_Mobs_Npcs");
            });

            modelBuilder.Entity<Mounts>(entity =>
            {
                entity.Property(e => e.ModelPath).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<NpcBaseAbilities>(entity =>
            {
                entity.Property(e => e.AnimationName).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<NpcConversations>(entity =>
            {
                entity.HasOne(d => d.DialogOption2Navigation)
                    .WithMany(p => p.NpcConversationsDialogOption2Navigation)
                    .HasForeignKey(d => d.DialogOption2)
                    .HasConstraintName("FK_NpcConversations_DialogOptions1");

                entity.HasOne(d => d.DialogOption3Navigation)
                    .WithMany(p => p.NpcConversationsDialogOption3Navigation)
                    .HasForeignKey(d => d.DialogOption3)
                    .HasConstraintName("FK_NpcConversations_DialogOptions2");

                entity.HasOne(d => d.DialogOptions1Navigation)
                    .WithMany(p => p.NpcConversationsDialogOptions1Navigation)
                    .HasForeignKey(d => d.DialogOptions1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NpcConversations_DialogOptions");
            });

            modelBuilder.Entity<Npcs>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.BaseAbility)
                    .WithMany(p => p.Npcs)
                    .HasForeignKey(d => d.BaseAbilityId)
                    .HasConstraintName("FK_Npcs_NpcBaseAbilities");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Npcs)
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK_Npcs_Zones");
            });

            modelBuilder.Entity<OwableBuildings>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.OwableBuildings)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OwableBuildings_BuildingTypes");
            });

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.Property(e => e.Page1).IsRequired();

                entity.Property(e => e.Page2).IsRequired();
            });

            modelBuilder.Entity<PotionIngridients>(entity =>
            {
                entity.Property(e => e.IngridientName).IsRequired();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PotionIngridients)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__PotionIng__ItemI__12C8C788");
            });

            modelBuilder.Entity<PotionRecepie>(entity =>
            {
                entity.Property(e => e.PotionName).IsRequired();

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.PotionRecepie)
                    .HasForeignKey(d => d.RewardId)
                    .HasConstraintName("FK__PotionRec__Rewar__11D4A34F");
            });

            modelBuilder.Entity<QuestItems>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.QuestItems)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_QuestItems_Items");
            });

            modelBuilder.Entity<QuestState>(entity =>
            {
                entity.Property(e => e.Remembered).HasMaxLength(200);
            });

            modelBuilder.Entity<QuestTypes>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Quests>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.QuestTypeNavigation)
                    .WithMany(p => p.Quests)
                    .HasForeignKey(d => d.QuestType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quests_QuestTypes");
            });

            modelBuilder.Entity<Slots>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stories>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<UnAllocatedPledgeSupport>(entity =>
            {
                entity.Property(e => e.LastName).IsRequired();
            });

            modelBuilder.Entity<UnassignedPledges>(entity =>
            {
                entity.Property(e => e.DateOfAssigment).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserAccounts>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BillingAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<WeaponPart>(entity =>
            {
                entity.Property(e => e.CpositionY).HasColumnName("CPositionY");

                entity.Property(e => e.CpositionZ).HasColumnName("CPositionZ");

                entity.Property(e => e.CpossitionX).HasColumnName("CPossitionX");
            });

            modelBuilder.Entity<Weapons>(entity =>
            {
                entity.Property(e => e.HasSecondary).IsRequired();

                entity.Property(e => e.ResourceLocation).IsRequired();

                entity.Property(e => e.SposX).HasColumnName("SPosX");

                entity.Property(e => e.SposY).HasColumnName("SPosY");

                entity.Property(e => e.SposZ).HasColumnName("SPosZ");

                entity.Property(e => e.SrotX).HasColumnName("SRotX");

                entity.Property(e => e.SrotY).HasColumnName("SRotY");

                entity.Property(e => e.SrotZ).HasColumnName("SRotZ");
            });

            modelBuilder.Entity<ZoneEntrances>(entity =>
            {
                entity.Property(e => e.CenterZ).HasColumnName("centerZ");

                entity.HasOne(d => d.ExitPointNavigation)
                    .WithMany(p => p.InverseExitPointNavigation)
                    .HasForeignKey(d => d.ExitPoint)
                    .HasConstraintName("FK__ZoneEntra__ExitP__467D75B8");
            });

            modelBuilder.Entity<Zones>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
