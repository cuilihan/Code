<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInvoice.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单开票申请</title>
    <script src="Script/OrderInvoice.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 5px 10px;">

        <h3 style="color: red; margin: 10px 2px 5px 2px;">一、待开票订单</h3>
        <table class="tblEdit">
            <tr>
                <th style="width: 140px;">订单编号</th>
                <th>订单名称</th>
                <th style="width: 100px;">订单日期</th>
                <th>客户名称</th>
                <th style="width: 100px;">订单金额</th>
                <th style="width: 100px;">已开票金额</th>
                <th style="width: 100px;">未开票金额</th>
            </tr>
            <tbody id="tblInvoice">
                <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <%# Eval("OrderNo") %>
                                <input type="hidden" value="<%# Eval("ID") %>" />
                            </td>
                            <td>
                                <%# Eval("OrderName") %>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("TourDate") %>
                            </td>
                            <td>
                                <%# Eval("CustomerName") %>
                            </td>
                            <td style="text-align: right;">
                                <%# Eval("OrderAmt") %>
                            </td>
                            <td style="text-align: right;">
                                <%# Eval("OrderInvoiceAmt") %>
                            </td>
                            <td>
                                <asp:Literal ID="lblInput" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <h3 style="color: red; margin: 20px 2px 5px 2px;">二、开票信息</h3>
        <table class="tblEdit">
            <tr>
                <td class="rowlabel"><span class="red">*</span>开票金额：
                </td>
                <td colspan="3" id="InvoiceAmt" style="color: red; font-weight: bold; font-family: Arial; font-size: 14px;">0</td>
            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>开票抬头：
                </td>
                <td>
                    <asp:TextBox ID="InvoiceName" data-options="required:true" Width="80%" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
                <td class="rowlabel"><span class="red">*</span>开票单位：
                </td>
                <td>
                    <asp:TextBox ID="InvoiceUnit" data-options="required:true" Width="80%" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>开票项目：
                </td>
                <td>
                    <asp:DropDownList ID="InvoiceItem" Width="150" data-options="required:true" runat="server" ClientIDMode="Static" AppendDataBoundItems="true">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="rowlabel">领取方式：
                </td>
                <td>
                    <asp:DropDownList ID="FetchType" Width="150" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="rowlabel">开票备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="Comment" ClientIDMode="Static" EnableTheming="false" Width="90%" Height="60" TextMode="MultiLine" CssClass="textbox" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin: 20px 0px;">
            <div id="btnOpt">
                <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" id="btnSave">提交开票信息</a>
            </div>
            <div style="color: Red;" id="tips" class="hide">
                <img src="../../UI/themes/default/images/loading.gif" />
                正在提交数据，请稍候... 
            </div>
        </div>
    </form>
</body>
</html>
