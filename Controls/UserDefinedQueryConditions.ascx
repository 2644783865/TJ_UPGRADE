<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDefinedQueryConditions.ascx.cs" Inherits="ZCZJ_DPF.UserDefinedQueryConditions" %>
<script language="javascript" type="text/javascript">
function CheckQueryString(obj)
{
  var table=obj.parentNode.parentNode.parentNode;
  var index=obj.parentNode.parentNode.rowIndex;
  if(index!=1)
  {
     var firstColumnsSelected=table.getElementsByTagName("tr")[1].getElementsByTagName("td")[2].getElementsByTagName("select")[0].value;
     if(firstColumnsSelected=="-请选择-")
     {
        alert("请将第一行的查询条件填写完整！！！");
        table.getElementsByTagName("tr")[index].getElementsByTagName("td")[2].getElementsByTagName("select")[0].value="-请选择-";
     }
  }
  else if(index==1&&obj.value=="-请选择-")
  {
      for(var i=2;i<table.rows.length;i++)
      {
          var otherColumnsSelected=table.rows[i].cells[2].getElementsByTagName("select")[0].value;
          if(otherColumnsSelected!="-请选择-")
          {
             alert("需要选择第一行查询【字段名】！！！");
             table.getElementsByTagName("tr")[index].getElementsByTagName("td")[2].getElementsByTagName("select")[0].value="-请选择-";
             table.rows[1].cells[2].getElementsByTagName("select")[0].options[1].selected =true;
             break;
          }
      }
  }
}
</script>
<table width="100%">
<tr>
<td>
  <asp:GridView ID="GridView1" runat="server" width="100%">
  <RowStyle BackColor="#EFF3FB" />
    <Columns>                 
    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
           <asp:DropDownList ID="ddlLogic" BackColor="AliceBlue" runat="server">
               <asp:ListItem Value="OR" Selected="True">或者</asp:ListItem>
               <asp:ListItem Value="AND">并且</asp:ListItem>
           </asp:DropDownList>
       </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
           <asp:DropDownList ID="ddlStartKuohao" BackColor="AliceBlue" runat="server">
               <asp:ListItem Value="" Selected="True"></asp:ListItem>
               <asp:ListItem Value="(">(</asp:ListItem>
           </asp:DropDownList>
       </ItemTemplate>
    </asp:TemplateField> 
    <asp:TemplateField HeaderText="字段名" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
         <ItemTemplate>
             <asp:DropDownList ID="ddlColumnsName" runat="server" BackColor="AliceBlue" onchange="CheckQueryString(this);">
             </asp:DropDownList>
         </ItemTemplate>
     </asp:TemplateField>
    <asp:TemplateField HeaderText="关系" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
         <ItemTemplate>
             <asp:DropDownList ID="ddlRelation" BackColor="AliceBlue" runat="server">
                <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                <asp:ListItem Value="7">不包含</asp:ListItem>
                <asp:ListItem Value="1">等于</asp:ListItem>
                <asp:ListItem Value="2">不等于</asp:ListItem>
                <asp:ListItem Value="3">大于</asp:ListItem>
                <asp:ListItem Value="4">大于或等于</asp:ListItem>
                <asp:ListItem Value="5">小于</asp:ListItem>
                <asp:ListItem Value="6">小于或等于</asp:ListItem>
             </asp:DropDownList>
         </ItemTemplate>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="数值" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
           <asp:TextBox ID="txtVaue" runat="server"></asp:TextBox>
       </ItemTemplate>
     </asp:TemplateField>  
     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
       <ItemTemplate>
           <asp:DropDownList ID="ddlEndKuohao" BackColor="AliceBlue" runat="server">
               <asp:ListItem Value="" Selected="True"></asp:ListItem>
               <asp:ListItem Value=")">)</asp:ListItem>
           </asp:DropDownList>
       </ItemTemplate>
    </asp:TemplateField>    
    </Columns>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" /> 
  </asp:GridView>
</td>
</tr>
<tr>
<td align="left">
    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Red" Visible="false" Text="选择的查询条件有误！！！"></asp:Label></td>
</tr>
</table>