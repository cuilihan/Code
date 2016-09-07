<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zTourNotice.aspx.cs" Inherits="DRP.WEB.Module.Order.zTourNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自主班团队出团任务书</title>
    <style type="text/css">
        .title {
            width: 120px;
            text-align: right;
        }

        .head {
            font-size: 15px;
            font-weight: bold;
            padding: 8px 0px;
        }
    </style>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color:#000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="font-size: 15px; text-align: right; padding-bottom: 5px; border-bottom: 1px solid #000;">
                <asp:Literal ID="OrgName" runat="server"></asp:Literal>
            </div>
            <div style="text-align: center; margin: 10px 0px 10px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrgBrand" runat="server"></asp:Literal>任务单
            </div>
            <table style="width: 100%; border-spacing: 0px;">
                <tr>
                    <td class="head" style="text-align: left;">团队编号：<asp:Literal ID="TourNo" runat="server"></asp:Literal>
                    </td>
                    <td class="head" style="width: 250px;">团队人数：
                        <asp:Literal ID="VisitorNum" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="head" style="text-align: left;">出团日期：
                        <asp:Literal ID="TourDate" runat="server"></asp:Literal></td>
                    <td class="head">导游姓名：
                        <asp:Literal ID="GuideName" runat="server"></asp:Literal></td>
                </tr>
            </table>

            <div style="font-size: 15px; padding-top: 5px; margin-bottom: 4px; font-weight: bold;">
                行程安排：
            </div>
            <div style="border: 1px solid #000; min-height: 200px; padding: 10px; margin-bottom: 15px;">
                <asp:Repeater ID="rptData" runat="server">
                    <ItemTemplate>
                        <div style="background-color: #F3F3F3; font-weight: bold; height: 25px; line-height: 25px; padding: 5px;">
                            第<%# Eval("DayNum") %>天：<%# Eval("Title") %>
                        </div>
                        <div style="border-top: 0; padding: 5px; line-height: 24px;">
                            <%# Eval("Schedule") %>
                        </div>
                        <div style="padding: 5px; line-height: 24px;">
                            <span style="font-weight: bold;">用餐：</span><%# string.IsNullOrEmpty( Eval("Dinner").ToString())?"x":Eval("Dinner") %>
                            <span style="padding-left: 1em;"></span>
                            <span style="font-weight: bold;">住宿：</span><%# string.IsNullOrEmpty( Eval("Stay").ToString())?"x":Eval("Stay") %> 
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div style="height: 15px;"></div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
             <div style="font-size: 15px; padding-top: 5px; margin-bottom: 4px; font-weight: bold;">
                服务标准：
            </div>
            <table class="tblPrint">
                <tr>
                    <th style="background-color: #F3F3F3; ">费用包含
                    </th>
                    <th style="background-color: #F3F3F3; ">费用不包含
                    </th>
                </tr>
                <tr>
                    <td style="vertical-align: top; font-size:12px; line-height:20px;">
                        <asp:Literal ID="PriceInclude" runat="server"></asp:Literal>
                    </td>
                    <td style="vertical-align: top; font-size:12px; line-height:20px;">
                        <asp:Literal ID="PriceNonIncude" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>

            <div style="margin-top:10px;">
            <asp:Literal ID="lblComment" runat="server"></asp:Literal>

            </div>
            <div style="text-align: right; margin-top: 20px;">
                打印人：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;打印时间：<asp:Literal ID="printDate" runat="server"></asp:Literal>
            </div>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
