﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行库版本:2.0.50727.42
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 2.0.50727.42 版自动生成。
// 
#pragma warning disable 1591

namespace CCT.WebReference.CtosOutYard {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="OutYardSoap", Namespace="http://tempuri.org/")]
    public partial class OutYard : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ConfirmWorkitemD2YOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConfirmWorkitemY2DOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public OutYard() {
            this.Url = global::CCT.WebReference.Properties.Settings.Default.CCT_WebReference_CtosOutYard_OutYard;
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
        public event ConfirmWorkitemD2YCompletedEventHandler ConfirmWorkitemD2YCompleted;
        
        /// <remarks/>
        public event ConfirmWorkitemY2DCompletedEventHandler ConfirmWorkitemY2DCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConfirmWorkitemD2Y", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long ConfirmWorkitemD2Y(ref System.Data.DataSet outYard) {
            object[] results = this.Invoke("ConfirmWorkitemD2Y", new object[] {
                        outYard});
            outYard = ((System.Data.DataSet)(results[1]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void ConfirmWorkitemD2YAsync(System.Data.DataSet outYard) {
            this.ConfirmWorkitemD2YAsync(outYard, null);
        }
        
        /// <remarks/>
        public void ConfirmWorkitemD2YAsync(System.Data.DataSet outYard, object userState) {
            if ((this.ConfirmWorkitemD2YOperationCompleted == null)) {
                this.ConfirmWorkitemD2YOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmWorkitemD2YOperationCompleted);
            }
            this.InvokeAsync("ConfirmWorkitemD2Y", new object[] {
                        outYard}, this.ConfirmWorkitemD2YOperationCompleted, userState);
        }
        
        private void OnConfirmWorkitemD2YOperationCompleted(object arg) {
            if ((this.ConfirmWorkitemD2YCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmWorkitemD2YCompleted(this, new ConfirmWorkitemD2YCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConfirmWorkitemY2D", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long ConfirmWorkitemY2D(ref System.Data.DataSet outYard) {
            object[] results = this.Invoke("ConfirmWorkitemY2D", new object[] {
                        outYard});
            outYard = ((System.Data.DataSet)(results[1]));
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void ConfirmWorkitemY2DAsync(System.Data.DataSet outYard) {
            this.ConfirmWorkitemY2DAsync(outYard, null);
        }
        
        /// <remarks/>
        public void ConfirmWorkitemY2DAsync(System.Data.DataSet outYard, object userState) {
            if ((this.ConfirmWorkitemY2DOperationCompleted == null)) {
                this.ConfirmWorkitemY2DOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmWorkitemY2DOperationCompleted);
            }
            this.InvokeAsync("ConfirmWorkitemY2D", new object[] {
                        outYard}, this.ConfirmWorkitemY2DOperationCompleted, userState);
        }
        
        private void OnConfirmWorkitemY2DOperationCompleted(object arg) {
            if ((this.ConfirmWorkitemY2DCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmWorkitemY2DCompleted(this, new ConfirmWorkitemY2DCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void ConfirmWorkitemD2YCompletedEventHandler(object sender, ConfirmWorkitemD2YCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmWorkitemD2YCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConfirmWorkitemD2YCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet outYard {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void ConfirmWorkitemY2DCompletedEventHandler(object sender, ConfirmWorkitemY2DCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmWorkitemY2DCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConfirmWorkitemY2DCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public System.Data.DataSet outYard {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591