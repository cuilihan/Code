<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DRP.eShop.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="UI/index.css" rel="stylesheet" />
    <script src="Script/jquery.slides.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#slides').slidesjs({
                navigation: {
                    active: false
                },
                play: {
                    auto: true,
                    interval: 2000,
                    swap: true
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin: 10px 0px">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:TextBox ID="txtKey" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="请输入目的地或关键字查询"></asp:TextBox>
                </td>
                <td>
                    <a class="btn btn-lg btn-primary btn-block" style="font-size: 12px; margin-left: 2px;">查询</a>
                </td>
            </tr>
        </table>
    </div>
    <div class="container" style="margin: 10px 0px">
        <div id="slides">
            <asp:Literal ID="lblImages" runat="server"></asp:Literal>
        </div>
    </div>
    <div style="text-align: center; padding: 5px; line-height: 25px;">
        <div>
            <asp:Image ID="ImgLogo" Visible="false" runat="server" />
        </div>
        <div style="font-size: 15px; font-family: 微软雅黑;">
            <asp:Literal ID="lblTravelName" runat="server"></asp:Literal>
        </div>
        <div style="font-size: 15px; font-family: 微软雅黑;">
            服务热线：<span style="color: #ff6a00; font-size: 15px; font-weight: bold; font-family: Arial;"><asp:Literal ID="lblPhone" runat="server"></asp:Literal></span>
        </div>
        <div class="mytravel">
            <asp:Literal ID="lblMyTravel" runat="server"></asp:Literal>
        </div>
        <div style="margin: 10px 0px 10px 0px;">
            如果您还没有绑定账号，请点击此处“<asp:Literal ID="lblReg" runat="server"></asp:Literal>”
        </div>
        <div class="menus">
            <ul>
                <asp:Literal ID="lblMenus" runat="server"></asp:Literal>
            </ul>
            <div style="clear: both;"></div>
        </div>
        <div class="buttons">
            <asp:Literal ID="lblButtons" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>
