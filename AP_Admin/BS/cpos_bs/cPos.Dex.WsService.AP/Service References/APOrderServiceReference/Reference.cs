﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cPos.Dex.WsService.AP.APOrderServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="APOrderServiceReference.OrderServiceSoap")]
    public interface OrderServiceSoap {
        
        // CODEGEN: Generating message contract since element name HelloWorldResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldResponse HelloWorld(cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldRequest request);
        
        // CODEGEN: Generating message contract since element name userId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SetCCOrderList", ReplyAction="*")]
        cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListResponse SetCCOrderList(cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SetCCOrderListRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SetCCOrderList", Namespace="http://tempuri.org/", Order=0)]
        public cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListRequestBody Body;
        
        public SetCCOrderListRequest() {
        }
        
        public SetCCOrderListRequest(cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SetCCOrderListRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string userId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string type;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string orderList;
        
        public SetCCOrderListRequestBody() {
        }
        
        public SetCCOrderListRequestBody(string userId, string type, string orderList) {
            this.userId = userId;
            this.type = type;
            this.orderList = orderList;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SetCCOrderListResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SetCCOrderListResponse", Namespace="http://tempuri.org/", Order=0)]
        public cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListResponseBody Body;
        
        public SetCCOrderListResponse() {
        }
        
        public SetCCOrderListResponse(cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SetCCOrderListResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SetCCOrderListResult;
        
        public SetCCOrderListResponseBody() {
        }
        
        public SetCCOrderListResponseBody(string SetCCOrderListResult) {
            this.SetCCOrderListResult = SetCCOrderListResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface OrderServiceSoapChannel : cPos.Dex.WsService.AP.APOrderServiceReference.OrderServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceSoapClient : System.ServiceModel.ClientBase<cPos.Dex.WsService.AP.APOrderServiceReference.OrderServiceSoap>, cPos.Dex.WsService.AP.APOrderServiceReference.OrderServiceSoap {
        
        public OrderServiceSoapClient() {
        }
        
        public OrderServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldResponse cPos.Dex.WsService.AP.APOrderServiceReference.OrderServiceSoap.HelloWorld(cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldRequest inValue = new cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldRequest();
            inValue.Body = new cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldRequestBody();
            cPos.Dex.WsService.AP.APOrderServiceReference.HelloWorldResponse retVal = ((cPos.Dex.WsService.AP.APOrderServiceReference.OrderServiceSoap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListResponse cPos.Dex.WsService.AP.APOrderServiceReference.OrderServiceSoap.SetCCOrderList(cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListRequest request) {
            return base.Channel.SetCCOrderList(request);
        }
        
        public string SetCCOrderList(string userId, string type, string orderList) {
            cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListRequest inValue = new cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListRequest();
            inValue.Body = new cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListRequestBody();
            inValue.Body.userId = userId;
            inValue.Body.type = type;
            inValue.Body.orderList = orderList;
            cPos.Dex.WsService.AP.APOrderServiceReference.SetCCOrderListResponse retVal = ((cPos.Dex.WsService.AP.APOrderServiceReference.OrderServiceSoap)(this)).SetCCOrderList(inValue);
            return retVal.Body.SetCCOrderListResult;
        }
    }
}
