<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_FJCB_ADD.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_FJCB_ADD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
     <div class="box-outer" align="center" style=" overflow:scroll;width:99%">
        <table width="100%">
             <tr>
                <th>
                    任务号
                </th>
                <th>
                    油漆种类
                </th>
                <th>
                    用量（L）
                </th>
                <th>
                    单价（含税）
                </th>
                <th>
                    金额（含税）
                </th>
                <th>
                    稀释剂用量（L）
                </th>
                <th>
                    稀释剂单价（含税）
                </th>
                <th>
                    稀释剂金额（含税）
                </th>
                <th>
                    合计金额（含税）
                </th>
                <th>
                    合计不含税金额
                </th>
                <th>
                    年份
                </th>
                <th>
                    月份
                </th>
             </tr>
             <tr>
                <td align="center">
                    <asp:TextBox ID="tbrwh" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbyqzl" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbyqyl" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbyqdj" runat="server"></asp:TextBox>
                </td>
                <td style="width:300px;white-space:normal" align="center">
                    <asp:TextBox ID="tbyqje" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbxsjyl" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbxsjdj" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbxsjje" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbhjje" runat="server" align="center"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbhjbhsje" runat="server" align="center"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbyear" runat="server" align="center"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="tbmonth" runat="server" align="center"></asp:TextBox>
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
