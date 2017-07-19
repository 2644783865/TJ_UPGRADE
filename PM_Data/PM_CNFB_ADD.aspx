<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_CNFB_ADD.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_CNFB_ADD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加数据</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="RightContent" 
            style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
        <div class="box-inner">
          <div class="box_right">
             <div class="box-title">
               <table width="100%" >             
               <tr><td>请填写新增的数据信息</td></tr>
               </table>
            </div> 
         </div>
         </div>
     <div class="box-outer" align="center" style=" overflow:scroll">
        <table width="100%">
             <tr>
                <th>
                    项目名称
                </th>
                <th>
                    合同号
                </th>
                <th>
                    任务号
                </th>
                <th>
                    图号
                </th>
                <th>
                    设备名称
                </th>
                <th>
                    数量
                </th>
                <th>
                    本月明义结算金额（元）
                </th>
                <th>
                    本月实际结算金额（元）
                </th>
                <th>
                    年份
                </th>
                <th>
                    月份
                </th>
                <th>
                    班组
                </th>
             </tr>
             <tr>
                <td align="center">
                    <asp:TextBox ID="tbprojname" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbprojid" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbrwh" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbth" runat="server"></asp:TextBox>
                </td>
                <td style="width:300px;white-space:normal" align="center">
                    <asp:TextBox ID="tbsbname" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbsl" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbbymymoney" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbbyrealmoney" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbyear" runat="server" align="center"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbmonth" runat="server" align="center"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbtype" runat="server" align="center"></asp:TextBox>
                </td>
              </tr> 
        </table>
       </div>
       <div style="height:20px; padding-top:5px;" align="center" >
                    <asp:Button ID="btnConfirm" runat="server" Text="添加" 
                        onclick="btnConfirm_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small"
                      />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="false" 
                            onclick="btnCancel_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small" />
       </div>
    </div>
    </form>
</body>
</html>
