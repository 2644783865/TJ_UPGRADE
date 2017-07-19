<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Tech_Assign_Split.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Tech_Assign_Split" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server"> 
    项目拆分</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<JSR:JSRegister ID="JSRegister1" runat="server" />
<script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script> 
<script type="text/javascript">

   function checkNums(obj)
   {
      var pattem=/^\d+$/;
      var txtnums=obj.value;
      if(!pattem.test(txtnums))
      {
         obj.value="1";
         alert("请输入正整数！！！");
      }
   }

/*工程拆分*/
function tsaAdd()
{
    var i=document.getElementById('<%=tsanum.ClientID%>').value;
    var pattem=/^\d+$/;
    if(!pattem.test(i))
    {
        document.getElementById('<%=tsanum.ClientID%>').value="1";
        alert('请输入正确行数!');
        return false;
    }
}

//项目任务确定是否删除
function check()
{
    var table=document.getElementById("<%=GridView1.ClientID%>");;
    if(table)
    {
        tr=table.getElementsByTagName("tr");
        var count=0;
        for(var i=1;i<tr.length;i++)
        {
            obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
            if(obj.checked)
            {    
               count=i;
               break;
            }
        }
        if(count>0)
        {
            var i=confirm('确定删除吗?');
            if(i==true)
            {
                document.getElementById("<%=numid.ClientID%>").value=count;
            }
            else
            {
                return false;
            }
        }
        else            
        {
            alert("请选择要删除的行!");
            return false;
        }
    }
}
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
<div class="RightContent">
     <div class="box-wrapper">
        <div class="box-outer">
           <div class="box-inner">
            <div class="box_right">
             <div class="box-title">
                 <table width="99%" >
                    <tr>
                    <td>工程拆分</td>
                    <td align="right">
                        <asp:HyperLink ID="goback" CssClass="hand" runat="server" onclick="history.back(-1);">
                        <asp:Image ID="Imageback" ImageUrl="~/Assets/icons/back.png" border="0" hspace="2" Height="16" Width="16"
                            align="absmiddle" runat="server" />返回</asp:HyperLink>                       
                    </td>
                    </tr>
                 </table>
             </div>
            </div>
            </div>
           <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
               <tr>
               <td class="tdleft1">生产制号:</td>
               <td class="tdright1">
                <asp:Label ID="tsaid" runat="server"></asp:Label>
                <input id="proname" type="text" runat="server" readonly="readonly" style="display: none" />
                <input id="taskname" type="text" runat="server" readonly="readonly" style="display: none" />               </td>
               <td colspan="2" align="right">拆分任务数:
                   <input ID="tsanum" runat="server" value="1" onblur="checkNums(this);" type="text" style="width:50px" />&nbsp;&nbsp;
                   <asp:Button ID="btnTsaAdd" runat="server" Text="增 加" 
                       OnClientClick="return tsaAdd()" onclick="btnTsaAdd_Click"/>
                   &nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnTsaSave" OnClientClick="return confirm('添加后不能修改,确认保存吗？');" runat="server" Text="保 存" 
                       onclick="btnTsaSave_Click" />
               </td>
               </tr>
               <tr>
               <td colspan="4">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Width="10px" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ENG_ID" HeaderText="工程简称" >
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TSA_ID" HeaderText="生产制号" >
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="工程名称" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="engname" runat="server" readonly="readonly" style="border-style:none; width:100%" type="text" value='<%# Eval("ENG_NAME") %>'/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TSA_ENGSTRSMTYPE" HeaderText="工程类型" >
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <AlternatingRowStyle BackColor="White" />                    
                 </asp:GridView>
                 <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                </td>
               </tr>
            </table>
            
         </div>
       </div>
    </div>
       <input id="numid" type="text" value="0" runat="server" readonly="readonly" style="display: none" />
  </ContentTemplate>
</asp:UpdatePanel>    
</asp:Content>

