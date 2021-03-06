﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketOrderEdit.aspx.cs" Inherits="DRP.WEB.Module.Order.TicketOrderEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机票订单维护</title>
    <link href="../../Scripts/Plugin/lhgdialog/skins/default.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/lhgdialog/lhgdialog.min.js?skin=iblue" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
     <script src="Script/OrderUtility.js" type="text/javascript"></script>
    <script src="Script/TicketOrderEdit.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 10px 5px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>订单名称：</td>
                    <td colspan="3">
                        <asp:TextBox ID="OrderName" runat="server" Width="80%" data-options="required:true" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td class="rowlabel">
                        <span class="red">*</span>机票类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="TicketType" ClientIDMode="Static" data-options="required:true" runat="server">
                            <asp:ListItem Text="国内" Value="国内"></asp:ListItem>
                            <asp:ListItem Text="国际" Value="国际"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">PNR记录编号：</td>
                    <td>
                        <asp:TextBox ID="PNR" ClientIDMode="Static" runat="server" ></asp:TextBox>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>去程日期：</td>
                    <td>
                        <asp:TextBox ID="TourDate" ClientIDMode="Static" onclick="WdatePicker()" runat="server" data-options="required:true"></asp:TextBox>
                    </td>
                    <td class="rowlabel">回程日期：</td>
                    <td>
                        <asp:TextBox ID="ReturnDate" ClientIDMode="Static" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>订票人数：</td>
                    <td>
                        <asp:TextBox ID="AdultNum" ClientIDMode="Static" runat="server" data-options="required:true,validType:'int'"></asp:TextBox>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>订票联系人：</td>
                    <td colspan="3">
                        <asp:TextBox ID="Contact" ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">公司名称：</td>
                    <td>
                        <asp:TextBox ID="Company" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel"><span class="red">*</span>联系人电话：</td>
                    <td colspan="3">
                        <asp:TextBox ID="ContactPhone" ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">航班信息：</td>
                    <td colspan="5" style="padding: 10px;">
                        <table class="tblEdit">
                            <tr>
                                <th style="width: 40px;"></th>
                                <th style="width: 100px;">行程</th>
                                <th>航班信息</th>
                                <th style="width: 90px;">出发/到达机场</th>
                                <th style="width: 140px;">航空公司</th>
                                <th style="width: 80px;">舱位</th>
                                <th style="width: 70px;">票价</th>
                                <th style="width: 40px;"></th>
                            </tr>
                            <tr>
                                <td style="color: #ccc; text-align: center;">示例</td>
                                <td style="color: #ccc; text-align: center;">上海-北京</td>
                                <td style="color: #ccc; text-align: center;">HO1251&nbsp;&nbsp;26FEB&nbsp;&nbsp;2155&nbsp;&nbsp;0025  </td>
                                <td style="color: #ccc; text-align: center;">SHA / PEK</td>
                                <td style="color: #ccc; text-align: center;">吉祥</td>
                                <td style="color: #ccc; text-align: center;">经济舱</td>
                                <td style="color: #ccc; text-align: center;">¥719</td>
                                <td></td>
                            </tr>
                            <tbody id="tblFlight">
                                <asp:Literal ID="lblFlight" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>客户信息：</td>
                    <td colspan="5" style="padding: 10px;">
                        <table class="tblEdit">
                            <tr>
                                <th style="width: 30px;"></th>
                                <th style="width: 120px;">姓名</th>
                                <th style="width: 60px;">性别</th>
                                <th style="width: 120px;">手机号</th>
                                <th style="width: 140px;">证件类型</th>
                                <th style="width: 200px;">证件证号</th>
                                <th style="width: 150px;">公司名称</th>
                                <th>备注</th>
                                <th style="width: 40px;"></th>
                            </tr>
                            <tbody id="tblCustomer">
                                <asp:Repeater runat="server" ID="rptCustomer" OnItemDataBound="rptCustomer_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                                <input type='hidden' value="<%# Eval("CustomerID") %>" />
                                            </td>
                                            <td>
                                                <input type="text" class="textbox" style="width: 110px; height: 26px;" value='<%# Eval("Name") %>' /></td>
                                            <td>
                                                <asp:Literal ID="lblSex" runat="server"></asp:Literal></td>
                                            <td>
                                                <input type="text" class="textbox" style="width: 100px; height: 26px;" value='<%# Eval("Mobile") %>' /></td>
                                            <td>
                                                <asp:Literal ID="lblIDType" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <input type="text" class="textbox" style="width: 170px; height: 26px;" value='<%# Eval("IDNo") %>' /></td>
                                            <td>
                                                <input type='text' class='textbox' value="<%# Eval("Company") %>" style='height: 26px; width: 140px;' /></td>
                                            <td>
                                                <input type="text" class="textbox" style="width: 98%; height: 26px;" value='<%# Eval("Comment") %>' /></td>
                                            <td><a href='javascript:;' onclick="c.fnDeleteCustomer(this)">删除</a></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="padding: 10px 5px; text-align: right;">
                            <a href="javascript:;" id="btnSelectCustomer" class="easyui-linkbutton" iconcls="icon-ok">选择客户</a>
                            <a href="javascript:;" id="btnAddCustomer" class="easyui-linkbutton" iconcls="icon-add">增加客户</a>
                        </div>
                    </td>
                </tr>
                    <tr>
                    <td class="rowlabel"><span class="red">*</span>成本项目：</td>
                    <td colspan="5" style="padding: 10px;">
                        <table class="tblEdit">
                            <tr>
                                <th style="width: 30px;"></th>
                                <th style="width: 120px;">类型</th>
                                <th style="width: 250px;">供应商名称</th>
                                <th style="width: 100px;">成本金额</th>
                                <th>备注</th>
                                <th style="width: 40px;"></th>
                            </tr>
                            <tbody id="tblCostItem">
                                <asp:Repeater runat="server" ID="rptCost" OnItemDataBound="rptCost_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <input type='hidden' value="<%# Eval("ID") %>" />
                                                <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                            </td>
                                            <td><%# Eval("ItemName") %>
                                                <input type='hidden' value='<%# Eval("ItemType") %>' />
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblSupplier" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <input name='amt' style='width: 90px; height: 26px; text-align: right; padding-right: 10px;' class='checkInt textbox' value="<%# Eval("CostAmt") %>" /></td>
                                            <td>
                                                <input style='width: 96%; height: 26px;' type='text' class='textbox' value="<%# Eval("Comment") %>" /></td>
                                            <td style='text-align: center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="padding: 10px 5px; text-align: right;">
                            项目成本类型：<asp:DropDownList ID="ddlCostItemType" runat="server" ClientIDMode="Static">
                                <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                                <asp:ListItem Text="供应商" Value="1"></asp:ListItem>
                                <asp:ListItem Text="景点门票" Value="2"></asp:ListItem>
                                <asp:ListItem Text="导游" Value="3"></asp:ListItem>
                                <asp:ListItem Text="酒店" Value="4"></asp:ListItem>
                                <asp:ListItem Text="车队" Value="5"></asp:ListItem>
                                <asp:ListItem Text="签证机构" Value="6"></asp:ListItem>
                                <asp:ListItem Text="保险公司" Value="7"></asp:ListItem>
                                <asp:ListItem Text="购物店" Value="8"></asp:ListItem>
                                <asp:ListItem Text="票务机构" Value="9"></asp:ListItem>
                                <asp:ListItem Text="其他供应商资源" Value="10"></asp:ListItem>
                            </asp:DropDownList>
                            <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-add" id="btnAddCostItem">增加</a>
                        </div>
                    </td>
                </tr>


              <%--  <tr>
                    <td class="rowlabel"><span class="red">*</span>供应商：</td>
                    <td>
                        <asp:DropDownList ID="SupplierName" data-options="required:true" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">机票成本：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="OrderCost" ClientIDMode="Static" runat="server" data-options="required:true,validType:'float'"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>应收款：</td>
                    <td colspan="5">
                        <asp:TextBox ID="OrderAmt" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td> 
                </tr>
                <tr>
                    <td class="rowlabel">订单备注：</td>
                    <td colspan="5">
                        <asp:TextBox ID="Remark" ClientIDMode="Static" Width="90%" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="OrderCostID" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">参与人员：</td>
                    <td colspan="5">
                        <asp:HiddenField ID="hfDept" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hfEmployee" ClientIDMode="Static" runat="server" />
                        <input class="easyui-combotree" id="Dept" style="height: 26px; width: 120px;" runat="server" />
                        <input class="easyui-combobox" id="Employee" style="height: 26px; width: 120px;" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">上传附件：
                    </td>
                    <td colspan="5" style="padding: 10px;">
                        <div>
                            <input type="file" name="uploadify" id="uploadify" runat="server" />
                        </div>
                        <table class="tblEdit" cellpadding="1" cellspacing="1">
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
                                <th style="width: 45px;"></th>
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
                                            <td style="text-align: center;">
                                                <a href="javascript:;" onclick="comm.fnDeleteFile(this)">删除</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center; padding: 10px;">
                        <div id="btnOpt">
                            <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" id="btnSave">提交订单</a>
                        </div>
                        <div style="color: Red;" id="tips" class="hide">
                            <img src="../../UI/themes/default/images/loading.gif" />
                            正在提交数据，请稍候... 
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
