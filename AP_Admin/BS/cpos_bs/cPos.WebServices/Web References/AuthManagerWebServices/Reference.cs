﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.17929.
// 
#pragma warning disable 1591

namespace cPos.WebServices.AuthManagerWebServices {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="AuthServiceSoap", Namespace="http://tempuri.org/")]
    public partial class AuthService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetLoginUserInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCustomerDBConnectionStringOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCustomerInfoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AuthService() {
            this.Url = global::cPos.WebServices.Properties.Settings.Default.cPos_WebServices_AuthManagerWebServices_AuthService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetLoginUserInfoCompletedEventHandler GetLoginUserInfoCompleted;
        
        /// <remarks/>
        public event GetCustomerDBConnectionStringCompletedEventHandler GetCustomerDBConnectionStringCompleted;
        
        /// <remarks/>
        public event GetCustomerInfoCompletedEventHandler GetCustomerInfoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLoginUserInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetLoginUserInfo(string token) {
            object[] results = this.Invoke("GetLoginUserInfo", new object[] {
                        token});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetLoginUserInfoAsync(string token) {
            this.GetLoginUserInfoAsync(token, null);
        }
        
        /// <remarks/>
        public void GetLoginUserInfoAsync(string token, object userState) {
            if ((this.GetLoginUserInfoOperationCompleted == null)) {
                this.GetLoginUserInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetLoginUserInfoOperationCompleted);
            }
            this.InvokeAsync("GetLoginUserInfo", new object[] {
                        token}, this.GetLoginUserInfoOperationCompleted, userState);
        }
        
        private void OnGetLoginUserInfoOperationCompleted(object arg) {
            if ((this.GetLoginUserInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetLoginUserInfoCompleted(this, new GetLoginUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCustomerDBConnectionString", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetCustomerDBConnectionString(string customerID) {
            object[] results = this.Invoke("GetCustomerDBConnectionString", new object[] {
                        customerID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetCustomerDBConnectionStringAsync(string customerID) {
            this.GetCustomerDBConnectionStringAsync(customerID, null);
        }
        
        /// <remarks/>
        public void GetCustomerDBConnectionStringAsync(string customerID, object userState) {
            if ((this.GetCustomerDBConnectionStringOperationCompleted == null)) {
                this.GetCustomerDBConnectionStringOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCustomerDBConnectionStringOperationCompleted);
            }
            this.InvokeAsync("GetCustomerDBConnectionString", new object[] {
                        customerID}, this.GetCustomerDBConnectionStringOperationCompleted, userState);
        }
        
        private void OnGetCustomerDBConnectionStringOperationCompleted(object arg) {
            if ((this.GetCustomerDBConnectionStringCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCustomerDBConnectionStringCompleted(this, new GetCustomerDBConnectionStringCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCustomerInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetCustomerInfo(string customer_code) {
            object[] results = this.Invoke("GetCustomerInfo", new object[] {
                        customer_code});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetCustomerInfoAsync(string customer_code) {
            this.GetCustomerInfoAsync(customer_code, null);
        }
        
        /// <remarks/>
        public void GetCustomerInfoAsync(string customer_code, object userState) {
            if ((this.GetCustomerInfoOperationCompleted == null)) {
                this.GetCustomerInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCustomerInfoOperationCompleted);
            }
            this.InvokeAsync("GetCustomerInfo", new object[] {
                        customer_code}, this.GetCustomerInfoOperationCompleted, userState);
        }
        
        private void OnGetCustomerInfoOperationCompleted(object arg) {
            if ((this.GetCustomerInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCustomerInfoCompleted(this, new GetCustomerInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetLoginUserInfoCompletedEventHandler(object sender, GetLoginUserInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetLoginUserInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetLoginUserInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetCustomerDBConnectionStringCompletedEventHandler(object sender, GetCustomerDBConnectionStringCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCustomerDBConnectionStringCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCustomerDBConnectionStringCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetCustomerInfoCompletedEventHandler(object sender, GetCustomerInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCustomerInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCustomerInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591