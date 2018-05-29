﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3615
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 2.0.50727.3615 版自动生成。
// 
#pragma warning disable 1591

namespace CCT.WebReference.EHRReference {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectByUsernameOperationCompleted;
        
        private System.Threading.SendOrPostCallback HasPermissionOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckPermissionOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectAppGroupModuleOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelectAppGroupModuleByStaffIDOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::CCT.WebReference.Properties.Settings.Default.CCT_WebReference_EHRReference_Service;
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
        public event SelectByUsernameCompletedEventHandler SelectByUsernameCompleted;
        
        /// <remarks/>
        public event HasPermissionCompletedEventHandler HasPermissionCompleted;
        
        /// <remarks/>
        public event CheckPermissionCompletedEventHandler CheckPermissionCompleted;
        
        /// <remarks/>
        public event SelectAppGroupModuleCompletedEventHandler SelectAppGroupModuleCompleted;
        
        /// <remarks/>
        public event SelectAppGroupModuleByStaffIDCompletedEventHandler SelectAppGroupModuleByStaffIDCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Login", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet Login(string username, string password) {
            object[] results = this.Invoke("Login", new object[] {
                        username,
                        password});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void LoginAsync(string username, string password) {
            this.LoginAsync(username, password, null);
        }
        
        /// <remarks/>
        public void LoginAsync(string username, string password, object userState) {
            if ((this.LoginOperationCompleted == null)) {
                this.LoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            this.InvokeAsync("Login", new object[] {
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectByUsername", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectByUsername(string username) {
            object[] results = this.Invoke("SelectByUsername", new object[] {
                        username});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectByUsernameAsync(string username) {
            this.SelectByUsernameAsync(username, null);
        }
        
        /// <remarks/>
        public void SelectByUsernameAsync(string username, object userState) {
            if ((this.SelectByUsernameOperationCompleted == null)) {
                this.SelectByUsernameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectByUsernameOperationCompleted);
            }
            this.InvokeAsync("SelectByUsername", new object[] {
                        username}, this.SelectByUsernameOperationCompleted, userState);
        }
        
        private void OnSelectByUsernameOperationCompleted(object arg) {
            if ((this.SelectByUsernameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectByUsernameCompleted(this, new SelectByUsernameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HasPermission", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool HasPermission(int staffID, int moduleID) {
            object[] results = this.Invoke("HasPermission", new object[] {
                        staffID,
                        moduleID});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void HasPermissionAsync(int staffID, int moduleID) {
            this.HasPermissionAsync(staffID, moduleID, null);
        }
        
        /// <remarks/>
        public void HasPermissionAsync(int staffID, int moduleID, object userState) {
            if ((this.HasPermissionOperationCompleted == null)) {
                this.HasPermissionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHasPermissionOperationCompleted);
            }
            this.InvokeAsync("HasPermission", new object[] {
                        staffID,
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectAppGroupModule", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectAppGroupModule(int appID, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] permission) {
            object[] results = this.Invoke("SelectAppGroupModule", new object[] {
                        appID,
                        permission});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectAppGroupModuleAsync(int appID, byte[] permission) {
            this.SelectAppGroupModuleAsync(appID, permission, null);
        }
        
        /// <remarks/>
        public void SelectAppGroupModuleAsync(int appID, byte[] permission, object userState) {
            if ((this.SelectAppGroupModuleOperationCompleted == null)) {
                this.SelectAppGroupModuleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectAppGroupModuleOperationCompleted);
            }
            this.InvokeAsync("SelectAppGroupModule", new object[] {
                        appID,
                        permission}, this.SelectAppGroupModuleOperationCompleted, userState);
        }
        
        private void OnSelectAppGroupModuleOperationCompleted(object arg) {
            if ((this.SelectAppGroupModuleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectAppGroupModuleCompleted(this, new SelectAppGroupModuleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectAppGroupModuleByStaffID", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet SelectAppGroupModuleByStaffID(int staffID, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] permission) {
            object[] results = this.Invoke("SelectAppGroupModuleByStaffID", new object[] {
                        staffID,
                        permission});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void SelectAppGroupModuleByStaffIDAsync(int staffID, byte[] permission) {
            this.SelectAppGroupModuleByStaffIDAsync(staffID, permission, null);
        }
        
        /// <remarks/>
        public void SelectAppGroupModuleByStaffIDAsync(int staffID, byte[] permission, object userState) {
            if ((this.SelectAppGroupModuleByStaffIDOperationCompleted == null)) {
                this.SelectAppGroupModuleByStaffIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectAppGroupModuleByStaffIDOperationCompleted);
            }
            this.InvokeAsync("SelectAppGroupModuleByStaffID", new object[] {
                        staffID,
                        permission}, this.SelectAppGroupModuleByStaffIDOperationCompleted, userState);
        }
        
        private void OnSelectAppGroupModuleByStaffIDOperationCompleted(object arg) {
            if ((this.SelectAppGroupModuleByStaffIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectAppGroupModuleByStaffIDCompleted(this, new SelectAppGroupModuleByStaffIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void SelectByUsernameCompletedEventHandler(object sender, SelectByUsernameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectByUsernameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectByUsernameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void SelectAppGroupModuleCompletedEventHandler(object sender, SelectAppGroupModuleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectAppGroupModuleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectAppGroupModuleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void SelectAppGroupModuleByStaffIDCompletedEventHandler(object sender, SelectAppGroupModuleByStaffIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectAppGroupModuleByStaffIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectAppGroupModuleByStaffIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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