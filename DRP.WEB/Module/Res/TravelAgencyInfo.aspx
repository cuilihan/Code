<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelAgencyInfo.aspx.cs" Inherits="DRP.WEB.Module.Res.TravelAgencyInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看综合供应商</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel">区域：</td>
                    <td colspan="5">【<asp:Literal ID="RouteType" runat="server"></asp:Literal>】 
                        <asp:Literal ID="Destination" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">供应商名称：
                    </td>
                    <td>
                        <asp:Literal ID="Name" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">品牌或简称：
                    </td>
                    <td>
                        <asp:Literal ID="Brand" runat="server"></asp:Literal>
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
                    <td class="rowlabel">职务：
                    </td>
                    <td>
                        <asp:Literal ID="Title" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">手机号码：
                    </td>
                    <td>
                        <asp:Literal ID="Mobile" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">联系电话：
                    </td>
                    <td>
                        <asp:Literal ID="Phone" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">传真号码：
                    </td>
                    <td>
                        <asp:Literal ID="Fax" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">QQ：
                    </td>
                    <td>
                        <asp:Literal ID="QQ" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开户行：
                    </td>
                    <td>
                        <asp:Literal ID="BankName" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">开户账号：
                    </td>
                    <td>
                        <asp:Literal ID="BankAcct" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">联系地址：
                    </td>
                    <td>
                        <asp:Literal ID="Addr" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">业务类型：
                    </td>
                    <td colspan="5">
                        <asp:Literal ID="BizType" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注说明：
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
