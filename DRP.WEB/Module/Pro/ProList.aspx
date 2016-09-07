<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProList.aspx.cs" Inherits="DRP.WEB.Module.Pro.ProList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>可预订产品查询</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <style type="text/css">
        .pd {
            border: 1px solid #DEC1A9;
            width: 340px;
            padding: 5px;
            position: absolute;
            background-color: #FFF6C3;
        }
    </style>
    <script src="Script/ProList.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-tabs">
            <div title="班次预订查询" iconcls="icon-search">
                <div id="toolbar">
                    目的地：
                        <input class="easyui-combotree" id="RouteTypeID" style="height: 26px; width: 90px;" />
                    <input class="easyui-combotree" id="DestinationID" style="height: 26px; width: 120px;" />
                    <span style="padding-left: 1em;">出团日期：</span>
                    <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <span style="padding-left: 1em;">线路名称：</span>
                    <asp:TextBox ID="TourName" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="EffectiveDays" runat="server" ClientIDMode="Static" Text="查询过期的团次" />
                    <a id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    <span style="position: absolute; display: inline-block; padding-top: 5px; right: 5px;">
                        <i class="red_arrow"></i><a href="ProNav.aspx" style="color: red;">目的地导航视图</a>
                    </span>
                </div>
                <table id="tblData"></table>
                <div id="PriceDetail" class="hide pd">
                    <table class="tblItem">
                        <tr>
                            <th>价格名称
                            </th>
                            <th style="width: 60px;">销售价格
                            </th>
                            <th style="width: 50px;">返佣
                            </th>
                            <th style="width: 50px;">单房差
                            </th>
                        </tr>
                        <tbody id="tblPriceData">
                            <tr>
                                <td colspan="4">正在加载价格...
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
