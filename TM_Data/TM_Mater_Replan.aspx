<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Mater_Replan.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Mater_Replan" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划</asp:Content>
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
                   <td class="notbrk"><b>材料需用计划查询</b></td>
                    <td align="right" class="notbrk">项目名称:</td>
                    <td style="width:18%; height:42px"  valign="top">
                    <asp:ComboBox ID="ddlProName"  runat="server" AutoPostBack="true" Height="15px"
                     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                     onselectedindexchanged="ddlProName_SelectedIndexChanged">
                    </asp:ComboBox>
                    </td>
                    <td align="right" class="notbrk">工程名称:</td>
                    <td  valign="top">
                        <asp:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlEngName_SelectedIndexChanged">
                        </asp:ComboBox>
                    </td>
                    <td align="right" class="notbrk">提交类别:</td>
                    <td>
                        <asp:DropDownList ID="ddlMpOutType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMpOutType_OnSelectedIndexChanged">
                             <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                              <asp:ListItem Text="全部提交" Value=""></asp:ListItem>
                             <asp:ListItem Text="非定尺板" Value="非定尺板"></asp:ListItem>
                              <asp:ListItem Text="定尺板" Value="定尺板"></asp:ListItem>
                              <asp:ListItem Text="标准件" Value="采"></asp:ListItem>
                             <asp:ListItem Text="型材" Value="型材"></asp:ListItem>
                             <asp:ListItem Text="锻件" Value="锻件"></asp:ListItem>
                             <asp:ListItem Text="铸件" Value="铸"></asp:ListItem>
                             <asp:ListItem Text="非金属" Value="非"></asp:ListItem>
                              <asp:ListItem Text="采购成品" Value="采购成品"></asp:ListItem>
                            
                           
                           
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                                <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                        align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                            <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-350"  OffsetY="0"  TargetControlID="HyperLink1" PopupControlID="palORG">
                            </asp:PopupControlExtender>
                            <asp:Panel ID="palORG" Wrap="false"  style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
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
                                 <td align="right" class="notbrk">设备名称:</td>
                                  <td style="width:24%" align="left">
                                    <asp:DropDownList ID="ddlSBname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSBname_SelectedIndexChanged">
                                     
                                    </asp:DropDownList>
                                </td>
                                <td align="right" class="notbrk">技术员:</td>
                                  <td style="width:24%" align="left">
                                    <asp:DropDownList ID="ddlFillman" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFillman_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                              </tr>
                              <tr>
                                 <td align="right" class="notbrk">部件名称:</td>
                                  <td style="width:24%" align="left" >
                                    <asp:DropDownList ID="ddlBJname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBJname_SelectedIndexChanged">
                                     
                                    </asp:DropDownList>
                                </td>
                                <td align="right" class="notbrk" >批号:</td>
                                    <td align="left">
                                     <asp:DropDownList ID="ddlPCode" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlPCode_SelectedIndexChanged">
                                      
                                    </asp:DropDownList>
                                </td>
                              </tr>
                              <tr>
                               <td align="right" class="notbrk">材料名称:</td>
                                    <td align="left">
                                     <asp:DropDownList ID="ddlmatername" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlmatername_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                               <td align="right" class="notbrk">计划类别:</td>
                                <td align="left" >
                                    <asp:DropDownList ID="ddlmptype" runat="server" AutoPostBack="true" onselectedindexchanged="ddlmatername_SelectedIndexChanged">
                                     <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                     <asp:ListItem Text="板-非定尺" Value="非定尺板"></asp:ListItem>
                                     <asp:ListItem Text="板-定尺" Value="定尺板"></asp:ListItem>
                                     <asp:ListItem Text="型材" Value="型材"></asp:ListItem>
                                     <asp:ListItem Text="标准件" Value="标准件"></asp:ListItem>
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
                                      <asp:ListItem Text="物料编码" Value="MP_MARID"></asp:ListItem>
                                      <asp:ListItem Text="中文名称" Value="MP_NAME"></asp:ListItem>
                                      <asp:ListItem Text="图号" Value="MP_TUHAO"></asp:ListItem>
                                      <asp:ListItem Text="规格" Value="MP_GUIGE"></asp:ListItem> 
                                      <asp:ListItem Text="材质" Value="MP_CAIZHI"></asp:ListItem>	
                                      <asp:ListItem Text="标准" Value="MP_STANDARD"></asp:ListItem>
                                      <asp:ListItem Text="计划跟踪号" Value="MP_TRACKNUM"></asp:ListItem>
                                      <asp:ListItem Text="备注" Value="MP_NOTE"></asp:ListItem>  
                                   </asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtQueryContent" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                   <asp:Button ID="btnQuery"  runat="server" Text="查询" UseSubmitBehavior="false" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
                                   <asp:Button ID="btnReset" runat="server" OnClick="btnReset_OnClick" UseSubmitBehavior="false" Text="重置" />
                               </td>
                             </tr>                             
                             </table>
                             <br />
                            </asp:Panel>
                            </td>
                             <td>
                                <asp:LinkButton ID="lnkBtnExport" OnClientClick='return confirm("确认导出吗？\r\r提示:导出时请选择项目、工程及提交类别！！！")' OnClick="lnkBtnExport_OnClick" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:LinkButton></td>
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
            AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333"  >
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" ToolTip='<%# Eval("MP_PID") %>' Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="MP_PID" HeaderText="批号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_SUBMITTM" HeaderText="编制日期" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_TUHAO" HeaderText="图号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_MARID" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_NAME" HeaderText="名称" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_UNIT" HeaderText="单位" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_LENGTH" HeaderText="长(mm)" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_WIDTH" HeaderText="宽(mm)" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_WEIGHT" HeaderText="重量(kg)" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_USAGE" HeaderText="用途" ItemStyle-HorizontalAlign="Center"   HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_TYPE" HeaderText="材料类别" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_TIMERQ" HeaderText="时间要求" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_ENVREFFCT" HeaderText="环保影响" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false"  ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="MP_TRACKNUM" HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />            
                <asp:BoundField DataField="MP_MASHAPERV" HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />                </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0" />
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

