<%@ Page Title="" Language="C#" MasterPageFile="~/Module/CheckAccount/ChkAcct.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DRP.WEB.Module.CheckAccount.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CheckAccount.css" rel="stylesheet" />
    <script src="Script/BalanceAccount.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="subject">
        <div class="subject_t">
            <span>在线报账
            </span>
        </div>
        <div style="padding: 20px 50px;">
            <div style="font-family: 微软雅黑; text-align: center; font-size: 20px; margin: 40px 0px 20px 0px;">
                 <asp:Literal ID="OrderName" runat="server"></asp:Literal>
            </div>
            
            <div style="width: 140px; height: 30px; float: right;">
                <span style="font-weight:bold;">出团日期</span>：
            <asp:Literal ID="TourDate" Text="" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint" style="margin-top: 5px;">
                <tbody id="tblItem">
                </tbody>
            </table>
            <div style="height: 5px;"></div>
            <div style="width: 200px; height: 30px; float: left;">
                导游名称：
            <asp:Label ClientIDMode="Static" ID="GuideName" runat="server"></asp:Label>
                <asp:Label ID="GuidePhone" runat="server" Text=""></asp:Label>
            </div>
            <div style="width: 140px; height: 30px; float: right;">
                报账日期：
            <asp:Literal ID="CreateDate" Text="" runat="server"></asp:Literal>
            </div>
            <div style="clear: both;"></div>
            <div style="margin: 20px; text-align: center;">
                <div style="text-align: center;" id="btnOpt">
                    <a id="btnSave" runat="server" clientidmode="Static" href="javascript:;"
                        class="btnSave">提交报账数据</a>
                </div>
                <div style="color: Red;" id="tips" class="hide">
                    正在提交数据，请稍候...                     
                </div>
                <asp:HiddenField ID="GuideDrawAmount" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HasUpLoadData" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="OrderID" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="OrderGuideID" ClientIDMode="Static" runat="server" />
                <asp:Label ID="lblTips" ClientIDMode="Static" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
