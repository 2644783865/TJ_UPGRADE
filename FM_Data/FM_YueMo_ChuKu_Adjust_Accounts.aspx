<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_YueMo_ChuKu_Adjust_Accounts.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_YueMo_ChuKu_Adjust_Accounts"
    Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True" AsyncPostBackTimeout="0">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
    
    function ShowViewModal(ID) {
    
        var frame = $get('IframeView');
        frame.width=getWidth();
        frame.height=getHeight(); 
        frame.src = "FM_YueMo_ChuKu_Adjust_Accounts_Error.aspx?errorID="+ID;
        
        $find('ViewModalPopup').show();
    }
    
    function ViewOkayScript()
    {
      //在这里可以写相应的js事件
      
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table style="width: 98%; height: 24px">
                            <tr>
                                <td style="width: 30%">
                                
                                    <asp:Label ID="LabelMessage" runat="server" Text="系统未入库核算，不能进行出库核算!" Visible="false" ForeColor="Red"></asp:Label>
                                    <asp:HiddenField ID="hfdTotalAmount" runat="server" />
                               
                                </td>
                                <td align="center" width="20%" style="font-size: medium">
                                    核算年度:<asp:Label ID="lb_year" runat="server" Text="" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </td>
                                <td align="left" width="20%" style="font-size: medium">
                                    核算期间:<asp:Label ID="lb_period" runat="server" Text="" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </td>
                                <td align="center" width="30%">
                                    <asp:Button ID="btn_hs" runat="server" Text="出库核算" OnClick="btn_hs_Click" OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False" />&nbsp;|
                                    <asp:Button ID="btn_antihs" runat="server" Text="反核算" OnClick="btn_Antihs_Click" OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False"
                                        Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable" runat="server" DataKeyNames="OutCode"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound"
                        ShowFooter="true">
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
                            <asp:TemplateField HeaderText="红蓝字">
                                <ItemTemplate>
                                    <asp:Label ID="lb_hlz" runat="server" ForeColor='<%#Eval("ROB").ToString()=="0"?System.Drawing.Color.Blue:System.Drawing.Color.Red %>'
                                        Text='<%#Eval("ROB").ToString()=="0"?"蓝":"红" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="核算状态">
                                <ItemTemplate>
                                    <asp:Label ID="lb_shenhe" runat="server" ForeColor='<%#Eval("HSFLAG").ToString()=="0"?System.Drawing.Color.Black:System.Drawing.Color.Red %>'
                                        Text='<%#Eval("HSFLAG").ToString()=="0"?"N":"Y" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="OutCode" HeaderText="出库单号" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Dep" HeaderText="领料部门" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Warehouse" HeaderText="发料仓库" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Verifier" HeaderText="审核人" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MaterialCode" HeaderText="物料代码" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MaterialName" HeaderText="物料名称" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Standard" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Attribute" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                            
                            <asp:BoundField DataField="RealNumber" HeaderText="实发数量" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="UnitPrice" HeaderText="单价" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Amount" HeaderText="金额" ItemStyle-HorizontalAlign="Center" />
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
                    
                    <asp:ModalPopupExtender ID="ModalPopupExtenderEdit" runat="server" CancelControlID="ButtonViewCancel"
                        OkControlID="ButtonViewDone" TargetControlID="ButtonEdit" PopupControlID="DivEViewWindow"
                        OnOkScript="ViewOkayScript();" BehaviorID="ViewModalPopup">
                    </asp:ModalPopupExtender>
                    
                    <div style="display: none">
                        <input id="ButtonViewDone" value="Done" type="button" />
                        <input id="ButtonViewCancel" value="Cancel" type="button" />
                    </div>
                    <div id="DivEViewWindow" style="display: none; background-color: #00FFFF;">
                        <iframe id="IframeView" frameborder="0" scrolling="no"></iframe>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 30%; right: 45%">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajaxloader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
