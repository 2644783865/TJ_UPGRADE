<%@ Page Language="C#" MasterPageFile="~/Masters/NoScollRightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="FM_RuKu_Adjust_Accounts.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_RuKu_Adjust_Accounts"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../SM_Data/SM_JS/SelectCondition.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="0">
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
  
    function ShowViewModal(INID,VID) {
  
        var frame = $get('IframeView');
        frame.width=getWidth();
        frame.height=getHeight(); 
        frame.src = "FM_RuKu_Adjust_Accounts_View.aspx?INID=" + INID+"&&VID="+VID;
        $find('EditModalPopup').show();
    }
    
    function EditOkayScript()
    {
      //在这里可以写相应的js事件
    }
    function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
   }
   
   //计算遮罩层的高
    function getHeight(){
        var winWidth
        if (document.documentElement.scrollHeight > document.documentElement.clientHeight) {
            winHeight = document.documentElement.scrollHeight;
        }
        else {
            winHeight = document.documentElement.clientHeight;
        }
        if (navigator.appName !== "Microsoft Internet Explorer") {
            winHeight = winHeight - 4;
        }
        else {
            winHeight = winHeight;
        }
        return winHeight;
    }
    
    //计算遮罩层的宽
    function getWidth(){
        var winWidth;
        if (document.documentElement.scrollWidth > document.documentElement.clientWidth) {
            winWidth = document.documentElement.scrollWidth;
        }
        else {
            winWidth = document.documentElement.clientWidth;
        }
        if (navigator.appName !== "Microsoft Internet Explorer") {
            winWidth = winWidth - 4;
        }
        else {
            winWidth = winWidth;
        }
        return winWidth;
    }
    function ClientSideClick(myButton) {
     
           // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            
            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.value = "加载中...";              
            }
            
           return true;
        }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:HiddenField ID="hfdTotalAmount" runat="server" />
    <asp:HiddenField ID="hfdTotalCTAmount" runat="server" />
    <%--<asp:HiddenField ID="hfdZanGuChaAmount" runat="server" />  --%>  
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:UpdatePanel ID="UpdatePanelOperate" runat="server">
                    <ContentTemplate>
                        <table style="width: 98%; height: 24px">
                            <tr>
                                <td style="width: 40%;">
                                    <asp:Label ID="LabelMessage" runat="server" Text="系统存在未勾稽的发票，不能进行入库核算!" Visible="false"
                                        ForeColor="Red"></asp:Label>
                                </td>
                                <td align="right" style="width: 40%;">
                                    <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                        PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" Y="30">
                                    </asp:ModalPopupExtender>
                                </td>
                                <td align="right" style="width: 30%;">
                                    <asp:Button ID="btn_hs" runat="server" Text="入库核算 " OnClick="btn_hs_Click" OnClientClick="ClientSideClick(this);"
                                        UseSubmitBehavior="False" />&nbsp;|
                                    <asp:Button ID="btn_antihs" runat="server" Text="反入库核算 " OnClick="btn_antihs_Click"
                                        OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Panel ID="PanelCondition" runat="server" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="500px" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td style="width: 10%" align="right">
                                    </td>
                                    <td align="center" width="40%" style="font-size: medium">
                                        勾稽年度:<asp:DropDownList ID="ddl_year" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" width="40%" style="font-size: medium">
                                        勾稽期间:
                                        <asp:DropDownList ID="ddl_month" runat="server">
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
                                    <td style="width: 10%" align="right">
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
                    <div style="overflow: auto; width: 100%; height: 450px">
                        <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                            DataKeyNames="GI_INCOED,GI_CODE" AutoGenerateColumns="False"
                            ShowFooter="true" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                            <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="GI_INCOED" HeaderText="入库单号" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_CODE" HeaderText="发票号码" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_SUPPLIERNM" HeaderText="供货单位" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_MATCODE" HeaderText="物料编码" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_NAME" HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_GUIGE" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_UNIT" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_NUM" HeaderText="数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_INAMTMNY" HeaderText="金额" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_INCATAMTMNY" HeaderText="含税金额" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_DATE" HeaderText="勾稽日期" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GI_ZDNM" HeaderText="勾稽人" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    
                                <asp:TemplateField HeaderText="查看" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask" runat="server">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                            查看
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:TemplateField>
                                
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                        </asp:GridView>
                        <asp:Panel ID="NoDataPanel" runat="server">
                            没有数据!</asp:Panel>
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
                            <iframe id="IframeView" frameborder="0" width="650px" height="530px" scrolling="no">
                            </iframe>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="AlertDiv" style="top: 50%; right: 45%; position: absolute; visibility: hidden;">
        <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
    </div>
</asp:Content>
