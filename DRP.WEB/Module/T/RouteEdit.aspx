<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteEdit.aspx.cs" Inherits="DRP.WEB.Module.T.RouteEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>线路模板维护</title>
    <script src="../../Scripts/Plugin/kindeditor/kindeditor-min.js"></script>
    <script src="Script/RouteEdit.js" type="text/javascript"></script>
    <style type="text/css">
        .tourprice
        {
            display: inline-block;
            font-size: 15px;
            font-weight: bold;
            color: red;
            width: 100px;
            margin-right: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px 5px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>线路名称：</td>
                    <td colspan="3">
                        <asp:TextBox ID="RouteName" runat="server" Width="90%" data-options="required:true" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td class="rowlabel">线路编号：</td>
                    <td>
                        <asp:TextBox ID="RouteNo" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>线路类型：</td>
                    <td>
                        <asp:DropDownList ID="RouteType" Width="150" ClientIDMode="Static" AppendDataBoundItems="true" data-options="required:true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>目的地：</td>
                    <td>
                        <asp:HiddenField ID="DestinationID" ClientIDMode="Static" runat="server" />
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Destination"></asp:TextBox>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>行程天数：</td>
                    <td>
                        <asp:TextBox ID="Days" ClientIDMode="Static" data-options="required:true,validType:'int'" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>团队人数：</td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" onblur='r.fnCalculateAmount()' ID="VisitorNum" data-options="required:true,validType:'int'"></asp:TextBox>
                    </td>
                    <td class="rowlabel">途径景点：</td>
                    <td colspan="3">
                        <asp:TextBox ID="ViewSpot" runat="server" Width="90%" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="6" style="padding: 10px; vertical-align: top;">
                        <ul class="ul_tabs" id="ul_tabs">
                            <li><a>线路行色</a></li>
                            <li><a>行程安排</a></li>
                            <li><a>服务标准</a></li>
                            <li><a>自费、购物说明</a></li>
                            <li><a>特别提醒</a></li>
                            <li><a>备注</a></li>
                        </ul>
                        <ul id="tab_conbox">
                            <li class="tab_con">
                                <asp:TextBox ID="Feature" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                            <li class="tab_con">
                                <!--行程-->
                                <div id="tblSchedule">
                                    <asp:Repeater runat="server" ID="rptSchedule" OnItemDataBound="rptSchedule_ItemDataBound">
                                        <ItemTemplate>
                                            <table class="tblItem" style='margin-bottom: 15px;'>
                                                <tr>
                                                    <td class='rowlabel_90'>第<span name='d'><asp:Literal ID="lblDayNum" runat="server"></asp:Literal></span>天</td>
                                                    <td>
                                                        <input type='text' class='textbox' style="width: 90%; height: 24px;" value="<%# Eval("Title") %>" /></td>

                                                    <td rowspan='4' style='text-align: center; width: 30px;'><a href='javascript:;' onclick="r.fnDeleteSchedule(this)">删除</a></td>
                                                </tr>
                                                <tr>
                                                    <td class='rowlabel_90'>行程</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="" EnableTheming="false"
                                                            Style="width: 90%; height: 160px; padding: 8px 5px; text-align: left; overflow: auto;" Text='<%# Eval("Schedule").ToString().Trim() %>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='rowlabel_90'>住宿</td>
                                                    <td>
                                                        <input type='text' value="<%# Eval("Stay") %>" class='textbox' style="width: 90%; height: 24px;" /></td>
                                                </tr>
                                                <tr>
                                                    <td class='rowlabel_90'>用餐</td>
                                                    <td>
                                                        <input type='text' value="<%# Eval("Dinner") %>" class='textbox' style="width: 90%; height: 24px;" /></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div style="text-align: right; padding-right: 10px 0px 10px 0px;">
                                    <a href="javascript:;" id="btnAddDay" class="easyui-linkbutton" iconcls="icon-add">增加一天行程</a>
                                </div>
                            </li>
                            <li class="tab_con">
                                <table class="tblItem">
                                    <tr>
                                        <th style="width: 30px;"></th>
                                        <th style="width: 150px;">服务项目</th>
                                        <th>服务内容</th>
                                        <th style="width: 60px;">单价</th>
                                        <th style="width: 60px;">数量</th>
                                        <th style="width: 80px;">本项累计</th>
                                        <th style="width: 50px;"></th>
                                    </tr>
                                    <tbody id="tblServiceItem">
                                        <asp:Repeater runat="server" ID="rptItem" OnItemDataBound="rptItem_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style='text-align: center;'>
                                                        <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                                                    <td>
                                                        <asp:Literal ID="lblItemName" runat="server"></asp:Literal></td>
                                                    <td>
                                                        <input type='text' class='textbox' value="<%# Eval("ItemRemark") %>" style='width: 96%; height: 26px;' /></td>
                                                    <td>
                                                        <input type='text' value="<%# Eval("ItemPrice") %>" onblur='r.fnCalculateAmount()' class='textbox checkInt' style='width: 50px; height: 26px; text-align: center;' /></td>
                                                    <td>
                                                        <input type='text' value="<%# Eval("ItemNum") %>" onblur='r.fnCalculateAmount()' class='textbox checkInt' style='width: 50px; height: 26px; text-align: center;' /></td>
                                                    <td style='text-align: right;'><%# Eval("ItemSum") %></td>
                                                    <td style='text-align: center;'><a href='javascript:;' onclick='r.fnDeleteServiceItem(this)'>删除</a></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tbody>
                                </table>
                                <div style="text-align: right; padding: 10px 0px 10px 0px;">
                                    <a href="javascript:;" id="btnAddServiceItem" class="easyui-linkbutton" iconcls="icon-add">增加服务标准</a>
                                </div>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="SelfItem" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="Notes" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="Comment" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>预期整团毛利：</td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Profit" onblur='r.fnCalculateAmount()' data-options="required:true,validType:'float'"></asp:TextBox>
                    </td>
                    <td class="rowlabel">儿童报价总额：</td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="ChildPrice" onblur='r.fnCalculateAmount()' data-options="validType:'float'"></asp:TextBox>
                    </td>
                    <td class="rowlabel">儿童成本总额：</td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="ChildCost" onblur='r.fnCalculateAmount()' data-options="validType:'float'"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="rowlabel">儿童报价说明：</td>
                    <td colspan="5">
                        <asp:TextBox ID="Remark" ClientIDMode="Static" Width="90%" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td colspan="6" style="padding: 10px 30px 10px 0px; text-align: right;">
                        <div>
                            <span style="font-weight: bold;">团队人数：</span><span class="tourprice" id="tourVisitorNum">0</span>人
                        </div>
                        <div>
                            <span style="font-weight: bold;">成本总额：</span><span class="tourprice" id="tourCost">0</span>元
                        </div>

                        <div>
                            <span style="font-weight: bold;">预期毛利：</span><span class="tourprice" id="tourProfit">0</span>元
                        </div>
                        <div>
                            <span style="font-weight: bold;">人均报价：</span><span class="tourprice" id="tourAvgPrice"></span>元
                        </div>
                        <div style="border-bottom: 1px solid #ccc; margin-bottom: 2px;"></div>
                        <div style="border-top: 1px solid #ccc; font-size: 15px; padding-top: 5px; font-weight: bold;">
                            整团报价合计：<span class="tourprice" id="tourTotalAmount">0</span>元
                        </div>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; margin: 30px 0px;">
                <a class="easyui-linkbutton" iconcls="icon-save" id="btnSave">保存</a>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
