<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarApplyAudit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarApplyAudit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务审核信息列表</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
//    function add()
//    {
//        var sRet = window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=add','obj','dialogWidth=900px;dialogHeight=600px');
//        if (sRet == "refresh") 
//        {
//            window.location.href = window.location.href;
//        }
//    }
window.onload=function(){
 $("#<%=GridView1.ClientID %> tr").mouseover(function(){
$(this).css("background","#C8F7FF");}
 );
 
  $("#<%=GridView1.ClientID %> tr").mouseout(function(){
$(this).css("background","white");}
);
}
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
             <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <div class="box_right">
                    <div class="box-title">
                        <table style="width: 100%">
                            <tr>
                                <td align="right">
                                    <%--<strong>查询类别:</strong>--%>
                                </td>
                                <td>
                                    <%--<asp:RadioButtonList ID="rblstate" RepeatColumns="2" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="rblstate_SelectedIndexChanged">                
                            <asp:ListItem Text="正常" value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="变更" Value="1" ></asp:ListItem>
                        </asp:RadioButtonList>--%>
                                </td>
                                <td>
                                    <%--<asp:RadioButtonList ID="rbltask" RepeatColumns="5" runat="server"
                            onselectedindexchanged="rbltask_SelectedIndexChanged" AutoPostBack="true">                
                        </asp:RadioButtonList>--%>
                                </td>
                                <td align="right">
                                    <strong>审核状态:</strong>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblstatus" RepeatColumns="5" runat="server" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <%--<asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                        align="absmiddle" runat="server" />更多筛选</asp:HyperLink>
                            <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="true" Position="Bottom"  Enabled="true" runat="server" OffsetX="-350"  OffsetY="0"  TargetControlID="HyperLink1" PopupControlID="palORG">
                            </asp:PopupControlExtender>
                           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          
                           <Triggers>
                           
                             <asp:PostBackTrigger ControlID="btnQuery" />
                             <asp:PostBackTrigger ControlID="btnClear" />
                            </Triggers>
                            <ContentTemplate>
                            <asp:Panel ID="palORG" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
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
                             <td align="right" class="notbrk"><strong>项 目 名 称:</strong></td>
                             <td style=" height:42px" >
                                 <asp:DropDownList ID="ddlpro" AutoPostBack="true" OnSelectedIndexChanged="ddlpro_OnSelectedIndexChanged" runat="server">
                                 </asp:DropDownList></td>
                             </tr>
                             <tr>
                             <td align="right" class="notbrk"><strong>工 程 名 称:</strong></td>
                             <td style=" height:42px" >
                                 <asp:DropDownList ID="ddlEngName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEngName_OnSelectedIndexChanged">
                                 </asp:DropDownList></td>
                             </tr>
                             <tr>
                             <td align="right" class="notbrk"><strong>编  制  人:</strong></td>
                             <td style=" height:42px" >
                                 <asp:DropDownList ID="ddlTecName" AutoPostBack="true" OnSelectedIndexChanged="rblstate_SelectedIndexChanged" runat="server">
                                 </asp:DropDownList></td>
                             </tr>
                             <tr>
                             <td align="right">
                                 <asp:DropDownList ID="ddlQueryType" runat="server">
                                   <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                   <asp:ListItem Text="批号" Value="批号"></asp:ListItem>
                                   <asp:ListItem Text="项目编号" Value="项目编号"></asp:ListItem>
                                   <asp:ListItem Text="项目名称" Value="项目名称"></asp:ListItem>
                                   <asp:ListItem Text="工程名称" Value="工程名称"></asp:ListItem>
                                   <asp:ListItem Text="生产制号" Value="生产制号"></asp:ListItem>
                                   <asp:ListItem Text="技术员" Value="技术员"></asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtQueryContent" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="btnQuery" runat="server" UseSubmitBehavior="false" OnClick="btnQuery_OnClick" Text="查 询" />&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="btnClear" runat="server" UseSubmitBehavior="false" Text="重 置" OnClick="btnClear_btnQuery" /></td>
                             </tr>
                             </table>
                             <br />
                            </asp:Panel></ContentTemplate>
                            </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer" style="width: 100%; overflow: auto">
                    <asp:GridView ID="GridView1" CssClass="toptable grid" runat="server" OnRowDataBound="GridView1_DATABOUND"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Width="25px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                            <%--                <asp:BoundField DataField="CODE" HeaderText="编号" 
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center"  />
                </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DEPARTMENT" HeaderText="用车部门" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="外出事由" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtWAICHU" Text='<%# Eval("REASON")%>' runat="server" Width="100px"
                                        ToolTip='<%# Eval("REASON")%>' ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SFPLACE" HeaderText="始发地">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="目的地" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMUDIDI" Text='<%# Eval("DESTINATION")%>' runat="server" Width="100px"
                                        ToolTip='<%# Eval("DESTINATION")%>' ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NUM" HeaderText="乘车人数" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="USETIME1" HeaderText="用车时间" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="APPLYER" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YDTIME" HeaderText="开始日期" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TIME2" HeaderText="结束日期" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="审核//查看" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask1" CssClass="link" NavigateUrl='<%#"OM_CarApplyDetail.aspx?action=view&diff=audit&id="+Eval("CODE")%>'
                                        runat="server">
                                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        <asp:Label ID="state1" runat="server" Text='<%# Eval("STATE").ToString()=="0"||Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"?"审核//查看":"查看" %>'></asp:Label>
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="FIRSTMANNM" HeaderText="一级" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FIRSTTIME" HeaderText="一级日期" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SECONDMANNM" HeaderText="二级" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SECONDTIME" HeaderText="二级日期" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="THIRDMANNM" HeaderText="三级" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="THIRDTIME" HeaderText="三级日期" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="status" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"待审中":Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"?"审核中...":Eval("STATE").ToString()=="8"?"通过":Eval("STATE").ToString()=="9"?"已取消":"驳回" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有任务!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
