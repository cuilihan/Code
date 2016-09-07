<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tOrdersToWord.aspx.cs" Inherits="DRP.WEB.Module.Order.tOrdersToWord" %>

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
            border-left: 1px solid #000;
            border-spacing: 0;
        }

            .tblPrint td, .tblPrint th {
                padding: 5px;
                line-height: 22px;
                border-right: 1px solid #000;
                border-top: 1px solid #000;
            }

            .tblPrint .label {
                text-align: right;
            }
    </style>
</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color: #000 !important">
    <form id="form1" runat="server">
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrderName" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; width: 300px; float: left;">
                出团日期：<asp:Literal ID="TourDate" runat="server"></asp:Literal>
                <i style="padding-left: 1em;"></i>返程日期：<asp:Literal ID="ReturnDate" runat="server"></asp:Literal>
            </div>
            <div style="margin-bottom: 8px; float: right; width: 200px; text-align: right;">
                目的地：<asp:Literal ID="DestinationName" runat="server"></asp:Literal>
            </div>
            <table class="tblPrint" style="border-collapse:collapse;">
                <tr>
                    <th rowspan="2" style="width: 130px;">订单信息
                    </th>
                    <th rowspan="2" style="width: 110px;">上车地点</th>
                    <th rowspan="2" style="width: 80px;">人数</th>
                    <th colspan="4">游客信息</th>
                </tr>
                <tr>
                    <th style="width: 40px;">姓名</th>
                    <th style="width: 70px;">手机号</th>
                    <th style="width: 90px;">身份证号</th>
                    <th>备注</th>
                </tr>
                <tbody id="tblData" runat="server">

                </tbody>

            </table>
        </asp:Panel>
    </form>
</body>
</html>
