<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderZZB.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderZZB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自主班订单修改</title>
    <link href="../../Scripts/Plugin/lhgdialog/skins/default.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/lhgdialog/lhgdialog.min.js?skin=iblue" type="text/javascript"></script>
    <link href="../Pro/Css/ProBook.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="Script/OrderUtility.js"></script>
    <script src="Script/OrderZZB.js"></script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 10px;">
        <table class="tblInfo">
            <tr>
                <td class="rowlabel">订单名称：</td>
                <td colspan="5" style="font-weight: bold;">【<asp:Literal ID="OrderNo" runat="server"></asp:Literal>】
                    <asp:Literal ID="OrderName" runat="server"></asp:Literal>
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
                                            <%--<span class="seatchart_footer_lock">预留座</span>--%>
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
                                                        <input type="hidden" value="<%# Eval("TourPriceID") %>" />
                                                        <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                                    </td>
                                                    <td><%# Eval("Name") %></td>
                                                    <td style="text-align: center;">
                                                        <%# Convert.ToBoolean(Eval("IsSeat"))?"√":"<span style='color:red;'>x</span>" %>
                                                    </td>
                                                    <td style="text-align: right;"><%# Eval("SalePrice") %></td>
                                                    <td style="text-align: right;"><%# Eval("RoomRate") %></td>
                                                    <td>
                                                        <input type="text" value="<%# Eval("VisitorNum") %>" name="calculate" class="textbox checkInt" style="width: 60px; text-align: center; height: 26px;" />
                                                    </td>
                                                    <td>
                                                        <input type="text" value="<%# Eval("RoomRate") %>" name="calculate" class="textbox checkInt" style="width: 60px; text-align: center; height: 26px;" /></td>
                                                    <td>
                                                        <input type="text" value="<%# Eval("InsuranceAmt") %>" name="calculate" class="textbox checkInt" style="width: 60px; text-align: center; height: 26px;" /></td>
                                                    <td style="text-align: right;"></td>
                                                    <td>
                                                        <input type="text" value="<%# Eval("InsuranceCost") %>" name="insuranceCost" class="textbox checkInt" style="width: 70px; text-align: center; height: 26px;" /></td>
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
                                        <th style="width: 200px;">公司名称</th>
                                        <th>备注
                                        </th>
                                        <th style="width: 40px;"></th>
                                    </tr>
                                    <tbody id="tblCustomer">
                                        <asp:Repeater runat="server" ID="rptCustomer" OnItemDataBound="rptCustomer_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style='text-align: center;'>
                                                        <asp:Literal ID="lblNo" runat="server"></asp:Literal><input type='hidden' value="<%# Eval("CustomerID") %>" />
                                                    </td>
                                                    <td>
                                                        <input type='text' class='textbox' style='height: 26px; width: 110px;' value="<%# Eval("Name") %>" /></td>
                                                    <td>
                                                        <asp:Literal ID="lblSex" runat="server"></asp:Literal>
                                                        <td>
                                                            <input type='text' class='textbox' value="<%# Eval("Mobile") %>" style='height: 26px; width: 100px;' /></td>
                                                        <td>
                                                            <input type='text' class='textbox' value="<%# Eval("IDNo") %>" style='height: 26px; width: 130px;' /></td>
                                                        <td>
                                                            <input type='text' class='textbox' value="<%# Eval("Company") %>" style='height: 26px; width: 98%;' /></td>
                                                        <td>
                                                            <input type='text' class='textbox' value="<%# Eval("Comment") %>" style='height: 26px; width: 98%;' /></td>
                                                        <td><a href='javascript:;' onclick="comm.fnDeleteCustomer(this)">删除</a></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
            <tr id="Participant" style="display:none" runat="server">
                <td class="rowlabel">参与人员：</td>
                <td colspan="5">
                     <asp:HiddenField ID="hfDept" ClientIDMode="Static" runat="server" />
                     <asp:HiddenField ID="hfEmployee" ClientIDMode="Static" runat="server" />
                    <input class="easyui-combotree" id="Dept" style="height: 26px; width: 120px;" runat="server" />
                    <input class="easyui-combobox" id="Employee" style="height: 26px; width: 120px;" runat="server" />
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
                    <td class="rowlabel">上传附件：
                    </td>
                    <td colspan="5" style="padding: 10px;">
                        <div>
                            <input type="file" name="uploadify" id="uploadify" runat="server" />
                        </div>
                        <table class="tblEdit" cellpadding="1" cellspacing="1">
                            <tr>
                                <th>文件名称
                                </th>
                                <th style="width: 90px;">文件类型
                                </th>
                                <th style="width: 90px;">文件大小
                                </th>
                                <th style="width: 120px;">上传日期
                                </th>
                                <th style="width: 80px;">上传人
                                </th>
                                <th style="width: 45px;"></th>
                            </tr>
                            <tbody id="tblFile">
                                <asp:Repeater runat="server" ID="rptFile">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type="hidden" value='<%# Eval("ID") %>' />
                                                <a href='<%# Eval("FilePath") %>' target='_blank'>
                                                    <%# Eval("FileName") %></a>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("FileType") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("FileSize") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("CreateDate") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("CreateUserName") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <a href="javascript:;" onclick="comm.fnDeleteFile(this)">删除</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
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
        <asp:HiddenField ID="HiddenField1" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="HiddenField2" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="VenueName" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="SendAmt" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="CollectTime" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="PickAmt" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hfSeatNum" ClientIDMode="Static" Value="0" runat="server" />
        <asp:HiddenField ID="TourID" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="hfOrderStatus" ClientIDMode="Static" runat="server" />
    </form>
</body>
</html>
