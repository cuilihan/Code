<%@ Page Title="" Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="DRP.eShop.Module.My.Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../UI/photo.css" rel="stylesheet" />
    <%--  <script src="../../Script/Uploadify/jquery.uploadify-3.1.min.js"></script>--%>
    <%--   <link href="../../Script/Uploadify/uploadify.css" rel="stylesheet" />--%>
    <script src="Script/Photo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="padding: 10px 5px;">

        <div class="wrapper_img">
            <div>
                当前头像
            </div>
            <asp:Literal ID="lblPhoto" runat="server"></asp:Literal>
            <div id="tips" style="color: red;"></div>
        </div>

        <%-- <fieldset>
            <legend>自定义
            </legend>
            <div style="text-align: center; padding: 15px 0px 5px 10px; margin: 0 auto;">
                <input type="file" name="uploadify" id="uploadify" />
                <div style="color: red; padding-top: 8px; margin-bottom: 20px;">
                    注：图片大小为100px * 100px;
                </div>
            </div>
        </fieldset>--%>
        <div id="SysPhoto">
            <fieldset>
                <legend>男生
                </legend>
                <div>
                    <asp:Literal ID="lblMalePhoto" runat="server"></asp:Literal>
                </div>
            </fieldset>
            <fieldset>
                <legend>女生
                </legend>
                <div>
                    <asp:Literal ID="lblFemalePhoto" runat="server"></asp:Literal>
                </div>
            </fieldset>
            <fieldset>
                <legend>卡通
                </legend>
                <div>
                    <asp:Literal ID="lblOtherPhoto" runat="server"></asp:Literal>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
