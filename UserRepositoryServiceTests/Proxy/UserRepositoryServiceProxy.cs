﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserRepositoryServiceTests.Proxy
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserInfo", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Data.DTO")]
    public partial class UserInfo : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Nullable<bool> AdvertisingOptInField;
        
        private UserRepositoryServiceTests.Proxy.ContactInfo ContactField;
        
        private string CountryIsoCodeField;
        
        private System.DateTime DateModifiedField;
        
        private string LocaleField;
        
        private System.Guid UserIdField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> AdvertisingOptIn
        {
            get
            {
                return this.AdvertisingOptInField;
            }
            set
            {
                this.AdvertisingOptInField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UserRepositoryServiceTests.Proxy.ContactInfo Contact
        {
            get
            {
                return this.ContactField;
            }
            set
            {
                this.ContactField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CountryIsoCode
        {
            get
            {
                return this.CountryIsoCodeField;
            }
            set
            {
                this.CountryIsoCodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateModified
        {
            get
            {
                return this.DateModifiedField;
            }
            set
            {
                this.DateModifiedField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Locale
        {
            get
            {
                return this.LocaleField;
            }
            set
            {
                this.LocaleField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid UserId
        {
            get
            {
                return this.UserIdField;
            }
            set
            {
                this.UserIdField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ContactInfo", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Data.DTO")]
    public partial class ContactInfo : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string EmailField;
        
        private string PhoneNumberField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneNumber
        {
            get
            {
                return this.PhoneNumberField;
            }
            set
            {
                this.PhoneNumberField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PagingParams", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Models")]
    public partial class PagingParams : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Nullable<UserRepositoryServiceTests.Proxy.FilterRuleFilterBy> FilterByField;
        
        private System.Nullable<UserRepositoryServiceTests.Proxy.OrderRuleOrderBy> OrderByField;
        
        private System.Nullable<int> PageNumberField;
        
        private System.Nullable<int> PageSizeField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<UserRepositoryServiceTests.Proxy.FilterRuleFilterBy> FilterBy
        {
            get
            {
                return this.FilterByField;
            }
            set
            {
                this.FilterByField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<UserRepositoryServiceTests.Proxy.OrderRuleOrderBy> OrderBy
        {
            get
            {
                return this.OrderByField;
            }
            set
            {
                this.OrderByField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> PageNumber
        {
            get
            {
                return this.PageNumberField;
            }
            set
            {
                this.PageNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> PageSize
        {
            get
            {
                return this.PageSizeField;
            }
            set
            {
                this.PageSizeField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FilterRule.FilterBy", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Models")]
    public enum FilterRuleFilterBy : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AdvertisingOptIn = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CountryIsoCode = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OrderRule.OrderBy", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Models")]
    public enum OrderRuleOrderBy : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DateModified = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Locale = 1,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserNotFoundFault", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Faults")]
    public partial class UserNotFoundFault : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid IdField;
        
        private string ReasonField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Reason
        {
            get
            {
                return this.ReasonField;
            }
            set
            {
                this.ReasonField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserRepositoryServiceTests.Proxy.IUserInfoProviderService")]
    public interface IUserInfoProviderService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserInfoProviderService/GetUserInfo", ReplyAction="http://tempuri.org/IUserInfoProviderService/GetUserInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(UserRepositoryServiceTests.Proxy.UserNotFoundFault), Action="http://tempuri.org/IUserInfoProviderService/GetUserInfoUserNotFoundFaultFault", Name="UserNotFoundFault", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Faults")]
        UserRepositoryServiceTests.Proxy.UserInfo GetUserInfo(System.Guid userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserInfoProviderService/GetUserInfoList", ReplyAction="http://tempuri.org/IUserInfoProviderService/GetUserInfoListResponse")]
        UserRepositoryServiceTests.Proxy.UserInfo[] GetUserInfoList(UserRepositoryServiceTests.Proxy.PagingParams pagingParams);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserInfoProviderService/UpdateUserInfo", ReplyAction="http://tempuri.org/IUserInfoProviderService/UpdateUserInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(UserRepositoryServiceTests.Proxy.UserNotFoundFault), Action="http://tempuri.org/IUserInfoProviderService/UpdateUserInfoUserNotFoundFaultFault", Name="UserNotFoundFault", Namespace="http://schemas.datacontract.org/2004/07/UserRepositoryServiceApp.Faults")]
        void UpdateUserInfo(UserRepositoryServiceTests.Proxy.UserInfo user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserInfoProviderServiceChannel : UserRepositoryServiceTests.Proxy.IUserInfoProviderService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserInfoProviderServiceClient : System.ServiceModel.ClientBase<UserRepositoryServiceTests.Proxy.IUserInfoProviderService>, UserRepositoryServiceTests.Proxy.IUserInfoProviderService
    {
        
        public UserInfoProviderServiceClient()
        {
        }
        
        public UserInfoProviderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public UserInfoProviderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public UserInfoProviderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public UserInfoProviderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public UserRepositoryServiceTests.Proxy.UserInfo GetUserInfo(System.Guid userId)
        {
            return base.Channel.GetUserInfo(userId);
        }
        
        public UserRepositoryServiceTests.Proxy.UserInfo[] GetUserInfoList(UserRepositoryServiceTests.Proxy.PagingParams pagingParams)
        {
            return base.Channel.GetUserInfoList(pagingParams);
        }
        
        public void UpdateUserInfo(UserRepositoryServiceTests.Proxy.UserInfo user)
        {
            base.Channel.UpdateUserInfo(user);
        }
    }
}
