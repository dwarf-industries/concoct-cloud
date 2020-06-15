namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;
    public class ChatContext : IDisposable
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;

        public ChatContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }


        internal List<ChatRoomRights> GetChatRoomRights(int id)
        {
            return Context.AssociatedChatRoomRights.Include(x => x.Right)
                                                   .Where(x => x.ChatRoomId == id)
                                                   .Select(x => x.Right)
                                                   .Distinct()
                                                   .ToList();
        }
           internal List<ChatRooms> GetAllChatChannels(int id)
        {
            return Context.AssociatedChatRoomRights.Include(x => x.ChatRoom)
                                                   .Where(x => x.ChatRoom.ProjectId == id)
                                                   .Select(x => x.ChatRoom)
                                                   .Distinct()
                                                   .ToList();
        }

        internal List<OutgoingChatItem> GetChatChannels(int id, int userId)
        {
            // var i = 1;
            var result = new List<OutgoingChatItem>();
            result = GetPersonalMessageNavigation(userId, result);
            Context.AssociatedChatRoomRights
            .Include(x => x.ChatRoom)
            .ThenInclude(ChatRoom => ChatRoom.ChatChannels)
            .Where(x => x.ChatRoom.ProjectId == id && x.Right.AssociatedUserChatRights.Any(y => y.ProjectId == id && y.UserId == userId))
            .Select(x => x)
            .Distinct()
            .ToList().ForEach(x =>
            {
                var cItem = new OutgoingChatItem
                {
                    InternalId = x.ChatRoomId,
                    // NodeId = i++,
                    NodeText = x.ChatRoom.RoomName,
                    IconCss = "icon-th icon",
                    Link = "",
                    ChannelType = 0,
                    IsParent = true,
                    ParentId = x.ChatRoomId,
                    NodeChild = x.ChatRoom.ChatChannels.Select(y => new OutgoingChatItemChild
                    {
                        InternalId = y.Id,
                        // NodeId = i++,
                        NodeText = y.ChannelName,
                        ChannelType = y.ChannelType.Value,
                        IconCss = "icon-circle-thin icon",
                        Link = "",
                        IsPersonal = false,
                        ParentId = x.ChatRoomId
                    }).ToList()
                };

                result.Add(cItem);
            });

            return result;
        }
        internal List<ChatRoomRights> GetUserChatRights(int id, int projectId)
        {
            return Context.AssociatedUserChatRights.Include(x => x.Right)
                                                    .Where(x => x.UserId == id && x.ProjectId == projectId)
                                                    .Select(x=>x.Right)
                                                    .ToList();
        }

        internal List<ChatRoomRights> GetProjectChatRights(int projectId)
        {
            var result = new  List<ChatRoomRights>();
            var AllRights = Context.AssociatedChatRoomRights.Include(x => x.Right)
                                                   .Where(x => x.Right.PojectId == projectId)
                                                   .Select(x => x.Right)
                                                   .ToList();
            AllRights.ForEach(x=>{
                if(!result.Any(y=>y.Id == x.Id))
                    result.Add(x);
            });
            return result;
        }

        internal int GetDefaultProjectChannel(int projectId)
        {
            
            return Context.ChatRooms.Include(x => x.ChatChannels)
                                    .FirstOrDefault(x => x.ProjectId == projectId).ChatChannels
                                    .FirstOrDefault().Id;
        }

        internal ChatRoomRights GetChatRightById(int tagId)
        {
            return Context.ChatRoomRights.FirstOrDefault(x=>x.Id == tagId);
        }

        internal int GetDefaultProjectChatRoom(int projectId)
        {
            var defaultRoom = Context.ChatRooms.FirstOrDefault(x=>x.ProjectId == projectId);
            if(defaultRoom == null)
                return 0;

            return defaultRoom.Id;
        }
        internal string AddChatRoomMessage(IncomingChatMessage messageData, int sender)
        {
            var chatRoom = Context.ChatChannels.FirstOrDefault(x=> x.Id == messageData.ActiveRoom);
            if(chatRoom == null && messageData.IsPersonal == 0)
                return string.Empty;
            
            if(messageData.IsPersonal == 0)
            {
                var projectUsers = Context.AssociatedProjectMembers.Where(x=>x.ProjectId == messageData.ProjectId).ToList();
                projectUsers.ForEach(x=>{
                    if(Context.AssociatedUserChatNotifications.FirstOrDefault(y=>y.ChatChannelId == messageData.ActiveRoom && y.ProjectId == x.ProjectId && y.UserId == x.UserAccountId) == null)
                    {
                        Context.AssociatedUserChatNotifications.Add(new AssociatedUserChatNotifications{
                            ChatChannelId = chatRoom.Id,
                            ProjectId = messageData.ProjectId,
                            UserId = x.UserAccountId
                        });
                        Context.SaveChanges();
                    }
                });
            }
            else
            {
                var senderName = Context.UserAccounts.FirstOrDefault(x=>x.Id == sender).Email;
                var notification = Context.Notifications.Add(new Notifications{
                    Content = $"{senderName} has sent you a new message.",
                    NotificationType = 1,
                    DateOfMessage = DateTime.Now,
                });
                Context.SaveChanges();
                Context.AssociatedUserNotifications.Add(new AssociatedUserNotifications{
                    NewNotification = 1,
                    UserId = messageData.ReciverId,
                    NotificationId = notification.Entity.Id
                });
                Context.SaveChanges();
            }
            var email = Context.UserAccounts.FirstOrDefault(x=>x.Id == sender).Email;
            var message = Context.PublicMessages.Add(new PublicMessages{
                IsNew = 1,
                SenderId = sender,
                MessageContent = messageData.Message,
                DateOfMessage = DateTime.Now,
                SenderName = email
            });
            Context.SaveChanges();
            if(messageData.IsPersonal == 0)
                Context.AssociatedChatChannelMessages.Add(new AssociatedChatChannelMessages{
                    ChatChannelId = messageData.ActiveRoom,
                    PublicMessageId = message.Entity.Id,
                });
            else
                Context.AssociatedChatPersonalMessages.Add(new AssociatedChatPersonalMessages{
                    ProjectId = messageData.ProjectId,
                    PublicMessageId = message.Entity.Id,
                    SenderId = sender,
                    ReciverId = messageData.ReciverId,                    
                });
            Context.SaveChanges();
            return email;
        }
      
        internal PublicMessages AddNewPublicMessage(IncomingPublicMessage newMessage)
        {
            var message = Context.PublicMessages.Add(new PublicMessages{
                DateOfMessage = DateTime.Now,
                SenderName = newMessage.SenderName,
                MessageContent = newMessage.MessageContent,
                IsNew = 1
            });
            Context.SaveChanges();
            var association = Context.AssociatedProjectPublicDiscussions.Add(new AssociatedProjectPublicDiscussions{
                ProjectId = newMessage.ProjectId,
                PublicMessageId = message.Entity.Id
            });
            Context.SaveChanges();
            return message.Entity;
        }
        internal List<PublicMessages> GetAllPublicMessagesForProject(int id, int v)
        {
            if(v == 1)
            {
                var result = Context.AssociatedProjectPublicDiscussions.Include(x => x.PublicMessage)
                                                                .Where(x => x.ProjectId == id && x.PublicMessage.IsNew == 1)
                                                                .Select(x => x.PublicMessage)
                                                                .ToList();
                result.ForEach(x=>{
                    x.IsNew = 0;
                    Context.Attach(x);
                    Context.Update(x);
                    Context.SaveChanges();
                });
                return result;
            }
            else
            {
                var allMesages = Context.AssociatedProjectPublicDiscussions.Include(x => x.PublicMessage)
                                                                .Where(x => x.ProjectId == id )
                                                                .Select(x => x.PublicMessage)
                                                                .ToList();
                
                return allMesages;
            }
        }

        private List<OutgoingChatItem> GetPersonalMessageNavigation(int userId, List<OutgoingChatItem> result)
        {
            var personalMesages = Context.AssociatedChatPersonalMessages.Include(x => x.Sender)
                                                                      .Where(x => x.ReciverId == userId)
                                                                      .Select(x => new
                                                                      {
                                                                          Email = x.Sender.Email,
                                                                          Id = x.Sender.Id
                                                                      })
                                                                      .Distinct()
                                                                      .ToList();
            var pMessageCategory = new OutgoingChatItem
            {
                InternalId = -1,
                IconCss = "icon-th icon",
                ChannelType = 0,
                NodeText = "Personal Messages",
                IsParent = true,
                ParentId = -1,
                Link = "",
                NodeChild = new List<OutgoingChatItemChild>()
            };
            personalMesages.ForEach(x =>
            {
                pMessageCategory.NodeChild.Add(new OutgoingChatItemChild
                {
                    InternalId = x.Id,
                    // NodeId = i++,
                    NodeText = x.Email,
                    ChannelType = 0,
                    IconCss = "icon-circle-thin icon",
                    Link = "",
                    IsPersonal = true,
                    ParentId = -1
                });
            });
            result.Add(pMessageCategory);
            return result;
        }

        internal void AddNewChatChannel(IncomingIdRequest request)
        {
            var chatRoom = Context.ChatRooms.Add(new ChatRooms{
                ProjectId = request.Id,
                RoomName = request.Phase
            });
            Context.SaveChanges();
            var ownerRight = Context.ChatRoomRights.FirstOrDefault(x=>x.PojectId == request.Id);  
            Context.AssociatedChatRoomRights.Add(new AssociatedChatRoomRights{
                ChatRoomId = chatRoom.Entity.Id,
                RightId = ownerRight.Id
            });
            Context.SaveChanges();
        }

        internal void AddNewChatRoom(IncomingIdRequest request)
        {
            var room = Context.ChatChannels.Add(new ChatChannels{
                ChannelType = 1,
                ChannelName = request.Phase,
                ChatRoom = request.Id
            });
            Context.SaveChanges();
        }

          internal ChatRoomRights AssignUserTag(int tagId, int projectId, int userId)
        {
            Context.AssociatedUserChatRights.Add(new AssociatedUserChatRights{
                ProjectId = projectId,
                UserId = userId,
                RightId = tagId
            });
            Context.SaveChanges();
            return Context.ChatRoomRights.FirstOrDefault(x=>x.PojectId == projectId && x.Id == tagId);
        }

        internal List<PublicMessages> GetCannelMessages(int id, int isPersonal, int userId)
        {
            if(isPersonal == 0)
                return Context.AssociatedChatChannelMessages.Include(x => x.PublicMessage)
                                                        .Where(x => x.ChatChannelId == id)
                                                        .Select(x => x.PublicMessage)
                                                        .ToList();
            else
            {
                var sender = Context.AssociatedChatPersonalMessages.Include(x=>x.PublicMessage)
                                                            .Where(x=>x.SenderId == id && x.ReciverId == userId)
                                                            .Select(x=>x.PublicMessage)
                                                            .ToList();
                var reciver = Context.AssociatedChatPersonalMessages.Include(x=>x.PublicMessage)
                                                            .Where(x=>x.SenderId == userId && x.ReciverId == id)
                                                            .Select(x=>x.PublicMessage)
                                                            .ToList();
                var result = new List<PublicMessages>();
                result.AddRange(sender);
                result.AddRange(reciver);
                return result.OrderBy(x=>x.DateOfMessage).ToList();
            }
        }



      
        internal void InserTag(IncomingChatRoomRights request)
        {
            request.Id = 0;
            var chatRoom = Context.ChatRoomRights.Add(new ChatRoomRights{
                RightName = request.RightName,
                Background = request.Background,
                Foreground = request.Foreground,
                PojectId = request.ProjectId
            });
            Context.SaveChanges();

            Context.AssociatedChatRoomRights.Add(new AssociatedChatRoomRights{
                    RightId = chatRoom.Entity.Id,
                    ChatRoomId = request.ChatRoomId
            });
            Context.SaveChanges();
        }

        internal void UpdateTag(IncomingChatRoomRights request)
        {
            var tag = Context.ChatRoomRights.FirstOrDefault(x=>x.Id == request.Id);
            tag.Foreground = request.Foreground;
            tag.Background = request.Background;
            tag.RightName = request.RightName;
            Context.Attach(tag);
            Context.Update(tag);
            Context.SaveChanges();
        }

        internal void RemoveUserTag(int tagId, int userId, int projectId)
        {
            var right = Context.AssociatedUserChatRights.FirstOrDefault(x=>x.UserId == userId
                                                                           && x.RightId == tagId
                                                                           && x.ProjectId == projectId);
            Context.AssociatedUserChatRights.Remove(right);
            Context.SaveChanges();
        }

        internal void UserChatChannelRead(int userId, int projectId, int channel)
        {
            var chatChanelNotifications = Context.AssociatedUserChatNotifications.FirstOrDefault(x=>x.UserId == userId
                                                                                           && x.ProjectId == projectId
                                                                                           && x.ChatChannelId == channel);
            if(chatChanelNotifications == null)
                return;
            Context.AssociatedUserChatNotifications.Remove(chatChanelNotifications);
            Context.SaveChanges();
        }
        
        internal List<AssociatedUserChatNotifications> GetChatNotifications(int id, int userId)
        {
            return Context.AssociatedUserChatNotifications
                          .Where(x=>x.ProjectId == id && x.UserId == userId)
                          .ToList();
        }

        internal Tuple<Notifications, string> NewChatPersonalMessage(IncomingChatMessage request, int userId)
        {
            var reciverName = Context.UserAccounts.FirstOrDefault(x=>x.Id == request.ReciverId).Email;
            var senderName = Context.UserAccounts.FirstOrDefault(x=>x.Id == userId).Email;
            var publicMessage = Context.PublicMessages.Add(new PublicMessages{
                IsNew = 1,
                SenderId = userId,
                DateOfMessage = DateTime.Now,
                MessageContent = request.Message,
                SenderName = senderName
            });
            Context.SaveChanges();
            Context.AssociatedChatPersonalMessages.Add(new AssociatedChatPersonalMessages{
                ProjectId = request.ProjectId,
                ReciverId = request.ReciverId,
                SenderId = userId,
                PublicMessageId = publicMessage.Entity.Id 
            });
            Context.SaveChanges();
            var notification = Context.Notifications.Add(new Notifications{
                DateOfMessage = DateTime.Now,
                Content = $"{senderName} has sent you a personal message",
                NotificationType = 1
            });
            Context.SaveChanges();
            Context.AssociatedUserNotifications.Add(new AssociatedUserNotifications{
                UserId = request.ReciverId,
                NewNotification = 1,
                NotificationId = notification.Entity.Id
            });
            Context.SaveChanges();
            return new Tuple<Notifications, string>(notification.Entity, reciverName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ChatContext()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}