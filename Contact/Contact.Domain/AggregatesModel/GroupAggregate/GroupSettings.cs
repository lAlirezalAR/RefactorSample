using Contact.Domain.AggregatesModel.GroupAggregate.Enums;
using Utilities.Framework;
using Utilities.Framework.Guards;

namespace Contact.Domain.AggregatesModel.GroupAggregate
{
    public class GroupSettings : AggregateRoot
    {
        public int GroupId { get; private set; }
        public AutoRegisterStatus AutoRegister { get; private set; }
        public string AutoRegisterKeyWord { get; private set; }
        public string AutoRegisterLineNumber { get; private set; }
        public string AutoRegisterMessage { get; private set; }
        public AutoRegisterStatus AutoRegisterCancel { get; private set; }
        public string AutoRegisterCancelKeyWord { get; private set; }
        public string AutoRegisterCancelLineNumber { get; private set; }
        public string AutoRegisterCancelMessage { get; private set; }
        public Group Group { get; private set; }
        protected GroupSettings() { }
        public GroupSettings(int groupId, string autoRegisterMessage, string autoRegisterCancelMessage, string autoRegisterKeyWord, string autoRegisterCancelKeyword, string autoregisterLineNumber, string autoRegisterCancelLineNumber, AutoRegisterStatus autoRegister = AutoRegisterStatus.Inactive, AutoRegisterStatus autoRegisterCancel = AutoRegisterStatus.Inactive)
        {
            Guard.AgainstNullValue(groupId, "شناسه گروه الزامی است");
            GroupId = groupId;
            AutoRegister = autoRegister;
            AutoRegisterMessage = autoRegisterMessage;
            AutoRegisterCancel = autoRegisterCancel;
            AutoRegisterCancelMessage = autoRegisterCancelMessage;
            AutoRegisterKeyWord = autoRegisterKeyWord;
            AutoRegisterCancelKeyWord = autoRegisterCancelKeyword;
            AutoRegisterLineNumber = autoregisterLineNumber;
            AutoRegisterCancelLineNumber = autoRegisterCancelLineNumber;
        }

        public void Edit(int groupId, string autoRegisterMessage, string autoRegisterCancelMessage, string autoRegisterKeyWord, string autoRegisterCancelKeyword, string autoregisterLineNumber, string autoRegisterCancelLineNumber, AutoRegisterStatus autoRegisterCancel = AutoRegisterStatus.Inactive, AutoRegisterStatus autoRegister = AutoRegisterStatus.Inactive)
        {
            Guard.AgainstNullValue(groupId, "شناسه گروه الزامی است");
            GroupId = groupId;
            AutoRegister = autoRegister;
            AutoRegisterMessage = autoRegisterMessage;
            AutoRegisterCancel = autoRegisterCancel;
            AutoRegisterCancelMessage = autoRegisterCancelMessage;
            AutoRegisterKeyWord = autoRegisterKeyWord;
            AutoRegisterCancelKeyWord = autoRegisterCancelKeyword;
            AutoRegisterLineNumber = autoregisterLineNumber;
            AutoRegisterCancelLineNumber = autoRegisterCancelLineNumber;
        }

        public void AutoRegisterActivation(AutoRegisterStatus activate, string autoRegisterMessage, string autoRegisterKeyWord, string autoregisterLineNumber)
        {
            AutoRegister = activate;
            AutoRegisterMessage = autoRegisterMessage;
            AutoRegisterKeyWord = autoRegisterKeyWord;
            AutoRegisterLineNumber = autoregisterLineNumber;
        }
        public void AutoRegisterCancelActivation(AutoRegisterStatus activate, string autoRegisterCancelMessage, string autoRegisterCancelKeyWord, string autoregisterCancelLineNumber)
        {
            AutoRegisterCancel = activate;
            AutoRegisterCancelMessage = autoRegisterCancelMessage;
            AutoRegisterCancelKeyWord = autoRegisterCancelKeyWord;
            AutoRegisterCancelLineNumber = autoregisterCancelLineNumber;
        }
    }
}
