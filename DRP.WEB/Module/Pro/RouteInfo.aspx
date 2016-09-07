<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteInfo.aspx.cs" Inherits="DRP.WEB.Module.Pro.RouteInfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .subject {
            text-align: right;
            font-size: 13px;
            font-family: 微软雅黑;
            font-weight: bold;
        }

        .wrapSchdule {
            font-weight: bold;
            padding: 3px 0px 3px 5px;
            background-color: #F3F3F3;
            border-bottom: 1px solid #E6E6E6;
        }

        .itemName {
            font-size: 13px;
            font-weight: bold;
            font-family: 微软雅黑;
            margin: 5px 0px;
            border-bottom: 1px solid #000;
            padding-bottom: 5px;
        }

        .wrapSchdule span {
            padding: 0px 3px 0px 3px;
            font-weight: bold;
        }

        .text {
            line-height: 20px;
        }
    </style>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color: #000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
            <asp:LinkButton ID="btnToWord" CssClass="easyui-linkbutton" iconCls="icon-word" OnClick="btnToWord_Click" runat="server">导出Word</asp:LinkButton>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="text-align: left;">
                <img src="" runat="server" id="img" style="width: 100%;" />
            </div>
            <div style="text-align: center; margin: 5px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="RouteName" runat="server"></asp:Literal>
            </div>

            <div style="width: 170px; float: left;">
                编号：<asp:Literal ID="RouteNo" runat="server"></asp:Literal>
            </div>
            <div style="text-align: right; float: right;">
                目的地：<asp:Literal ID="Destination" runat="server"></asp:Literal>
                <span style="padding-left: 1em;">天数：</span><asp:Literal ID="ScheduleDays" runat="server"></asp:Literal>天
            </div>
            <div style="clear: both; height: 5px;"></div>
            <table class="tblPrint">
                <tr>
                    <td colspan="4">
                        <asp:Literal ID="lblRouteBaseInfo" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding: 10px;">
                        <asp:Repeater ID="rptData" runat="server">
                            <ItemTemplate>
                                <div style="border: 1px solid #E6E6E6; margin-bottom: 10px;">
                                    <div class="wrapSchdule">
                                        第<span><%# Eval("DayNum") %></span>天：<%#Eval("Title") %>
                                    </div>
                                    <div style="line-height: 20px; padding: 3px;">
                                        <%# string.IsNullOrEmpty(Eval("Schedule").ToString()) ? "" : Eval("Schedule").ToString().Replace("\r", "<br/>").Replace("\n", "<br/>")%>
                                    </div>
                                    <div style="border-top: 1px solid #E6E6E6; padding: 5px;">

                                        <%# string.IsNullOrEmpty(Eval("Stay").ToString())?"":"<span style=\"font-weight: bold;\">住宿：</span>"+Eval("Stay") %>


                                        <%# string.IsNullOrEmpty(Eval("Dinner").ToString())?"":"<span style=\"font-weight: bold;padding-left:1em;\">用餐：</span>"+Eval("Dinner") %>
                                        <%# Eval("Traffic")==null?"":"<span style=\"font-weight: bold;padding-left:1em;\">交通：</span>"+Eval("Traffic") %>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Literal ID="lblPriceInfo" runat="server"></asp:Literal>
                        <asp:Literal ID="lblItems" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div style="text-align: right; margin-top: 10px;">
                <asp:Literal ID="lblFooterInfo" runat="server"></asp:Literal>

                <%-- <asp:LinkButton ID="btnToWord" OnClick="btnToWord_Click" runat="server">To Word</asp:LinkButton>--%>
            </div>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
