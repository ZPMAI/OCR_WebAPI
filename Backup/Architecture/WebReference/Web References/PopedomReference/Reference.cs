﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3625
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 2.0.50727.3625 版自动生成。
// 
#pragma warning disable 1591

namespace CCT.WebReference.PopedomReference {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PopedomServiceSoap", Namespace="http://tempuri.org/")]
    public partial class PopedomService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectUserinfoByIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectDefaultPropertyOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectPropertyByPropertyNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectDefaultPropertyByPropertyNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback HasPermissionOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckPermissionOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectEntitledUserinfoByCompanyNoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PopedomService() {
            this.Url = global::CCT.WebReference.Properties.Settings.Default.CCT_WebReference_PopedomReference_PopedomService;
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
        public event LoginCompletedEventHandler LoginCompleted;
        
        /// <remarks/>
        public event SelectUserinfoByIDCompletedEventHandler SelectUserinfoByIDCompleted;
        
        /// <remarks/>
        public event SelectDefaultPropertyCompletedEventHandler SelectDefaultPropertyCompleted;
        
        /// <remarks/>
        public event SelectPropertyByPropertyNameCompletedEventHandler SelectPropertyByPropertyNameCompleted;
        
        /// <remarks/>
        public event SelectDefaultPropertyByPropertyNameCompletedEventHandler SelectDefaultPropertyByPropertyNameCompleted;
        
        /// <remarks/>
        public event HasPermissionCompletedEventHandler HasPermissionCompleted;
        
        /// <remarks/>
        public event CheckPermissionCompletedEventHandler CheckPermissionCompleted;
        
        /// <remarks/>
        public event SelectEntitledUserinfoByCompanyNoCompletedEventHandler SelectEntitledUserinfoByCompanyNoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Login", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet Login(string companyNo, string username, string password) {
            object[] results = this.Invoke("Login", new object[] {
                        companyNo,
                        username,
                        password});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void LoginAsync(string companyNo, string username, string password) {
            this.LoginAsync(companyNo, username, password, null);
        }
        
