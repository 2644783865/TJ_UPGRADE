<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPaging.ascx.cs" 
Inherits="ZCZJ_DPF.UCPaging" %>
<script type="text/javascript" language="javascript">
function checkNum(obj)
{
var page=obj.value;
var patten=/^[1-9][0-9]{0,9}$/;
if(!patten.test(page))
{
alert('请输入正确数字格式！');
obj.value="1";
obj.focus();
}
}
</script>
<table width="100%">
  <tr>             
    <td style="text-align:right">
     第<asp:Label ID="lblPageIndex" runat="server" />
     /<asp:Label ID="lblPageCount" runat="server" />页 
     <asp:Button ID="btnFirst" runat="server"  causesvalidation="False" BorderStyle="None" 
             commandname="First" text="首页" 
            onclick="btnFirst_Click" style="height: 16px" BackColor="White" width="38px" 
            Font-Size="12px" />
     <asp:Button ID="btnPrev" runat="server"  causesvalidation="False"  BorderStyle="None"   style="height: 16px" BackColor="White" 
            Font-Size="12px"
             commandname="Previous" text="上一页"  width="38px" 
            onclick="btnPrev_Click" />       
         <asp:Button ID="btnNext" runat="server"  causesvalidation="False"  BorderStyle="None" width="38px"    style="height: 16px" BackColor="White" 
             commandname="Next" text="下一页" onclick="btnNext_Click" />   
    <%-- <asp:LinkButton ID="btnNext" runat="server"  causesvalidation="False" 
             commandname="Next" text="下一页" onclick="btnNext_Click" />   --%>                       
     <asp:Button ID="btnLast" runat="server"  causesvalidation="False"  BorderStyle="None"  width="38px"   style="height: 16px" BackColor="White" 
             commandname="Last" text="尾页" onclick="btnLast_Click" />                                            
     转到第<asp:TextBox ID="txtNewPageIndex" runat="server" width="50px"  onblur="javascript:checkNum(this);"/>页
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtNewPageIndex"></asp:RequiredFieldValidator>
        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入正确数字格式！" ControlToValidate="txtNewPageIndex" ValidationExpression="^[1-9][0-9]{0,9}$" Display="Dynamic"></asp:RegularExpressionValidator> 
    --%> <asp:Button ID="btnGo" runat="server" causesvalidation="False"  BorderStyle="None" width="38px"    style="height: 16px" BackColor="White" 
       OnClick="GoIndex" commandname="Page" text="GO" />
     </td>
  </tr>
</table>
