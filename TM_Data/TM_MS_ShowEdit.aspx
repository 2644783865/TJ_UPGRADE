<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MS_ShowEdit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_ShowEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>制作明细修改</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <div class="box-wrapper">
        <div class="box-outer">
           <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
               <tr>
                <td align="center"><strong>序号:</strong></td>
                <td colspan="3">
                    <asp:TextBox ID="txtXuhao" Enabled="false" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
               <td align="right"><strong>明细序号:</strong></td>
               <td>
                   <asp:TextBox ID="txtMSXuhao" runat="server"></asp:TextBox></td>
               <td align="right"><strong>图号:</strong></td>
               <td>
                   <asp:TextBox ID="txtTuHao" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
               <td align="right"><strong>名称:</strong></td>
               <td>
                   <asp:TextBox ID="txtCHName" runat="server"></asp:TextBox></td>
               <td align="right"><strong>规格:</strong></td>
               <td>
                   <asp:TextBox ID="txtGuiGe" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
               <td align="right"><strong>库:</strong></td>
               <td>
                   <asp:TextBox ID="txtKu" runat="server"></asp:TextBox></td>
               <td align="right"><strong>工艺流程:</strong></td>
               <td>
                   <asp:TextBox ID="txtProcess" runat="server"></asp:TextBox></td>
               </tr>
               <tr>
               <td align="right"><strong>备注:</strong></td>
               <td colspan="3">
                   <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Width="60%" Height="50"></asp:TextBox></td>
               </tr>
               <tr>
               <td colspan="4" align="center">
                   <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_OnClick" OnClientClick="return confirm('确认提交吗？');" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <input id="btnCancel" type="button" value="关 闭" title="不保存数据,直接关闭窗口" onclick="window.close();" />
                   
               </td>
               </tr>
           </table>
        </div>
        </div>
    </form>
</body>
</html>
