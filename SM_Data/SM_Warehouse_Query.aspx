<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_Query.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_Query"
    Title="����ѯ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="SM_JS/superTables.js" type="text/javascript"></script>

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
 var postBack=true;
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args){
 if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id=="<%= WarehouseDropDownList.ClientID %>"||args.get_postBackElement().id=="<%= TypeDropDownList.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>")
   {
     postBack=false;
   }
   else
   {
    ActivateAlertDiv('visible', 'AlertDiv', '');
   }
    
}
function EndRequestHandler(sender, args){
if(postBack){
    document.getElementById("GridView1").parentNode.className = "fakeContainer";

    (function() {
        superTable("GridView1", {
            cssSkin : "Default",
           fixedCols : 2,
           onFinish : function () 
              {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                   
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;

                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        return function () 
                              {
                                    if (clicked) 
                                    {
                                        dataRow.style.backgroundColor = "#ffffff";
                                        fixedRow.style.backgroundColor = "#e4ecf7";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#BFDFFF";
                                        fixedRow.style.backgroundColor = "#409FFF";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                            }
                         return this;
                   }
        });
    })();
    
ActivateAlertDiv('hidden', 'AlertDiv', '');
}
postBack=true;
 }
         
function ActivateAlertDiv(visstring, elem, msg){
     var adiv = $get(elem);
     adiv.style.visibility = visstring;                
}

function confirmapp() {
        window.returnValue = true;
        window.close(); 
}

function cancelapp() {
    window.returnValue = false;
    window.close();
}

function closewin() {

if(window.opener!=null)
{
   window.opener.location = window.opener.location.href;
}
    window.close();
}

function storageexport(type,time) {

 var date=new Date();
var time=date.getTime();

    var retVal = window.showModalDialog("SM_WarehouseStorage_Export.aspx?type="+type, "", "dialogWidth=650px;dialogHeight=400px;help=no;scroll=no");
}

      
function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="hfdtn" runat="server" />
                <asp:HiddenField ID="hfdtp" runat="server" />
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/SM_Data/SM_MatCompare.aspx"
                        Target="_blank" Font-Underline="false">
                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />���ϱȶ�</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="Append" runat="server" Text="׷��" OnClick="Append_Click" />&nbsp;
                            <input id="Cancel" type="button" value="ȡ��" onclick="cancelapp()" runat="server" />&nbsp;
                            <asp:Button ID="btnShowPopup" runat="server" Text="ɸѡ" OnClientClick="viewCondition()" />&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </asp:ModalPopupExtender>
                            <input id="StorageExport" type="button" value="����" onclick="storageexport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="����" OnClick="BtnShowExport_Click" />&nbsp;
                            <asp:Label ID="LabelPush" runat="server" Text="����:" Visible="false"></asp:Label><asp:DropDownList
                                ID="DropDownListPush" runat="server" OnSelectedIndexChanged="DropDownListPush_SelectedIndexChanged"
                                AutoPostBack="true" Visible="false">
                                <asp:ListItem Text="--��ѡ��--" Value=""></asp:ListItem>
                                <asp:ListItem Text="�������Ͽⵥ" Value="0"></asp:ListItem>
                                <asp:ListItem Text="ί����ⵥ" Value="1"></asp:ListItem>
                                <asp:ListItem Text="���۳��ⵥ" Value="2"></asp:ListItem>
                                <%--<asp:ListItem Text="������" Value="3" ></asp:ListItem>--%>
                                <asp:ListItem Text="MTO������" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="��Ŀ��ת" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btn_llout" runat="server" Text="�������Ͽⵥ" OnCommand="btn_out_Command"
                                CommandArgument="0" />
                            <asp:Button ID="btn_wwout" runat="server" Text="ί����ⵥ" OnCommand="btn_out_Command"
                                CommandArgument="1" />
                            <asp:Button ID="btn_xsout" runat="server" Text="���۳��ⵥ" OnCommand="btn_out_Command"
                                CommandArgument="2" />
                            <asp:Button ID="btn_alout" runat="server" Text="������" OnCommand="btn_out_Command"
                                CommandArgument="3" />
                            <asp:Button ID="btn_mtoout" runat="server" Text="MTO������" OnCommand="btn_out_Command"
                                CommandArgument="4" />&nbsp;
                            <asp:Button ID="btn_projtemp" runat="server" Text="��Ŀ��ת" OnCommand="btn_out_Command"
                                CommandArgument="5" />&nbsp;
                            <asp:Button ID="btn_qtout" runat="server" Text="��������" OnCommand="btn_out_Command"
                                CommandArgument="6" />&nbsp;
                            <input id="Close" type="button" value="�ر�" onclick="closewin()" runat="server" />&nbsp;
                            <asp:Button ID="btn_QRExport" runat="server" Text="������ά����Ϣ" OnClick="btn_QRExport_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="100%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td style="white-space: nowrap;" align="left" colspan="2">
                                        &nbsp;&nbsp;&nbsp;��ʾ������<asp:TextBox ID="TextBoxCount" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="��ѯ" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="ȡ��" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="����" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ֿ⣺<asp:DropDownList ID="WarehouseDropDownList"
                                            runat="server" OnSelectedIndexChanged="WarehouseDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;�Ӳֿ⣺<asp:DropDownList ID="ChildWarehouseDropDownList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;�������ࣺ<asp:DropDownList ID="TypeDropDownList" runat="server" OnSelectedIndexChanged="TypeDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        �������ࣺ<asp:DropDownList ID="SubTypeDropDownList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;��ֹ���ڣ�<asp:TextBox ID="DateTextBox" runat="server"></asp:TextBox><asp:CalendarExtender
                                                        ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="DateTextBox">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ϲ���<asp:DropDownList ID="DropDownListMerge" runat="server">
                                                        <asp:ListItem Text="--��ѡ��--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="������" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;���ϴ��룺<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;�������ƣ�<asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;����ͺţ�<asp:TextBox ID="TextBoxStandard" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;�ʣ�<asp:TextBox ID="TextBoxAttribute"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��ʶ�ţ�<asp:TextBox ID="TextBoxNO" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    �ƻ����ٺţ�<asp:TextBox ID="TextBoxPTC" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����<asp:TextBox
                                                        ID="TextBoxLength" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��<asp:TextBox
                                                        ID="TextBoxWidth" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;�ţ�<asp:TextBox ID="TextBoxLotNumber"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                     &nbsp;&nbsp;&nbsp;�Ƿ񶨳ߣ�<asp:TextBox ID="TextBoxFixed" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;ע��<asp:TextBox ID="TextBoxNote"
                                                        runat="server"></asp:TextBox>
                                              </td>
                                               <td>
                                                 &nbsp;&nbsp;&nbsp;������ţ�<asp:TextBox ID="TextBoxOrderCode" runat="server"></asp:TextBox>                                                       
                                              </td>                                         
                                              
                                            </tr>
                                            <tr>
                                              <td></td>
                                              <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;&nbsp;&nbsp;����<asp:DropDownList ID="DropDownListFatherLogic"
                                                        runat="server">
                                                        <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                                        <asp:ListItem Value="OR">����</asp:ListItem>
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                              </td>                                            
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="2">
                                        <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="�߼�">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="OR" Selected="True">����</asp:ListItem>
                                                            <asp:ListItem Value="AND">����</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="����">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="�ȽϹ�ϵ">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>  
                                                            <asp:ListItem Value="7">������</asp:ListItem>
                                                            <asp:ListItem Value="8">�����</asp:ListItem>
                                                            <asp:ListItem Value="9">�Ұ���</asp:ListItem>                                                  
                                                            <asp:ListItem Value="1">����</asp:ListItem>
                                                            <asp:ListItem Value="2">������</asp:ListItem>
                                                            <asp:ListItem Value="3">����</asp:ListItem>
                                                            <asp:ListItem Value="4">���ڻ����</asp:ListItem>
                                                            <asp:ListItem Value="5">С��</asp:ListItem>
                                                            <asp:ListItem Value="6">С�ڻ����</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="��ֵ">
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
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 15%">
                                    <asp:Panel ID="Panel_Operation" runat="server" Visible="false">
                                        &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="ȫѡ" onclick="allsel()" />&nbsp;&nbsp;&nbsp;
                                        <input id="continue" type="button" value="��ѡ" onclick="consel()" />&nbsp;&nbsp;&nbsp;
                                        <input id="Button1" type="button" value="ȡ��" onclick="cancelsel()" />
                                    </asp:Panel>
                                </td>
                                <td valign="middle" align="right" style="width: 30%">
                                    <asp:DropDownList ID="DropDownListType" runat="server" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="MaterialCode" Selected="True">���ϴ���</asp:ListItem>
                                        <asp:ListItem Value="MaterialName">��������</asp:ListItem>
                                        <asp:ListItem Value="Attribute">����</asp:ListItem>
                                        <asp:ListItem Value="Standard">���</asp:ListItem>
                                        <asp:ListItem Value="PTC">�ƻ���</asp:ListItem>
                                        <asp:ListItem Value="LotNumber">����</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td valign="top" align="left" style="width: 55%">
                                    <asp:RadioButtonList ID="RadioButtonListOrderBy" runat="server" RepeatDirection="Horizontal"
                                        BorderStyle="None" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1">����</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="PanelBody" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                            û�����������Ϣ!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                            ���
                                        </td>
                                        <td>
                                            �ƻ����ٺ�
                                        </td>
                                        <td>
                                            ���ϴ���
                                        </td>
                                        <td>
                                            ��������
                                        </td>
                                        <td>
                                            ����ͺ�
                                        </td>
                                        <td>
                                            ����
                                        </td>
                                        <td>
                                            �Ƿ񶨳�
                                        </td>
                                        <td>
                                            ��
                                        </td>
                                        <td>
                                            ��
                                        </td>
                                        <td>
                                            ����
                                        </td>
                                        <td>
                                            ����
                                        </td>
                                        <td>
                                            ��λ
                                        </td>
                                        <td>
                                            ����
                                        </td>
                                        <td>
                                            ��(֧)
                                        </td>
                                        <td>
                                            �ֿ�����
                                        </td>
                                        <td>
                                            ��λ����
                                        </td>
                                        <td>
                                            �������
                                        </td>
                                        <td>
                                            �ƻ�ģʽ
                                        </td>
                                        <td>
                                            ��ʶ��
                                        </td>
                                        <td>
                                            ��ע
                                        </td>
                                        
                                        <td>
                                            MTO��������
                                        </td>
                                        <td>
                                            MTO������(֧)
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="LabelSQCODE" runat="server" Text='<%#Eval("SQCODE")%>' Visible="false"></asp:Label>
                                            <asp:HiddenField ID="hid_ptc" runat="server" Value='<%#Eval("PTC")%>' />
                                            <asp:Label ID="lb_symbol" runat="server" Width="25px" Height="12px" Visible="false" ToolTip="����Ŀ������ϴ���"></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("PTC")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialCode")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialStandard")%>
                                        </td>
                                        <td>
                                            <%#Eval("Attribute")%>
                                        </td>
                                        <td>
                                            <%#Eval("Fixed")%>
                                        </td>
                                        <td>
                                            <%#Eval("Length")%>
                                        </td>
                                        <td>
                                            <%#Eval("Width")%>
                                        </td>
                                        <td>
                                            <%#Eval("GB")%>
                                        </td>
                                        <td>
                                            <%#Eval("LotNumber")%>
                                        </td>
                                        <td>
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("Warehouse")%>
                                        </td>
                                        <td>
                                            <%#Eval("Position")%>
                                        </td>
                                        <td>
                                            <%#Eval("OrderCode")%>
                                        </td>
                                        <td>
                                            <%#Eval("PlanMode")%>
                                        </td>
                                        <td>
                                            <%#Eval("CGMODE")%>
                                        </td>
                                        <td>
                                            <%#Eval("Comment")%>
                                        </td>
                                        <td>
                                            <%#Eval("OUTTZNUM")%>
                                        </td>
                                        <td>
                                            <%#Eval("OUTTZFZNUM")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            �ܼƣ�
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTN" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTP" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />

                    <script type="text/javascript">
  
