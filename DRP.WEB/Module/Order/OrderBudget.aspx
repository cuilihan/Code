<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderBudget.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderBudget" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单预算</title>
    <script src="Script/OrderUtility.js" type="text/javascript"></script>
    <script src="Script/OrderBudget.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 10px;">
        <div class="iBread iBreadFive" style="margin: 3px">
            <ul class="iCls">
                <li>
                    <strong class="one">1</strong>
                    填写订单信息 
                    <i></i>
                </li>
                <li class="on">
                    <strong class="two">2</strong>
                    订单预算 
                    <i></i><em></em>
                </li>
                <li>
                    <strong class="three">3</strong>
                    导游报账            
                     <i></i><em></em>
                </li>
                <li>
                    <strong class="four">4</strong>
                    订单决算               
                     <i></i><em></em>
                </li>
                <li class="last">
                    <strong class="five">5</strong>
                    完成订单               
                     <em></em>
                </li>
            </ul>
        </div>
        <table class="tblInfo">
            <tr>
                <td colspan="6" style="text-align: center; font-weight: bold; font-size: 15px;">【<span style="color: red;">预算</span>】
                    <asp:Literal ID="lblOrderName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>应收款：</td>
                <td>
                    <asp:TextBox ID="OrderAmt" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                <td class="rowlabel"><span class="red">*</span>成人人数：</td>
                <td>
                    <asp:TextBox ID="AdultNum" ClientIDMode="Static" data-options="required:true,validType:'int'" runat="server"></asp:TextBox></td>
                <td class="rowlabel">儿童人数：</td>
                <td>
                    <asp:TextBox ID="ChildNum" ClientIDMode="Static" data-options="validType:'int'" runat="server"></asp:TextBox></td>
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
                            <asp:Literal ID="lblCostItem" runat="server"></asp:Literal>
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
                            <asp:ListItem Text="其他供应商" Value="10"></asp:ListItem>
                        </asp:DropDownList>
                        <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-add" id="btnAddCostItem">增加</a>&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="isCustom" runat="server" ClientIDMode="Static" />
                        <label for="isCustom" style="color: red; font-size: 16px; font-weight: bold;">临时添加</label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="rowlabel">导游领款：
                    <div>（备用金）</div>
                </td>
                <td colspan="5" style="padding: 10px;">申领金额：<asp:TextBox ID="DrawMoney" ClientIDMode="Static" data-options="validType:'float'" runat="server"></asp:TextBox>
                    <span style="padding-left: 2em;">领款方式：</span><asp:DropDownList ID="Method" ClientIDMode="Static" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <div style="margin-top: 5px;">
                        领款备注：<asp:TextBox ID="Comment" Width="90%" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="rowlabel">预算备注：</td>
                <td colspan="5" style="padding: 10px;">
                    <table class="tblEdit">
                        <tr>
                            <th style="width: 30px;"></th>
                            <th style="width: 120px;">类型</th>
                            <th>备注内容</th>
                        </tr>
                        <tbody id="tblComment">
                            <asp:Literal ID="lblComment" runat="server"></asp:Literal>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <div style="padding: 20px 0px 50px 0px; text-align: center;">
            <div id="btnOpt">
                <asp:HiddenField ID="hfIsEdit" runat="server" />
                <a href="javascript:;" runat="server" class="easyui-linkbutton" iconcls="icon-save" id="btnSave">提交预算</a>
            </div>
            <div style="color: Red;" id="tips" class="hide">
                <img src="../../UI/themes/default/images/loading.gif" />
                正在提交数据，请稍候... 
            </div>

        </div>
    </form>
</body>
</html>