        /// <remarks/>
        public void LoginAsync(string companyNo, string username, string password, object userState) {
            if ((this.LoginOperationCompleted == null)) {
                this.LoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            this.InvokeAsync("Login", new object[] {
                        companyNo,
                        username,
                        password}, this.LoginOperationCompleted, userState);
        }
        
        private void OnLoginOperationCompleted(object arg) {
            if ((this.LoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginCompleted(this, new LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectUserinfoByID", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectUserinfoByID(int userinfoID) {
            object[] results = this.Invoke("SelectUserinfoByID", new object[] {
                        userinfoID});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectUserinfoByIDAsync(int userinfoID) {
            this.SelectUserinfoByIDAsync(userinfoID, null);
        }
        
        /// <remarks/>
        public void SelectUserinfoByIDAsync(int userinfoID, object userState) {
            if ((this.SelectUserinfoByIDOperationCompleted == null)) {
                this.SelectUserinfoByIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectUserinfoByIDOperationCompleted);
            }
            this.InvokeAsync("SelectUserinfoByID", new object[] {
                        userinfoID}, this.SelectUserinfoByIDOperationCompleted, userState);
        }
        
        private void OnSelectUserinfoByIDOperationCompleted(object arg) {
            if ((this.SelectUserinfoByIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectUserinfoByIDCompleted(this, new SelectUserinfoByIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectDefaultProperty", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectDefaultProperty(int userinfoID, int moduleID) {
            object[] results = this.Invoke("SelectDefaultProperty", new object[] {
                        userinfoID,
                        moduleID});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectDefaultPropertyAsync(int userinfoID, int moduleID) {
            this.SelectDefaultPropertyAsync(userinfoID, moduleID, null);
        }
        
        /// <remarks/>
        public void SelectDefaultPropertyAsync(int userinfoID, int moduleID, object userState) {
            if ((this.SelectDefaultPropertyOperationCompleted == null)) {
                this.SelectDefaultPropertyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectDefaultPropertyOperationCompleted);
            }
            this.InvokeAsync("SelectDefaultProperty", new object[] {
                        userinfoID,
                        moduleID}, this.SelectDefaultPropertyOperationCompleted, userState);
        }
        
        private void OnSelectDefaultPropertyOperationCompleted(object arg) {
            if ((this.SelectDefaultPropertyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectDefaultPropertyCompleted(this, new SelectDefaultPropertyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectPropertyByPropertyName", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectPropertyByPropertyName(int userinfoID, int moduleID, string propertyName) {
            object[] results = this.Invoke("SelectPropertyByPropertyName", new object[] {
                        userinfoID,
                        moduleID,
                        propertyName});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectPropertyByPropertyNameAsync(int userinfoID, int moduleID, string propertyName) {
            this.SelectPropertyByPropertyNameAsync(userinfoID, moduleID, propertyName, null);
        }
        
        /// <remarks/>
        public void SelectPropertyByPropertyNameAsync(int userinfoID, int moduleID, string propertyName, object userState) {
            if ((this.SelectPropertyByPropertyNameOperationCompleted == null)) {
                this.SelectPropertyByPropertyNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectPropertyByPropertyNameOperationCompleted);
            }
            this.InvokeAsync("SelectPropertyByPropertyName", new object[] {
                        userinfoID,
                        moduleID,
                        propertyName}, this.SelectPropertyByPropertyNameOperationCompleted, userState);
        }
        
        private void OnSelectPropertyByPropertyNameOperationCompleted(object arg) {
            if ((this.SelectPropertyByPropertyNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectPropertyByPropertyNameCompleted(this, new SelectPropertyByPropertyNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectDefaultPropertyByPropertyName", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectDefaultPropertyByPropertyName(int userinfoID, int moduleID, string propertyName) {
            object[] results = this.Invoke("SelectDefaultPropertyByPropertyName", new object[] {
                        userinfoID,
                        moduleID,
                        propertyName});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectDefaultPropertyByPropertyNameAsync(int userinfoID, int moduleID, string propertyName) {
            this.SelectDefaultPropertyByPropertyNameAsync(userinfoID, moduleID, propertyName, null);
        }
        
        /// <remarks/>
        public void SelectDefaultPropertyByPropertyNameAsync(int userinfoID, int moduleID, string propertyName, object userState) {
            if ((this.SelectDefaultPropertyByPropertyNameOperationCompleted == null)) {
                this.SelectDefaultPropertyByPropertyNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectDefaultPropertyByPropertyNameOperationCompleted);
            }
            this.InvokeAsync("SelectDefaultPropertyByPropertyName", new object[] {
                        userinfoID,
                        moduleID,
                        propertyName}, this.SelectDefaultPropertyByPropertyNameOperationCompleted, userState);
        }
        
        private void OnSelectDefaultPropertyByPropertyNameOperationCompleted(object arg) {
            if ((this.SelectDefaultPropertyByPropertyNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectDefaultPropertyByPropertyNameCompleted(this, new SelectDefaultPropertyByPropertyNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HasPermission", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool HasPermission(int userinfoID, int moduleID) {
            object[] results = this.Invoke("HasPermission", new object[] {
                        userinfoID,
                        moduleID});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void HasPermissionAsync(int userinfoID, int moduleID) {
            this.HasPermissionAsync(userinfoID, moduleID, null);
        }
        
        /// <remarks/>
        public void HasPermissionAsync(int userinfoID, int moduleID, object userState) {
            if ((this.HasPermissionOperationCompleted == null)) {
                this.HasPermissionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHasPermissionOperationCompleted);
            }
            this.InvokeAsync("HasPermission", new object[] {
                        userinfoID,
                        moduleID}, this.HasPermissionOperationCompleted, userState);
        }
        
        private void OnHasPermissionOperationCompleted(object arg) {
            if ((this.HasPermissionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HasPermissionCompleted(this, new HasPermissionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckPermission", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckPermission([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] permission, int moduleID) {
            object[] results = this.Invoke("CheckPermission", new object[] {
                        permission,
                        moduleID});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CheckPermissionAsync(byte[] permission, int moduleID) {
            this.CheckPermissionAsync(permission, moduleID, null);
        }
        
        /// <remarks/>
        public void CheckPermissionAsync(byte[] permission, int moduleID, object userState) {
            if ((this.CheckPermissionOperationCompleted == null)) {
                this.CheckPermissionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckPermissionOperationCompleted);
            }
            this.InvokeAsync("CheckPermission", new object[] {
                        permission,
                        moduleID}, this.CheckPermissionOperationCompleted, userState);
        }
        
        private void OnCheckPermissionOperationCompleted(object arg) {
            if ((this.CheckPermissionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckPermissionCompleted(this, new CheckPermissionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectEntitledUserinfoByCompanyNo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectEntitledUserinfoByCompanyNo(string companyNo, int moduleID) {
            object[] results = this.Invoke("SelectEntitledUserinfoByCompanyNo", new object[] {
                        companyNo,
                        moduleID});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectEntitledUserinfoByCompanyNoAsync(string companyNo, int moduleID) {
            this.SelectEntitledUserinfoByCompanyNoAsync(companyNo, moduleID, null);
        }
        
        /// <remarks/>
        public void SelectEntitledUserinfoByCompanyNoAsync(string companyNo, int moduleID, object userState) {
            if ((this.SelectEntitledUserinfoByCompanyNoOperationCompleted == null)) {
                this.SelectEntitledUserinfoByCompanyNoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectEntitledUserinfoByCompanyNoOperationCompleted);
            }
            this.InvokeAsync("SelectEntitledUserinfoByCompanyNo", new object[] {
                        companyNo,
                        moduleID}, this.SelectEntitledUserinfoByCompanyNoOperationCompleted, userState);
        }
        
        private void OnSelectEntitledUserinfoByCompanyNoOperationCompleted(object arg) {
            if ((this.SelectEntitledUserinfoByCompanyNoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectEntitledUserinfoByCompanyNoCompleted(this, new SelectEntitledUserinfoByCompanyNoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void SelectUserinfoByIDCompletedEventHandler(object sender, SelectUserinfoByIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectUserinfoByIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectUserinfoByIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void SelectDefaultPropertyCompletedEventHandler(object sender, SelectDefaultPropertyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectDefaultPropertyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectDefaultPropertyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void SelectPropertyByPropertyNameCompletedEventHandler(object sender, SelectPropertyByPropertyNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectPropertyByPropertyNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectPropertyByPropertyNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void SelectDefaultPropertyByPropertyNameCompletedEventHandler(object sender, SelectDefaultPropertyByPropertyNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectDefaultPropertyByPropertyNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectDefaultPropertyByPropertyNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void HasPermissionCompletedEventHandler(object sender, HasPermissionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HasPermissionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HasPermissionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void CheckPermissionCompletedEventHandler(object sender, CheckPermissionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckPermissionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckPermissionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void SelectEntitledUserinfoByCompanyNoCompletedEventHandler(object sender, SelectEntitledUserinfoByCompanyNoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectEntitledUserinfoByCompanyNoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectEntitledUserinfoByCompanyNoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591