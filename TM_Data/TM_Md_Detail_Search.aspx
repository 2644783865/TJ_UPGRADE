<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Md_Detail_Search.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Md_Detail_Search" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    最新版制作明细</asp:Content>
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
                <table width="94%">
                    <tr>
                    <td style="width:16%"><b>制作明细查询</b></td>
                    <td align="right">项目名称:</td>
                    <td style="width:18%; height:42px" valign="top">
                    <asp:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px"
                     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                     onselectedindexchanged="ddlProName_SelectedIndexChanged">
                    </asp:ComboBox>
                    </td>
                    
                    <td align="right">设备名称:</td>
                    <td style="width:24%" valign="top">
                        <asp:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlEngName_SelectedIndexChanged">
                        </asp:ComboBox>
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
                             </tr>                                <tr>
                                 <td align="right" class="notbrk">设备名称:</td>
                                  <td style="width:24%" align="left">
                                    <asp:DropDownList ID="ddlSBname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSBname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" class="notbrk">技术员:</td>
                                  <td style="width:24%" align="left">
                                    <asp:DropDownList ID="ddlFillman"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFillman_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                              </tr>
                              <tr>
                                 <td align="right">部件名称:</td>
                                  <td style="width:24%" align="left" >
                                    <asp:DropDownList ID="ddlBJname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBJname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                
                              </tr>
                              <tr>
                               <td colspan="4"><div style="text-align:left;"><strong>自定义查询</strong></div><hr style="height:2px;" /></td>
                             </tr>
                             <tr>
                               <td align="right">查询条件:</td>
                               <td colspan="3" align="left">
                                   <asp:DropDownList ID="ddlQueryType" runat="server">
                                     <asp:ListItem Text="-请选择-" Value="0" ></asp:ListItem>
                                      <asp:ListItem Text="明细序号" Value="MS_MSXUHAO"></asp:ListItem>                                      
                                      <asp:ListItem Text="序号" Value="MS_NEWINDEX"></asp:ListItem> 
                                      <asp:ListItem Text="总序" Value="MS_ZONGXU"></asp:ListItem>
                                      <asp:ListItem Text="中文名称" Value="MS_NAME"></asp:ListItem>
                                      <asp:ListItem Text="图号" Value="MS_TUHAO"></asp:ListItem>
                                      <asp:ListItem Text="规格" Value="MS_GUIGE"></asp:ListItem> 
                                      <asp:ListItem Text="材质" Value="MS_CAIZHI"></asp:ListItem>	
                                      <asp:ListItem Text="毛坯" Value="MS_MASHAPE"></asp:ListItem> 
                                      <asp:ListItem Text="状态" Value="MS_MASTATE"></asp:ListItem>
                                      <asp:ListItem Text="工艺流程" Value="MS_PROCESS"></asp:ListItem>
                                      <asp:ListItem Text="库" Value="MS_KU"></asp:ListItem>                                     
                                      <asp:ListItem Text="备注" Value="MS_NOTE"></asp:ListItem>  
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
                                <asp:LinkButton ID="lnkBtnExport" OnClientClick='return confirm("确认导出吗？\r\r提示:导出时请选择项目、工程！！！")' OnClick="lnkBtnExport_OnClick" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:LinkButton>
                            </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
         <asp:Panel ID="NoDataPanel" runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel>
         <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" 
            AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" OnRowDataBound="GridView1_OnRowDataBound" >
            <RowStyle BackColor="#EFF3FB" />
             <Columns>
                                <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                        <asp:HiddenField ID="hdfMSState" Value='<%#Eval("BM_MSSTATE").ToString() %>' runat="server" />
                                        <asp:HiddenField ID="hdfMSChg" Value='<%#Eval("BM_MSSTATUS").ToString() %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                 
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                             
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="材料规格" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TUUNITWGHT" HeaderText="图纸单重" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TUTOTALWGHT" HeaderText="图纸总重" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料种类" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MALENGTH" HeaderText="长度" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAWIDTH" HeaderText="宽度" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_NOTE" HeaderText="备注" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_XIALIAO" HeaderText="下料方式" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="材料总长(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MABGZMY" DataFormatString="{0:F2}" HeaderText="面域(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="计划面域(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="备注" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="变更状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSChangeState" CssClass="notbrk" runat="server" Text='<%#Eval("BM_MSSTATUS").ToString()=="1"?"删除":Eval("BM_MSSTATUS").ToString()=="2"?"增加":Eval("BM_MSSTATUS").ToString()=="3"?"修改":"正常" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
            <PagerStyle CssClass="bomcolor" Wrap="false" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" Wrap="false" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0" />                    
        </yyc:SmartGridView>
        <uc1:UCPaging ID="UCPaging1" runat="server" Visible="false" /> 
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

