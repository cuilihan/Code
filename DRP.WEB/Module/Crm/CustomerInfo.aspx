<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfo.aspx.cs" Inherits="DRP.WEB.Module.Crm.CustomerInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户信息</title>
    <script src="Script/CustomerInfo.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel">客户名称：
                    </td>
                    <td>
                        <asp:Literal ID="Name" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">英文名称：
                    </td>
                    <td>
                        <asp:Literal ID="EngName" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel">性别：
                    </td>
                    <td>
                        <asp:Literal ID="Sex" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">手机号码：
                    </td>
                    <td>
                        <asp:Literal ID="Mobile" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">办公电话：
                    </td>
                    <td>
                        <asp:Literal ID="Phone" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">传真号码：
                    </td>
                    <td>
                        <asp:Literal ID="Fax" runat="server"></asp:Literal>
                    </td>

                </tr>
                <tr>
                    <td class="rowlabel">身份证号：
                    </td>
                    <td>
                        <asp:Literal ID="IDNum" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">客户类型：
                    </td>
                    <td>
                        <asp:Literal ID="CustomerType" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">QQ：
                    </td>
                    <td>
                        <asp:Literal ID="QQ" runat="server"></asp:Literal>
                    </td>

                </tr>
                <tr>
                    <td class="rowlabel">公司名称：
                    </td>
                    <td colspan="5">
                        <asp:Literal ID="Company" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">证件信息：
                    </td>
                    <td colspan="5" style="padding: 10px;">
                        <table class="tblEdit">
                            <tr>
                                <th style="width: 200px;">证件类型</th>
                                <th>证件号码</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptCert">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;"><%# Eval("ItemType") %></td>
                                        <td><%# Eval("ItemVal") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">地址：
                    </td>
                    <td colspan="5">
                        <asp:Literal ID="Addr" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="5">
                        <asp:Literal ID="Remark" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div class="history" id="customervisit">
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
