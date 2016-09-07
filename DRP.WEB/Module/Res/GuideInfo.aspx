<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuideInfo.aspx.cs" Inherits="DRP.WEB.Module.Res.GuideInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导游信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">导游名称：
                    </td>
                    <td style="width: 150px;">
                        <asp:Literal ID="Name" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel">地区：
                    </td>
                    <td>
                        <asp:Literal ID="DepartureName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">性别：
                    </td>
                    <td>
                        <asp:Literal ID="Sex" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">手机号：
                    </td>
                    <td>
                        <asp:Literal ID="Mobile" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">导游等级：
                    </td>
                    <td>
                        <asp:Literal ID="GuideLevel" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">语种：
                    </td>
                    <td>
                        <asp:Literal ID="Language" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">身份证号：
                    </td>
                    <td>
                        <asp:Literal ID="IDNum" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">特长：
                    </td>
                    <td>
                        <asp:Literal ID="Skill" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">邮箱：
                    </td>
                    <td>
                        <asp:Literal ID="Mail" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">QQ：
                    </td>
                    <td>
                        <asp:Literal ID="QQ" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">导游证：
                    </td>
                    <td>
                        <asp:Literal ID="IsIDCard" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">导游证号：
                    </td>
                    <td>
                        <asp:Literal ID="IDCardNum" runat="server"></asp:Literal>
                    </td>
                </tr>

                <tr>
                    <td class="rowlabel">领队证：
                    </td>
                    <td>
                        <asp:Literal ID="IsLeaderCard" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">状态：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="IsEnable" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开户行名：
                    </td>
                    <td>
                        <asp:Literal ID="BankName" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">开户账号：
                    </td>
                    <td>
                        <asp:Literal ID="BankAcct" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Remark" runat="server"></asp:Literal>

                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
