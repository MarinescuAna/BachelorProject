using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.ApplicationLogic.Service.Utils
{
    public static class Messages
    {
        public static readonly string GetTeacherEmailByGroupIdAsync = "GroupServiceImpl -> GetTeacherEmailByGroupIdAsync ->";
        public static readonly string JoinToGroupAsync = "GroupServiceImpl -> JoinToGroupAsync ->";
        public static readonly string DeleteUserFromGroupAsync = "GroupServiceImpl -> DeleteUserFromGroupAsync ->";
        public static readonly string DeleteGroupAsync = "GroupServiceImpl -> DeleteGroupAsync ->";
        public static readonly string UpdateGroupAsync = "GroupServiceImpl -> UpdateGroupAsync ->";
        public static readonly string UpdateGroupMemberAsync = "GroupServiceImpl -> UpdateGroupMemberAsync ->";
        public static readonly string AddMemberByEmailAsync = "GroupServiceImpl -> AddMemberByEmailAsync ->";
        public static readonly string CreateGroupByUserAsync = "GroupServiceImpl -> CreateGroupByUserAsync ->";

        public static readonly string CreateChatAsync = "ChatService -> CreateChatAsync ->";
        public static readonly string SaveMessageByGroupKeyAsync = "ChatService -> SaveMessageByGroupKeyAsync ->";
        public static readonly string UpdateMessageAsync = "ChatService -> UpdateMessageAsync ->";
        public static readonly string DeleteMessageAsync = "ChatService -> DeleteMessageAsync ->";
        
        public static readonly string InsertImageAsync = "ImageService -> InsertImageAsync ->";
        public static readonly string UpdateImageAsync = "ImageService -> UpdateImageAsync ->";


    }
}
