<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogInfo.aspx.cs" Inherits="DRP.WEB.Module.Sys.LogInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>显示日志详情</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" style="padding:10px;">
            <table class="tblInfo" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="rowlabel" style="width:100px;">记录时间：
                    </td>
                    <td style="width:160px;">
                        <asp:Literal ID="LogDate" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel" style="width:100px;">操作人：
                    </td>
                    <td>
                        <asp:Literal ID="Creator" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">操作客户端IP：
                    </td>
                    <td>
                        <asp:Literal ID="IP" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel">浏览器类型：
                    </td>
                    <td>
                        <asp:Literal ID="Browser" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">日志类型：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Lv" runat="server"></asp:Literal>
                    </td>
                  
                </tr>
                <tr>
                    <td class="rowlabel">日志消息：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Message" runat="server"></asp:Literal>
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel">错误内容：
                    </td>
                    <td colspan="3" style="height:80px;">
                        <asp:Literal ID="Exception" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>

        </asp:Panel>
    </form>
</body>
</html>
