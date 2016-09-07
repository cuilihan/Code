<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourEdit.aspx.cs" Inherits="DRP.WEB.Module.Pro.TourEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改团次</title>
    <script src="Script/TourEdit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>团队名称：
                    </td>
                    <td>
                        <asp:TextBox ID="TourName" ClientIDMode="Static" runat="server" Width="200" data-options="required:true"></asp:TextBox>
                    </td>
                    <td class="rowlabel">出团日期：
                    </td>
                    <td>
                        <asp:Literal ID="TourDate" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>人数：
                    </td>
                    <td>计划：<asp:TextBox ID="PlanNum" ClientIDMode="Static" runat="server" Width="50" data-options="required:true"></asp:TextBox>
                        成团：<asp:TextBox ID="ClusterNum" ClientIDMode="Static" runat="server" Width="50" data-options="required:true"></asp:TextBox>
                    </td>
                    <td class="rowlabel">报名截止日期：
                    </td>
                    <td>
                        <asp:TextBox ID="ExpiryDate" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="rowSeat">
                    <td class="rowlabel"><span class="red">*</span>座位数：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="SeatNum" ClientIDMode="Static" runat="server" Width="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding: 10px;">

                        <table class="tblEdit">
                            <tr>
                                <th style="width: 30px; text-align: center;">
                                    <input id="Checkbox1" type="checkbox" onclick="t.fnSelectAllVenue(this)" />
                                </th>
                                <th style="width: 80px;">出发地
                                </th>
                                <th>集合地点
                                </th>
                                <th style="width: 120px;">集合时间
                                </th>
                                <th style="width: 80px;">接加格
                                </th>
                                <th style="width: 80px;">送加格
                                </th>
                            </tr>
                            <tbody id="tblTourVenue">
                                <asp:Repeater runat="server" ID="rptVenue">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <input type="checkbox" checked="checked" name="chkItem" value="1" />
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("Departure") %>
                                                <input type="hidden" value='<%# Eval("DepartureID") %>' />
                                            </td>
                                            <td>
                                                <input type="text" class="textbox" value='<%# Eval("Name") %>' style="width: 98%; height: 26px;" />
                                            </td>
                                            <td>
                                                <input type="text" class="textbox" value='<%# Eval("MeetTime") %>' style="width: 110px; height: 26px;" />
                                            </td>
                                            <td>
                                                <input type="text" class="checkInt textbox" value='<%# Eval("PickAmt") %>' style="width: 70px; text-align: center; height: 26px;" />
                                            </td>
                                            <td>
                                                <input type="text" class="checkInt textbox" value='<%# Eval("SendAmt") %>' style="width: 70px; text-align: center; height: 26px;" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding: 10px;">
                        <table class="tblEdit">
                            <tr>
                                <th style="width: 30px; text-align: center;"></th>
                                <th>价格名称</th>
                                <th style="width: 70px;">销售价格</th>
                                <th style="width: 70px;">返佣</th>
                                <th style="width: 70px;">单房差</th>
                                <th style="width: 70px;">是否占座</th>
                                <th style="width: 70px;">是否儿童</th>
                                <th style="width: 50px;">默认价</th>
                                <th style="width: 40px;">删除</th>
                            </tr>
                            <tbody id="tblPrice">
                                <asp:Repeater runat="server" ID="rptPrice" OnItemDataBound="rptPrice_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center; font-family: Arial;">
                                                <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                                <input type="hidden" value="<%# Eval("ID") %>" />
                                            </td>
                                            <td>
                                                <input type="text" class='textbox' style="width: 96%; height: 26px;" value='<%# Eval("Name") %>' /></td>
                                            <td>
                                                <input type="text" class="checkInt textbox" value="<%# Eval("SalePrice") %>" style="width: 60px; height: 26px; padding-right: 10px; text-align: right;" /></td>
                                            <td>
                                                <input type="text" class="checkInt textbox" value="<%# Eval("Rebate") %>" style="width: 60px; height: 26px; padding-right: 10px; text-align: right;" /></td>
                                            <td>
                                                <input type="text" class="checkInt textbox" value="<%# Eval("RoomRate") %>" style="width: 60px; height: 26px; padding-right: 10px; text-align: right;" /></td>
                                            <td style="text-align: center;">
                                                <asp:Literal ID="lblSeat" runat="server"></asp:Literal>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Literal ID="lblChild" runat="server"></asp:Literal>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Literal ID="lblDefault" runat="server"></asp:Literal> 
                                            </td>
                                            <td style='text-align: center;'><a href='javascript:;' onclick="t.fnDeletePrice(this)">删除</a></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tr>
                                <td colspan="9" style="text-align: right;">
                                    <a href="javascript:;" onclick="t.fnLoadPrice('')">增加价格</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; margin: 20px 0px;">
                <a href="javascript:;" class="easyui-linkbutton" onclick="return t.fnSave();" iconcls="icon-save">保存</a>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
