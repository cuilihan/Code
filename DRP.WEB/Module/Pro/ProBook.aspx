<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="ProBook.aspx.cs" Inherits="DRP.WEB.Module.Pro.ProBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品预订</title>
    <link href="../../Scripts/Plugin/lhgdialog/skins/default.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/lhgdialog/lhgdialog.min.js?skin=iblue" type="text/javascript"></script>
    <link href="Css/ProBook.css" rel="stylesheet" />
    <script src="../Order/Script/OrderUtility.js" type="text/javascript"></script>
    <script src="Script/ProBook.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" title="产品预订" iconcls="icon-reload" style="padding: 10px 30px 10px 10px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel">预订产品：</td>
                    <td colspan="5" style="font-weight: bold;">【<asp:Literal ID="TourNo" runat="server"></asp:Literal>】<asp:Literal ID="TourName" runat="server"></asp:Literal>
                        <span style="padding-left: 2em;">【<a href="ProNav.aspx" style="color: blue; text-decoration: underline; font-weight: normal;">重新选择产品</a>】</span>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">出团日期：
                    </td>
                    <td>
                        <asp:Literal ID="TourDate" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">回程日期：
                    </td>
                    <td>
                        <asp:Literal ID="ReturnDate" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">目的地：
                    </td>
                    <td>
                        <asp:Literal ID="Destination" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">出发地：
                    </td>
                    <td>
                        <asp:DropDownList ID="Departure" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">上车地点：
                    </td>
                    <td>
                        <asp:DropDownList ID="TourVenue" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">订单来源：
                    </td>
                    <td>
                        <asp:DropDownList ID="OrderSource" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="padding: 10px;">
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td id="cellSeat" style="vertical-align: top; width: 210px;">
                                    <table class="seatchart">
                                        <tr>
                                            <td class="seatchart_title" style="height: 22px;" colspan="5">
                                                <span>在线选座</span></td>
                                        </tr>
                                        <tbody id="seatNum">
                                            <!--座位表-->
                                        </tbody>
                                        <tr>
                                            <td colspan="5" class="seatchart_footer">
                                                <span class="seatchart_footer_chose">已选座</span>
                                                <span class="seatchart_footer_used">已占座</span>
                                                 <span class="seatchart_footer_lock">预留座</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top;">
                                    <table class="tblEdit">
                                        <tr>
                                            <th style="width: 30px;"></th>
                                            <th>价格名称
                                            </th>
                                            <th style="width: 50px;">占座  
                                            </th>
                                            <th style="width: 80px;">销售价
                                            </th>
                                            <th style="width: 70px;">单房差
                                            </th>
                                            <th style="width: 70px;">报名人数
                                            </th>
                                            <th style="width: 70px;">单房差金额
                                            </th>
                                            <th style="width: 70px;">保险总金额
                                            </th>
                                            <th style="width: 70px;">小计
                                            </th>
                                            <th style="width: 80px;">保险总成本
                                            </th>
                                        </tr>
                                        <tbody id="tblPrice">
                                            <asp:Repeater runat="server" ID="rptPrice" OnItemDataBound="rptPrice_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <input type="hidden" value="<%# Eval("ID") %>" />
                                                            <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                                        </td>
                                                        <td><%# Eval("Name") %></td>
                                                        <td style="text-align: center;">
                                                            <input type="hidden" value="<%# Eval("IsSeat") %>" />
                                                            <%# Convert.ToBoolean(Eval("IsSeat"))?"√":"<span style='color:red;'>x</span>" %>
                                                        </td>
                                                        <td style="text-align: right;"><%# Eval("SalePrice") %></td>
                                                        <td style="text-align: right;"><%# Eval("RoomRate") %></td>
                                                        <td>
                                                            <input type="text" name="calculate" class="textbox checkInt" style="width: 60px; text-align: center; height: 26px;" />
                                                        </td>
                                                        <td>
                                                            <input type="text" name="calculate" class="textbox checkInt" style="width: 60px; text-align: center; height: 26px;" /></td>
                                                        <td>
                                                            <input type="text" name="calculate" class="textbox checkInt" style="width: 60px; text-align: center; height: 26px;" /></td>
                                                        <td style="text-align: right;"></td>
                                                        <td>
                                                            <input type="text" name="insuranceCost" class="textbox checkInt" style="width: 70px; text-align: center; height: 26px;" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <table class="tblEdit" style="margin-top: 10px;">
                                        <tr>
                                            <th style="width: 30px;"></th>
                                            <th style="width: 100px;">姓名
                                            </th>
                                            <th style="width: 70px;">性别  
                                            </th>
                                            <th style="width: 140px;">电话
                                            </th>
                                            <th style="width: 130px;">身份证号
                                            </th>
                                            <th style="width: 140px;">公司名称
                                            </th>
                                            <th>备注
                                            </th>
                                            <th style="width: 40px;"></th>
                                        </tr>
                                        <tbody id="tblCustomer">
                                        </tbody>
                                        <tr>
                                            <td colspan="8" style="text-align: right; padding: 5px;">
                                                <a href="javascript:;" id="btnSelectCustomer" iconcls="icon-ok" class="easyui-linkbutton">选择客户</a>
                                                <a href="javascript:;" id="btnAddCustomer" iconcls="icon-add" class="easyui-linkbutton">增加客户</a>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">订单备注：</td>
                    <td colspan="5">
                        <asp:TextBox ID="Comment" Style="overflow: auto;" ClientIDMode="Static" TextMode="MultiLine" EnableTheming="false" CssClass="textbox" Width="90%" Height="40" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="orderAmt">
                        <div>
                            接送费用：<span>
                                <asp:Label ID="lblBusAmt" runat="server">0</asp:Label>
                            </span>
                        </div>
                        <div style="margin: 5px 0px;">
                            订单金额：<span>
                                <asp:Label ID="lblOrderAmt" runat="server">0</asp:Label>
                            </span>
                        </div>
                        <div>
                            调整金额：<span><asp:TextBox ID="AdjustAmt" ClientIDMode="Static" EnableTheming="false" CssClass="textbox" Style="text-align: right; height: 26px; width: 50px; padding-right: 4px;" runat="server"></asp:TextBox>
                            </span>
                        </div>
                        <div style="border-bottom: 1px solid #ccc; margin-top: 5px; margin-bottom: 2px;"></div>
                        <div style="border-top: 1px solid #ccc; font-size: 15px; padding-top: 5px; font-weight: bold;">
                            订单实收款：<span class="tourprice" id="OrderAmt">0</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center; padding: 10px 0px;">
                        <div id="btnOpt">
                            <input type="button" id="btnSave" value="" class="btnBook" />
                        </div>
                        <div id="tips" class="hide">
                            <div>
                                <img src="../../UI/themes/default/images/loading.gif" />
                            </div>
                            <div style="margin-top: 10px; color: Red; font-size: 20px;">
                                正在处理订单，请稍候...
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfOrderStatus" ClientIDMode="Static" Value="2" runat="server" />
            <asp:HiddenField ID="hfSeatNum" ClientIDMode="Static" Value="0" runat="server" />
        </div>
    </form>
</body>
</html>
