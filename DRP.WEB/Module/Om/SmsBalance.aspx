<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsBalance.aspx.cs" Inherits="DRP.WEB.Module.Om.SmsAalance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>短信余额查询</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" title="短信余额查询" iconcls="icon-reload" style="padding: 10px;">
            <fieldset>
                <legend style="padding-left: 10px; font-weight: bold;">短信账号余额查询</legend>
                <%--   <div style="padding: 10px 20px; line-height: 25px;">
                    <div style="border-bottom: 1px dotted #ccc; font-weight: bold; margin-bottom: 5px;">模板类</div>
                    账号：<asp:Label ID="lblAcct_T" runat="server" Text=""></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;余额：
                    <asp:Label ID="lblBalance_T" runat="server" Text="0"></asp:Label>
                </div>--%>
                <div style="padding: 10px 20px; line-height: 25px;">
                    <div style="border-bottom: 1px dotted #ccc; font-weight: bold; margin-bottom: 5px;">通用短信</div>
                    账号：<asp:Label ID="lblAcct_N" runat="server" Text=""></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;余额：
                    <asp:Label ID="lblBalance_N" runat="server" Text="0"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="btnStatus" runat="server" OnClick="btnStatus_Click">状态报告查询</asp:LinkButton>
                    <div>
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div style="padding: 10px 20px; line-height: 25px;">
                    <div style="border-bottom: 1px dotted #ccc; font-weight: bold; margin-bottom: 5px;">模板短信测试</div>
                    手机号：<asp:TextBox ID="Mobile" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnSmsT_Click">发送</asp:LinkButton>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
