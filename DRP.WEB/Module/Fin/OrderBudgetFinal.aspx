<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderBudgetFinal.aspx.cs" Inherits="DRP.WEB.Module.Fin.OrderBudgetFinal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>决算</title>
    <script src="../Order/Script/OrderUtility.js" type="text/javascript"></script>
    <script src="Script/OrderFinalBudget.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
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
                <li>
                    <strong class="two">2</strong>
                    订单预算 
                    <i></i><em></em>
                </li>
                <li>
                    <strong class="three">3</strong>
                    导游报账            
                     <i></i><em></em>
                </li>
                <li class="on">
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
                <td colspan="6" style="text-align: center; font-weight: bold; font-size: 15px;">【<span style="color: red;">决算</span>】
                    <asp:Literal ID="lblOrderName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="rowlabel"><span class="red">*</span>订单金额：</td>
                <td>
                    <asp:TextBox ID="OrderAmt" ClientIDMode="Static" data-options="required:true,validType:'float'" runat="server"></asp:TextBox></td>
                <td class="rowlabel"><span class="red">*</span>成人人数：</td>
                <td>
                    <asp:TextBox ID="AdultNum" ClientIDMode="Static" data-options="required:true,validType:'int'" runat="server"></asp:TextBox></td>
                <td class="rowlabel">儿童人数：</td>
                <td>
                    <asp:TextBox ID="ChildNum" ClientIDMode="Static" data-options="validType:'int'" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="6" style="padding: 10px;">
                    <table class="tblEdit">
                        <tr>
                            <th colspan="2"></th>
                            <th colspan="3">预算
                            </th>
                            <th colspan="3">决算
                            </th>
                            <th></th>
                        </tr>
                        <tr>
                            <th style="width: 25px;"></th>
                            <th style="width: 90px;">类型</th>
                            <th style="width: 200px;">供应商名称</th>
                            <th style="width: 80px;">成本金额</th>
                            <th>备注</th>
                            <th style="width: 250px;">供应商名称</th>
                            <th style="width: 90px;">成本金额</th>
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
                            <asp:ListItem Text="地接社" Value="1"></asp:ListItem>
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
                        <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-add" id="btnAddCostItem">增加</a>
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
            <tr>
                <td class="rowlabel">决算备注：</td>
                <td colspan="5" style="padding: 10px;">
                    <asp:TextBox ID="txtComment" Width="98%" Height="60" TextMode="MultiLine" CssClass="textbox" EnableTheming="false" runat="server"></asp:TextBox>
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
        </table>
        <div style="padding: 20px 0px 50px 0px; text-align: center;">
            <div id="btnOpt">
                <asp:HiddenField ID="hfIsEdit" runat="server" />
                <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" id="btnSave">保存决算</a>
                <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" id="btnFinalSave">保存并确认</a>
                <asp:HyperLink ID="lnkBill" CssClass="easyui-linkbutton" Target="_blank" runat="server">查看预决算单</asp:HyperLink>
            </div>
            <div style="color: Red;" id="tips" class="hide">
                <img src="../../UI/themes/default/images/loading.gif" />
                正在提交数据，请稍候... 
            </div>

        </div>
    </form>
</body>
</html>
