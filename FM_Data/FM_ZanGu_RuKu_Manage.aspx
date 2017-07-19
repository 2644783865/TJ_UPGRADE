<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_ZanGu_RuKu_Manage.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_ZanGu_RuKu_Manage"
    Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../SM_Data/SM_JS/SelectCondition.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
    
    function ShowViewModal(ExpanseID) {
        var frame = $get('IframeView');
        frame.src = "FM_ZanGu_RuKu_Manage_View.aspx?ID=" + ExpanseID;
        $find('EditModalPopup').show();
    }
    
    function EditOkayScript()
    {
      //在这里可以写相应的js事件
    }
    
    function viewCondition()
    {
      document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
    }
    
  
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 98%; height: 24px">
                    <tr>
                        <td align="left">
                            <asp:HyperLink ID="HyperLinkdywgjzg" runat="server" NavigateUrl="~/FM_Data/FM_DYWGJZG.aspx"><asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />未勾稽暂估单查询</asp:HyperLink>
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="HyperLinkdygjzg" runat="server" NavigateUrl="~/FM_Data/FM_DYGJZG.aspx"><asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />当月已勾稽暂估单查询</asp:HyperLink>
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/FM_Data/FM_KTBGJZG.aspx"><asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" runat="server" />本月被勾稽暂估单查询</asp:HyperLink>
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/FM_Data/FM_KYGJZG.aspx"><asp:Image ID="Image4" ImageUrl="~/Assets/images/res.gif" runat="server" />本月勾稽以前暂估单查询</asp:HyperLink> 
                        </td> 
                        
                        
                        <td align="left" width="20%">
                            <asp:HiddenField ID="hfdTotalAmount" runat="server" />
                            <asp:HiddenField ID="hfdTotalCTAmount" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="ckbhs" runat="server" Text="历史暂估" Visible="false" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" Y="30">
                            </asp:ModalPopupExtender>&nbsp;&nbsp;
                            <asp:Button ID="btn_export" runat="server" Text="导 出" OnClick="btn_export_Click" />&nbsp;&nbsp;&nbsp;
                            
                            <asp:HyperLink ID="hlksummary" runat="server" Font-Size="Larger" Target="_blank">
                               <asp:Image ID="Image5" ImageUrl="~/Assets/images/res.gif" runat="server" />按供货商汇总查询</asp:HyperLink>&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="500px" style="background-color: #CCCCFF; border: solid 1px black;">
                                <%--<tr>
                                    <td align="center" colspan="4">
                                        从勾稽年月<asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                            TargetControlID="txtStartTime">
                                        </asp:CalendarExtender>
                                        到勾稽年月<asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                            TargetControlID="txtEndTime">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td align="center" colspan="2" width="50%">
                                        勾稽年
                                        <asp:DropDownList ID="ddlYear" runat="server" Enabled="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" colspan="2" width="50%">
                                        勾稽月<asp:DropDownList ID="ddlMonth" runat="server" Enabled="true">
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
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        ShowFooter="true" DataKeyNames="WG_CODE" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                        <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="23px" Width="35px" VerticalAlign="Middle">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Year" HeaderText="会计年" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Month" HeaderText="会计期" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WG_CODE" HeaderText="入库单号" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="COMPANY" HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="WG_VERIFYDATE" HeaderText="日期" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DOC" HeaderText="制单人" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="VERIFIER" HeaderText="审核人" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="拆单标志">
                                <ItemTemplate>
                                    <asp:Label ID="lb_chadan" runat="server" Text='<%#Eval("WG_TEARFLAG").ToString()=="0"?"N":"Y" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="核销标志">
                                <ItemTemplate>
                                    <asp:Label ID="lb_hexiao" runat="server" Text='<%#Eval("WG_CAVFLAG").ToString()=="0"?"N":"Y" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="勾稽标志">
                                <ItemTemplate>
                                    <asp:Label ID="lb_GJ" runat="server" Text='<%#Eval("GJSTATE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ZGAMT" HeaderText="金额" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ZGCTAMT" HeaderText="含税金额" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="查看">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask" runat="server">
                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                        查看
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        <font color="Red">没有数据!</font></asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                    <asp:Button ID="ButtonEdit" runat="server" Text="查看" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtenderEdit" runat="server" CancelControlID="ButtonEditCancel"
                        OkControlID="ButtonEditDone" TargetControlID="ButtonEdit" PopupControlID="DivEditWindow"
                        OnOkScript="EditOkayScript();" BehaviorID="EditModalPopup">
                    </asp:ModalPopupExtender>
                    <div style="display: none">
                        <input id="ButtonEditDone" value="Done" type="button" />
                        <input id="ButtonEditCancel" value="Cancel" type="button" />
                    </div>
                    <div id="DivEditWindow" style="display: none; background-color: #00FFFF;">
                        <iframe id="IframeView" frameborder="0" width="700px" height="500px" scrolling="no">
                        </iframe>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
