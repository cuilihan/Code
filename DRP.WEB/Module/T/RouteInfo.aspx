<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteInfo.aspx.cs" Inherits="DRP.WEB.Module.T.RouteInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报价单详情</title>
    <style type="text/css">
        .subject
        {
            text-align: right;
            font-size: 13px;
            font-family: 微软雅黑;
            font-weight: bold;
        }

        .wrapSchdule
        {
            font-weight: bold;
            padding: 3px 0px 3px 5px;
            background-color: #F3F3F3;
            border-bottom: 1px solid #E6E6E6;
        }

        .itemName
        {
            font-size: 16px;
            font-weight: bold;
            font-family: 微软雅黑;
            margin: 15px 0px;
            padding-bottom: 5px;
        }

        .wrapSchdule span
        {
            padding: 0px 3px 0px 3px;
            font-weight: bold;
        }

        .text
        {
            line-height: 24px;
        }

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
<body style="margin: 0 auto; background-color: #EEEEEE; color:#000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
            <asp:LinkButton ID="btnToPDF" OnClick="btnToPDF_Click" CssClass="easyui-linkbutton" runat="server">生成PDF</asp:LinkButton>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="text-align: center; margin: 10px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="RouteName" runat="server"></asp:Literal>
            </div>
            <div style="width: 170px; float: left;">
                编号：<asp:Literal ID="RouteNo" runat="server"></asp:Literal>
            </div>
            <div style="text-align: right; float: right;">
                目的地：<asp:Literal ID="Destination" runat="server"></asp:Literal>
                <span style="padding-left: 1em;">天数：</span><asp:Literal ID="Days" runat="server"></asp:Literal>天
            </div>
            <div style="clear: both; height: 5px;"></div>
            <table class="tblPrint">
                <asp:Literal ID="lblFeature" runat="server"></asp:Literal>
                <tr>
                    <td style="padding: 10px;">
                        <asp:Repeater ID="rptData" runat="server">
                            <ItemTemplate>
                                <div style="border: 1px solid #E6E6E6; margin-bottom: 10px;">
                                    <div class="wrapSchdule">
                                        第<span><%# Eval("DayNum") %></span>天：<%#Eval("Title") %>
                                    </div>
                                    <div style="line-height: 22px; padding: 5px;">
                                        <%# string.IsNullOrEmpty(Eval("Schedule").ToString()) ? "" : Eval("Schedule").ToString().Replace("\r", "<br/>").Replace("\n", "<br/>")%>
                                    </div>
                                    <div style="border-bottom: 1px solid #E6E6E6; border-top: 1px solid #E6E6E6; padding: 5px;">
                                        <span style="font-weight: bold;">住宿： </span>
                                        <%# Eval("Stay") %>
                                    </div>
                                    <div style="padding: 0px 0px 5px 5px;">
                                        <span style="font-weight: bold;">用餐：</span>
                                        <%# Eval("Dinner") %>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 20px 15px;">
                        <table class="tblPrint">
                            <tr>
                                <th style="width: 20px;"></th>
                                <th style="width: 90px;">服务项目</th>
                                <th>服务内容</th>
                            </tr>
                            <tbody id="tblServiceItem">
                                <asp:Repeater runat="server" ID="rptItem" OnItemDataBound="rptItem_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style='text-align: center;'>
                                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                                            <td>
                                                <%# Eval("ItemName") %></td>
                                            <td>
                                                <%# Eval("ItemRemark") %></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="padding-top: 10px;">
                            <asp:Literal ID="lblStandard" runat="server"></asp:Literal>
                        </div>

                    </td>
                </tr>
                <asp:Literal ID="lblItems" runat="server"></asp:Literal>
                <tr>
                    <td style="padding: 10px 30px 10px 10px; text-align: right;">
                        <div style="text-align: left; padding: 10px 0px 15px 10px;">
                            按照
                            <asp:Label Font-Bold="true" Font-Size="15" ID="VisitorNum" runat="server"></asp:Label>
                            人独立成团核价，人数增减会导致价格相应变化！住宿若有单男/女，需补足单房差
                            <div>
                                <asp:Literal ID="Remark" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div>
                            <span style="font-weight:bold;">团队人数：</span><span runat="server" class="tourprice" id="tourVisitorNum">0</span>
                        </div>
                        <div>
                            <span style="font-weight:bold;">人均报价：</span><asp:Label ID="AvgPrice" CssClass="tourprice" runat="server" Text="0"></asp:Label>
                        </div>
                        <div>
                            <span style="font-weight:bold;">儿童报价：</span>
                            <asp:Label ID="ChildPrice" runat="server" Text="0" CssClass="tourprice"></asp:Label>
                        </div>
                        <div style="border-bottom: 1px solid #ccc; margin-bottom: 2px;"></div>
                        <div style="border-top: 1px solid #ccc; font-size: 15px; padding-top: 5px; font-weight: bold;">
                            整团综合报价：<span class="tourprice" id="tourTotalAmount" runat="server">0</span>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="text-align: right; margin-top: 10px;">
                <asp:Literal ID="lblFooterInfo" runat="server"></asp:Literal>
            </div>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