document.getElementById("GridView1").parentNode.className = "fakeContainer";

(function() {
    superTable("GridView1", {
        cssSkin : "Default",
        fixedCols : 2,
        onFinish : function () 
        {  
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                    
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;
                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];

                        return function () 
                        {
                            if (clicked) 
                            {
                              
                                dataRow.style.backgroundColor = "#ffffff";
                                fixedRow.style.backgroundColor = "#e4ecf7";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                clicked = false;
                             
                            }
                            else 
                            {
                              if( fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                              {
                                dataRow.style.backgroundColor = "#BFDFFF";
                                fixedRow.style.backgroundColor = "#409FFF";
                                 
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                clicked = true;
                              }
                            }
                        }
                    }.call(this, i);
                }
             return this;
       }
    });
})();

function allsel()
{
    for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
    {
//    ��1�������Ǳ�β
       this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
       this.sDataTable.tBodies[0].rows[i].style.backgroundColor = "#BFDFFF";
       this.sFDataTable.tBodies[0].rows[i].style.backgroundColor = "#409FFF";
       this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
       {
            var clicked = this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
            var dataRow = this.sDataTable.tBodies[0].rows[i];
            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
           
            return function () 
            {
                if (clicked) 
                {
                    dataRow.style.backgroundColor = "#ffffff";
                    fixedRow.style.backgroundColor = "#e4ecf7";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                    clicked = false;
                }
                else 
                {
                     dataRow.style.backgroundColor = "#BFDFFF";
                     fixedRow.style.backgroundColor = "#409FFF";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                    clicked = true;
                }
            }
        }.call(this, i);
    }
 return this;
}

