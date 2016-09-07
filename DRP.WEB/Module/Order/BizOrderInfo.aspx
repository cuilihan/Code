<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizOrderInfo.aspx.cs" Inherits="DRP.WEB.Module.Order.BizOrderInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>单项业务订单查看</title>

</head>
<body style="margin: 0 auto; background-color: #EEEEEE; color: #000 !important">
    <form id="form1" runat="server">
        <div style="padding: 3px 10px; text-align: center; margin: 10px;">
            <a id="btnPrint" onclick="printScope()" href="javascript:;" class="easyui-linkbutton"
                iconcls="icon-print">打印</a>
            <asp:HyperLink ID="lnkToWord" CssClass="easyui-linkbutton" iconCls="icon-word" runat="server" Target="_blank">导出Word</asp:HyperLink>
        </div>
        <asp:Panel ClientIDMode="Static" ID="pnlWraper" CssClass="wrapper" runat="server">
            <!--start print-->
            <div style="text-align: left;">
                <img src="" runat="server" id="img" style="width: 100%;" />
            </div>
            <div style="text-align: center; margin: 10px 0px 30px 0px; font-family: 微软雅黑; font-size: 20px; font-weight: bold;">
                <asp:Literal ID="OrderName" runat="server"></asp:Literal>
            </div>


            <div style="width: 250px;">
                订单编号：<asp:Literal ID="OrderNo" runat="server"></asp:Literal>
            </div>
            <div style="float: right; text-align: right; margin: 8px 0px; width: 200px;">
                创建日期：<asp:Literal ID="CreateDate" runat="server"></asp:Literal>
            </div>
            <div style="margin: 8px 0;">
                订单金额：<asp:Literal ID="OrderAmt" runat="server"></asp:Literal>
            </div>
            <div style="float: right; text-align: right; width: 200px;">
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

            一、客户信息
            <table class="tblPrint" style="margin-top: 5px;">
                <tr>
                    <th style="width: 25px;">序
                    </th>
                    <th style="width: 60px;">客户名称  
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
                二、成本信息
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

            <div style="margin: 20px 0px 10px 0px; font-weight: bold;">
                附件
            </div>
            <table class="tblPrint" cellpadding="1" cellspacing="1">
                <tr>
                    <th>文件名称
                    </th>
                    <th style="width: 90px;">文件类型
                    </th>
                    <th style="width: 90px;">文件大小
                    </th>
                    <th style="width: 120px;">上传日期
                    </th>
                    <th style="width: 80px;">上传人
                    </th>
                </tr>
                <tbody id="tblFile">
                    <asp:Repeater runat="server" ID="rptFile">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input type="hidden" value='<%# Eval("ID") %>' />
                                    <a href='<%# Eval("FilePath") %>' target='_blank'>
                                        <%# Eval("FileName") %></a>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("FileType") %>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("FileSize") %>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("CreateDate") %>
                                </td>
                                <td style="text-align: center;">
                                    <%# Eval("CreateUserName") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <div style="text-align: right; margin-top: 20px;">
                打印人：<asp:Literal ID="UserName" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;打印时间：<asp:Literal ID="printDate" runat="server"></asp:Literal>
            </div>
            <!--end print-->
        </asp:Panel>
    </form>
</body>
</html>
