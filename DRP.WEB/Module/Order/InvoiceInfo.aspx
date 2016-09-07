<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceInfo.aspx.cs" Inherits="DRP.WEB.Module.Order.InvoiceInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看发票信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlContainer" runat="server">
            <div style="font-weight: bold; text-align: right; margin-bottom: 4px;">
                发票状态：
                <asp:Literal ID="InvoiceStatus" runat="server"></asp:Literal>
            </div>
            <table class="tblInfo">
                <tr>
                    <td colspan="4" style="font-weight: bold;">一、 发票申请信息
                        <span style="padding-left: 1em;">【操作人：<asp:Literal ID="CreateUserName" runat="server"></asp:Literal>
                            <span style="padding-left: 1em;">
                                <asp:Literal ID="CreateDate" runat="server"></asp:Literal></span>】 </span>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">发票抬头：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceName" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">发票金额：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceAmt" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">发票内容：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceItem" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">领取方式：
                    </td>
                    <td>
                        <asp:Literal ID="FetchType" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开票单位：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="InvoiceUnit" runat="server"></asp:Literal>
                    </td>

                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Comment" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="font-weight: bold;">二、 财务开票信息
                        <span style="padding-left: 1em;">【操作人：<asp:Literal ID="Auditor" runat="server"></asp:Literal>
                            <span style="padding-left: 1em;">
                                <asp:Literal ID="AuditDate" runat="server"></asp:Literal></span>】 </span>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">发票编号：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceNo" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">开票日期：
                    </td>
                    <td>
                        <asp:Literal ID="InvoiceDate" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="AuditRemark" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="font-weight: bold;">三、 开票相关的订单 
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding: 10px;">
                        <table id="tblData" class="tblEdit">
                            <tr>
                                <th style="width: 30px;"></th>
                                <th style="width: 140px;">订单编号</th>
                                <th style="width: 100px;">订单类型</th>
                                <th>订单名称</th>
                                <th style="width: 150px;">客户名称</th>

                                <th style="width: 90px;">订单金额</th>
                                <th style="width: 90px;">订单成本</th>
                                <th style="width: 90px;">订单毛利</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <%# Eval("OrderNo") %>
                                        </td>
                                        <td>
                                            <asp:Literal ID="lblOrderType" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <%# Eval("OrderName") %>
                                        </td>
                                        <td>
                                            <%# Eval("CustomerName") %>
                                        </td>

                                        <td style="text-align:right;">
                                            <%# Eval("OrderAmt") %>
                                        </td>
                                        <td style="text-align:right;">
                                            <%# Eval("OrderCost") %>
                                        </td>
                                        <td style="text-align:right;">
                                            <%# Convert.ToDecimal(Eval("OrderAmt"))-Convert.ToDecimal(Eval("OrderCost")) %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
