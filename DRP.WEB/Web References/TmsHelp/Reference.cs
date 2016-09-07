﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace DRP.WEB.TmsHelp {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceInfoSoap", Namespace="http://tempuri.org/")]
    public partial class WebServiceInfo : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetHelpInfobyPageIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback QueryByConditionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebServiceInfo() {
            this.Url = global::DRP.WEB.Properties.Settings.Default.DRP_WEB_TmsHelp_WebServiceInfo;
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
        public event GetHelpInfobyPageIDCompletedEventHandler GetHelpInfobyPageIDCompleted;
        
        /// <remarks/>
        public event QueryByConditionCompletedEventHandler QueryByConditionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetHelpInfobyPageID", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetHelpInfobyPageID(string pageID) {
            object[] results = this.Invoke("GetHelpInfobyPageID", new object[] {
                        pageID});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetHelpInfobyPageIDAsync(string pageID) {
            this.GetHelpInfobyPageIDAsync(pageID, null);
        }
        
        /// <remarks/>
        public void GetHelpInfobyPageIDAsync(string pageID, object userState) {
            if ((this.GetHelpInfobyPageIDOperationCompleted == null)) {
                this.GetHelpInfobyPageIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetHelpInfobyPageIDOperationCompleted);
            }
            this.InvokeAsync("GetHelpInfobyPageID", new object[] {
                        pageID}, this.GetHelpInfobyPageIDOperationCompleted, userState);
        }
        
        private void OnGetHelpInfobyPageIDOperationCompleted(object arg) {
            if ((this.GetHelpInfobyPageIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetHelpInfobyPageIDCompleted(this, new GetHelpInfobyPageIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/QueryByCondition", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable QueryByCondition(string subject) {
            object[] results = this.Invoke("QueryByCondition", new object[] {
                        subject});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void QueryByConditionAsync(string subject) {
            this.QueryByConditionAsync(subject, null);
        }
        
        /// <remarks/>
        public void QueryByConditionAsync(string subject, object userState) {
            if ((this.QueryByConditionOperationCompleted == null)) {
                this.QueryByConditionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnQueryByConditionOperationCompleted);
            }
            this.InvokeAsync("QueryByCondition", new object[] {
                        subject}, this.QueryByConditionOperationCompleted, userState);
        }
        
        private void OnQueryByConditionOperationCompleted(object arg) {
            if ((this.QueryByConditionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.QueryByConditionCompleted(this, new QueryByConditionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void GetHelpInfobyPageIDCompletedEventHandler(object sender, GetHelpInfobyPageIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetHelpInfobyPageIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetHelpInfobyPageIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void QueryByConditionCompletedEventHandler(object sender, QueryByConditionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class QueryByConditionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal QueryByConditionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591