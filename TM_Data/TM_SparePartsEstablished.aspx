<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.master"  CodeBehind="TM_SparePartsEstablished.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_SparePartsEstablished" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/UserDefinedQueryConditions.ascx" tagname="UserDefinedQueryConditions" tagprefix="uc3" %>
<asp:Content ID="Content1" runat="server"  contentplaceholderid="RightContentTitlePlace">
 <table width="100%" >
  <tr>
    <td align="left">生成备件</td>
  </tr>
 </table>
 <script language="javascript" type="text/javascript">
  function selectOrgAll(obj)
  {
     table=document.getElementById("ctl00_PrimaryContent_GridView1");
     if(obj.checked)
     {
         for(i=1;i<table.rows.length;i++)
         {
            objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            objstr.checked=true;
         }
     }
     else
     {
        for(i=1;i<table.rows.length;i++)
        {
            objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            objstr.checked=false;
        }
     }
}

function ItemSelected()
{
   var table=document.getElementById("<%=GridView1.ClientID%>");
   var tr=table.getElementsByTagName("tr");
   var checkbox;
   var index=0;
   for(i=1;i<tr.length;i++)
   {
      checkbox=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
      if(checkbox.checked)
      {
         index++;
      }
   }
   if(index==0)
   {
      alert("请勾选要生成备件的记录！！！");
      return false;
   }
   else
   {
       var ok=confirm("确认生成备件计划吗？");
       if(ok==true)
       {
          document.getElementById("<%=btnCreate.ClientID%>").click();
          document.getElementById("btnBJCreate").disabled=true;
       }
       return ok;
   }
   
}
function CheckXishu(obj)
{
   var pattem=/^\d+(\.\d+)?$/;//数量验证
   if(!pattem.test(obj.value))
   {
      alert("请输入正确的系数！！！");
      obj.value="0.1";
   }
}
</script>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
       <table width="100%">
      <tr>
      <td>项目名称:<asp:Label ID="lblProjName" runat="server" Text="Label"></asp:Label></td>
        <td>工程名称:<asp:Label ID="lblEngName"  runat="server" Text="Label"></asp:Label>
                   <asp:HiddenField ID="hdfType" runat="server" />
           <asp:HiddenField ID="hdfEngid" runat="server" />
           <asp:HiddenField ID="hdfProid" runat="server" />
        </td>
        <td>台数:<asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>
          <input id="hdfNumber" runat="server" type="hidden" /></td>
          <td align="right">设定生成系数:<asp:TextBox ID="txtXiShu" Width="40px" onchange="CheckXishu(this);" Text="0.01" runat="server"></asp:TextBox></td>
          <td align="right">标识前缀:<asp:TextBox ID="txtBiaoshi" onblur="if(this.value==''){this.value='如:FER-SRC135.02.04.04';this.style.color='Gray';}" onfocus="if(this.value=='如:FER-SRC135.02.04.04'){this.value='';this.style.color='Black';}" runat="server"></asp:TextBox></td>
         <td align="center">
             <input id="btnBJCreate" type="button" value="生成备件" onclick="ItemSelected();" />
              <asp:Button ID="btnCreate" runat="server" CssClass="hidden" Text="生成备件"  OnClick="btnCreate_OnClick" />
          </td>
          <td>
          <asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server"><asp:Image ID="Image8" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"  align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
        <cc1:PopupControlExtender  ID="PopupControlExtender5" CacheDynamicResults="false" Position="Left"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="0"  TargetControlID="HyperLink5" PopupControlID="palMS3">
        </cc1:PopupControlExtender>
        <asp:Panel ID="palMS3" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Always" runat="server">
        <ContentTemplate>
        <table>
         <tr>       
         <td>
              <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                  <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
              </div>
              <br /><br />
         </td>
         </tr> 
            <tr>
            <td align="right"><asp:Button ID="btnQurey" runat="server" UseSubmitBehavior="false" OnClick="btnQurey_OnClick" Text="查 询" />
            &nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick" Text="清 空" />&nbsp;&nbsp;</td>
         </tr>
          <tr>
          <td align="left" style="width:98%">
             <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="BJ"  QueryRows="6" />
          </td>
          </tr>
        </table>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
          </td>
        </tr>
       </table>
       
       </div>
       </div>
       </div>
    <div class="box-wrapper" >
        <div class="box-outer">
        <table width="100%">
          <tr>
        <td align="center">
            全选/取消<input id="Checkbox2" runat="server" onclick="selectOrgAll(this);" type="checkbox" />&nbsp;&nbsp;
              <asp:Button ID="btnSelect_org" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_org_OnClick" Text="连选" />
        </td>
         <td>生成方式:<asp:DropDownList ID="ddlBJType" runat="server" ToolTip="按单条记录生产备件:图号保留；按物料编码生成备件:按物料编码汇总">
             <asp:ListItem Text="按单条记录生成备件" Value="0"></asp:ListItem>
             <asp:ListItem Text="按物料编码生成备件" Value="1" Selected="True"></asp:ListItem>
           </asp:DropDownList></td>
          <td>部件名称:<asp:DropDownList ID="ddlParts" runat="server"  OnSelectedIndexChanged="btnQurey_OnClick" AutoPostBack="true">
              </asp:DropDownList>
          </td> 
          <td>计算方式:<asp:DropDownList ID="ddlJiSuanType" runat="server" ToolTip="单台数量乘以系数后的舍入舍出方式">
               <asp:ListItem Text="不足1舍入(1.4>2;0.4>1)" Value="0"></asp:ListItem>
               <asp:ListItem Text="不足1舍出(1.4>1;0.4>0)" Value="1"></asp:ListItem>
               <asp:ListItem Text="不足1舍出,为0置1(1.4>1;0.4>1)" Value="2" Selected="True"></asp:ListItem>
              </asp:DropDownList>
          </td>
          <td>
          生成备件父级:<asp:DropDownList ID="ddlBJFA" runat="server" ToolTip='序号只含有1个"."，名称为"随机备件"' AutoPostBack="true">
              </asp:DropDownList>
          </td>
          </tr>
        </table>
      <asp:Panel ID="NoDataPanel" Visible="false" runat="server"  HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />没有记录!</asp:Panel>
          <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
    CellPadding="2" ForeColor="#333333" Width="100%">
    <RowStyle BackColor="#EFF3FB" />
    <Columns>
     <asp:TemplateField>
       <ItemTemplate>
           <asp:CheckBox ID="CheckBox2" runat="server" />
       </ItemTemplate>
     </asp:TemplateField>
     <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="行号">
       <ItemTemplate>
           <%# Convert.ToInt32(Container.DataItemIndex +1) %><asp:Label ID="Label1" Visible="false" runat="server"
               Text='<%# Eval("BM_ID") %>'></asp:Label>
       </ItemTemplate>
     </asp:TemplateField>
            <asp:BoundField DataField="BM_XUHAO" HeaderStyle-Wrap="false" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" HeaderStyle-Wrap="false" HeaderText="图号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE" HeaderText="规格" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" HeaderText="材料长度" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH" HeaderText="材料宽度" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="材料单重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="材料总重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT" DataFormatString="{0:F2}" HeaderText="单重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT" DataFormatString="{0:F2}" HeaderText="总重" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MATOTALLGTH" HeaderText="材料总长" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE" HeaderText="毛坯形状" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField> 
            <asp:BoundField DataField="BM_MASTATE" HeaderText="毛坯状态" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField> 
             <asp:BoundField DataField="BM_KU" HeaderText="库" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>        
          </Columns>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />
    <FixRowColumn TableHeight="450px" TableWidth="100%" FixRowType="Header" FixColumns="0,1,2,3" />
</yyc:SmartGridView>
      </div>
      </div>
      </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 70%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
