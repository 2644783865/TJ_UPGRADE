<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Gongshi_Daoru.aspx.cs"
    Inherits="ZCZJ_DPF.PM_Data.PM_Gongshi_Daoru" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Excel导入SQL数据库</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 640px;height:200px; border-collapse: separate; text-align: center ;border-style:double" >
            <tr> <td colspan="3">操作步骤：先输入要导入的文件的统计月份,选择要上传的Excel文件,然后点击按钮导入. </td></tr>   
            <tr>
             <td style="width:30%;text-align:right">
             请输入工时汇总年月：
             </td>
             <td style="text-align:left">
             <asp:TextBox ID="txt_date" runat="server" ></asp:TextBox>(例如：2014.5)
             <asp:RequiredFieldValidator ControlToValidate="txt_date" Text="请输入年月信息！！！" runat="server"></asp:RequiredFieldValidator>
             </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="305px" />
                    &nbsp; &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="点此将Excel表导入到系统中" OnClick="Button1_Click" />
                </td>
                </tr>
                <tr> <td colspan="3"><span style="color:Red">注：要导入的工作表名默认为Sheet1，如不是Sheet1,请修改！</span></td></tr> 
        </table>
    </div>
    </form>
</body>
</html>
