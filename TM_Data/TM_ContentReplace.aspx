<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_ContentReplace.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_ContentReplace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/UserDefinedQueryConditions.ascx" tagname="UserDefinedQueryConditions" tagprefix="uc3" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>内容替换</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function openLink(url) 
    {
        var date=new Date();
        var time=date.getTime();
        var returnVlue=window.showModalDialog("TM_Original_Data_Operate.aspx?NoUse="+time+"&register=" + url,'',"dialogHeight:700px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                      <table width="100%" >
                      <tr>
                      <td align="center">项目名称:<asp:Label ID="lblProjName" runat="server" Text="Label"></asp:Label></td>
                        <td align="center">工程名称:<asp:Label ID="lblEngName"  runat="server" Text="Label"></asp:Label>
                                   <asp:HiddenField ID="hdfType" runat="server" />
                           <asp:HiddenField ID="hdfEngid" runat="server" />
                           <asp:HiddenField ID="hdfProid" runat="server" />
                        </td>
                      </tr>
                      </table>
                    </div>
                </div>
            </div>
      <div class="box-wrapper">
        <div class="box-outer">
        <table width="100%">
         <tr>
         <td align="right">
          查询类别:<asp:DropDownList ID="ddlQueryType" runat="server">
              <asp:ListItem Text="-请选择-" Value="0" ></asp:ListItem>
              <asp:ListItem Text="总序" Value="BM_ZONGXU"></asp:ListItem>
              <asp:ListItem Text="序号" Value="BM_XUHAO"></asp:ListItem> 
              <asp:ListItem Text="明细序号" Value="BM_MSXUHAO"></asp:ListItem> 
              <asp:ListItem Text="中文名称" Value="BM_CHANAME"></asp:ListItem>
              <asp:ListItem Text="图号" Value="BM_TUHAO"></asp:ListItem>
               <asp:ListItem Text="规格" Value="BM_GUIGE"></asp:ListItem> 
               <asp:ListItem Text="条件属性" Value="BM_BEIZHUATR"></asp:ListItem>
              <asp:ListItem Text="物料编码" Value="BM_MARID"></asp:ListItem> 
              <asp:ListItem Text="物料名称" Value="BM_MANAME"></asp:ListItem>
              <asp:ListItem Text="物料规格" Value="BM_MAGUIGE"></asp:ListItem> 
              <asp:ListItem Text="国标(标准)" Value="BM_STANDARD"></asp:ListItem>
              <asp:ListItem Text="材质" Value="BM_MAQUALITY"></asp:ListItem>
               <asp:ListItem Text="备注" Value="BM_NOTE"></asp:ListItem>  
            </asp:DropDownList></td>
         <td align="left">
            <asp:TextBox ID="txtMCorZX" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnOrgQuery" runat="server" OnClick="btnOrgQuery_OnClick" Text="查询" />
            &nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" Text="重置" OnClick="btnClear_OnClick"  />
        </td>
         <td align="center">排序方式:<asp:DropDownList ID="ddlSort" runat="server">
             <asp:ListItem Text="综合" Value="BM_ORDERINDEX"></asp:ListItem>
              <asp:ListItem Text="序号" Value="BM_XUHAO"></asp:ListItem>
              <asp:ListItem Text="总序" Value="BM_ZONGXU" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
         <td>
                 <asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server"><asp:Image ID="Image8" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"  align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
        <cc1:PopupControlExtender  ID="PopupControlExtender5" CacheDynamicResults="false" Position="Left"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="-55"  TargetControlID="HyperLink5" PopupControlID="palMS3">
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
            <td align="right"><asp:Button ID="Button1" runat="server" UseSubmitBehavior="false" OnClick="btnOrgQuery_OnClick"  Text="查 询" />
            &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnClear_Ud" Text="清 空" />&nbsp;&nbsp;</td>
         </tr>
          <tr>
          <td align="left" style="width:98%">
             <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskView"  QueryRows="8" />
          </td>
          </tr>
        </table>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
        </td>
         </tr>
         <tr>
           <td align="right">
               <asp:DropDownList ID="ddlRepType" runat="server">
                 <asp:ListItem Text="图号" Value="BM_TUHAO" Selected="True"></asp:ListItem>
                 <asp:ListItem Text="备注" Value="BM_NOTE"></asp:ListItem>
                 <asp:ListItem Text="毛坯形状" Value="BM_MASHAPE"></asp:ListItem>
                 <asp:ListItem Text="毛坯状态" Value="BM_MASTATE"></asp:ListItem>
               </asp:DropDownList>&nbsp;&nbsp;
               <asp:DropDownList ID="ddlReplaceType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReplaceType_OnSelectedIndexChanged">
                 <asp:ListItem Text="替换" Value="Replace" Selected="True"></asp:ListItem>
                 <asp:ListItem Text="追加" Value="Add"></asp:ListItem>
               </asp:DropDownList>
           </td>
           <td align="left">
             <div id="replace" runat="server">
                 由<asp:TextBox ID="txtStart" runat="server"></asp:TextBox>替换为<asp:TextBox ID="txtEnd" runat="server"></asp:TextBox></div>
             <div id="add" runat="server"  visible="false">
                 <asp:TextBox ID="txtAdd" runat="server"></asp:TextBox>
                 </div>
           </td>
           <td>&nbsp;&nbsp; <asp:Button ID="btnSave" runat="server" CommandName="Replace" OnClick="btnSave_Replace" OnClientClick="return confirm('确认提交吗？');" Text="替 换" />
           
           </td>
         <td align="left">
        <span style="color:Red;">以下显示数据为可替换项,请输入较精确的筛选条件，已减少错误替换！！！</span>
        </td>
           <td></td>
         </tr>
         <tr>
          <td colspan="5"><hr /></td>
         </tr>
        </table>
        
        
    <asp:CheckBoxList ID="cklHiddenShow" runat="server" RepeatDirection="Horizontal" RepeatColumns="17" OnSelectedIndexChanged="cklHiddenShow_OnSelectedIndexChanged"  AutoPostBack="true" RepeatLayout="Table">
    </asp:CheckBoxList> 
    
    <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" Font-Size="Large">
        没有记录!</asp:Panel>
    <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" DataKeyNames="BM_XUHAO" 
        onrowdatabound="GridView1_RowDataBound" AllowPaging="False">
        <RowStyle BackColor="#EFF3FB"  Wrap="false" />
        <HeaderStyle Wrap="false" />
        <Columns>
            <asp:TemplateField HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="chkDelete()" CssClass="checkBoxCss"/>
                    <input id="Hidden1" type="hidden" runat="server" />
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false" >
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                    <asp:HiddenField ID="hdfOrgState" runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_OSSTATE").ToString()+"-"+Eval("BM_OSSTATUS").ToString()+"-"+Eval("BM_CONDICTIONATR").ToString()+"-"+Eval("BM_MARID").ToString()+"-"+Eval("BM_WMARPLAN").ToString() %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="C" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="Z" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="W" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="BM_MSXUHAO"  HeaderText="明细序号"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" HeaderStyle-Wrap="false" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO"  HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID"   HeaderText="物料编码"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU"   HeaderText="总序"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME"   HeaderText="中文名称"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE"   HeaderText="规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU"   HeaderText="库"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>            
            <asp:BoundField DataField="BM_BEIZHUATR"   HeaderText="条件属性"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KEYCOMS"   HeaderText="关键部件"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="委外部门" HeaderStyle-Wrap="false">
            <ItemTemplate>
                <asp:Label ID="condiction" runat="server"  CssClass="notbrk"
                Text='<%# Eval("BM_CONDICTIONATR").ToString()=="03"?"技术部":Eval("BM_CONDICTIONATR").ToString()=="06"?"采购部":"" %>'></asp:Label>                           
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BM_MANAME"   HeaderText="材料名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE"   HeaderText="材料规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH"   DataFormatString="{0:F2}" HeaderText="宽度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT"   DataFormatString="{0:F2}" HeaderText="理论重量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT"   DataFormatString="{0:F2}" HeaderText="材料单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT"   DataFormatString="{0:F2}" HeaderText="材料总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALLGTH"   DataFormatString="{0:F2}" HeaderText="材料总长(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MABGZMY"   DataFormatString="{0:F2}" HeaderText="面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY"   HeaderText="材质"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER"   HeaderText="单台数量|总数量|计划数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT"   DataFormatString="{0:F2}" HeaderText="单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT"   DataFormatString="{0:F2}" HeaderText="总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUUNITWGHT"   DataFormatString="{0:F2}" HeaderText="图纸上单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUTOTALWGHT"   DataFormatString="{0:F2}" HeaderText="图纸上总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE"   HeaderText="毛坯形状"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASTATE"   HeaderText="毛坯状态"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS"   HeaderText="工艺流程"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_STANDARD"   HeaderText="国标"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NOTE"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px"  TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    <uc1:UCPaging ID="UCPagingOrg" runat="server" />
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
    </form>
</body>
</html>
