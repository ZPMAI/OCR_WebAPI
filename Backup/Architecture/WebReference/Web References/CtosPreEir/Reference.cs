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

namespace CCT.WebReference.CtosPreEir {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="PreEirSoap", Namespace="http://tempuri.org/")]
    public partial class PreEir : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback PreEirImportManageOperationCompleted;
        
        private System.Threading.SendOrPostCallback PreEirExportManageOperationCompleted;
        
        private System.Threading.SendOrPostCallback PreEirEmptyManageOperationCompleted;
        
        private System.Threading.SendOrPostCallback PreEirCombineManageOperationCompleted;
        
        private System.Threading.SendOrPostCallback PreEirCancelOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PreEir() {
            this.Url = global::CCT.WebReference.Properties.Settings.Default.CCT_WebReference_CtosPreEir_PreEir;
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
        public event PreEirImportManageCompletedEventHandler PreEirImportManageCompleted;
        
        /// <remarks/>
        public event PreEirExportManageCompletedEventHandler PreEirExportManageCompleted;
        
        /// <remarks/>
        public event PreEirEmptyManageCompletedEventHandler PreEirEmptyManageCompleted;
        
        /// <remarks/>
        public event PreEirCombineManageCompletedEventHandler PreEirCombineManageCompleted;
        
        /// <remarks/>
        public event PreEirCancelCompletedEventHandler PreEirCancelCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PreEirImportManage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long PreEirImportManage(ref System.Data.DataSet preEirs, out string errorMessage) {
            object[] results = this.Invoke("PreEirImportManage", new object[] {
                        preEirs});
            preEirs = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void PreEirImportManageAsync(System.Data.DataSet preEirs) {
            this.PreEirImportManageAsync(preEirs, null);
        }
        
        /// <remarks/>
        public void PreEirImportManageAsync(System.Data.DataSet preEirs, object userState) {
            if ((this.PreEirImportManageOperationCompleted == null)) {
                this.PreEirImportManageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPreEirImportManageOperationCompleted);
            }
            this.InvokeAsync("PreEirImportManage", new object[] {
                        preEirs}, this.PreEirImportManageOperationCompleted, userState);
        }
        
        private void OnPreEirImportManageOperationCompleted(object arg) {
            if ((this.PreEirImportManageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PreEirImportManageCompleted(this, new PreEirImportManageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PreEirExportManage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long PreEirExportManage(ref System.Data.DataSet preEirs, out string errorMessage) {
            object[] results = this.Invoke("PreEirExportManage", new object[] {
                        preEirs});
            preEirs = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void PreEirExportManageAsync(System.Data.DataSet preEirs) {
            this.PreEirExportManageAsync(preEirs, null);
        }
        
        /// <remarks/>
        public void PreEirExportManageAsync(System.Data.DataSet preEirs, object userState) {
            if ((this.PreEirExportManageOperationCompleted == null)) {
                this.PreEirExportManageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPreEirExportManageOperationCompleted);
            }
            this.InvokeAsync("PreEirExportManage", new object[] {
                        preEirs}, this.PreEirExportManageOperationCompleted, userState);
        }
        
        private void OnPreEirExportManageOperationCompleted(object arg) {
            if ((this.PreEirExportManageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PreEirExportManageCompleted(this, new PreEirExportManageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PreEirEmptyManage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long PreEirEmptyManage(ref System.Data.DataSet preEirs, out string errorMessage) {
            object[] results = this.Invoke("PreEirEmptyManage", new object[] {
                        preEirs});
            preEirs = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void PreEirEmptyManageAsync(System.Data.DataSet preEirs) {
            this.PreEirEmptyManageAsync(preEirs, null);
        }
        
        /// <remarks/>
        public void PreEirEmptyManageAsync(System.Data.DataSet preEirs, object userState) {
            if ((this.PreEirEmptyManageOperationCompleted == null)) {
                this.PreEirEmptyManageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPreEirEmptyManageOperationCompleted);
            }
            this.InvokeAsync("PreEirEmptyManage", new object[] {
                        preEirs}, this.PreEirEmptyManageOperationCompleted, userState);
        }
        
        private void OnPreEirEmptyManageOperationCompleted(object arg) {
            if ((this.PreEirEmptyManageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PreEirEmptyManageCompleted(this, new PreEirEmptyManageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PreEirCombineManage", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long PreEirCombineManage(ref System.Data.DataSet preEirs, out string errorMessage) {
            object[] results = this.Invoke("PreEirCombineManage", new object[] {
                        preEirs});
            preEirs = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void PreEirCombineManageAsync(System.Data.DataSet preEirs) {
            this.PreEirCombineManageAsync(preEirs, null);
        }
        
        /// <remarks/>
        public void PreEirCombineManageAsync(System.Data.DataSet preEirs, object userState) {
            if ((this.PreEirCombineManageOperationCompleted == null)) {
                this.PreEirCombineManageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPreEirCombineManageOperationCompleted);
            }
            this.InvokeAsync("PreEirCombineManage", new object[] {
                        preEirs}, this.PreEirCombineManageOperationCompleted, userState);
        }
        
        private void OnPreEirCombineManageOperationCompleted(object arg) {
            if ((this.PreEirCombineManageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PreEirCombineManageCompleted(this, new PreEirCombineManageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PreEirCancel", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long PreEirCancel(ref System.Data.DataSet preEirs, out string errorMessage) {
            object[] results = this.Invoke("PreEirCancel", new object[] {
                        preEirs});
            preEirs = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void PreEirCancelAsync(System.Data.DataSet preEirs) {
            this.PreEirCancelAsync(preEirs, null);
        }
        
        /// <remarks/>
        public void PreEirCancelAsync(System.Data.DataSet preEirs, object userState) {
            if ((this.PreEirCancelOperationCompleted == null)) {
                this.PreEirCancelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPreEirCancelOperationCompleted);
            }
            this.InvokeAsync("PreEirCancel", new object[] {
                        preEirs}, this.PreEirCancelOperationCompleted, userState);
        }
        
        private void OnPreEirCancelOperationCompleted(object arg) {
            if ((this.PreEirCancelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PreEirCancelCompleted(this, new PreEirCancelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void PreEirImportManageCompletedEventHandler(object sender, PreEirImportManageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PreEirImportManageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PreEirImportManageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataSet preEirs {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void PreEirExportManageCompletedEventHandler(object sender, PreEirExportManageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PreEirExportManageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PreEirExportManageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataSet preEirs {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void PreEirEmptyManageCompletedEventHandler(object sender, PreEirEmptyManageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PreEirEmptyManageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PreEirEmptyManageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataSet preEirs {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void PreEirCombineManageCompletedEventHandler(object sender, PreEirCombineManageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PreEirCombineManageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PreEirCombineManageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataSet preEirs {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void PreEirCancelCompletedEventHandler(object sender, PreEirCancelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PreEirCancelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PreEirCancelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataSet preEirs {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string errorMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
}

#pragma warning restore 1591