<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizToWord.aspx.cs" Inherits="DRP.WEB.Module.Order.BizToWord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导出Word</title>
    <style type="text/css">
        body {
            font-size: 14px;
        }

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

            .wrapSchdule span {
                padding: 0px 3px 0px 3px;
                font-weight: bold;
            }

        .text {
            line-height: 24px;
        }

        .tblPrint {
            width: 100%;
            border-top: 1px solid #000;
            border-left: 1px solid #000;
        }

            .tblPrint td, .tblPrint th {
                padding: 5px;
                line-height: 22px;
                border-right: 1px solid #000;
                border-bottom: 1px solid #000;
            }

            .tblPrint .label {
                text-align: right;
            }
    </style>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color: #000 !important">
    <form id="form1" runat="server">
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrderName" runat="server"></asp:Literal>
            </div>


            <div style="width: 250px;">
                订单编号：<asp:Literal ID="OrderNo" runat="server"></asp:Literal>
            </div>
            <div style="width: 200px; margin: 8px 0;">
                创建日期：<asp:Literal ID="CreateDate" runat="server"></asp:Literal>
            </div>
            <div style="margin: 8px 0;">
                订单金额：<asp:Literal ID="OrderAmt" runat="server"></asp:Literal>
            </div>
            <div style="width: 200px; margin: 8px 0;">
                订单日期：<asp:Literal ID="TourDate" runat="server"></asp:Literal>
            </div>

            <div style="margin: 8px 0;">
                订单毛利：<asp:Literal ID="lblProfit" runat="server"></asp:Literal>
            </div>
            <div>
                毛 利 率：<asp:Literal ID="lblProfitRate" runat="server"></asp:Literal>
            </div>
            <div style="margin: 8px 0;">
                订单备注：<asp:Literal ID="Remark" runat="server"></asp:Literal>
            </div>
            <div style="margin: 8px 0px 20px 0px; display: none;" id="Part" runat="server">
                参与人员：<asp:Literal ID="Participant" runat="server"></asp:Literal>
            </div>

            <strong>一、客户信息</strong>
            <table class="tblPrint" style="margin-top: 5px;">
                <tr>
                    <th style="width: 25px;">序
                    </th>
                    <th style="width: 80px;">客户名称  
                    </th>
                    <th style="width: 50px;">性别
                    </th>
                    <th style="width: 100px;">手机号
                    </th>
                    <th style="width: 150px;">身份证号
                    </th>
                    <th style="width: 100px;">公司名称</th>
                    <th>备注
                    </th>
                </tr>
                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptCost_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td style="text-align: center;"><%# Eval("Name") %></td>
                            <td style="text-align: center;"><%# Eval("Sex") %></td>
                            <td style="text-align: center;"><%# Eval("Mobile") %></td>
                            <td><%# Eval("IDNo") %></td>
                            <td style="text-align: center;"><%# Eval("Company") %></td>
                            <td><%# Eval("Comment") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="margin-top: 25px;">
                <strong>二、成本信息</strong>
            </div>
            <table class="tblPrint" style="margin-top: 5px;">
                <tr>
                    <th style="width: 25px;">序
                    </th>
                    <th style="width: 80px;">类型  
                    </th>
                    <th style="width: 160px;">供应商名称
                    </th>
                    <th style="width: 80px;">成本金额
                    </th>
                    <th>备注
                    </th>
                </tr>
                <asp:Repeater ID="rptCost" runat="server" OnItemDataBound="rptCost_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal></td>
                            <td style="text-align: center;"><%# Eval("ItemName") %></td>
                            <td><%# Eval("Supplier") %></td>
                            <td style="text-align: right;"><%# Eval("CostAmt") %></td>
                            <td><%# Eval("Comment") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
