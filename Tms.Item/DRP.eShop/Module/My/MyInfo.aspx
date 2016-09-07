<%@ Page Title="个人中心" EnableViewState="false"  Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="DRP.eShop.Module.My.MyInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../UI/myinfo.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
        <div style="border-bottom: 1px solid #eee; padding-bottom: 10px; clear: both;">
            <div class="wrapper_img">
                <asp:Literal ID="lblPhoto" runat="server"></asp:Literal>
            </div>
            <div class="wrapper_info">
                <ul>
                    <li>姓名：<asp:Literal ID="lblUserName" runat="server"></asp:Literal> 
                    </li>
                    <li>手机号：<asp:Literal ID="lblMobile" runat="server"></asp:Literal>
                    </li>
                    <li>
                        <asp:Literal ID="lblButtons" runat="server"></asp:Literal>
                        <asp:LinkButton ID="btnExit" OnClick="btnExit_Click" runat="server">退出</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div style="clear: both;"></div>
        </div>
        <div style="padding: 10px; text-align: left;">
            <div class="mytravel">
                我的旅行足迹
            </div>
            <div>
                <img style="width: 100%;" src="../../Files/Img/trace.jpg" />
            </div>
        </div>
        <div style="border-top: 1px solid #eee; line-height: 30px; padding-top: 10px; clear: both;">
            <a class="btn btn-lg btn-primary btn-block">我的投诉记录</a>
            <a class="btn btn-lg btn-primary btn-block">我的分享记录</a>
        </div>
    </div>
</asp:Content>
