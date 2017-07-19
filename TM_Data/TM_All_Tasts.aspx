<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_All_Tasts.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_All_Tasts" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术员任务信息列表&nbsp;&nbsp;&nbsp;<span style="color:Red; font-size:smaller;">双击某带“-”任务号所在行，查看该任务号下所有数据</span>
    <JSR:JSRegister ID="JSRegister1" runat="server" />
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
 <script src="../JS/DatePicker.js" language="javascript" type="text/javascript"></script>
 <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
 <script language="javascript" type="text/javascript">
  function ShowOrg(taskid)
  {
     var date=new Date();
     var time=date.getTime();
     window.open("TM_Task_View.aspx?NoUse="+time+"&action="+taskid);
  }
 </script>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <Triggers>
    <asp:PostBackTrigger ControlID="btnQuery" />
    <asp:PostBackTrigger ControlID="btnClear" />
    <asp:PostBackTrigger ControlID="lnkBtnExport" />
  </Triggers>
   <ContentTemplate>
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
            
                <table style="width:100%">
                    <tr>
                    <td style="width:10%"><strong>技术员任务</strong></td>
                    <td>
                        <asp:CheckBox ID="ckbTaskId" runat="server" AutoPostBack="true" ToolTip="只显示生产制号" OnCheckedChanged="ckbTaskId_OnCheckedChanged" /></td>
                    <td align="right">技术员:</td>
                    <td>
                        <asp:DropDownList ID="ddlselect" runat="server" AutoPostBack="true" 
                            onselectedindexchanged="ddlselect_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right">项目名称:</td>
                    <td valign="top" style="width:10%; height:42px">
                        <cc1:ComboBox ID="ddlpro" runat="server" Width="60px" AutoPostBack="true" Height="15px"
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                        onselectedindexchanged="ddlpro_SelectedIndexChanged">
                        </cc1:ComboBox>
                    </td>
                    <td align="right">技术准备状态:</td>
                    <td>
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                        <asp:ListItem Text="全部" value="全部"></asp:ListItem>   
                        <asp:ListItem Text="待分工" value="0"></asp:ListItem>              
                        <asp:ListItem Text="进行中" value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="完工" Value="2" ></asp:ListItem>
                        <asp:ListItem Text="停工" Value="3" ></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">排序:</td>
                    <td>
                        <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true">
                          <asp:ListItem Text="生产制号" Value="[dbo].[TM_TaskOrder](TSA_ID)" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="登记日期" Value="[dbo].[TM_TaskOrder2](TSA_ID),TSA_ADDTIME"></asp:ListItem>
                          <asp:ListItem Text="接收日期" Value="[dbo].[TM_TaskOrder2](TSA_ID),TSA_RECVDATE"></asp:ListItem>
                          <asp:ListItem Text="技术员" Value="[dbo].[TM_TaskOrder2](TSA_ID),TSA_TCCLERKNM"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">电气制号:</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" onselectedindexchanged="ddlstatus_SelectedIndexChanged">
                           <asp:ListItem Text="- -" Value="-请选择-"></asp:ListItem>
                           <asp:ListItem Text="N" Value="N"></asp:ListItem>
                           <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                       <asp:LinkButton ID="lnkBtnExport" OnClientClick='return confirm("确认导出吗？\r\r提示:导出的生产制号为当前查询条件下的生产制号！！！")' OnClick="lnkBtnExport_OnClick" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:LinkButton>
                    </td>
                    <td align="right">
                    <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"  align="absmiddle" runat="server" />更多筛选</asp:HyperLink>&nbsp;&nbsp;
                            
                            <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-200"  OffsetY="0"  TargetControlID="HyperLink1" PopupControlID="palORG">
                            </cc1:PopupControlExtender>
                            <asp:Panel ID="palORG"  style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu; width:250px;" runat="server">
                             <table width="100%" >
                             <tr>       
                             <td colspan="2">
                                  <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                                      <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                                  </div>
                                  <br /><br />
                             </td>
                             </tr>
                             <tr>
                               <td align="right"><strong>登记日期:</strong></td>
                               <td align="left"><asp:DropDownList ID="ddlDJYear" runat="server" AutoPostBack="true"   onselectedindexchanged="ddlFY_OnSelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="ddlDJMonth" runat="server" AutoPostBack="true"   onselectedindexchanged="ddlFY_OnSelectedIndexChanged">
                              </asp:DropDownList></td>
                             </tr>
                             <tr>
                               <td align="right"><strong>开始日期:</strong></td>
                               <td align="left"><asp:DropDownList ID="ddlKSYear" runat="server" AutoPostBack="true"   onselectedindexchanged="ddlFY_OnSelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="ddlKSMonth" runat="server" AutoPostBack="true"    onselectedindexchanged="ddlFY_OnSelectedIndexChanged">
                              </asp:DropDownList></td>
                             </tr>
                             <tr>
                               <td align="right"><strong>计划日期:</strong></td>
                               <td align="left"><asp:DropDownList ID="ddlJHYear" runat="server" AutoPostBack="true"   onselectedindexchanged="ddlFY_OnSelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="ddlJHMonth" runat="server" AutoPostBack="true"    onselectedindexchanged="ddlFY_OnSelectedIndexChanged">
                              </asp:DropDownList></td>
                             </tr>
                             <tr>
                               <td align="right"><strong>完成日期:</strong></td>
                               <td align="left"><asp:DropDownList ID="ddlWCYear" runat="server" AutoPostBack="true"   onselectedindexchanged="ddlFY_OnSelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="ddlWCMonth" runat="server" AutoPostBack="true"   onselectedindexchanged="ddlFY_OnSelectedIndexChanged">
                              </asp:DropDownList></td>
                             </tr>
                             <tr>
                                <td align="right"><strong>生产制号:</strong></td>
                                <td align="left">
                                    <asp:TextBox ID="txtTaskID" runat="server"></asp:TextBox></td>   
                             </tr>
                             <tr>
                               <td align="right"><strong>项目名称:</strong></td>
                               <td align="left">
                                   <asp:TextBox ID="txtPjName" runat="server"></asp:TextBox></td>  
                             </tr>
                             <tr>
                             <td align="right"><strong>工程名称:</strong></td>
                             <td align="left">
                                 <asp:TextBox ID="txtEngName" runat="server"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td colspan="2" align="center">
                                <br />
                                  <asp:Button ID="btnQuery" runat="server" Text="查 询" UseSubmitBehavior="false" onclick="btnQuery_Click"/>&nbsp;&nbsp;
                                  <asp:Button ID="btnClear" runat="server" UseSubmitBehavior="false" OnClick="btnClear_Click" Text="重 置" />
                              </td>
                             </tr>
                             </table>
                             <br />
                            </asp:Panel>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" OnRowDataBound="GridView1_OnRowDataBound" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>                
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TSA_ID" HeaderText="生产制号/任务号" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="TSA_PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_ENGNAME" HeaderText="工程名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_ENGSTRSMTYPE" HeaderText="工程类型" ItemStyle-HorizontalAlign="Center" />
                 <asp:BoundField DataField="TSA_NUMBER" HeaderText="台数" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_ADDTIME" HeaderText="登记日期" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_TCCLERKNM" HeaderText="技术员" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_CONTYPE" HeaderText="负责部分" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TSA_STARTDATE" HeaderText="开始时间" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_PLANFSDATE" HeaderText="计划时间" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_REALFSDATE" HeaderText="实际时间" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TSA_MANCLERKNAME" HeaderText="分工人" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" >
                    <ItemTemplate>
                        <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("TSA_STATE").ToString()=="2"?"完工":Eval("TSA_STATE").ToString()=="1"?"进行中...":Eval("TSA_STATE").ToString()=="3"?"停工":"待分工" %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
        </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有任务!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
