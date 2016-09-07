<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerImp.aspx.cs" Inherits="DRP.WEB.Module.Crm.CustomerImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>导入客户资料</title>
    <script type="text/javascript">
        function fnValid() {
            var file = $("#FileCustomer").val();
            if (file == "") {
                Alert("请选择文件");
                return false;
            }
            return confirm("确定要导入吗");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px; margin-top: 20px;" class="line">
            一、 导入基础资料要求
        </div>
        <div style="padding: 0px 40px; line-height: 26px;">
            <ul>
                <li>1、只能从Excel 2007及以上版本中导入；</li>
                <li>2、导入客户的资料属性分别为：
                    <table class="tblInfo">
                        <tr>
                            <th>姓名</th>
                            <th>性别</th>
                            <th>手机号</th>
                            <th>传真</th>
                            <th>QQ</th>
                            <th>公司名称</th>
                            <th>身份证号</th>
                            <th>备注</th>
                            <th>客户类型</th>
                            <th>办公电话</th>
                            <th>地址</th>
                        </tr>
                    </table>
                </li>
                <li>3、手机号重复的客户不重复导入；
                </li>
                <li>4、手机号为空或格式不正确的客户不导入；
                </li>
                <li>5、客户属性不分前后顺序；
                </li>
                <li>6、表格样式下载【<a href='Resource/客户资料导入表格.xlsx' target="_blank">下载</a>】
                </li>
            </ul>
        </div>
        <div style="padding: 10px; margin-top: 20px;" class="line">
            二、 导入客户资料
        </div>
        <div style="line-height: 26px; padding-left: 40px;">
            <asp:FileUpload ID="FileCustomer" runat="server" ClientIDMode="Static" Width="300" />
            <asp:LinkButton ID="btnImport" OnClientClick="return fnValid()" OnClick="btnImport_Click" ClientIDMode="Static" CssClass="easyui-linkbutton" iconcls="icon-import" runat="server">立即导入</asp:LinkButton>
            <div style="padding-top: 10px; color: red;">
                注：导入过程可能需要一定的时间，请勿重复导入！
            </div>
        </div>
    </form>
</body>
</html>
