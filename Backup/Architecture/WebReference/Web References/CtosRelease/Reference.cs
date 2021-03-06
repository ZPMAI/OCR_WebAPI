﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5420
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 2.0.50727.5420 版自动生成。
// 
#pragma warning disable 1591

namespace CCT.WebReference.CtosRelease {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ReleaseSoap", Namespace="http://tempuri.org/")]
    public partial class Release : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ReleaseAddOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReleaseAdd_NEWOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReleaseDelOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReleaseDel_NEWOperationCompleted;
        
        private System.Threading.SendOrPostCallback VirtualEReleaseInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReleaseByCtnNOAndVesselInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback Release_B2BOperationCompleted;
        
        private System.Threading.SendOrPostCallback Release_updateAutoStateOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Release() {
            this.Url = global::CCT.WebReference.Properties.Settings.Default.CCT_WebReference_CtosRelease_Release;
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
        public event ReleaseAddCompletedEventHandler ReleaseAddCompleted;
        
        /// <remarks/>
        public event ReleaseAdd_NEWCompletedEventHandler ReleaseAdd_NEWCompleted;
        
        /// <remarks/>
        public event ReleaseDelCompletedEventHandler ReleaseDelCompleted;
        
        /// <remarks/>
        public event ReleaseDel_NEWCompletedEventHandler ReleaseDel_NEWCompleted;
        
        /// <remarks/>
        public event VirtualEReleaseInfoCompletedEventHandler VirtualEReleaseInfoCompleted;
        
        /// <remarks/>
        public event ReleaseByCtnNOAndVesselInfoCompletedEventHandler ReleaseByCtnNOAndVesselInfoCompleted;
        
        /// <remarks/>
        public event Release_B2BCompletedEventHandler Release_B2BCompleted;
        
        /// <remarks/>
        public event Release_updateAutoStateCompletedEventHandler Release_updateAutoStateCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReleaseAdd", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long ReleaseAdd(ref System.Data.DataSet releases, out string errorMessage) {
            object[] results = this.Invoke("ReleaseAdd", new object[] {
                        releases});
            releases = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void ReleaseAddAsync(System.Data.DataSet releases) {
            this.ReleaseAddAsync(releases, null);
        }
        
        /// <remarks/>
        public void ReleaseAddAsync(System.Data.DataSet releases, object userState) {
            if ((this.ReleaseAddOperationCompleted == null)) {
                this.ReleaseAddOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReleaseAddOperationCompleted);
            }
            this.InvokeAsync("ReleaseAdd", new object[] {
                        releases}, this.ReleaseAddOperationCompleted, userState);
        }
        
