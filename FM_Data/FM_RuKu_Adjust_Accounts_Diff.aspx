<%@ Page Language="C#" MasterPageFile="~/Masters/NoScollRightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="FM_RuKu_Adjust_Accounts_Diff.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_RuKu_Adjust_Accounts_Diff"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../SM_Data/SM_JS/SelectCondition.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
    
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

function BeginRequestHandler(sender, args){
 
    ActivateAlertDiv('visible', 'AlertDiv', '');
}

function EndRequestHandler(sender, args){

ActivateAlertDiv('hidden', 'AlertDiv', '');

}

function ActivateAlertDiv(visstring, elem, msg){
     var adiv = $get(elem);
     adiv.style.visibility = visstring;                
}

function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}
    </script>
    <asp:HiddenField ID="hfdTotalAmount" runat="server" />
    <asp:HiddenField ID="hfdTotalCTAmount" runat="server" />
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 98%; height: 24px">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" Y="30">
                            </asp:ModalPopupExtender>
                        </td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <td>
                            <asp:Button ID="btn_export" runat="server" Text="导 出" OnClick="btn_export_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="500px" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                   <td align="center" >
                            从核算年月<asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
                                         <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="txtStartTime">
                                        </asp:CalendarExtender>
                            到核算年月<asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
                                         <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="txtEndTime">
                                        </asp:CalendarExtender>
                            </td>
                                </tr>
                                 <tr>
                                    <td align="center" >
                                        <asp:DropDownList ID="ddlYear" runat="server" Enabled="true" Visible="false" >
                                        </asp:DropDownList>
                                        
                                        <asp:DropDownList ID="ddlMonth" runat="server" Enabled="true" Visible="false" >
                                            <asp:ListItem Value="01">-01-</asp:ListItem>
                                            <asp:ListItem Value="02">-02-</asp:ListItem>
                                            <asp:ListItem Value="03">-03-</asp:ListItem>
                                            <asp:ListItem Value="04">-04-</asp:ListItem>
                                            <asp:ListItem Value="05">-05-</asp:ListItem>
                                            <asp:ListItem Value="06">-06-</asp:ListItem>
                                            <asp:ListItem Value="07">-07-</asp:ListItem>
                                            <asp:ListItem Value="08">-08-</asp:ListItem>
                                            <asp:ListItem Value="09">-09-</asp:ListItem>
                                            <asp:ListItem Value="10">-10-</asp:ListItem>
                                            <asp:ListItem Value="11">-11-</asp:ListItem>
                                            <asp:ListItem Value="12">-12-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                      </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="逻辑">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="OR" Selected="True">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="名称">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="比较关系">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">等于</asp:ListItem>
                                                            <asp:ListItem Value="2">不等于</asp:ListItem>
                                                            <asp:ListItem Value="3">大于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="5">小于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于或等于</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数值">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" height="23px">
                                        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="Query_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        AutoGenerateColumns="False" ShowFooter="true" CellPadding="4" ForeColor="#333333"
                        OnRowDataBound="GridView1_RowDataBound"  >
                        <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                   <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="DIFYEAR" HeaderText="年度" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                             <asp:BoundField DataField="DIFMONTH" HeaderText="会计期间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="MNAME" HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="GUIGE" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="CAIZHI" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="GB" HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="DIFAMTMNY" HeaderText="差异金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有数据!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="AlertDiv" style="top: 50%; right: 45%; position: absolute; visibility: hidden;">
        <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
    </div>
</asp:Content>
