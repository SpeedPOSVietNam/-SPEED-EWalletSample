﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _SPEED__EWalletSample.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"sandbox\":\"https://test-payment.momo.vn\",\"production\":\"https://payment.momo.vn\"," +
            "\"partnerCode\":\"MOMO\",\"accessKey\":\"F8BBA842ECF85\",\"secretkey\":\"K951B6PE1waDMi640x" +
            "X08PD3vg6EkVlz\"}")]
        public string Momo {
            get {
                return ((string)(this["Momo"]));
            }
            set {
                this["Momo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"{""url"":""http://14.160.87.123:18080"",""createQrcodeSecretKey"":""vnpay@MERCHANT"",""checkTransactionSecretKey"":""vnpay@123"",""appId"":""MERCHANT"",""merchantName"":""SPEED JSC"",""serviceCode"":""03"",""countryCode"":""VN"",""masterMerCode"":""A000000775"",""merchantType"":""5045"",""merchantCode"":""0305066541"",""terminalId"":""SPEED001"",""payType"":""03"",""ccy"":""704"",""expMin"":""30"",""productId"":""""}")]
        public string VNPay {
            get {
                return ((string)(this["VNPay"]));
            }
            set {
                this["VNPay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"{""url"":""https://sandbox.zalopay.com.vn"",""appId"":""15062"",""macKey"":""qk3f6zw5bbyCeP3Chp47SS1ZRiO0Vuyn"",""callbackKey"":""fNfVlVR9AzjMrrkUSXuGorWZVSUwrGIQ"",""rsaKey"":""PFJTQUtleVZhbHVlPjxNb2R1bHVzPmpwakV4bW1nQzI3Q3RtdlJKTWRYTW5WSzhaTjNSRjlGYmxEdUpCQ2lDWWVobFJGM08wSnNsMEQrREU5OFBsbThiZFcwVFFYNHJFWnhNTlhUWjBTQTdRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+""}")]
        public string ZaloPay {
            get {
                return ((string)(this["ZaloPay"]));
            }
            set {
                this["ZaloPay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("9000")]
        public string WSPort {
            get {
                return ((string)(this["WSPort"]));
            }
            set {
                this["WSPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public string ECCLevel {
            get {
                return ((string)(this["ECCLevel"]));
            }
            set {
                this["ECCLevel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("32")]
        public string QRLogoSize {
            get {
                return ((string)(this["QRLogoSize"]));
            }
            set {
                this["QRLogoSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public string PixelsPerModule {
            get {
                return ((string)(this["PixelsPerModule"]));
            }
            set {
                this["PixelsPerModule"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("200")]
        public string ResizeMax {
            get {
                return ((string)(this["ResizeMax"]));
            }
            set {
                this["ResizeMax"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public string TimeCheck {
            get {
                return ((string)(this["TimeCheck"]));
            }
            set {
                this["TimeCheck"] = value;
            }
        }
    }
}