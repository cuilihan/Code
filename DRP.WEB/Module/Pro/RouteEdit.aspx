<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteEdit.aspx.cs" Inherits="DRP.WEB.Module.Pro.RouteEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>线路编辑</title>
    <script src="../../Scripts/Plugin/kindeditor/kindeditor-min.js"></script>
    <script src="Script/RouteEdit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px 5px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>线路名称：</td>
                    <td colspan="3">
                        <asp:TextBox ID="RouteName" runat="server" Width="500" data-options="required:true" ClientIDMode="Static"></asp:TextBox>
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
                        <asp:TextBox ID="ScheduleDays" ClientIDMode="Static" data-options="required:true,validType:'int'" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">线路来源：</td>
                    <td colspan="5">
                        <asp:DropDownList ID="RouteSource" Width="150" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="padding: 10px; vertical-align: top;">
                        <ul class="ul_tabs" id="ul_tabs">
                            <li><a>线路行色</a></li>
                            <li><a>行程安排</a></li>
                            <li><a>报价已含</a></li>
                            <li><a>报价不含</a></li>
                            <li><a>自费项目说明</a></li>
                            <li><a>特别提醒</a></li>
                            <li><a>购物说明</a></li>
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

                                                    <td rowspan='5' style='text-align: center; width: 30px;'><a href='javascript:;' onclick="r.fnDeleteSchedule(this)">删除</a></td>
                                                </tr>
                                                <tr>
                                                    <td class='rowlabel_90'>行程</td>
                                                    <td>
                                                        <asp:TextBox runat="server" TextMode="MultiLine" CssClass="" EnableTheming="false"
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
                                                <tr>
                                                    <td class='rowlabel_90'>交通</td>
                                                    <td>
                                                        <input type='text' value="<%# Eval("Traffic") %>" class='textbox' style="width: 90%; height: 24px;" /></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div style="text-align: right; padding-right: 20px;">
                                    <a href="javascript:;" id="btnAddDay">增加一天行程</a>
                                </div>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="PriceInclude" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>

                            <li class="tab_con">
                                <asp:TextBox ID="PriceNonIncude" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="SelfItem" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="Remind" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="Shopping" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                            <li class="tab_con">
                                <asp:TextBox ID="Comment" runat="server" TextMode="MultiLine" Width="100%" Height="200"></asp:TextBox>
                            </li>
                        </ul>
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
