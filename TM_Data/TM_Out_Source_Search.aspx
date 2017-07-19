<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Out_Source_Search.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_Source_Search" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术外协</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server"><asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <Triggers >
  <asp:PostBackTrigger ControlID="lnkBtnExport" />
 </Triggers>
  <ContentTemplate>
    <div  class="box-inner" style="vertical-align:top">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                    <td><b>技术外协查询</b></td>
                    <td align="right">项目名称:</td>
                    <td style="width:18%; height:42px" valign="top">
                    <asp:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px"
                     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                     onselectedindexchanged="ddlProName_SelectedIndexChanged">
                    </asp:ComboBox>
                    </td>
                    <td align="right">工程名称:</td>
                    <td valign="top">
                        <asp:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlEngName_SelectedIndexChanged">
                        </asp:ComboBox>
                    </td>
                    <td align="right" >外协类别:</td>
                       <td align="left">
                         <asp:DropDownList ID="ddlOutType" AutoPostBack="true" runat="server"
                             onselectedindexchanged="ddlOutType_SelectedIndexChanged">
                             <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                              <asp:ListItem Text="技术外协" Value="技术外协"></asp:ListItem>
                              <asp:ListItem Text="协A" Value="协A"></asp:ListItem>
                              <asp:ListItem Text="协B" Value="协B"></asp:ListItem>
                              <asp:ListItem Text="协A/协B" Value="协A/协B"></asp:ListItem>
                        </asp:DropDownList>
                      </td>
                    <td align="center">
                                <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                        align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                            <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-330"  OffsetY="0"  TargetControlID="HyperLink1" PopupControlID="palORG">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="palORG" Width="500px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">  
                             <table width="100%" >
                             <tr>       
                             <td>
                                  <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                                      <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                                  </div>
                                  <br /><br />
                             </td>
                             </tr>
                             <tr>
                               <td colspan="4"><div style="text-align:left;"><strong>常规查询</strong></div><hr style="height:2px;" /></td>
                             </tr>                               
                             <tr>
                                 <td  align="right">设备名称:</td>
                                  <td   align="left">
                                    <asp:DropDownList ID="ddlSBname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSBname_SelectedIndexChanged">
                                     
                                    </asp:DropDownList>
                                </td>
                                <td  align="right">技术员:</td>
                                  <td  align="left">
                                    <asp:DropDownList ID="ddlFillman" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFillman_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                              </tr>
                              <tr>
                                 <td  align="right">部件名称:</td>
                                  <td   align="left" >
                                    <asp:DropDownList ID="ddlBJname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBJname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" >批号:</td>
                                    <td align="left" >
                                     <asp:DropDownList ID="ddlPCode" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlBJname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                              </tr>
                              <tr>
                                <td align="right" >材料名称:</td>
                                 <td align="left"  ><asp:DropDownList ID="ddlmatername" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlOutType_SelectedIndexChanged">
                                </asp:DropDownList>
                                 </td>
                                 <td align="right"  >材料形状:</td>
                                 <td align="left" >
                                     <asp:DropDownList ID="ddlOrgShape" runat="server" AutoPostBack="true"  onselectedindexchanged="ddlOutType_SelectedIndexChanged">
                                     </asp:DropDownList>
                                 </td> 
                              </tr>
                              <tr>
                                  <td colspan="4"><div style="text-align:left;"><strong>自定义查询</strong></div><hr style="height:2px;" /></td>
                              </tr>
                              <tr>
                               <td align="right">查询条件:</td>
                               <td colspan="3" align="left" >
                                   <asp:DropDownList ID="ddlQueryType" runat="server">
                                     <asp:ListItem Text="-请选择-" Value="0" ></asp:ListItem>
                                      <asp:ListItem Text="序号" Value="OSL_NEWXUHAO"></asp:ListItem> 
                                      <asp:ListItem Text="总序" Value="OSL_ZONGXU"></asp:ListItem>
                                      <asp:ListItem Text="中文名称" Value="OSL_NAME"></asp:ListItem>
                                      <asp:ListItem Text="图号" Value="OSL_OUTSOURCENO"></asp:ListItem>
                                      <asp:ListItem Text="规格" Value="OSL_GUIGE"></asp:ListItem> 
                                      <asp:ListItem Text="材质" Value="OSL_CAIZHI"></asp:ListItem>	
                                      <asp:ListItem Text="毛坯" Value="OSL_MASHAPE"></asp:ListItem> 
                                      <asp:ListItem Text="状态" Value="MS_MASTATE"></asp:ListItem>                                
                                      <asp:ListItem Text="备注" Value="OSL_NOTE"></asp:ListItem>  
                                   </asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtQueryContent" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                   <asp:Button ID="btnQuery"  runat="server" Text="查询" UseSubmitBehavior="false" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
                                   <asp:Button ID="btnReset" runat="server" OnClick="btnReset_OnClick" UseSubmitBehavior="false" Text="重置" />
                               </td>
                             </tr>
                             </table>
                             <br />
                             </asp:Panel>
                             <td>
                                <asp:LinkButton ID="lnkBtnExport" OnClientClick='return confirm("确认导出吗？\r\r提示:导出时请选择项目、工程及外协类别！！！")' OnClick="lnkBtnExport_OnClick" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:LinkButton></td>
                            </td>     
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
          <asp:Panel ID="NoDataPanel" runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel>
         <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" OnPreRender="GridView1_OnPreRender" 
                AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="行号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OSL_OUTSOURCENO" HeaderText="批号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OST_MDATE" HeaderText="编制日期" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NEWXUHAO" HeaderText="序号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="图号(标识)" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_ZONGXU" HeaderText="总序" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OSL_TRACKNUM" HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OST_OUTTYPE" HeaderText="" ItemStyle-HorizontalAlign="Center"    HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="480px" TableWidth="100%" FixColumns="0" />
        </yyc:SmartGridView>
         <uc1:UCPaging ID="UCPaging1" Visible="false" runat="server" /> 
        </div>
    </div>
</ContentTemplate>
 </asp:UpdatePanel>
  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 50%; right:40%">
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