function cancelsel()
{
   for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
   {
        this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
        this.sFDataTable.tBodies[0].rows[i].style.backgroundColor = "#e4ecf7";
        this.sDataTable.tBodies[0].rows[i].style.backgroundColor = "#ffffff";
        this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
        {
            var clicked = false;
            var dataRow = this.sDataTable.tBodies[0].rows[i];
            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
           
            return function () 
            {
                if (clicked) 
                {
                    dataRow.style.backgroundColor = "#ffffff";
                    fixedRow.style.backgroundColor = "#e4ecf7";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                    clicked = false;
                }
                else 
                {
                     dataRow.style.backgroundColor = "#BFDFFF";
                     fixedRow.style.backgroundColor = "#409FFF";
                     fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                     clicked = true;
                }
            }
        }.call(this, i);
    }
 return this;
}

function consel()
{
    for (var i=0; i<this.sDataTable.tBodies[0].rows.length-1; i++) 
    {
      obj=this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
      if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
      {
       if(obj.checked)
        {
              obj.checked=true;
              for (var j=i+1; j<this.sDataTable.tBodies[0].rows.length-1; j++) 
              {
                  var nextobj=this.sFDataTable.tBodies[0].rows[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                  if(nextobj!=null)
                  {
                        if(nextobj.type.toLowerCase()=="checkbox" && nextobj.value!="")
                        {
                            if(nextobj.checked)
                            {
                                for(var k=i+1;k<j;k++)
                                {
                                    this.sFDataTable.tBodies[0].rows[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                    this.sDataTable.tBodies[0].rows[k].style.backgroundColor = "#BFDFFF";
                                    this.sFDataTable.tBodies[0].rows[k].style.backgroundColor ="#409FFF";
                                }
                              break;
                            }
                        } 
                  } 
              }
        }
      }
      this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
      {
            var clicked = this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
            var dataRow = this.sDataTable.tBodies[0].rows[i];
            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
           
            return function () 
            {
                if (clicked) 
                {
                    dataRow.style.backgroundColor = "#ffffff";
                    fixedRow.style.backgroundColor = "#e4ecf7";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                    clicked = false;
                }
                else 
                {
                     dataRow.style.backgroundColor = "#BFDFFF";
                     fixedRow.style.backgroundColor = "#409FFF";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                    clicked = true;
                }
            }
        }.call(this, i);
    }
 return this;
}


                    </script>

                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="AlertDiv" class="AlertStyle">
                <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
            </div>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
