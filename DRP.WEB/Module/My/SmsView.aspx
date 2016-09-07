<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsView.aspx.cs" Inherits="DRP.WEB.Module.My.SmsView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>查看短消息</title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:Panel runat="server" ID="pnlContainer">
        <table class="tblEdit">
            <tr>
                <td class="rowlabel">
                    消息内容：
                </td>
                <td style="vertical-align:top; min-height:150px;">
                    <asp:Literal ID="MsgContent" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="rowlabel">
                    发送时间：
                </td>
                <td>
                    <asp:Literal ID="CreateDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="rowlabel">
                    接收手机号：
                </td>
                <td>
                    <asp:Literal ID="RecMobile" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
     </asp:Panel>
    </form>
</body>
</html>
