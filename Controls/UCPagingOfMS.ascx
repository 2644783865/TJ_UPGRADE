<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPagingOfMS.ascx.cs" 
Inherits="ZCZJ_DPF.UCPagingOfMS" %>
<table width="100%">
  <tr>             
    <td style="text-align:right">
     第<asp:Label ID="lblPageIndex" runat="server" />
     /<asp:Label ID="lblPageCount" runat="server" />页 
     <asp:Button ID="btnFirst" runat="server" OnClientClick="return confirm('请确认已保存！！！\r\r如果已保存请点击【确定】，否则点击【取消】');"  causesvalidation="False" BorderStyle="None" 
             commandname="First" text="首页" 
            onclick="btnFirst_Click" style="height: 16px" BackColor="White" width="38px" 
            Font-Size="12px" />
     <asp:Button ID="btnPrev" runat="server" OnClientClick="return confirm('请确认已保存！！！\r\r如果已保存请点击【确定】，否则点击【取消】');"  causesvalidation="False"  BorderStyle="None"   style="height: 16px" BackColor="White" 
            Font-Size="12px"
             commandname="Previous" text="上一页"  width="38px" 
            onclick="btnPrev_Click" />       
         <asp:Button ID="btnNext" runat="server" OnClientClick="return confirm('请确认已保存！！！\r\r如果已保存请点击【确定】，否则点击【取消】');"  causesvalidation="False"  BorderStyle="None" width="38px"    style="height: 16px" BackColor="White" 
             commandname="Next" text="下一页" onclick="btnNext_Click" />                        
     <asp:Button ID="btnLast" runat="server" OnClientClick="return confirm('请确认已保存！！！\r\r如果已保存请点击【确定】，否则点击【取消】');"  causesvalidation="False"  BorderStyle="None"  width="38px"   style="height: 16px" BackColor="White" 
             commandname="Last" text="尾页" onclick="btnLast_Click" />                                            
     转到第<asp:TextBox ID="txtNewPageIndex" runat="server" width="50px" />页
     <asp:Button ID="btnGo" runat="server" OnClientClick="return confirm('请确认已保存！！！\r\r如果已保存请点击【确定】，否则点击【取消】');" causesvalidation="False"  BorderStyle="None" width="38px"    style="height: 16px" BackColor="White" 
       OnClick="GoIndex" commandname="Page" text="GO" />
     </td>
  </tr>
</table>
