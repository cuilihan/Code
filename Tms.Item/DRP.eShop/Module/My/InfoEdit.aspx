<%@ Page Title="" Language="C#" MasterPageFile="~/eShop.Master" AutoEventWireup="true" CodeBehind="InfoEdit.aspx.cs" Inherits="DRP.eShop.Module.My.InfoEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                var mobile = $("#Mobile").val();
                if (mobile == "") {
                    $("#lblTips").text("手机号不能为空");
                    return false;
                }
                if (mobile.length != 11) {
                    $("#lblTips").text("手机号格式错误");
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 10px 5px;">
        <div style="margin-bottom: 10px;">
            <asp:TextBox ID="Mobile" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="手机号"></asp:TextBox>
        </div>
        <div style="margin-bottom: 10px;">
            <asp:TextBox ID="NickName" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="昵称"></asp:TextBox>
        </div>
        <div style="margin-bottom: 10px;">
            <asp:TextBox ID="UserName" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="姓名"></asp:TextBox>
        </div>
        <div style="margin-bottom: 10px;">
            <asp:TextBox ID="IDNo" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="身份证号"></asp:TextBox>
        </div>
        <div class="checkbox" style="color: red;">
            <div style="text-align: left; margin-bottom: 5px;">
                手机号码为必填项目
            </div>

            <asp:Label ClientIDMode="Static" ID="lblTips" runat="server"></asp:Label>
        </div>
        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-lg btn-primary btn-block" ClientIDMode="Static" OnClick="btnSave_Click" Text="保存" />

    </div>
</asp:Content>
