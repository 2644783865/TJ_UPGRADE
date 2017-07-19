<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_OrgDataImport.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OrgDataImport" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPagingOfMS.ascx" tagname="UCPagingOfMS" tagprefix="uc2" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<%@ Register src="../Controls/UserDefinedQueryConditions.ascx" tagname="UserDefinedQueryConditions" tagprefix="uc3" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
  <table width="100%" >
      <tr>
      <td align="left">导入数据至目标工程</td>
      <td>项目名称:<asp:Label ID="lblProjName" runat="server" Text="Label"></asp:Label></td>
        <td>工程名称:<asp:Label ID="lblEngName"  runat="server" Text="Label"></asp:Label>
                   <asp:HiddenField ID="hdfType" runat="server" />
           <asp:HiddenField ID="hdfEngid" runat="server" />
           <asp:HiddenField ID="hdfProid" runat="server" />
        </td>
        <td>台数:<asp:TextBox ID="lblNumber" runat="server" Width="40px"></asp:TextBox></td>
      </tr>
      </table>
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" /> 
    <JSR:JSRegister ID="JSRegister1" runat="server" />
    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TMOrgInput.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/ChoiceRcmd.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/TM_ShowInput.js" type="text/javascript" charset="GB2312"></script>    
<script type="text/javascript" language="javascript">
        function SelectSourceAll(obj)
        {
             var table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel1_GridView1");
             if(obj.checked)
             {
                 for(i=1;i<table.rows.length;i++)
                 {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
                    objstr.checked=true;
                 }
             }
             else
             {
                for(i=1;i<table.rows.length;i++)
                {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
                    objstr.checked=false;
                }
             }
        }
        
        function SelectTargetAll(obj)
        {
             var table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel2_SmartGridView1");
             if(obj.checked)
             {
                 for(i=1;i<table.rows.length;i++)
                 {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
                    objstr.checked=true;
                 }
             }
             else
             {
                for(i=1;i<table.rows.length;i++)
                {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
                    objstr.checked=false;
                }
             }
        }
        
        function getClientId()        {            var paraId1 = '<%= SmartGridView1.ClientID %>';//注册控件1            var paraId2='<%= txtid.ClientID %>';            var paraId3='<%= istid.ClientID %>';            var paraId4='<%= lblNumber.ClientID %>';            var paraId5='<%= txtBxishu.ClientID %>';            var paraId6='<%= txtXxishu.ClientID %>';            return {Id1:paraId1,Id2:paraId2,Id3:paraId3,Id4:paraId4,BXishu:paraId5,XXishu:paraId6};//生成访问器        }

</script>
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0" >
      <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="数据源" TabIndex="0">
            <ContentTemplate>
            <div style="width:100%;">
              <table style="width:100%;">
                <tr>
                  <td>&nbsp;&nbsp;&nbsp;全选/取消<input id="ckbSelectAll" onclick="SelectSourceAll(this);" type="checkbox" />&nbsp;&nbsp;
              <asp:Button ID="btnSourceSelect" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSourceSelect_OnClick" Text="连选" />&nbsp;&nbsp;
                      <asp:Button ID="btnSelected" runat="server" OnClick="btnSelected_OnClick" ToolTip="勾选后的记录标记后可在目标工程页修改及查看" Text="标 记" />
              </td>
                  <td  class="notbrk">项目名称:</td>
                  <td style="width:12%; height:42px" align="left" valign="top"><cc1:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px" Width="80px"
                             AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                             onselectedindexchanged="ddlProName_SelectedIndexChanged">
                            </cc1:ComboBox></td>
                  <td  class="notbrk">工程名称:</td>
                  <td style="width:12%; height:42px" align="left" valign="top"><cc1:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px" Width="80px"
                            AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                            onselectedindexchanged="ddlEngName_SelectedIndexChanged">
                            </cc1:ComboBox></td>
                  <td  class="notbrk">部件名称:</td>
                  <td style="width:12%; height:42px" align="left" valign="top"><cc1:ComboBox ID="ddlbjname" runat="server" AutoPostBack="true" Height="15px" Width="80px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlbjname_SelectedIndexChanged">
                        </cc1:ComboBox></td>
                        <td class="notbrk">排序:</td>
                        <td style="height:42px" valign="top"><cc1:ComboBox ID="ddlSort" runat="server" AutoPostBack="true" Height="15px" Width="30px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlbjname_SelectedIndexChanged">
                         <asp:ListItem Text="综合" Value="BM_ORDERINDEX"></asp:ListItem>
                         <asp:ListItem Text="序号" Value="BM_XUHAO"></asp:ListItem>
                         <asp:ListItem Text="总序" Value="BM_ZONGXU" Selected="True"></asp:ListItem>
                        </cc1:ComboBox></td>
                  <td> <asp:HyperLink ID="HyperLink5" CssClass="hand" runat="server"><asp:Image ID="Image8" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"  align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
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
            <td align="right"><asp:Button ID="Button1" runat="server" UseSubmitBehavior="false" OnClick="ddlbjname_SelectedIndexChanged" Text="查 询" />
            &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick" Text="清 空" />&nbsp;&nbsp;</td>
         </tr>
          <tr>
          <td align="left" style="width:98%">
             <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskView"  QueryRows="8" />
          </td>
          </tr>
        </table>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel></td>
                  <td><asp:Button ID="btnClear" runat="server" Text="清空重选" OnClientClick="return confirm('重新选择项目工程！！！\r\r确认继续吗？');" OnClick="btnClear_OnClick" /></td>      
                  <td align="center"><asp:LinkButton ID="lnkbtnGotoTarget" OnClick="lnkbtnGotoTarget_OnClick" runat="server"><asp:Image ID="ImageTo" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />转到目标页</asp:LinkButton></td>
                         
                </tr>
              </table>
            </div>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" Font-Size="Large">
        没有记录!</asp:Panel>
    <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="BM_XUHAO"  AllowPaging="False">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px"  CssClass="checkBoxCss"/>
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
            <asp:BoundField DataField="BM_MSXUHAO" HeaderStyle-Wrap="false"  HeaderText="明细序号" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_XUHAO" HeaderStyle-Wrap="false" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" HeaderStyle-Wrap="false"  HeaderText="图号(标识号)" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID" HeaderStyle-Wrap="false"   HeaderText="物料编码" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU" HeaderStyle-Wrap="false"   HeaderText="总序" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderStyle-Wrap="false"   HeaderText="中文名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ENGSHNAME" HeaderStyle-Wrap="false"   HeaderText="英文名称" Visible="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE" HeaderStyle-Wrap="false"   HeaderText="规格" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU" HeaderStyle-Wrap="false"   HeaderText="库" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>            
            <asp:BoundField DataField="BM_BEIZHUATR" HeaderStyle-Wrap="false"   HeaderText="条件属性" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KEYCOMS" HeaderStyle-Wrap="false"   HeaderText="关键部件" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MANAME" HeaderStyle-Wrap="false"   HeaderText="材料名称">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE" HeaderStyle-Wrap="false"   HeaderText="材料规格" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" HeaderStyle-Wrap="false" DataFormatString="{0:f2}" HeaderText="长度(mm)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH" HeaderStyle-Wrap="false"   DataFormatString="{0:f2}" HeaderText="宽度(mm)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT" HeaderStyle-Wrap="false"   DataFormatString="{0:f2}" HeaderText="理论重量" >
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT" HeaderStyle-Wrap="false"   DataFormatString="{0:f2}" HeaderText="材料单重(kg)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT" HeaderStyle-Wrap="false"   DataFormatString="{0:f2}" HeaderText="材料总重(kg)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALLGTH" HeaderStyle-Wrap="false"   DataFormatString="{0:f2}" HeaderText="材料总长(mm)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MABGZMY" HeaderStyle-Wrap="false"   DataFormatString="{0:f2}" HeaderText="面域(m2)" >
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY" HeaderStyle-Wrap="false"  HeaderText="材质" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER" HeaderStyle-Wrap="false"  HeaderText="单台数量|总数量|计划数量" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT" HeaderStyle-Wrap="false"  DataFormatString="{0:f2}" HeaderText="单重(kg)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT" HeaderStyle-Wrap="false" DataFormatString="{0:f2}" HeaderText="总重(kg)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE" HeaderStyle-Wrap="false"  HeaderText="毛坯形状" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASTATE" HeaderStyle-Wrap="false" HeaderText="毛坯状态" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS" HeaderStyle-Wrap="false" HeaderText="工艺流程" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_STANDARD" HeaderStyle-Wrap="false" HeaderText="国标" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NOTE" HeaderStyle-Wrap="false"  HeaderText="备注" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager"  TableHeight="450px" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
    </ContentTemplate>
     </cc1:TabPanel>
     <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="目标工程" TabIndex="1">
            <ContentTemplate>
               <div>
                 <table width="100%">
                   <tr>
                   <td>
                   &nbsp;&nbsp;&nbsp;全选/取消<input id="Checkbox2" onclick="SelectTargetAll(this);" type="checkbox" />&nbsp;&nbsp;
                   <asp:Button ID="btnTargetSelect" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnTargetSelect_OnClick" Text="连选" />&nbsp;&nbsp;
                   <asp:Button ID="btnInsert" runat="server" Text=" 插 入" OnClientClick="return insert()"
                                OnClick="btnInsert_OnClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="删 除" OnClientClick="return check()"
                                OnClick="btnDelete_OnClick" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnReload" runat="server" OnClick="btnReload_OnClick" OnClientClick="return confirm('页面上数据全部清空！！！\r\r确认继续吗？')" Text="全部删除" />
                   &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" OnClientClick="return BomInputCheck(); " Text="保 存" OnClick="btnSave_OnClick" />
                   &nbsp;&nbsp;&nbsp;&nbsp;<input id="ckbTuhao" type="checkbox" title="勾选后,将鼠标放置到要复制的单元格中,按“↓”键复制至下一单元格" />自动添加图号
                       &nbsp;&nbsp;&nbsp;&nbsp;
                   <input id="ckbXuhao" type="checkbox" checked="checked" />自动添加序号
                   </td>
                 <td><strong>计划系数:</strong>(板)<input id="txtBxishu" runat="server" type="text" onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.1";this.foucs();}' style="width:30px"  value="1.1" />(型/圆钢)<input id="txtXxishu" runat="server" style="width:30px"  onblur='var pattem=/^\d+(\.\d+)?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1.05";this.foucs();}'  type="text" value="1.05" />(其它)<input id="txtQxishu" runat="server" style="width:30px" disabled="disabled"  type="text" value="1" /></td>

                        <td><asp:Button ID="btnClearColum" OnClick="btnClearColum_OnClick"  runat="server" Visible="false" Text="部件重量置0" /><span style="color:Red;"></span></td>
                        <td>
                            <asp:HyperLink ID="hylMore" CssClass="hand" runat="server"><asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />更多操作</asp:HyperLink>
                            
                            <%--<cc1:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-100"  OffsetY="0"  TargetControlID="hylMore" PopupControlID="Panel1">
                            </cc1:PopupControlExtender>--%>
                            <cc1:HoverMenuExtender ID="HoverMenuExtender1" TargetControlID="hylMore" OffsetX="-100" OffsetY="20" PopupControlID="Panel2" runat="server">
                            </cc1:HoverMenuExtender>
                            <asp:Panel ID="Panel2" runat="server" style="display:none;">
                                <table style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>
                                <tr>
                                 <td>
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnShowAll" runat="server" OnClick="btnShowAll_OnClick" BackColor="Transparent" BorderStyle="None" Text="全部显示" /></td>
                                </tr>
                                <tr>
                                   <td>&nbsp;<asp:Button ID="btnHidden" runat="server"  BackColor="Transparent" BorderStyle="None" OnClick="btnHidden_OnClick"  Text="隐藏非输入列" /></td>
                                </tr>
                                <tr>
                                   <td><asp:Button ID="btnHiddenPaper" runat="server" BackColor="Transparent" BorderStyle="None" OnClick="btnHiddenPaper_OnClick"  Text="隐藏图纸信息列" /></td>
                                </tr>
                                <tr>
                                 <td>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" ForeColor="Black" runat="server">自定义列显示</asp:HyperLink>
                                     <cc1:HoverMenuExtender ID="HoverMenuExtender2" TargetControlID="HyperLink1" OffsetX="-320" OffsetY="20" PopupControlID="Panel3" runat="server">
                                     </cc1:HoverMenuExtender>
                                        <asp:Panel ID="Panel3" runat="server" style="display:none;">
                                         <table width="320px" style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>
                                           <tr>
                                            <td>以下勾选列将不显示</td>
                                           </tr>
                                           <tr>
                                             <td>
                                                 <asp:CheckBoxList ID="ckbColumn" RepeatColumns="4" RepeatDirection="Horizontal" runat="server">
                                                   <asp:ListItem Text="序号" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="图号" Value="2"></asp:ListItem>
                                                   <asp:ListItem Text="中文名称" Value="5"></asp:ListItem>
                                                   <asp:ListItem Text="备注" Value="6"></asp:ListItem>
                                                   <asp:ListItem Text="材料长度" Value="7"></asp:ListItem>
                                                   <asp:ListItem Text="材料宽度" Value="8"></asp:ListItem>
                                                   <asp:ListItem Text="面域" Value="13"></asp:ListItem>
                                                   <asp:ListItem Text="条件属性" Value="14"></asp:ListItem>
                                                   <asp:ListItem Text="规格" Value="20"></asp:ListItem>
                                                   <asp:ListItem Text="毛坯" Value="28"></asp:ListItem>
                                                   <asp:ListItem Text="状态" Value="29"></asp:ListItem>
                                                   <asp:ListItem Text="工艺流程" Value="30"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </td>
                                           </tr>
                                           <tr>
                                             <td align="right">
                                                 <asp:Button ID="btnSubmit" OnClick="btnSubmit_OnClick" runat="server" Text="确 定" /></td>
                                           </tr>
                                         </table>
                                        </asp:Panel>
                                     </td>
                                </tr>
                                <tr>
                                 <td>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="hylClear" ForeColor="Black" runat="server">清空选定列</asp:HyperLink>
                                     <cc1:HoverMenuExtender ID="HoverMenuExtender3" TargetControlID="hylClear" OffsetX="-320" OffsetY="20" PopupControlID="Panel4" runat="server">
                                     </cc1:HoverMenuExtender>
                                        <asp:Panel ID="Panel4" runat="server" style="display:none;">
                                         <table width="320px" style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>
                                           <tr>
                                            <td>以下勾选列将清空</td>
                                           </tr>
                                           <tr>
                                             <td>
                                                 <asp:CheckBoxList ID="ckbClearColumns" RepeatColumns="3" RepeatDirection="Horizontal" runat="server">
                                                    <asp:ListItem Text="图号" Value="2-tuhao"></asp:ListItem>
                                                    <asp:ListItem Text="材料长度/总长" Value="7-cailiaocd,25-cailiaozongchang"></asp:ListItem>
                                                    <asp:ListItem Text="数量" Value="9-shuliang"></asp:ListItem>
                                                    <asp:ListItem Text="总序" Value="4-zongxu"></asp:ListItem>
                                                    <asp:ListItem Text="材料宽度" Value="8-cailiaokd"></asp:ListItem>
                                                    <asp:ListItem Text="单重/总重" Value="10-dzh,24-zongzhong"></asp:ListItem>
                                                    <asp:ListItem Text="条件属性" Value="14-tjsx"></asp:ListItem>
                                                    <asp:ListItem Text="面域" Value="13-bgzmy"></asp:ListItem>
                                                    <asp:ListItem Text="材料单重/总重" Value="11-cailiaodzh,12-cailiaozongzhong"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </td>
                                           </tr>
                                           <tr>
                                             <td align="right">
                                                 <asp:Button ID="btnClearColumn" runat="server"  OnClick="btnClearColumn_OnClick" OnClientClick="return confirm('确认清空以上选定列值吗？');" Text="清 空" /></td>
                                           </tr>
                                         </table>
                                        </asp:Panel></td>
                                </tr> 
               </table>
               </asp:Panel>
               </td>
                    <td>
                        <asp:LinkButton ID="lnkbtnGotoSource" OnClick="lnkbtnGotoSource_OnClick" runat="server"><asp:Image ID="Image3" ToolTip="返回继续勾选，每次最多可复制50条" CssClass="hand" Height="16" Width="16" runat="server" ImageUrl="~/Assets/icons/back.png" />返回数据源</asp:LinkButton></td>
                   </tr>
                 </table>
               </div>
                   <asp:CheckBoxList ID="cklHiddenShow" runat="server" RepeatDirection="Horizontal" RepeatColumns="15" OnSelectedIndexChanged="cklHiddenShow_OnSelectedIndexChanged"  AutoPostBack="true" RepeatLayout="Table">
            </asp:CheckBoxList>        
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Font-Size="Large">
        没有记录!</asp:Panel>
        <yyc:SmartGridView ID="SmartGridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="4" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>      
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" 
                                CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle Width="8px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="lblIndex" runat="server" readonly="readonly" style="border-style: none;
                                width: 20px" type="text" value="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="tuhao" runat="server" style="border-style: none; width: 150px" type="text" onblur="AutoTuHao(this);"
                                value='<%#Eval("BM_TUHAO") %>'   ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码" >
                        <ItemTemplate>
                            <asp:TextBox ID="marid"  runat="server" ToolTip="物料编码为空时,表明为部件，部件保存后无法修改物料编码！！！" BorderStyle="None"
                                onchange="autoCode(this)" onfocus="this.select();" Text='<%# DataBinder.Eval(Container.DataItem, "BM_MARID")%>'
                                Width="90px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender"  runat="server" CompletionSetCount="15" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true" 
                                ServicePath="~/Ajax.asmx" TargetControlID="marid" UseContextKey="True" CompletionInterval="10">
                            </cc1:AutoCompleteExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <input id="zongxu" runat="server" onblur="verify(this)" onfocus="autoZongxu(this);this.select();" style="border-style: none;
                                width: 80px" type="text" value='<%#Eval("BM_ZONGXU") %>'   ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中文名称">
                        <ItemTemplate>
                            <input id="ch_name" runat="server" style="border-style: none; width: 80px" onfocus="this.select();" title="请在输入物料编码后再修改此项！！！"
                                type="text" value='<%#Eval("BM_CHANAME") %>'   ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <input id="beizhu" runat="server" style="border-style: none; width: 100px" type="text"
                                value='<%#Eval("BM_NOTE") %>' onfocus="this.select();"   ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料长度">
                        <ItemTemplate>
                            <input id="cailiaocd" runat="server" title="mm" onchange="auto(this)" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MALENGTH") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料宽度">
                        <ItemTemplate>
                            <input id="cailiaokd" runat="server" title="mm" onchange="auto(this)" style="border-style: none;
                                width: 50px" type="text" value='<%#Eval("BM_MAWIDTH") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="shuliang" runat="server"  onchange="autoshuliang(this)" style="border-style: none;
                                width: 36px" type="text" value='<%#Eval("BM_NUMBER") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                                |&nbsp;<input id="total_shuliang" runat="server" tabindex="-1" value='<%#Eval("BM_TOTALNUMBER") %>' class="notbrk" title="不能修改"  readonly="readonly" style="border-style: none;width: 36px" type="text" /> 
                                |&nbsp;<input id="plan_shuliang" runat="server" tabindex="-1" onchange="autop_shuliang(this)" class="notbrk" title="材料计划数量" value='<%#Eval("BM_PNUMBER") %>' onfocus="this.select();"   style="border-style: none;width: 36px" type="text" />  
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实际单重" >
                        <ItemTemplate>
                            <input id="dzh" runat="server" onchange="auto1(this)" title="kg:制作明细" style="border-style: none; width: 70px"
                                type="text" value='<%#Eval("BM_UNITWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料单重" >
                        <ItemTemplate>
                            <input id="cailiaodzh" runat="server" title="kg" style="border-style: none; width: 70px" onchange="auto2(this)"
                                type="text" value='<%#Eval("BM_MAUNITWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总重">
                        <ItemTemplate>
                            <input id="cailiaozongzhong" title="kg:材料计划" runat="server" onchange="MarTotalWeightChange(this);" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MATOTALWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="面域|计划面域(m2)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="bgzmy" runat="server" onchange="automy(this)" title="平方米:输入单个数量面域即可，不包含双面！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MABGZMY") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                            |&nbsp;<input id="mpmy" runat="server" title="平方米:按【平米】采购计划，在此输入单个面域！！！" style="border-style: none;
                                width: 60px" type="text" value='<%#Eval("BM_MPMY") %>' onfocus="this.select();" onblur='var pattem=/^\d+(\.\d+)?$/; if(this.value!=""){ if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="";this.select();}}'  onkeydown='grControlFocus(this.parentNode.getElementsByTagName("input")[0])'/>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="条件属性">
                        <ItemTemplate>
                            <input id="tjsx" runat="server" onchange="CheckOrgTJSX(this);" onfocus="this.select();"  style="border-style: none;width: 50px" type="text" value='<%#Eval("BM_BEIZHUATR") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="图纸上单重">
                        <ItemTemplate>
                            <input id="tudz" runat="server" title="kg" style="border-style: none; width: 110px" type="text" onchange="TudanZhong(this);"
                                value='<%#Eval("BM_TUUNITWGHT") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上材质">
                        <ItemTemplate>
                            <input id="tucz" runat="server" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUMAQLTY") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上标准">
                        <ItemTemplate>
                            <input id="tubz" runat="server" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUSTAD") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="图纸上问题">
                        <ItemTemplate>
                            <input id="tuwt" runat="server" style="border-style: none; width: 110px" type="text"
                                value='<%#Eval("BM_TUPROBLEM") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="caizhi" runat="server" readonly="readonly" style="border-style: none;
                                width: 50px" type="text" onfocus="this.select();" value='<%#Eval("BM_MAQUALITY") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="guige" runat="server"  style="border-style: none; width: 100px"
                                type="text" value='<%#Eval("BM_GUIGE") %>' onfocus="this.select();"   ondblclick="ShowInput(this);"  onkeydown="grControlFocus(this)"/><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaoname" runat="server" readonly="readonly" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MANAME") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料规格" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="cailiaoguige" runat="server" readonly="readonly" style="border-style: none;
                                width: 120px" type="text" value='<%#Eval("BM_MAGUIGE") %>' onfocus="this.select();" onkeydown="grControlFocus(this)" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="理论重量" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="lilunzhl" runat="server" readonly="readonly" style="border-style: none;
                                width: 56px" type="text" value='<%#Eval("BM_THRYWGHT") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="zongzhong" runat="server" readonly="readonly" style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_TOTALWGHT") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总长" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="cailiaozongchang" runat="server"  style="border-style: none;
                                width: 70px" type="text" value='<%#Eval("BM_MATOTALLGTH") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="labunit" runat="server" readonly="readonly" title="(技术单位)-(采购单位)" style="border-style: none;
                                width: 32px" type="text" value='<%#Eval("BM_MAUNIT") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国标" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#EDEDED">
                        <ItemTemplate>
                            <input id="biaozhun" runat="server" readonly="readonly" style="border-style: none;
                                width: 150px" type="text" value='<%#Eval("BM_STANDARD") %>' onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="毛坯" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="xinzhuang" runat="server"  onfocus="this.select();" style="border-style: none; width: 32px" type="text" onchange="ChangeofMarShape(this);"
                                value='<%#Eval("BM_MASHAPE") %>' onkeydown="grControlFocus(this)"/>
                                 <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
                                </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="zhuangtai" runat="server" onfocus="findChoice(this,1,29);this.select();" style="border-style: none; width: 32px" type="text"
                                value='<%#Eval("BM_MASTATE") %>' onkeydown="grControlFocus(this)"/>
                                   <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square; text-align:left; line-height:normal;"></ul>
                                </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工艺流程">
                        <ItemTemplate>
                            <input id="process" runat="server" style="border-style: none; width: 100px" type="text"
                                value='<%#Eval("BM_PROCESS") %>' onfocus="findChoice(this,3,30);this.select();" onkeydown="grControlFocus(this)"/>
                                   <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square; text-align:left; line-height:normal;"></ul>
                                   </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="英文名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <input id="en_name" runat="server" style="border-style: none; width: 70px" type="text"
                                value='<%#Eval("BM_ENGSHNAME") %>' onfocus="this.select();" onkeydown="grControlFocus(this)"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="关键部件">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlKeyComponents" runat="server" SelectedValue='<%#Eval("BM_KEYCOMS") %>'>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="定尺">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlFixedSize" runat="server" onchange="CtrlFixSize(this);"  SelectedValue='<%#Eval("BM_FIXEDSIZE") %>' >
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料计划">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlWmp" runat="server" SelectedValue='<%#Eval("BM_WMARPLAN") %>'>
                                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="库" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <input id="ku" runat="server" onfocus="findChoice(this,2,35);this.select();"  onkeydown="grControlFocus(this)" style="border-style: none; width:60px;" type="text"
                                value='<%#Eval("BM_KU") %>' /><br />
                                   <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
                                   <ul   style="list-style-type:square; text-align:left; line-height:normal;"></ul>
                        </ItemTemplate>
                    </asp:TemplateField>
                   </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn TableHeight="500px" TableWidth="100%" FixColumns="" />
            </yyc:SmartGridView>
            </ContentTemplate>
     </cc1:TabPanel>
</cc1:TabContainer>
</ContentTemplate>
</asp:UpdatePanel>
<input id="txtid" type="text" runat="server" readonly="readonly" style="display: none" />
<input id="add_id" type="text" runat="server" readonly="readonly" style="display: none" />
<input id="istid" type="text" runat="server" readonly="readonly" style="display: none" />
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 20%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:large;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
