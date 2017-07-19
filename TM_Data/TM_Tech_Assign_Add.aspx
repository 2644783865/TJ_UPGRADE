<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Tech_Assign_Add.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Tech_Assign_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server"> 
    项目拆分</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<JSR:JSRegister ID="JSRegister1" runat="server" />
<script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script> 
<script type="text/javascript">
/*项目拆分增加*/
function proAdd()
{
    var i=document.getElementById('<%=num.ClientID%>').value;
    var ddlid=document.getElementById('<%=ddlid.ClientID%>').value;
    if(ddlid =="0")
    {
        alert('请选择项目编号!');
        return false;
    }
    else
    {
        if(i=="")
        {
            alert('请输入行数!');
            return false;
        } 
    }
}


//项目任务确定是否删除
function check()
{
    var table=document.getElementById("<%=GridView1.ClientID%>");
    if(table)
    {
        var obj=table.getElementsByTagName("input");
        var objstr = '';
        for(var i=0;i<obj.length;i++)
        {
            if(obj[i].type.toLowerCase()=="checkbox" && obj[i].value!="")
            {
                if( obj[i].checked)
                {    
                    objstr="checked";
                }
            }
        }
        if(objstr=="checked")
        {
            var i=confirm('确定删除吗?');
            if(i==true)
            {
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
    else
    {
        return false;
    }
}

//数值检查
function CheckNum(obj)
{
    var pattem=/^[1-9][0-9]*$/;//数量验证
    var testnum=obj.value;
    if(pattem.test(testnum))
    {
       if(parseInt(testnum)>10)
       {
          alert("请输入的数值大于10,请重新输入！！！");
          obj.value="1";
       }
    }
    else
    {
        alert("请输入正确的数值！！！");
        obj.value="1";
    }
}
</script>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
   <div class="RightContent">
     <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="99%" >
                    <tr>
                    <td> 项目拆分</td>
                     <td align="right">
                        <td align="right">
                        <asp:HyperLink ID="goback" CssClass="hand" runat="server" onclick="history.go(-1);">
                        <asp:Image ID="Imageback" ImageUrl="~/Assets/icons/back.png" border="0" hspace="2" Height="16" Width="16"
                            align="absmiddle" runat="server" />返回</asp:HyperLink>                       
                    </td>                      
                    </td>
                    </tr>
                 </table>
             </div>
         </div>
     </div>
    
     <div class="box-wrapper">
        <div class="box-outer">
        
           <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <tr>
                    <td class="tdleft1" style="width:15%;">项目编号:</td>          
                    <td class="tdright1" style="width:20%;">
                        <asp:ComboBox ID="proid" runat="server" AutoCompleteMode="SuggestAppend"
                            AutoPostBack="true" DropDownStyle="DropDownList" Height="15px" 
                            Width="90px" onselectedindexchanged="proid_SelectedIndexChanged">
                        </asp:ComboBox><span class="red">*</span>
                        <input id="ddlid" type="text" value="0" runat="server" readonly="readonly" style="display: none" />
                    </td>
                    <td class="tdleft1" style="width:15%;">项目名称:</td>     
                    <td class="tdright1" style="width:20%;">
                    <asp:Label ID="proname" runat="server"></asp:Label>
                    </td>
                    <td class="tdleft1" style="width:15%;"><asp:Label ID="lblRedTip" runat="server" Text="电气制号:"></asp:Label></td>     
                    <td class="tdright1" style="width:15%;">
                        <asp:DropDownList ID="ddlElecQup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlElecQup_OnSelectedIndexChanged">
                          <asp:ListItem Text="否" Value="N"></asp:ListItem>
                          <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    </td>
               </tr>
                <tr>
               <td colspan="6" align="right">项目拆分数:
                   <input ID="num" runat="server" value="1" type="text" style="width:50px" onblur="CheckNum(this);" />&nbsp;&nbsp;
                   <asp:Button ID="btnadd" runat="server" Text="增 加" OnClientClick="return proAdd()" onclick="btnadd_Click" />
                   &nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnConfirm" runat="server" Text="保 存" OnClientClick=" return confirm('确认添加吗？\r\r提示:添加后无法修改及删除,请仔细确认！！！');" onclick="btnConfirm_Click"/>
               </td>
               </tr>
               <tr>
                <td colspan="6">
                <asp:GridView ID="GridView1" width="100%" runat="server"
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
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工程简称" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ComboBox ID="engid" runat="server" AutoCompleteMode="SuggestAppend"
                                    AutoPostBack="true" DropDownStyle="DropDownList" Height="15px" DataValueField='<%#DataBinder.Eval(Container.DataItem, "ENG_ID") %>'
                                    Width="90px" onselectedindexchanged="engid_SelectedIndexChanged">
                                </asp:ComboBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TSA_ID" HeaderText="生产制号" >
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="工程名称" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="engname" runat="server" style="border-style:none;height:21px" type="text" value='<%# Eval("ENG_NAME") %>'/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工程量(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="engtotal" runat="server" style="border-style:none; height:21px" type="text" value='<%# Eval("TSA_TOTALWGHT") %>'/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="ENG_STRTYPE" HeaderText="工程类型" >
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
</ContentTemplate>
</asp:UpdatePanel>    
</asp:Content>

