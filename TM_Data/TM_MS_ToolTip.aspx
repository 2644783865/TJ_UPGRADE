<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MS_ToolTip.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_ToolTip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPagingOfMS.ascx" tagname="UCPagingOfMS" tagprefix="uc2" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<base target="_self" />
<head runat="server">
    <title>制作明细调整注意事项</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager> 
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="100%">
          <tr>
             <td align="right" style="width:10%">显示内容:</td>
             <td align="left" style="width:30%">
                <asp:RadioButtonList ID="rblMSAdjust" runat="server" OnSelectedIndexChanged="rblMSAdjust_OnSelectedIndexChanged" AutoPostBack="true" RepeatColumns="3" RepeatDirection="Horizontal">
                   <asp:ListItem Text="当前调整数据" Value="0"></asp:ListItem>
                   <asp:ListItem Text="全部数据" Value="1"></asp:ListItem>
                   <asp:ListItem Text="问题序号" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
              </td>
              <td align="right" style="width:10%">
                  <asp:Label ID="lblQueryXuhao" runat="server" Text="序号查询:"></asp:Label></td>
              <td>
                 <asp:DropDownList ID="dplAllXuhao" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplAllXuhao_OnSelectedIndexChanged">
                 </asp:DropDownList>
                  </td>
             <td align="right">
                 <asp:Label ID="lalInMS" runat="server" Text="是否在制作明细中体现:"></asp:Label></td>
             <td>
                 <asp:RadioButtonList ID="rblInMs" runat="server" AutoPostBack="true" RepeatColumns="3" OnSelectedIndexChanged="rblInMs_OnSelectedIndexChanged" RepeatDirection="Horizontal">
                    <asp:ListItem Text="全部" Value="All"></asp:ListItem>
                    <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="否" Value="N"></asp:ListItem>
                 </asp:RadioButtonList>
             </td>
          </tr>
        </table>

    </div>
    </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
       <yyc:SmartGridView ID="grv" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
             CellPadding="4" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB"/>
        <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="BM_MSXUHAO" HeaderText="明细序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE" HeaderText="材料规格" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NUMBER" HeaderText="数量" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT" DataFormatString="{0:N2}" HeaderText="单重(kg)" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT" DataFormatString="{0:N2}" HeaderText="总重(kg)" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE" HeaderText="毛坯" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASTATE" HeaderText="状态" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="体现" DataField="BM_ISMANU" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="定尺" DataField="BM_FIXEDSIZE" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="变更状态" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblMSChangeState" runat="server" Text='<%#Eval("BM_MSSTATUS").ToString()=="1"?"删除":Eval("BM_MSSTATUS").ToString()=="2"?"增加":Eval("BM_MSSTATUS").ToString()=="3"?"修改":"正常" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BM_STANDARD" HeaderText="国标" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NOTE" HeaderText="备注" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="BM_KEYCOMS" HeaderText="关键部件" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>--%>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableWidth="99.9%" TableHeight="800px" FixColumns="0" />        
    </yyc:SmartGridView>
            <uc1:UCPaging ID="UCPagingMS" runat="server" />
            <asp:Panel ID="NoDataPanel" runat="server">没有数据！
            </asp:Panel>
            
       <yyc:SmartGridView ID="grvProb" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
             CellPadding="4" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB"/>
        <Columns>
            <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PRBDESCRIBE" HeaderText="原因描述" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Red" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="BM_MARID" HeaderText="物料编码" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE" HeaderText="规格" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NUMBER" HeaderText="数量" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT" DataFormatString="{0:N2}" HeaderText="单重(kg)" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT" DataFormatString="{0:N2}" HeaderText="总重(kg)" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE" HeaderText="毛坯" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASTATE" HeaderText="状态" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="体现在制作明细中" DataField="BM_ISMANU" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="定尺" DataField="BM_FIXEDSIZE" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="变更状态" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblMSChangeState" runat="server" Text='<%#Eval("BM_MSSTATUS").ToString()=="1"?"删除":Eval("BM_MSSTATUS").ToString()=="2"?"增加":Eval("BM_MSSTATUS").ToString()=="3"?"修改":"正常" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BM_NOTE" HeaderText="备注" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KEYCOMS" HeaderText="关键部件" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0" />        
    </yyc:SmartGridView>
        </div>
    </div>
        </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 10%; right:40%">
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