        private void OnReleaseAddOperationCompleted(object arg) {
            if ((this.ReleaseAddCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReleaseAddCompleted(this, new ReleaseAddCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReleaseAdd_NEW", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long ReleaseAdd_NEW(ref System.Data.DataSet releases, out string errorMessage) {
            object[] results = this.Invoke("ReleaseAdd_NEW", new object[] {
                        releases});
            releases = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void ReleaseAdd_NEWAsync(System.Data.DataSet releases) {
            this.ReleaseAdd_NEWAsync(releases, null);
        }
        
        /// <remarks/>
        public void ReleaseAdd_NEWAsync(System.Data.DataSet releases, object userState) {
            if ((this.ReleaseAdd_NEWOperationCompleted == null)) {
                this.ReleaseAdd_NEWOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReleaseAdd_NEWOperationCompleted);
            }
            this.InvokeAsync("ReleaseAdd_NEW", new object[] {
                        releases}, this.ReleaseAdd_NEWOperationCompleted, userState);
        }
        
        private void OnReleaseAdd_NEWOperationCompleted(object arg) {
            if ((this.ReleaseAdd_NEWCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReleaseAdd_NEWCompleted(this, new ReleaseAdd_NEWCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReleaseDel", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long ReleaseDel(ref System.Data.DataSet releases, out string errorMessage) {
            object[] results = this.Invoke("ReleaseDel", new object[] {
                        releases});
            releases = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void ReleaseDelAsync(System.Data.DataSet releases) {
            this.ReleaseDelAsync(releases, null);
        }
        
        /// <remarks/>
        public void ReleaseDelAsync(System.Data.DataSet releases, object userState) {
            if ((this.ReleaseDelOperationCompleted == null)) {
                this.ReleaseDelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReleaseDelOperationCompleted);
            }
            this.InvokeAsync("ReleaseDel", new object[] {
                        releases}, this.ReleaseDelOperationCompleted, userState);
        }
        
        private void OnReleaseDelOperationCompleted(object arg) {
            if ((this.ReleaseDelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReleaseDelCompleted(this, new ReleaseDelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReleaseDel_NEW", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long ReleaseDel_NEW(ref System.Data.DataSet releases, out string errorMessage) {
            object[] results = this.Invoke("ReleaseDel_NEW", new object[] {
                        releases});
            releases = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void ReleaseDel_NEWAsync(System.Data.DataSet releases) {
            this.ReleaseDel_NEWAsync(releases, null);
        }
        
        /// <remarks/>
        public void ReleaseDel_NEWAsync(System.Data.DataSet releases, object userState) {
            if ((this.ReleaseDel_NEWOperationCompleted == null)) {
                this.ReleaseDel_NEWOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReleaseDel_NEWOperationCompleted);
            }
            this.InvokeAsync("ReleaseDel_NEW", new object[] {
                        releases}, this.ReleaseDel_NEWOperationCompleted, userState);
        }
        
        private void OnReleaseDel_NEWOperationCompleted(object arg) {
            if ((this.ReleaseDel_NEWCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReleaseDel_NEWCompleted(this, new ReleaseDel_NEWCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/VirtualEReleaseInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long VirtualEReleaseInfo(ref System.Data.DataSet ds) {
            object[] results = this.Invoke("VirtualEReleaseInfo", new object[] {
                        ds});
            ds = ((System.Data.DataSet)(results[1]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void VirtualEReleaseInfoAsync(System.Data.DataSet ds) {
            this.VirtualEReleaseInfoAsync(ds, null);
        }
        
        /// <remarks/>
        public void VirtualEReleaseInfoAsync(System.Data.DataSet ds, object userState) {
            if ((this.VirtualEReleaseInfoOperationCompleted == null)) {
                this.VirtualEReleaseInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVirtualEReleaseInfoOperationCompleted);
            }
            this.InvokeAsync("VirtualEReleaseInfo", new object[] {
                        ds}, this.VirtualEReleaseInfoOperationCompleted, userState);
        }
        
        private void OnVirtualEReleaseInfoOperationCompleted(object arg) {
            if ((this.VirtualEReleaseInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VirtualEReleaseInfoCompleted(this, new VirtualEReleaseInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ReleaseByCtnNOAndVesselInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long ReleaseByCtnNOAndVesselInfo(ref System.Data.DataSet ds, out string errorMessage) {
            object[] results = this.Invoke("ReleaseByCtnNOAndVesselInfo", new object[] {
                        ds});
            ds = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void ReleaseByCtnNOAndVesselInfoAsync(System.Data.DataSet ds) {
            this.ReleaseByCtnNOAndVesselInfoAsync(ds, null);
        }
        
        /// <remarks/>
        public void ReleaseByCtnNOAndVesselInfoAsync(System.Data.DataSet ds, object userState) {
            if ((this.ReleaseByCtnNOAndVesselInfoOperationCompleted == null)) {
                this.ReleaseByCtnNOAndVesselInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReleaseByCtnNOAndVesselInfoOperationCompleted);
            }
            this.InvokeAsync("ReleaseByCtnNOAndVesselInfo", new object[] {
                        ds}, this.ReleaseByCtnNOAndVesselInfoOperationCompleted, userState);
        }
        
        private void OnReleaseByCtnNOAndVesselInfoOperationCompleted(object arg) {
            if ((this.ReleaseByCtnNOAndVesselInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReleaseByCtnNOAndVesselInfoCompleted(this, new ReleaseByCtnNOAndVesselInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Release_B2B", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long Release_B2B(ref System.Data.DataSet ds, out string errorMessage) {
            object[] results = this.Invoke("Release_B2B", new object[] {
                        ds});
            ds = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void Release_B2BAsync(System.Data.DataSet ds) {
            this.Release_B2BAsync(ds, null);
        }
        
        /// <remarks/>
        public void Release_B2BAsync(System.Data.DataSet ds, object userState) {
            if ((this.Release_B2BOperationCompleted == null)) {
                this.Release_B2BOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRelease_B2BOperationCompleted);
            }
            this.InvokeAsync("Release_B2B", new object[] {
                        ds}, this.Release_B2BOperationCompleted, userState);
        }
        
        private void OnRelease_B2BOperationCompleted(object arg) {
            if ((this.Release_B2BCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Release_B2BCompleted(this, new Release_B2BCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Release_updateAutoState", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long Release_updateAutoState(ref System.Data.DataSet ds, out string errorMessage) {
            object[] results = this.Invoke("Release_updateAutoState", new object[] {
                        ds});
            ds = ((System.Data.DataSet)(results[1]));
            errorMessage = ((string)(results[2]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void Release_updateAutoStateAsync(System.Data.DataSet ds) {
            this.Release_updateAutoStateAsync(ds, null);
        }
        
        /// <remarks/>
        public void Release_updateAutoStateAsync(System.Data.DataSet ds, object userState) {
            if ((this.Release_updateAutoStateOperationCompleted == null)) {
                this.Release_updateAutoStateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRelease_updateAutoStateOperationCompleted);
            }
            this.InvokeAsync("Release_updateAutoState", new object[] {
                        ds}, this.Release_updateAutoStateOperationCompleted, userState);
        }
        
        private void OnRelease_updateAutoStateOperationCompleted(object arg) {
            if ((this.Release_updateAutoStateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Release_updateAutoStateCompleted(this, new Release_updateAutoStateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ReleaseAddCompletedEventHandler(object sender, ReleaseAddCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReleaseAddCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReleaseAddCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet releases {
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ReleaseAdd_NEWCompletedEventHandler(object sender, ReleaseAdd_NEWCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReleaseAdd_NEWCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReleaseAdd_NEWCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet releases {
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ReleaseDelCompletedEventHandler(object sender, ReleaseDelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReleaseDelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReleaseDelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet releases {
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ReleaseDel_NEWCompletedEventHandler(object sender, ReleaseDel_NEWCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReleaseDel_NEWCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReleaseDel_NEWCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet releases {
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void VirtualEReleaseInfoCompletedEventHandler(object sender, VirtualEReleaseInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VirtualEReleaseInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VirtualEReleaseInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet ds {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ReleaseByCtnNOAndVesselInfoCompletedEventHandler(object sender, ReleaseByCtnNOAndVesselInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReleaseByCtnNOAndVesselInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReleaseByCtnNOAndVesselInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet ds {
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void Release_B2BCompletedEventHandler(object sender, Release_B2BCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Release_B2BCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Release_B2BCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet ds {
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void Release_updateAutoStateCompletedEventHandler(object sender, Release_updateAutoStateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Release_updateAutoStateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Release_updateAutoStateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet ds {
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