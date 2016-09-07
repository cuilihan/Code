<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenTour.aspx.cs" Inherits="DRP.WEB.Module.Pro.OpenTour" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>开班发团</title>
    <link href="../../Scripts/Plugin/date_time/skin/WdatePicker.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/CreateTour.js"></script>
    <style type="text/css">
        .weekend
        {
            color: Red;
        }

        .today
        {
            font-style: italic;
            color: Blue;
            font-weight: bold;
        }

        .tblCalendar
        {
            width: 740px;
            height: 260px;
            background-color: #9CB8E7;
            text-align: center;
        }

            .tblCalendar tr
            {
                background-color: #fff;
            }

            .tblCalendar table
            {
                float: left;
                width: 362px;
                height: 250px; 
            }

                .tblCalendar table td
                {
                    font: 12px microsoft yahei, simsun, sans-serif;
                    cursor: pointer;
                }

        .nextMonth
        {
            width: 40px;
            text-align: right;
        }

        .preMonth
        {
            width: 40px;
            text-align: left;
        }

        .TourDay
        {
            background-image: url(../../Themes/default/images/comm/flag.png);
            background-position: left center;
            background-repeat: no-repeat;
            border-right: 1px solid #9CB8E7;
            border-bottom: 1px solid #9CB8E7;
            background-color: #EEF1FC;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="padding: 5px;">
        <table class="tblInfo">
            <tr>
                <td class="rowlabel"><span class="red">*</span>团队名称：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtTourName" runat="server" ClientIDMode="Static" Width="300"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>团次人数：</td>
                <td>计划：<asp:TextBox ID="txtPlanNum" Width="50" ClientIDMode="Static" CssClass="checkInt" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;成团：<asp:TextBox ID="txtClusterNum" Width="50" ClientIDMode="Static" CssClass="checkInt" runat="server"></asp:TextBox>
                </td>
                <td class="rowlabel">每天发班数：</td>
                <td>
                    <asp:TextBox ID="txtTourNum" ClientIDMode="Static" Text="1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>开团日期范围：</td>
                <td>
                    从<asp:TextBox ID="txtStDate" CssClass="Wdate" ClientIDMode="Static" onclick="WdatePicker()" runat="server" Width="90"></asp:TextBox>
                    至
                    <asp:TextBox ID="txtEtDate" ClientIDMode="Static" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtStDate\')}'})" Width="90" runat="server"></asp:TextBox>
                </td>
                <td class="rowlabel"><span class="red">*</span>报名截止日期：</td>
                <td>
                    <asp:DropDownList ID="ddlExpiryDate" ClientIDMode="Static" Width="150" runat="server"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="rowlabel">座位数：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSeatNum" runat="server"></asp:TextBox>
                    短线有效，用于下单时选择座位号 
                </td>
            </tr>
            <tr>
                <td class="rowlabel">开班规则：</td>
                <td colspan="3" style="padding: 10px;">
                    <ul class="ul_tabs" id="ul_tabs">
                        <li tabindex="1" class="selectTag"><a href="javascript:void(0)"><i></i>按天数</a> </li>
                        <li tabindex="2"><a href="javascript:void(0)"><i></i>按周几</a></li>
                        <li tabindex="3"><a href="javascript:void(0)"><i></i>按日期</a></li>
                    </ul>
                    <div id="tagContent" style="padding: 10px; min-height: 30px;">
                        每隔
                        <input type='text' value="0" class='textbox checkInt' style='width: 30px; height: 26px; text-align: center;' id='txtIntervalDay' />
                        天发团，如每隔0天发团则表示天天都发团
                    </div>
                </td>
            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>上车地点：</td>
                <td colspan="3" style="padding: 15px;">
                    <table class="tblEdit">
                        <tr>
                            <th style="width: 30px; text-align: center;">
                                <input id="Checkbox1" type="checkbox" onclick="t.fnSelectAllVenue(this)" />
                            </th>
                            <th style="width: 120px;">出发地
                            </th>
                            <th>集合地点
                            </th>
                            <th style="width: 200px;">集合时间
                            </th>
                            <th style="width: 120px;">接加价
                            </th>
                            <th style="width: 120px;">送加价
                            </th>
                        </tr>
                        <tbody id="tblTourVenue">
                            <asp:Repeater runat="server" ID="rptVenue">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;">
                                            <input type="checkbox" name="chkItem" value="1" />
                                        </td>
                                        <td style="text-align: center;">
                                            <%# Eval("Departure") %>
                                            <input type="hidden" value='<%# Eval("DepartureID") %>' />
                                        </td>
                                        <td>
                                            <input type="text" class="textbox" value='<%# Eval("Name") %>' style="width: 99%; height: 26px;" />
                                        </td>
                                        <td>
                                            <input type="text" class="textbox" value='<%# Eval("MeetTime") %>' style="width: 190px; height: 26px;" />
                                        </td>
                                        <td>
                                            <input type="text" class="checkInt textbox" value='<%# Eval("PickAmt") %>' style="width: 100px; text-align: center; height: 26px;" />
                                        </td>
                                        <td>
                                            <input type="text" class="checkInt textbox" value='<%# Eval("SendAmt") %>' style="width: 100px; text-align: center; height: 26px;" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tbody>
                        <tr>
                            <td colspan="6" style="text-align: right;">添加临时上车地点：
                                <asp:DropDownList ID="Departure" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                                    <asp:ListItem Text="请选择出发地" Value=""></asp:ListItem>
                                </asp:DropDownList><a style="margin-left:1em;" class="easyui-linkbutton" onclick="t.fnAddVenue()" iconcls="icon-add">增加</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>价格体系：</td>
                <td colspan="3" style="padding: 15px;">
                    <table class="tblEdit">
                        <tr>
                            <th style="width: 30px; text-align: center;"></th>
                            <th>价格名称</th>
                            <th style="width: 120px;">销售价格</th>
                            <th style="width: 80px;">返佣</th>
                            <th style="width: 80px;">单房差</th>
                            <th style="width: 70px;">是否占座</th>
                            <th style="width: 70px;">是否儿童</th>
                            <th style="width: 50px;">默认价</th>
                            <th style="width: 40px;">删除</th>
                        </tr>
                        <tbody id="tblPrice">
                        </tbody>
                        <tr>
                            <td colspan="9" style="text-align: right;">
                                <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-add" onclick="t.fnLoadPrice('')">增加价格</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin: 40px 0px;">
            <a href="javascript:;" id="btnSave" class="easyui-linkbutton" iconcls="icon-save">创建团次</a>
        </div>
    </form>
</body>
</html>
