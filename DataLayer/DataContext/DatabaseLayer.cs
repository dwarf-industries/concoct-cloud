
namespace StoriesUntoldDataLayer.DataContext
{
    using DbScaffold.Models;
    using Microsoft.EntityFrameworkCore;
    using StoriesUntoldDataLayer.Model;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DatabaseLayer
    {
        #region AuthenicationLayer
        public static async Task<Tuple<bool, string>> Login(string username, string password)
        {
            using (var context = new WWContext())
            {

                var loginUser = await LoginUser(username, null);
                if (loginUser == null)
                    return new Tuple<bool, string>(false, string.Empty);
                var userGuid = Guid.NewGuid();
                //var authenicateUser = new LoggedUsers
                //{
                //    userGuid = userGuid,
                //    userId = loginUser.Id,
                //    AccessLevel = loginUser.SubscriptionId
                //};

                return new Tuple<bool, string>(true, loginUser.Id.ToString());
            }
        }

        public static Characters AssociateTraits(Characters character, List<AssociatedCharacterTraits> traitsData)
        {
            using(var context = new WWContext())
            {
                traitsData.ForEach(x =>
                {
                    var trait = x;
                    trait.CharacterId = character.Id;
                    context.AssociatedCharacterTraits.Add(trait);
                    context.SaveChanges();
                });
            }
            character.AssociatedCharacterTraits = traitsData;
            return character;
        }

        public static List<Traits> GetAllTraits()
        {
            var result = new List<Traits>();
            using(var context = new WWContext())
            {
                result = context.Traits.ToList();
            }
            return result;
        }

        public static List<ActiveMovementSpeeds> GetPossibleSpeeds()
        {
            var result = new List<ActiveMovementSpeeds>();
            using (var context = new WWContext())
            {
                result = context.ActiveMovementSpeeds.ToList();
            }
            return result;
        }

        public static string GetCharacterName(int loggedInCharacterId)
        {
            var result = string.Empty;
            using (var context = new WWContext())
            {
                result = context.Characters.FirstOrDefault(x => x.Id == loggedInCharacterId).CharacterName;
            }
            return result;
        }

        public static Mounts GetCharacterMount(int loggedInCharacterId, int mountId)
        {
            var result = default(Mounts);
            using (var context = new WWContext())
            {
                var resultAss = context.AssociatedCharacterMounts
                                 .Include(x => x.Mount)
                                 .FirstOrDefault(x => x.CharacterId == loggedInCharacterId);
                if (resultAss != null)
                    result = resultAss.Mount;
            }
            return result;
        }

        public static Books GetBookById(int refferenceId, int characterId)
        {
            var result = default(Books);
            using (var context = new WWContext())
            {

                var book = context.AssociatedCharacterBooks
                                .Include(x => x.Book)
                                .ThenInclude(Book => Book.AssociatedBookPages)
                                .ThenInclude(AssociatedBookPages => AssociatedBookPages.Page)
                                .FirstOrDefault(x => x.BookId == refferenceId && x.CharacterId == characterId);
                if (book != null)
                    result = book.Book;

                result.AssociatedBookPages.ToList().ForEach(x =>
                {
                    x.Page.AssociatedBookPages = null;
                });
            }
            return result;
        }

        public static Abilities GetAbilityById(int abilityId)
        {
            var result = default(Abilities);
            using (var context = new WWContext())
            {
                result = context.Abilities
                                .Include(x => x.Effect)
                                .FirstOrDefault(x => x.Id == abilityId);
            }
            return result;
        }

        public static AssociatedCharacterAbilities GetAbilityInCharacter(int abilityId, int characterId)
        {
            var result = default(AssociatedCharacterAbilities);
            using (var context = new WWContext())
            {
                result = context.AssociatedCharacterAbilities
                                .Include(x => x.Ability)
                                .Include(x => x.Ability.AbilityTypeNavigation)
                                .Include(x => x.Ability.Effect)
                                .Include(x => x.Ability.AssociatedAbilityCombos)
                                .ThenInclude(AssociatedAbilityCombos => AssociatedAbilityCombos.Combo)
                                .ThenInclude(Combo => Combo.TypeNavigation)
                                .FirstOrDefault(x =>
                                                x.AbilityId == abilityId &&
                                                x.CharacterId == characterId
                                );
            }
            return result;
        }

        public static List<AssociatedAccountCharacters> GetAccountCharacters(int accountId)
        {
            var result = default(List<AssociatedAccountCharacters>);
            using (var context = new WWContext())
            {
                result = context.AssociatedAccountCharacters
                                                   .Include(x => x.Character)

                                                   .Where(x => x.AccountId == accountId)
                                                   .ToList();

            }
            return result;
        }

        public static CharacterLookHandlerDTO GetCharacterModel(CharacterLookHandlerDTO currentInput)
        {
            using (var context = new WWContext())
            {
                var condition = currentInput.Gender == true ? 1 : 0;
                //var data = context.PlayableCharacters
                //                  .Include(x => x.SkinColorNavigation)
                //                  .Include(x => x.EyeColorNavigation)
                //                  .FirstOrDefault(x =>
                //                          x.Weight == currentInput.Weight &&
                //                          x.HairType == 1 &&
                //                          x.Gender ==condition
                //                  );

            }
            return currentInput;
        }

        public static int GetAccountId(string email)
        {
            var res = 0;
            using (var context = new WWContext())
            {
                res = context.UserAccounts.FirstOrDefault(x => x.Username == email).Id;
            }
            return res;
        }

        public static List<List<AssociatedNpcConversations>> GetNpcConversationData(List<object> data)
        {
            var result = new List<List<AssociatedNpcConversations>>();
            data.Remove(data.FirstOrDefault());
            data.Remove(data.FirstOrDefault());
            using (var context = new WWContext())
            {
                data.ForEach(y =>
                {
                    var id = y;
                    var res = context.AssociatedNpcConversations
                                    .Include(x => x.NpcConversation)
                                    .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                    .ThenInclude(NpcConversation => NpcConversation.AssociatedDialogOptionNcc)
                                    .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                    .Include(x => x.NpcConversation)
                                    .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                    .ThenInclude(NpcConversation => NpcConversation.AssociatedDialogOptionNcc)
                                    .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                    .Include(x => x.NpcConversation)
                                    .ThenInclude(NpcConversation => NpcConversation.DialogOption3Navigation)
                                    .ThenInclude(NpcConversation => NpcConversation.AssociatedDialogOptionNcc)
                                    .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                    .Include(x => x.NpcConversation)
                                    .Where(x => x.NpcId == int.Parse(id.ToString()))
                                    .ToList();
                    result.Add(res);
                });
            }
            return result;
        }

        public static async Task<UserAccounts> LoginUser(string username, byte[] password)
        {
            var result = default(UserAccounts);
            using (var context = new WWContext())
            {
                result = await Task.Run(() => context.UserAccounts.FirstOrDefault(x => x.Username == username));
            }

            return result;
        }

        public static void UpdateCharacterPossition(int characterId, ClientPossition clientPossition)
        {
            if (clientPossition == null)
                return;
            if (clientPossition.X == 0 && clientPossition.Y == 0 && clientPossition.Z == 0)
                return;

            using (var context = new WWContext())
            {
                var getCharacter = context.Characters.FirstOrDefault(x => x.Id == characterId);
                getCharacter.PossitionX = clientPossition.X;
                getCharacter.PossitionY = clientPossition.Y;
                getCharacter.PossitionZ = clientPossition.Z;
                getCharacter.RotationX = clientPossition.Rx;
                getCharacter.RotationY = clientPossition.Ry;
                getCharacter.RotationZ = clientPossition.Rz;
                getCharacter.RotationW = clientPossition.Rw;
                context.Entry(getCharacter).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void CharacterHungerUpdate(int loggedInCharacterId, float hunger)
        {
            using (var context = new WWContext())
            {
                var character = context.Characters.FirstOrDefault(x => x.Id == loggedInCharacterId);
                context.Attach(character);
                character.Hunger = hunger;
                //  context.Entry(character).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static OwableBuildings GetOwnableBuilding(int shopId, int zoneId)
        {
            var result = default(OwableBuildings);
            using (var context = new WWContext())
            {
                result = context.AssociatedAreaBuildings.Include(x => x.Building.AssociatedBuildingFunctionality)
                                                .ThenInclude(AssociatedBuildingFunctionality => AssociatedBuildingFunctionality.BuildingFunctionalityNavigation)
                                                .Include(x => x.Building.AssociatedProductsForSale)
                                                .ThenInclude(AssociatedProductsForSale => AssociatedProductsForSale.Item)
                                                .ThenInclude(Item => Item.WeaponPart)
                                                .FirstOrDefault(x => x.AreaId == zoneId && x.BuildingId == shopId)
                                                .Building;
            }
            return result;
        }
        public static List<ZoneEntrances> GetZoneEntrances(int zoneId)
        {
            var result = new List<ZoneEntrances>();
            using (var context = new WWContext())
            {
                result = context.AssociatedZoneEntrancePoints
                .Include(x => x.Point)
                .ThenInclude(Point => Point.ExitPointNavigation)
                .Where(x => x.ZoneId == zoneId && x.Point.ExitPointNavigation != null)
                .Select(x => x.Point)
                .ToList();
            }
            return result;
        }
        public static int GetCharacterZone(int characterId)
        {
            var zone = default(int);
            using (var context = new WWContext())
            {
                zone = context.Characters.FirstOrDefault(x => x.Id == characterId).InZone.Value;
            }
            return zone;
        }
        public static bool ChangeCharacterBoundAbility(int abilityId, int slotId, int loggedInCharacterId)
        {
            using (var context = new WWContext())
            {
                var newAbility = context.Abilities.FirstOrDefault(x => x.Id == abilityId);
                var abilityAssociation = context.AssociatedCharacterAbilities.FirstOrDefault(x => x.SlotPosition == slotId && x.CharacterId == loggedInCharacterId);
                if (abilityAssociation == null)
                    context.AssociatedCharacterAbilities.Add(new AssociatedCharacterAbilities
                    {
                        AbilityId = newAbility.Id,
                        CharacterId = loggedInCharacterId,
                        SlotPosition = slotId
                    });
                else
                {
                    abilityAssociation.AbilityId = newAbility.Id;
                    context.Attach(abilityAssociation);
                    context.Update(abilityAssociation);
                }
                context.SaveChanges();
            }
            return true;
        }
        public static Tuple<string, string> GetSalt(string username)
        {
            var result = default(Tuple<string, string>);
            try
            {
                using (var context = new WWContext())
                {
                    var user = context.UserAccounts.FirstOrDefault(x => x.Username == username);
                    result = new Tuple<string, string>(user.Salt, user.Password);
                }
            }
            catch
            {
                Console.WriteLine($"Account doesn't exist! Provided user input: {username} on salt select.");
            }

            return result;
        }




        #endregion

        #region Quest Related queries
        public static List<AssociatedNpcConversations> GetNpcDialogForCertainPlayer(int playerId, int npcId)
        {
            var npcDialog = new List<AssociatedNpcConversations>();
            using (var context = new WWContext())
            {
                npcDialog = context.AssociatedNpcConversations
                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                   .ThenInclude(DialogOptions1Navigation => DialogOptions1Navigation.Quest)
                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                   .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.Quest)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption3Navigation)
                                   .ThenInclude(DialogOption3Navigation => DialogOption3Navigation.Quest)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.AssociatedDialogOptionNcc)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                   .ThenInclude(DialogOptions1Navigation => DialogOptions1Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                   .ThenInclude(DialogOptions1Navigation => DialogOptions1Navigation.Quest)


                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                   .ThenInclude(DialogOptions1Navigation => DialogOptions1Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                   .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.Quest)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                   .ThenInclude(DialogOptions1Navigation => DialogOptions1Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption3Navigation)
                                   .ThenInclude(DialogOption3Navigation => DialogOption3Navigation.Quest)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                   .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                    .ThenInclude(DialogOptions1Navigation => DialogOptions1Navigation.Quest)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                   .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                   .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.Quest)


                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                   .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption3Navigation)
                                   .ThenInclude(DialogOption3Navigation => DialogOption3Navigation.Quest)

                                    .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                   .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOptions1Navigation)
                                    .ThenInclude(DialogOptions1Navigation => DialogOptions1Navigation.Quest)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption3Navigation)
                                   .ThenInclude(DialogOption3Navigation => DialogOption3Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption2Navigation)
                                    .ThenInclude(DialogOption2Navigation => DialogOption2Navigation.Quest)

                                   .Include(x => x.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption3Navigation)
                                   .ThenInclude(DialogOption3Navigation => DialogOption3Navigation.AssociatedDialogOptionNcc)
                                   .ThenInclude(AssociatedDialogOptionNcc => AssociatedDialogOptionNcc.NpcConversation)
                                   .ThenInclude(NpcConversation => NpcConversation.DialogOption3Navigation)
                                   .ThenInclude(DialogOption3Navigation => DialogOption3Navigation.Quest)


                                   // .Where(x => x.CharacterId == playerId && x.NpcId == npcId)
                                   .ToList();
            }
            return npcDialog;
        }

        public static void CollectQuestItem(int playerId, int questId, int itemId, int collectionId)
        {
            using (var context = new WWContext())
            {
                var quest = context.AssociatedCharacterQuests.Include(x => x.Quest)
                                                            .Include(x => x.Quest.AssociatedQuestCollectable)
                                                            .ThenInclude(AssociatedQuestCollectable => AssociatedQuestCollectable.QuestItem)
                                                            .FirstOrDefault(x => x.QuestId == questId && x.CharacterId == playerId);
                var questItem = quest.Quest.AssociatedQuestCollectable.FirstOrDefault(x => x.QuestItem.Id == collectionId && x.QuestItemId == itemId);
                questItem.QuestItem.Active = 0;
                context.Attach(questItem).State = EntityState.Modified;
                context.SaveChanges();
                //Todo update the quest item to inactive;
            }
        }
        public static Quests CharacterAcceptQuest(int characterId, int questId)
        {
            using (var context = new WWContext())
            {
                var quest = context
                            .Quests
                            .Include(x => x.QuestTypeNavigation)
                            .Include(x => x.AssociatedQuestCollectable)
                            .Include(x => x.AssociatedMobs)
                            .Include(x => x.AssociatedQuestQuestions)
                            .Include(x => x.AssociatedQuestBookReadings)
                            .FirstOrDefault(x => x.Id == questId);

                var questState = context.QuestState.Add(new QuestState
                {
                    IsMob = quest.AssociatedMobs.Count > 0 ? 1 : 0,
                    IsCollectable = quest.AssociatedQuestCollectable.Count > 0 ? 1 : 0,
                    IsClass = quest.AssociatedQuestBookReadings.Count > 0 ? 1 : 0
                });
                context.SaveChanges();
                context.AssociatedCharacterQuestStates.Add(new AssociatedCharacterQuestStates
                {
                    CharacterId = characterId,
                    QuestId = questId,
                    QuestStateId = questState.Entity.Id
                });
                context.SaveChanges();
                return quest;
            }
        }
        public static List<AssociatedCharacterQuests> GetCharacterQuests(int loggedInCharacterId)
        {
            var quest = new List<AssociatedCharacterQuests>();
            using (var context = new WWContext())
            {

                quest = context.AssociatedCharacterQuests.Include(x => x.Quest)
                                                         .Include(x => x.Quest.AssociatedQuestCollectable)
                                                         //.Include(x=> x.Quest.AssociatedQuestCollectable.Select(y=>y.QuestItem))
                                                         .Include(x => x.Quest.AssociatedQuestRewards)
                                                         .Include(x => x.Quest.AssociatedMobs)
                                                         //.Include(x=> x.Quest.AssociatedMobs.Select(y=>y.Mob))
                                                         .Where(x => x.CharacterId == loggedInCharacterId).ToList();

                var copyQuest = quest;
                copyQuest.ForEach(x =>
                {
                    x.Quest.AssociatedCharacterQuests = null;
                    x.Quest.AssociatedQuestCollectable.ToList().ForEach(y =>
                    {
                        y.Quest = null;
                        var questItem = quest.FirstOrDefault(z => z == x)
                         .Quest.AssociatedQuestCollectable
                         .FirstOrDefault(t => t.QuestItemId == y.QuestItemId)
                         .QuestItem;
                        questItem = context.QuestItems.FirstOrDefault(o => o.Id == y.QuestItemId);
                        questItem.AssociatedQuestCollectable = null;


                    });
                    x.Quest.AssociatedMobs.ToList().ForEach(n =>
                    {
                        n.Quest = null;
                        var mob = quest.FirstOrDefault(c => c == x)
                        .Quest.AssociatedMobs.FirstOrDefault(v => v.MobId == n.MobId)
                        .Mob;
                        mob = context.Mobs.FirstOrDefault(m => m.Id == n.MobId);
                        mob.AssociatedMobs = null;
                    });
                });
                quest = copyQuest;
            }
            return quest;
        }

        public static List<Npcs> GetNpcsByZone(int zoneId)
        {
            var result = new List<Npcs>();
            using (var context = new WWContext())
            {
                result = context.Npcs.Include(x => x.AssociatedNpcMovingPoints)
                                     .ThenInclude(AssociatedNpcMovingPoints => AssociatedNpcMovingPoints.Point)
                                     .Include(x => x.BaseAbility)
                                     .Where(x => x.ZoneId == zoneId).ToList();
            }
            return result;
        }
        public static BookReadingQuest GetBookReadingQuestById(int id, int characterId, int questId, string question)
        {
            var bookReadingQuest = default(BookReadingQuest);
            using (var context = new WWContext())
            {
                bookReadingQuest = context.BookReadingQuest.FirstOrDefault(x => x.Id == id);
                var getAssociation = context.AssociatedQuestBookReadings.FirstOrDefault(x => x.QuestId == questId
                                                                        && x.BookReadingId == id
                                                                        && x.Quest.AssociatedCharacterQuests.Any(x => x.CharacterId == characterId));
                getAssociation.HasReadTrough = 1;
                getAssociation.Remembred = question;
                context.Attach(getAssociation);
                context.Update(getAssociation);
                context.SaveChanges();
            }
            return bookReadingQuest;
        }
        public static void CharacterQuestCompleated(int rewardMultiplier, int questId, int characterId)
        {
            using (var context = new WWContext())
            {
                var quest = context.Quests.FirstOrDefault(x => x.Id == questId);
                var character = context.Characters.FirstOrDefault(x => x.Id == characterId);
                character.Gold = quest.Gold * rewardMultiplier;
                character.Experience += quest.Experience * rewardMultiplier;
                context.Attach(character);
                context.Update(character);
                context.SaveChanges();
                var association = context.AssociatedCharacterQuests.FirstOrDefault(x => x.CharacterId == characterId && x.QuestId == questId);
                association.Compleate = 1;
                context.Attach(association);
                context.Update(association);
                context.SaveChanges();
            }
        }

        public static List<InventoryItems> GetQuestItemsById(int questId)
        {
            var result = new List<InventoryItems>();
            using (var context = new WWContext())
            {
                context.AssociatedQuestRewards.Include(x => x.Reward).Where(x => x.QuestId == questId).Select(x => x.Reward).ToList().ForEach(x =>
                {
                    result.Add(context.InventoryItems.FirstOrDefault(y => y.InGameModel == x.RewardName));
                });
            }
            return result;
        }
        public static List<QuestQuestistions> GetQuestQuestistions(int questId)
        {
            var result = new List<QuestQuestistions>();
            using (var context = new WWContext())
            {
                result = context.AssociatedQuestQuestions.Include(x => x.Question)
                                                         .Where(x => x.QuestId == questId)
                                                         .Select(x => x.Question)
                                                         .ToList();
            }
            return result;
        }
        public static Quests GetQuestById(int id)
        {
            var quest = default(Quests);
            using (var context = new WWContext())
            {
                quest = context.Quests.Include(x => x.AssociatedQuestBookReadings)
                                      .ThenInclude(AssociatedQuestBookReadings => AssociatedQuestBookReadings.BookReading)
                                      .FirstOrDefault(x => x.Id == id);
            }
            return quest;
        }
        public static List<AssociatedZoneQuests> GetZoneQuests(int loggedInCharacterId)
        {
            var quest = new List<AssociatedZoneQuests>();
            using (var context = new WWContext())
            {
                var charZone = context.Characters.FirstOrDefault(x => x.Id == loggedInCharacterId).InZone;
                quest = context.AssociatedZoneQuests.Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedMobs)
                                                    .Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedQuestCollectable)
                                                    .ThenInclude(AssociatedQuestCollectable => AssociatedQuestCollectable.QuestItem)
                                                    .ThenInclude(QuestItem => QuestItem.Object)
                                                    .Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedQuestCollectable)
                                                    .ThenInclude(AssociatedQuestCollectable => AssociatedQuestCollectable.AreaSizeNavigation)
                                                    .Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedQuestRewards)

                                                    .Where(x => x.ZoneId == charZone.Value).ToList();
            }
            return quest;
        }
        public static List<AssociatedZoneQuests> GetZoneQuestsById(int zoneId)
        {
            var quest = new List<AssociatedZoneQuests>();
            using (var context = new WWContext())
            {

                quest = context.AssociatedZoneQuests.Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedMobs)
                                                    .Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedQuestCollectable)
                                                    .ThenInclude(AssociatedQuestCollectable => AssociatedQuestCollectable.QuestItem)
                                                    .ThenInclude(QuestItem => QuestItem.Object)
                                                    .Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedQuestCollectable)
                                                    .ThenInclude(AssociatedQuestCollectable => AssociatedQuestCollectable.AreaSizeNavigation)
                                                    .Include(x => x.Quest)
                                                    .ThenInclude(Quest => Quest.AssociatedQuestRewards)

                                                    .Where(x => x.ZoneId == zoneId).ToList();
            }
            return quest;
        }

        public static CharacterRaces GetRaceDetails(int raceId)
        {
            var race = default(CharacterRaces);
            using (var context = new WWContext())
            {
                race = context.CharacterRaces.FirstOrDefault(x => x.Id == raceId);
            }
            return race;
        }
        public static List<WeaponPart> GetWeaponParts(int weaponId)
        {
            var result = new List<WeaponPart>();
            using (var context = new WWContext())
            {
                result = context.WeaponPart.Where(x => x.WeaponType == weaponId).ToList();
            }
            return result;
        }
        public static Characters GetCharacterData(int accountId, int chracterId)
        {
            var character = default(Characters);
            using (var context = new WWContext())
            {
                character = context.Characters
                                .Include(x => x.AssociatedCharacterQuests)
                                    .ThenInclude(associatedcharacterquests => associatedcharacterquests.Quest)
                                        .ThenInclude(quest => quest.AssociatedMobs)
                                .Include(x => x.AssociatedCharacterQuests)
                                    .ThenInclude(associatedcharacterquests => associatedcharacterquests.Quest)
                                        .ThenInclude(quest => quest.AssociatedQuestRewards)
                                .Include(x => x.AssociatedCharacterQuests)
                                    .ThenInclude(associatedcharacterquests => associatedcharacterquests.Quest)
                                        .ThenInclude(quest => quest.AssociatedQuestCollectable)
                                .Include(x => x.AssociatedInventoryItems)
                                .ThenInclude(AssociatedInventoryItems => AssociatedInventoryItems.Item)
                                .Include(x => x.AssociatedCharacterAbilities)
                                .ThenInclude(AssociatedCharacterAbilities => AssociatedCharacterAbilities.Ability)
                                .ThenInclude(Ability => Ability.Effect)
                                .Include(x => x.ActiveMovementSpeedNavigation)
                                .Include(x => x.ActiveRotationSpeedNavigation)
                                .Include(x => x.EquipedWeapon)
                                    .ThenInclude(EquipedWeapon => EquipedWeapon.AssociatedWeaponPart)
                                        .ThenInclude(AssociatedWeaponPart => AssociatedWeaponPart.Part)
                                .Include(x => x.ActiveRecepieNavigation)
                                .FirstOrDefault(x => x.Id == chracterId);
            }
            return character;
        }

        public static Characters CreateNewCharacter(int account, CharacterLookHandlerDTO characterData, string characterName)
        {
            var character = default(Characters);
            using (var context = new WWContext())
            {
                var condition = characterData.Gender == true ? 1 : 0;

                character = context.Characters.Add(new Characters
                {
                    CharacterName = characterName,
                    AssociatedAccountCharacters = null,
                    AssociatedCharacterAbilities = null,
                    AssociatedCharacterCompletedQuests = null,
                    AssociatedCharacterQuests = null,
                    AssociatedEquippedCharacterItems = null,
                    CharacterAge = 16,
                    CharacterGender = characterData.Gender == true ? 1 : 0,
                    CharacterHeight = characterData.Height,
                    CharacterLevel = 1,
                    CharacterWeight = characterData.Weight,
                    HairColor = 1,
                    Hidratation = 100,
                    Hunger = 100,
                    InZone = 1,
                    MainStackAbilities = 1,
                    Sleep = 100,
                    WeaponType = 0,
                    SecondaryStackAbilities = 0,
                    Stamina = 100,
                    PossitionX = 0,
                    PossitionY = 0,
                    PossitionZ = 0,
                    RotationX = 0,
                    RotationY = 0,
                    RotationZ = 0,
                    RotationW = 0,
                    CharacterRaceNavigation = null,
                    CharacterId = 1,
                    CharacterRecepie = characterData.LookRecepie,
                    CharacterRace = 1,
                    EyeColor = 1
                }).Entity;
                context.SaveChanges();
                context.AssociatedAccountCharacters.Add(new AssociatedAccountCharacters
                {
                    CharacterId = character.Id,
                    AccountId = account

                });
                context.SaveChanges();
            }
            return character;
        }

        public static List<AssociatedTradedBussinessItems> GetTradingCenterItems(int centerId, int category)
        {
            var items = new List<AssociatedTradedBussinessItems>();
            using (var context = new WWContext())
            {
                if (category != 0)
                    items = context.AssociatedTradedBussinessItems
                                   .Include(x => x.BusinessItem)
                                   .Include(x => x.Category)
                                   .Include(x => x.TradingCenter)
                                   .Where(x => x.TradingCenterId == centerId && x.CategoryId == category)
                                   .ToList();
                else
                    items = context.AssociatedTradedBussinessItems
                              .Include(x => x.BusinessItem)
                              .Include(x => x.Category)
                              .ThenInclude(Category => Category.AssociatedItemSubCategoriesMain)
                              .ThenInclude(AssociatedItemSubCategoriesMain => AssociatedItemSubCategoriesMain.SubCategory)
                              .Include(x => x.TradingCenter)
                              .Where(x => x.TradingCenterId == centerId)
                              .ToList();
            }
            return items;

        }
        #endregion
    }
}
