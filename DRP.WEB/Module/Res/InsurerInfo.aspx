<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsurerInfo.aspx.cs" Inherits="DRP.WEB.Module.Res.InsurerInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblInfo">

                <tr>
                    <td class="rowlabel">保险公司名称：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Name" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel">状态：
                    </td>
                    <td>
                        <asp:Literal ID="IsEnable" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">负责人：
                    </td>
                    <td>
                        <asp:Literal ID="Contact" runat="server"></asp:Literal>
                    </td>

                    <td class="rowlabel">手机号码：
                    </td>
                    <td>
                        <asp:Literal ID="Mobile" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">联系电话：
                    </td>
                    <td>
                        <asp:Literal ID="Phone" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">联系地址：
                    </td>
                    <td colspan="5">
                        <asp:Literal ID="Addr" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">签证区域：
                    </td>
                    <td colspan="5">
                        <asp:Literal ID="Remark" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="padding: 10px;">
                        <ul class="ul_tabs" id="ul_tabs">
                            <li class="thistab"><a>业务联系人</a></li>
                        </ul>
                        <div style="padding: 10px;">
                            <table class="tblItem">
                                <tr>
                                    <th style="width: 25px;">序
                                    </th>
                                    <th style="width: 100px;">姓名
                                    </th>
                                    <th style="width: 150px;">电话</th>
                                    <th style="width: 150px;">传真</th>
                                    <th>备注</th>
                                </tr>

                                <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                            </td>
                                            <td><%# Eval("Name") %></td>
                                            <td>
                                                <%# Eval("Phone") %></td>
                                            <td>
                                                <%# Eval("Fax") %>
                                            </td>
                                            <td>
                                                <%# Eval("Remark") %> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
